using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmStart : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmStart()
        {
            InitializeComponent();
        }

        #region イベントハンドラ
        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStart_Load(object sender, EventArgs e)
        {
            try
            {
                // バージョンアップの同期
                Util.SyncConfigData();
                Util.SyncRegisterProcessName();

                ShowProcessList();

                if (!File.Exists(SystemSettingInfo.FontSettingFilePath))
                {
                    // ファイルが存在しない場合は初期値で作成
                    Util.WriteXml(SystemSettingInfo.FontSettingDt, SystemSettingInfo.FontSettingFilePath);
                }

                if (!File.Exists(SystemSettingInfo.SystemSettingFilePath))
                {
                    // ファイルが存在しない場合は初期値で作成
                    var dr = SystemSettingInfo.SystemSettingDt.NewRow();
                    SystemSettingInfo.SystemSettingDt.Rows.Add(dr);
                    Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// セルペインティング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (dgvProcessList.Rows.Count > 0 && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvProcessList.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                    {
                        if (dgvProcessList[e.ColumnIndex, e.RowIndex].Value.Equals(string.Empty))
                        {
                            // セルの選択状態によって描画色を変える
                            var selected = DataGridViewElementStates.Selected ==
                                (e.State & DataGridViewElementStates.Selected);
                            e.PaintBackground(e.CellBounds, selected);
                            //描画が完了したことを知らせる
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 選択状態変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvProcessList.Rows.Count == 0)
                {
                    return;
                }

                var selectedRows = dgvProcessList.SelectedRows;

                if (selectedRows.Count == 0)
                {
                    return;
                }

                int rowIndex = selectedRows[0].Index;

                if ((bool)dgvProcessList[noneActivePrcs.Name, rowIndex].Value)
                {
                    btnSelect.Enabled = false;
                }
                else
                {
                    btnSelect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// セルコンテンツクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvProcessList.Rows.Count == 0)
                {
                    return;
                }

                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    return;
                }

                // 追加
                if (e.ColumnIndex == dgvProcessList.Columns[prcsAdd.Name].Index)
                {
                    if (string.IsNullOrEmpty(dgvProcessList[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    {
                        // ボタンがない場合は中断
                        return;
                    }

                    using (var form = new frmCreateAliasProcess())
                    {
                        form.ProcessName = dgvProcessList[prcsName.Name, e.RowIndex].Value.ToString();
                        form.ShowDialog();
                        ShowProcessList();
                    }
                }

                // 削除
                if (e.ColumnIndex == dgvProcessList.Columns[prcsDel.Name].Index)
                {
                    if (string.IsNullOrEmpty(dgvProcessList[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    {
                        // ボタンがない場合は中断
                        return;
                    }

                    // 削除確認
                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "プロセスに紐づく全てのデータを削除します。よろしいですか？\r\n※データを削除した場合は復旧できません。") == DialogResult.No)
                    {
                        // 「いいえ」の場合は中断
                        return;
                    }

                    // 削除処理
                    string processName = dgvProcessList[prcsName.Name, e.RowIndex].Value.ToString();
                    var row = ProcessNameInfo.ProcessNameDt.AsEnumerable()
                        .Where(w => w[ProcessNameColumns.ProcessAliasName].Equals(processName))
                        .FirstOrDefault();

                    ProcessNameInfo.ProcessNameDt.Rows.Remove(row);

                    // 更新後を出力
                    Util.WriteXml(ProcessNameInfo.ProcessNameDt, ProcessNameInfo.ProcessManagerFilePath);

                    // data\プロセス名.cfgファイルを削除
                    string dataFilePath = $@"data\{processName}.cfg";
                    if (File.Exists(dataFilePath))
                    {
                        File.Delete(dataFilePath);
                    }

                    // image\プロセス名フォルダを削除
                    string imgDirPath = $@"image\{processName}";
                    if (Directory.Exists(imgDirPath))
                    {
                        Util.DeleteDirectory(imgDirPath);
                    }

                    // history\プロセス名.cfgファイルを削除
                    string historyFilePath = $@"history\{processName}.cfg";
                    if (File.Exists(historyFilePath))
                    {
                        File.Delete(historyFilePath);
                    }

                    // 処理が終わったら表示を更新
                    ShowProcessList();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// リスト更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ShowProcessList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// システム設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmSetting())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 設定のある全てのプロセスを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ShowProcessList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                // リストから選択されたプロセスを取得する
                var selectedRow = dgvProcessList.SelectedCells[0].RowIndex;
                var name = dgvProcessList[prcsName.Name, selectedRow].Value.ToString();
                var processNameItem = ProcessNameInfo.ProcessNameDt.AsEnumerable()
                    .Where(w => w[ProcessNameColumns.ProcessAliasName].Equals(name))
                    .Select(s => new
                    {
                        processName = s[ProcessNameColumns.ProcessName].ToString(),
                        processAliasName = s[ProcessNameColumns.ProcessAliasName].ToString()
                    }).FirstOrDefault();

                string processName = string.Empty;
                if (processNameItem == null)
                {
                    processName = name;
                }
                else
                {
                    processName = processNameItem.processName;
                }

                var prcs = Util.GetProcess(processName);

                if (prcs == null)
                {
                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Warning, "該当プロセスの取得に失敗しました。");

                    ShowProcessList();

                    return;
                }

                // フォント設定を読み込む
                SystemSettingInfo.FontSettingDt.Clear();
                SystemSettingInfo.FontSettingDt.ReadXml(SystemSettingInfo.FontSettingFilePath);

                // システム設定を読み込む
                SystemSettingInfo.SystemSettingDt.Clear();
                SystemSettingInfo.SystemSettingDt.ReadXml(SystemSettingInfo.SystemSettingFilePath);

                // ショートカット設定を読み込む
                if (File.Exists(ShortcutInfo.ShortcutFilePath))
                {
                    ShortcutInfo.ShortcutDt.Clear();
                    ShortcutInfo.ShortcutDt.ReadXml(ShortcutInfo.ShortcutFilePath);
                }

                var showTrans = new frmShowTranslatedText();
                showTrans.FormClosed += OnShowTranslatedTextFormClose;
                // プロセス名設定
                if(processNameItem == null)
                {
                    // データを保持していない場合
                    ProcessNameInfo.SelectedProcessName = name;
                    ProcessNameInfo.SelectedProcessAliasName = name;
                }
                else
                {
                    // データを保持している場合
                    ProcessNameInfo.SelectedProcessName = processNameItem.processName;
                    ProcessNameInfo.SelectedProcessAliasName = processNameItem.processAliasName;
                }

                // 設定ファイルパスを取得
                TranslateInfo.ConfigFilePath = TranslateInfo.GetConfigFilePath();
                // 画像翻訳画面情報初期化
                frmImageList.RestoreWindowStateInfo.Reset();
                showTrans.Show();
                Hide();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳結果表示画面が閉じた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowTranslatedTextFormClose(object sender, EventArgs e)
        {
            try
            {
                Util.SyncRegisterProcessName();
                TranslateInfo.Init();
                ShowProcessList();
                Show();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// データテーブル作成
        /// </summary>
        /// <returns>データテーブルのひな型</returns>
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(prcsTitle.Name, typeof(string));
            dt.Columns.Add(prcsName.Name, typeof(string));
            dt.Columns.Add(prcsAdd.Name, typeof(string));
            dt.Columns.Add(prcsDel.Name, typeof(string));
            dt.Columns.Add(hasSettingData.Name, typeof(bool));
            dt.Columns.Add(noneActivePrcs.Name, typeof(bool));
            dt.Columns.Add(isAliasPrcs.Name, typeof(bool));
            return dt;
        }

        /// <summary>
        /// プロセスリストを表示
        /// </summary>
        private void ShowProcessList()
        {
            var dt = CreateDataTable();

            var processNameList = ProcessNameInfo.ProcessNameDt.AsEnumerable()
                .Select(s => new
                {
                    processName = s[ProcessNameColumns.ProcessName].ToString(),
                    processAliasName = s[ProcessNameColumns.ProcessAliasName].ToString(),
                    isAliasProcess = (bool)s[ProcessNameColumns.IsAliasProcess]
                }).ToList();

            foreach (var prcs in Process.GetProcesses()
                .Where(w =>
                !string.IsNullOrEmpty(w.MainWindowTitle) &&
                w.MainWindowHandle != IntPtr.Zero &&
                w.ProcessName != "PCOT"))
            {
                // 見つかったプロセスは問答無用で追加
                {
                    // 設定データを保持しているか
                    var processNameItem = processNameList
                        .Where(w => w.processName == prcs.ProcessName && !w.isAliasProcess).FirstOrDefault();

                    var dr = dt.NewRow();
                    dr[prcsTitle.Name] = prcs.MainWindowTitle;
                    dr[prcsName.Name] = prcs.ProcessName;
                    dr[prcsAdd.Name] = "追加";
                    dr[prcsDel.Name] = processNameItem == null ? "" : "削除";
                    dr[hasSettingData.Name] = processNameItem == null ? false : true;
                    dr[noneActivePrcs.Name] = false;
                    dr[isAliasPrcs.Name] = false;
                    dt.Rows.Add(dr);

                    if(processNameItem != null)
                    {
                        // 登録後にリストから削除
                        processNameList.Remove(processNameItem);
                    }
                }

                var processNameItems = processNameList
                    .Where(w => w.processName == prcs.ProcessName && w.isAliasProcess).ToList();

                if (processNameItems.Count > 0)
                {
                    foreach (var item in processNameItems.ToArray())
                    {
                        // 設定データを保持している
                        var dr = dt.NewRow();
                        dr[prcsTitle.Name] = "*別名プロセス";
                        dr[prcsName.Name] = item.processAliasName;
                        dr[prcsAdd.Name] = item.isAliasProcess ? "" : "追加";
                        dr[prcsDel.Name] = "削除";
                        dr[hasSettingData.Name] = true;
                        dr[noneActivePrcs.Name] = false;
                        dr[isAliasPrcs.Name] = item.isAliasProcess;
                        dt.Rows.Add(dr);

                        // 登録後にリストから削除
                        processNameList.Remove(item);
                    }
                }
            }

            if (chkShowAll.Checked)
            {
                // プロセス一覧に存在しないプロセス名を表示
                foreach (var item in processNameList)
                {
                    var dr = dt.NewRow();
                    dr[prcsTitle.Name] = "-非アクティブプロセス";
                    dr[prcsName.Name] = item.processAliasName;
                    dr[prcsAdd.Name] = "";
                    dr[prcsDel.Name] = "削除";
                    dr[hasSettingData.Name] = true;
                    dr[noneActivePrcs.Name] = true;
                    dr[isAliasPrcs.Name] = item.isAliasProcess;
                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                // データバインド
                dgvProcessList.DataSource = dt.AsEnumerable()
                    .OrderBy(o => o[prcsName.Name]).ToArray().CopyToDataTable();

                // 非アクティブプロセスの背景色を変更
                dgvProcessList.Rows.Cast<DataGridViewRow>()
                    .Where(w => (bool)w.Cells[noneActivePrcs.Name].Value)
                    .Select(s => s.DefaultCellStyle.BackColor = Color.LightGray).ToList();
            }
        }
        #endregion
    }
}
