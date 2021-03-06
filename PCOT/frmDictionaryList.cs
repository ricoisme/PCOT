using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmDictionaryList : Form
    {
        #region 定数
        /// <summary>登録プロセス：共通</summary>
        private const string PRCS_TYPE_COMMON = "共通";
        /// <summary>登録プロセス：共通以外</summary>
        private const string PRCS_TYPE_OTHER_COMMON = "共通以外";
        /// <summary>フィルターボタンON表示</summary>
        private const string LBL_FILTER_ON = "フィルター";
        /// <summary>フィルターボタンOFF表示</summary>
        private const string LBL_FILTER_OFF = "フィルター解除";
        #endregion

        #region 変数
        /// <summary>辞書ファイルパス</summary>
        private string dicFilePath = string.Empty;
        /// <summary>辞書リスト表示用データテーブル</summary>
        private DataTable dicDispDt = new DataTable();
        /// <summary>オリジナル比較用データテーブル</summary>
        private DataTable dicOrgDt = new DataTable();
        /// <summary>削除した行の特定用</summary>
        private List<int> removedIdList = new List<int>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmDictionaryList()
        {
            InitializeComponent();
        }
        #endregion

        #region イベントハンドラ
        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDictionary_Load(object sender, EventArgs e)
        {
            try
            {
                // フィルターボタン表示設定
                btnFilter.Text = LBL_FILTER_ON;

                dgvDicList.Columns[DicTypeName.Name].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                // 辞書ファイルパスを取得
                dicFilePath = Util.GetDictionaryFilePath();

                if (!File.Exists(dicFilePath))
                {
                    // データが1件も存在しない場合
                    btnEnabled.Enabled = false;
                    cboFilter.Enabled = false;
                    btnFilter.Enabled = false;
                    return;
                }

                var dt = Util.CreateDicDataTable();

                // 存在する場合はデータを取得
                dt.ReadXml(dicFilePath);

                if (dt.Rows.Count > 0)
                {
                    // 登録プロセスが埋まってない場合は「共通」を設定
                    dt.AsEnumerable()
                        .Where(w => w[DicColumns.RegistProcess] == DBNull.Value)
                        .Select(s => s[DicColumns.RegistProcess] = PRCS_TYPE_COMMON).ToList();

                    // 辞書ファイルを更新
                    Util.WriteXml(dt, Util.GetDictionaryFilePath());
                }
                else
                {
                    // データが1件も存在しない場合
                    btnEnabled.Enabled = false;
                    cboFilter.Enabled = false;
                    btnFilter.Enabled = false;
                    return;
                }

                // 表示用データテーブルに列定義を作成
                dicDispDt = Util.CreateDicDispDataTable();

                // それぞれのデータテーブルにキーを設定(Id)
                dt.PrimaryKey = new DataColumn[] { dt.Columns[DicColumns.Id] };
                dicDispDt.PrimaryKey = new DataColumn[] { dicDispDt.Columns[DicDispColumns.Id] };

                // 二つのデータテーブルをマージ
                dicDispDt.Merge(dt);
                dicDispDt.AsEnumerable()
                    .Where(w => w[DicDispColumns.DicTypeCd].Equals(0))
                    .Select(s => s[DicDispColumns.DicTypeName] = "置換").ToList();
                dicDispDt.AsEnumerable()
                    .Where(w => w[DicDispColumns.DicTypeCd].Equals(1))
                    .Select(s => s[DicDispColumns.DicTypeName] = "無視").ToList();

                // ID順にソート
                dicDispDt = dicDispDt.AsEnumerable()
                    .OrderBy(o => (int)o[DicDispColumns.Id]).ToArray().CopyToDataTable();
                dicDispDt.TableName = Util.DICTIONARY_DISP_TABLE;

                // 閉じる時の比較用にオリジナルデータを退避
                dicOrgDt = dicDispDt.Copy();

                // 登録プロセスの右クリックメニューアイテム設定
                SetProcessNamesContextMenuItem();

                // バインド
                dgvDicList.DataSource = dicDispDt;

                // 無視の場合は置換後テキストを触れないようにする
                DisableAfterTextCell();

                // コンボボックスアイテム設定
                SetComboBoxItem();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 列ソート後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDicList_Sorted(object sender, EventArgs e)
        {
            try
            {
                // 無視の場合は置換後テキストを触れないようにする
                DisableAfterTextCell();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDicList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == dgvDicList.Columns[Delete.Name].Index)
                {
                    // 削除ボタン
                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "対象データを削除します。よろしいですか？") == DialogResult.No)
                    {
                        // いいえの場合は処理を中断
                        return;
                    }

                    // 削除
                    removedIdList.Add((int)dgvDicList.Rows[e.RowIndex].Cells[Id.Name].Value);
                    dgvDicList.Rows.RemoveAt(e.RowIndex);

                    if(dgvDicList.Rows.Count == 0)
                    {
                        // 全て削除した場合は制御系のコントロールを全て非活性にする
                        btnEnabled.Enabled = false;
                        cboFilter.Enabled = false;
                        btnFilter.Enabled = false;
                    }

                    dicDispDt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 右クリックメニュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDicList_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dgvDicList.Columns[DicTypeName.Name].Index == e.ColumnIndex ||
                dgvDicList.Columns[RegistProcess.Name].Index == e.ColumnIndex)
            {
                dgvDicList.ClearSelection();
                var cell = dgvDicList[e.ColumnIndex, e.RowIndex];
                cell.Selected = true;
            }
        }

        /// <summary>
        /// 右クリックメニュー置換ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuReplaceOn_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = dgvDicList.SelectedCells.Cast<DataGridViewCell>().FirstOrDefault().RowIndex;
                dgvDicList[DicTypeCd.Name, rowIndex].Value = 0;
                dgvDicList[DicTypeName.Name, rowIndex].Value = "置換";
                dgvDicList[AfterText.Name, rowIndex].ReadOnly = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// 右クリックメニュー無視ON
        private void mnuIgnoreOn_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = dgvDicList.SelectedCells.Cast<DataGridViewCell>().FirstOrDefault().RowIndex;
                dgvDicList[DicTypeCd.Name, rowIndex].Value = 1;
                dgvDicList[DicTypeName.Name, rowIndex].Value = "無視";
                dgvDicList[AfterText.Name, rowIndex].Value = string.Empty;
                dgvDicList[AfterText.Name, rowIndex].ReadOnly = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 登録プロセス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSelectProcessName_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = dgvDicList.SelectedCells.Cast<DataGridViewCell>().FirstOrDefault().RowIndex;
                dgvDicList[RegistProcess.Name, rowIndex].Value = ((ToolStripMenuItem)sender).Text;
                SetComboBoxItem();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 有効/無効切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnabled_Click(object sender, EventArgs e)
        {
            try
            {
                var processNameList = dicDispDt.AsEnumerable()
                    .Select(s => s[DicDispColumns.RegistProcess].ToString())
                    .Distinct().ToList();

                // チェック状態一覧の取得
                List<dynamic> chkStateList = new List<dynamic>();
                foreach (var process in processNameList)
                {
                    chkStateList.Add(GetCheckState(process));
                }

                var dt = Util.CreateDicEnabledDataTable();
                foreach (var chkState in chkStateList)
                {
                    var dr = dt.NewRow();
                    CheckState state;
                    if (chkState.isEnabledAll)
                    {
                        state = CheckState.Checked;
                    }
                    else if (chkState.isDisabledAll)
                    {
                        state = CheckState.Unchecked;
                    }
                    else
                    {
                        state = CheckState.Indeterminate;
                    }

                    dr[DicEnabledColumns.ProcessName] = chkState.prcsName;
                    dr[DicEnabledColumns.CheckState] = state;
                    dr[DicEnabledColumns.IsIndeterminate] = state == CheckState.Indeterminate ? true : false;
                    dt.Rows.Add(dr);
                }

                using (var form = new frmEnabled())
                {
                    form.enabledDt = dt;
                    form.ShowDialog();

                    foreach (var processName in processNameList)
                    {
                        var state = form.enabledDt.AsEnumerable()
                            .Where(w => w[DicEnabledColumns.ProcessName].Equals(processName))
                            .Select(s => (CheckState)s[DicEnabledColumns.CheckState]).FirstOrDefault();

                        if (state != CheckState.Indeterminate)
                        {
                            // 不確定チェック以外は編集があったものとする
                            bool check = state == CheckState.Checked ? true : false;
                            dicDispDt.AsEnumerable()
                                .Where(w => w[DicDispColumns.RegistProcess].Equals(processName))
                                .Select(s => s[DicDispColumns.IsEnabled] = check).ToList();
                        }
                    }

                    dicDispDt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フィルターON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnFilter.Text == LBL_FILTER_ON)
                {
                    // ボタン表示切替
                    btnFilter.Text = LBL_FILTER_OFF;

                    // 共通があるかどうかチェック
                    var isExistsCommon = dicDispDt.AsEnumerable()
                        .Any(a => a[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON));

                    DataTable dt = new DataTable();
                    if (isExistsCommon)
                    {
                        if ((int)cboFilter.SelectedValue == 0)
                        {
                            // 共通
                            dt = dicDispDt.AsEnumerable()
                                .Where(w => w[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON))
                                .ToArray().CopyToDataTable();
                            dgvDicList.DataSource = dt;
                        }
                        else if ((int)cboFilter.SelectedValue == 1)
                        {
                            // 共通以外
                            dt = dicDispDt.AsEnumerable()
                                .Where(w => !w[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON))
                                .ToArray().CopyToDataTable();
                            dgvDicList.DataSource = dt;
                        }
                        else
                        {
                            // 各種プロセス
                            var processName = cboFilter.Text;
                            dt = dicDispDt.AsEnumerable()
                                .Where(w => w[DicDispColumns.RegistProcess].Equals(processName))
                                .ToArray().CopyToDataTable();
                            dgvDicList.DataSource = dt;
                        }
                    }
                    else
                    {
                        // 各種プロセス
                        var processName = cboFilter.Text;
                        dt = dicDispDt.AsEnumerable()
                            .Where(w => w[DicDispColumns.RegistProcess].Equals(processName))
                            .ToArray().CopyToDataTable();
                        dgvDicList.DataSource = dt;
                    }

                    DisableAfterTextCell();
                    cboFilter.Enabled = false;
                    btnEnabled.Enabled = false;
                }
                else
                {
                    // ボタン表示切替
                    btnFilter.Text = LBL_FILTER_ON;
                    var dt = (DataTable)dgvDicList.DataSource;
                    dt.PrimaryKey = new DataColumn[] { dt.Columns[DicDispColumns.Id] };

                    // マージ
                    dicDispDt.Merge(dt);

                    dicDispDt.AcceptChanges();

                    dgvDicList.DataSource = dicDispDt;

                    SetComboBoxItem();

                    cboFilter.Enabled = true;
                    btnEnabled.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                // フィルター中の場合は解除する
                if (dgvDicList.Rows.Count > 0)
                {
                    if (btnFilter.Text == LBL_FILTER_OFF)
                    {
                        var dt = (DataTable)dgvDicList.DataSource;
                        dt.PrimaryKey = new DataColumn[] { dt.Columns[DicDispColumns.Id] };

                        // マージ
                        dicDispDt.Merge(dt);

                        dicDispDt.AcceptChanges();
                    }
                }

                // オリジナルデータと比較
                if (Util.IsSameValue(dicOrgDt, dicDispDt))
                {
                    // データが同じだったら閉じる
                    Close();
                    return;
                }

                // 確認メッセージ
                var result = CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                    "編集されたデータが存在します。保存しますか？", true);

                switch (result)
                {
                    case DialogResult.Yes:
                        // 入力チェック
                        var requiredErr = dicDispDt.AsEnumerable()
                            .Where(w => string.IsNullOrEmpty(w[DicDispColumns.BeforeText].ToString()))
                            .Select(s => new { err = true, index = dicDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (requiredErr != null && requiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                                "置換前テキストまたは、無視テキストの入力は必須です。");
                            dgvDicList.ClearSelection();
                            dgvDicList.FirstDisplayedScrollingRowIndex = requiredErr.index;
                            dgvDicList[BeforeText.Name, requiredErr.index].Selected = true;
                            return;
                        }

                        var afterTxtErr = dicDispDt.AsEnumerable()
                            .Where(w => (int)w[DicDispColumns.DicTypeCd] == 0 &&
                            string.IsNullOrEmpty(w[DicDispColumns.AfterText].ToString()))
                            .Select(s => new { err = true, index = dicDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (afterTxtErr != null && afterTxtErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                                "処理タイプが置換の場合、置換後テキストの入力は必須です。");
                            dgvDicList.ClearSelection();
                            dgvDicList.FirstDisplayedScrollingRowIndex = afterTxtErr.index;
                            dgvDicList[AfterText.Name, afterTxtErr.index].Selected = true;
                            return;
                        }

                        // 保存処理
                        var dicColumns = Util.CreateDicDataTable()
                            .Columns.Cast<DataColumn>()
                            .Select(s => s.ColumnName).ToArray();

                        // 書き込み用に列を抽出
                        var dt = dicDispDt.DefaultView.ToTable(Util.DICTIONARY_TABLE, false, dicColumns);
                        if (dt.Rows.Count == 0)
                        {
                            // 1件も存在しない場合はファイルを削除
                            if (File.Exists(dicFilePath))
                            {
                                File.Delete(dicFilePath);
                            }
                        }
                        else
                        {
                            // 出力
                            Util.WriteXml(dt, dicFilePath);
                        }

                        Close();
                        break;
                    case DialogResult.No:
                        // 保存せずに終了
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// コンボボックスアイテム設定
        /// </summary>
        private void SetComboBoxItem()
        {
            if (dicDispDt.Rows.Count == 0)
            {
                return;
            }

            // 共通があるかどうかチェック
            var isExistsCommon = dicDispDt.AsEnumerable()
                .Any(a => a[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON));

            // 「共通」以外のプロセス名を重複を排除して取得
            var processNames = dicDispDt.AsEnumerable()
                .Where(w => !w[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON))
                .Select(s => s[DicDispColumns.RegistProcess].ToString())
                .Distinct().ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add(cboFilter.ValueMember, typeof(int));
            dt.Columns.Add(cboFilter.DisplayMember, typeof(string));

            if (isExistsCommon)
            {
                // 共通
                {
                    var dr = dt.NewRow();
                    dr[cboFilter.ValueMember] = 0;
                    dr[cboFilter.DisplayMember] = PRCS_TYPE_COMMON;
                    dt.Rows.Add(dr);
                }

                if (processNames.Count > 0)
                {
                    // 共通以外が存在する場合
                    var dr = dt.NewRow();
                    dr[cboFilter.ValueMember] = 1;
                    dr[cboFilter.DisplayMember] = PRCS_TYPE_OTHER_COMMON;
                    dt.Rows.Add(dr);

                }
            }

            // 各種プロセス
            for (int i = 0; i < processNames.Count; i++)
            {
                var dr = dt.NewRow();
                dr[cboFilter.ValueMember] = isExistsCommon ? (i + 2) : i;
                dr[cboFilter.DisplayMember] = processNames[i];
                dt.Rows.Add(dr);
            }

            cboFilter.DataSource = dt;
        }

        /// <summary>
        /// プロセス名一覧のコンテキストメニューアイテム設定
        /// </summary>
        private void SetProcessNamesContextMenuItem()
        {
            cmsProcessNames.Items.Clear();

            // 共通以外のプロセス名を取得
            var processNames = dicDispDt.AsEnumerable()
                .Where(w => !w[DicDispColumns.RegistProcess].Equals(PRCS_TYPE_COMMON))
                .Select(s => s[DicDispColumns.RegistProcess].ToString())
                .Distinct().ToList();

            // 共通が一番上に来るようにする
            processNames.Insert(0, PRCS_TYPE_COMMON);

            if (ProcessNameInfo.DisconnectProcess)
            {
                // 切断中
                if (!processNames.Any(a => a == TranslateInfo.PCOT_DESKTOP_CONNECT))
                {
                    // デスクトップ接続が存在しない場合は追加
                    processNames.Add(TranslateInfo.PCOT_DESKTOP_CONNECT);
                }
            }
            else
            {
                // 接続中
                if (!processNames.Any(a => a == ProcessNameInfo.SelectedProcessAliasName))
                {
                    // 対象プロセスが存在しない場合は追加
                    processNames.Add(ProcessNameInfo.SelectedProcessAliasName);
                }
            }

            foreach (var processName in processNames)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = processName;
                item.Click += mnuSelectProcessName_Click;
                cmsProcessNames.Items.Add(item);
            }
        }

        /// <summary>
        /// 置換後テキストの無効化
        /// </summary>
        private void DisableAfterTextCell()
        {
            // 無視行の場合は置換後テキストを読み取り専用に設定
            dgvDicList.Rows.Cast<DataGridViewRow>()
                .Where(w => w.Cells[DicTypeCd.Name].Value.Equals(1))
                .Select(s => s.Cells[AfterText.Name].ReadOnly = true)
                .ToList();
        }

        /// <summary>
        /// プロセス名とチェック状態の一覧を取得
        /// </summary>
        /// <param name="processName">プロセス名(共通を含む)</param>
        /// <returns>チェック状態の一覧</returns>
        private dynamic GetCheckState(string processName)
        {
            if (
                dicDispDt.AsEnumerable()
                .Where(w => w[DicDispColumns.RegistProcess].Equals(processName))
                .All(a => (bool)a[DicDispColumns.IsEnabled])
                )
            {
                return new { prcsName = processName, isEnabledAll = true, isDisabledAll = false };
            }
            else if (
               dicDispDt.AsEnumerable()
               .Where(w => w[DicDispColumns.RegistProcess].Equals(processName))
               .All(a => !(bool)a[DicDispColumns.IsEnabled])
               )
            {
                return new { prcsName = processName, isEnabledAll = false, isDisabledAll = true };
            }
            else
            {
                return new { prcsName = processName, isEnabledAll = false, isDisabledAll = false };
            }
        }
        #endregion
    }
}
