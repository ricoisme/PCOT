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
    public partial class frmHistoryList : Form
    {
        #region 定数
        /// <summary>プロセスタイトル</summary>
        private const string TITLE = "：{0}";
        /// <summary>フィルターボタンON表示</summary>
        private const string LBL_FILTER_ON = "フィルター";
        /// <summary>フィルターボタンOFF表示</summary>
        private const string LBL_FILTER_OFF = "フィルター解除";
        #endregion

        #region 変数
        /// <summary>履歴ファイルパス</summary>
        private string historyPath = string.Empty;
        /// <summary>編集用データテーブル</summary>
        private DataTable historyDispDt = new DataTable();
        /// <summary>オリジナル比較用データテーブル</summary>
        private DataTable historyOrgDt = new DataTable();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmHistoryList()
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
        private void frmHistoryList_Load(object sender, EventArgs e)
        {
            try
            {
                // フィルターボタン表示設定
                btnFilter.Text = LBL_FILTER_ON;

                dgvHistoryList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 切断中
                    Text += string.Format(TITLE, TranslateInfo.PCOT_DESKTOP_CONNECT);
                }
                else
                {
                    // 接続中
                    Text += string.Format(TITLE, ProcessNameInfo.SelectedProcessAliasName);
                }

                // フォント設定から表示用の原文と訳文のフォントを取得
                Color orgColor = Color.Empty;
                Font orgFont = Util.GetFont(0, ref orgColor);
                Color rstColor = Color.Empty;
                Font rstFont = Util.GetFont(1, ref rstColor);

                // リストにフォントを設定
                dgvHistoryList.Columns[OriginalText.Name].DefaultCellStyle.Font = orgFont;
                dgvHistoryList.Columns[OriginalText.Name].DefaultCellStyle.ForeColor = orgColor;
                dgvHistoryList.Columns[ResultText.Name].DefaultCellStyle.Font = rstFont;
                dgvHistoryList.Columns[ResultText.Name].DefaultCellStyle.ForeColor = rstColor;

                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 切断中
                    historyPath = Util.GetHistoryFilePath(TranslateInfo.PCOT_DESKTOP_CONNECT);
                }
                else
                {
                    // 接続中
                    historyPath = Util.GetHistoryFilePath(ProcessNameInfo.SelectedProcessAliasName);
                }

                if (!File.Exists(historyPath))
                {
                    // データが存在しない場合は処理を中断
                    btnFilter.Enabled = false;
                    return;
                }

                // 既存データ読み込み
                var dt = Util.CreateHistoryDataTable();
                dt.ReadXml(historyPath);

                if(dt.Rows.Count == 0)
                {
                    // データが存在しない場合は処理を中断
                    btnFilter.Enabled = false;
                    return;
                }

                // 表示用データテーブルに列定義を作成
                historyDispDt = Util.CreateHistoryDispDataTable();

                // それぞれのデータテーブルにキーを設定(Id)
                dt.PrimaryKey = new DataColumn[] { dt.Columns[HistoryColumns.Id] };
                historyDispDt.PrimaryKey = new DataColumn[] { historyDispDt.Columns[HistoryDispColumns.Id] };

                // 二つのデータテーブルをマージ
                historyDispDt.Merge(dt);

                // ID順にソート
                historyDispDt = historyDispDt.AsEnumerable()
                    .OrderBy(o => (int)o[HistoryDispColumns.Id]).ToArray().CopyToDataTable();
                historyDispDt.TableName = Util.HISOTRY_DISP_TABLE;

                // 閉じる時の比較用にオリジナルデータを退避
                historyOrgDt = historyDispDt.Copy();

                // バインド
                dgvHistoryList.DataSource = historyDispDt;
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
        private void dgvHistoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == dgvHistoryList.Columns[Delete.Name].Index)
                {
                    // 削除ボタン
                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "対象データを削除します。よろしいですか？") == DialogResult.No)
                    {
                        // いいえの場合は処理を中断
                        return;
                    }

                    // 削除
                    dgvHistoryList.Rows.RemoveAt(e.RowIndex);

                    if (dgvHistoryList.Rows.Count == 0)
                    {
                        btnFilter.Enabled = false;
                    }

                    historyDispDt.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フィルター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if(btnFilter.Text == LBL_FILTER_ON)
                {
                    var labelNameList = historyDispDt.AsEnumerable()
                        .Select(s => s[HistoryDispColumns.Label].ToString())
                        .Distinct().ToList();

                    var dt = Util.CreateHistoryFilterDataTable();
                    foreach(var labelName in labelNameList)
                    {
                        var dr = dt.NewRow();
                        dr[HistoryFilterColumns.LabelName] = labelName;
                        dr[HistoryFilterColumns.Checked] = true;
                        dt.Rows.Add(dr);
                    }

                    using(var form = new frmSelectFilter())
                    {
                        form.FilterDt = dt;
                        form.ShowDialog();

                        // 表示にチェックがあるもののみを取得
                        var showLabelNameList = form.FilterDt.AsEnumerable()
                            .Where(w => (bool)w[HistoryFilterColumns.Checked])
                            .Select(s => s[HistoryFilterColumns.LabelName].ToString()).ToList();

                        List<DataRow> showRows = new List<DataRow>();
                        foreach(var labelName in showLabelNameList)
                        {
                            var rows = historyDispDt.AsEnumerable()
                                .Where(w => w[HistoryDispColumns.Label].Equals(labelName)).ToList();
                            if (rows.Count > 0)
                            {
                                showRows.AddRange(rows);
                            }
                        }

                        if(showRows.Count != historyDispDt.Rows.Count)
                        {
                            // フィルタリング
                            dgvHistoryList.DataSource = showRows.CopyToDataTable();

                            // ボタン表示切替
                            btnFilter.Text = LBL_FILTER_OFF;
                        }
                    }
                }
                else
                {
                    // ボタン表示切替
                    btnFilter.Text = LBL_FILTER_ON;

                    var dt = (DataTable)dgvHistoryList.DataSource;
                    dt.PrimaryKey = new DataColumn[] { dt.Columns[HistoryDispColumns.Id] };

                    historyDispDt.Merge(dt);

                    historyDispDt.AcceptChanges();

                    dgvHistoryList.DataSource = historyDispDt;
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
                if (historyDispDt.Rows.Count > 0)
                {
                    if (btnFilter.Text == LBL_FILTER_OFF)
                    {
                        var dt = (DataTable)dgvHistoryList.DataSource;
                        dt.PrimaryKey = new DataColumn[] { dt.Columns[HistoryDispColumns.Id] };

                        historyDispDt.Merge(dt);

                        historyDispDt.AcceptChanges();
                    }
                }

                if (Util.IsSameValue(historyOrgDt, historyDispDt))
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
                        var orgTextRequiredErr = historyDispDt.AsEnumerable()
                            .Where(w => string.IsNullOrEmpty(w[HistoryDispColumns.OriginalText].ToString()))
                            .Select(s => new { err = true, index = historyDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (orgTextRequiredErr != null && orgTextRequiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, "原文の入力は必須です。");
                            dgvHistoryList.ClearSelection();
                            dgvHistoryList.FirstDisplayedScrollingRowIndex = orgTextRequiredErr.index;
                            dgvHistoryList[OriginalText.Name, orgTextRequiredErr.index].Selected = true;
                            return;
                        }

                        var resultTextRequiredErr = historyDispDt.AsEnumerable()
                            .Where(w => string.IsNullOrEmpty(w[HistoryDispColumns.ResultText].ToString()))
                            .Select(s => new { err = true, index = historyDispDt.Rows.IndexOf(s) }).FirstOrDefault();

                        if (resultTextRequiredErr != null && resultTextRequiredErr.err)
                        {
                            CommonUtil.PutMessage(CommonEnums.MessageType.Error, "訳文の入力は必須です。");
                            dgvHistoryList.ClearSelection();
                            dgvHistoryList.FirstDisplayedScrollingRowIndex = resultTextRequiredErr.index;
                            dgvHistoryList[ResultText.Name, resultTextRequiredErr.index].Selected = true;
                            return;
                        }

                        // 保存処理
                        var historyColumns = Util.CreateHistoryDataTable()
                            .Columns.Cast<DataColumn>()
                            .Select(s => s.ColumnName).ToArray();

                        // 書き込み用に列を抽出
                        var dt = historyDispDt.DefaultView.ToTable(Util.HISTORY_TABLE, false, historyColumns);

                        // 履歴フォルダにデータが存在しない場合
                        if (dt.Rows.Count == 0)
                        {
                            // ファイルが存在する場合はファイルを削除
                            if (File.Exists(historyPath))
                            {
                                File.Delete(historyPath);
                                if (!Util.IsExistsProcessData())
                                {
                                    ProcessNameInfo.DeleteProcessName();
                                }
                            }

                            int fileCount = Directory.GetFileSystemEntries(
                                "history", "*.cfg", SearchOption.TopDirectoryOnly).Length;

                            if (fileCount == 0)
                            {
                                Directory.Delete("history");
                            }
                        }
                        else
                        {
                            // 出力
                            Util.WriteXml(dt, historyPath);
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
    }
}
