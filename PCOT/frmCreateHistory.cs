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
    public partial class frmCreateHistory : Form
    {
        #region 変数
        /// <summary>プロセス名</summary>
        public string ProcessName = string.Empty;
        /// <summary>原文</summary>
        public string OriginalText = string.Empty;
        /// <summary>訳文</summary>
        public string ResultText = string.Empty;

        /// <summary>ファイルパス</summary>
        private string historyPath = string.Empty;
        /// <summary>既存データ確認用</summary>
        private DataTable historyDt = new DataTable();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmCreateHistory()
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
        private void frmCreateHistory_Load(object sender, EventArgs e)
        {
            try
            {
                // フォルダパス作成
                historyPath = Util.GetHistoryFilePath(ProcessName);

                historyDt = Util.CreateHistoryDataTable();

                if (File.Exists(historyPath))
                {
                    // 既存データ読み込み
                    historyDt.ReadXml(historyPath);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    // 未入力確認
                    if (CommonUtil.PutMessage(CommonEnums.MessageType.Confirm,
                        "ラベルタイトルが未入力です。そのまま登録しますか？") == DialogResult.No)
                    {
                        return;
                    }
                }

                int id = 1;
                if (historyDt.Rows.Count > 0)
                {
                    // IDは最大値＋1
                    id = historyDt.AsEnumerable().Select(s => (int)s[HistoryColumns.Id]).Max() + 1;
                }

                var dr = historyDt.NewRow();
                dr[HistoryColumns.Id] = id;
                dr[HistoryColumns.Label] = txtTitle.Text.Trim();
                dr[HistoryColumns.OriginalText] = OriginalText;
                dr[HistoryColumns.ResultText] = ResultText;
                historyDt.Rows.Add(dr);

                Util.WriteXml(historyDt, historyPath);
                ProcessNameInfo.RegisterProessName();

                Close();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion
    }
}
