using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmCreateNoun : Form
    {
        #region 変数
        /// <summary>入力テキスト</summary>
        public string InputText = string.Empty;
        /// <summary>出力テキスト</summary>
        public string OutputText = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmCreateNoun()
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
        private void frmCreateNoun_Load(object sender, EventArgs e)
        {
            try
            {
                txtBeforeReplace.Text = InputText;
                txtAfterReplace.Text = InputText;
                chkEnabled.Checked = true;
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
                // 置換前テキスト入力チェック
                if (string.IsNullOrEmpty(txtBeforeReplace.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換前テキストが未入力です。");
                    return;
                }

                // 置換後テキスト入力チェック
                if (string.IsNullOrEmpty(txtAfterReplace.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換後テキストが未入力です。");
                    return;
                }

                var dt = TranslateInfo.SettingDs.Tables[Util.NOUN_TABLE];

                // 新規作成
                int id = 1;

                if (dt.Rows.Count > 0)
                {
                    id = dt.AsEnumerable().Select(s => (int)s[NounColumns.Id]).Max() + 1;
                }
                var dr = dt.NewRow();
                dr[NounColumns.Id] = id;
                dr[NounColumns.IsEnabled] = chkEnabled.Checked;
                dr[NounColumns.BeforeNoun] = txtBeforeReplace.Text.Trim();
                dr[NounColumns.AfterNoun] = txtAfterReplace.Text.Trim();
                dt.Rows.Add(dr);

                // 出力
                if (ProcessNameInfo.DisconnectProcess)
                {
                    // 切断中
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.DesktopConfigFilePath);
                }
                else
                {
                    // 接続中
                    Util.WriteXml(TranslateInfo.SettingDs, TranslateInfo.ConfigFilePath);
                }

                if (!string.IsNullOrEmpty(txtAfterReplace.Text))
                {
                    OutputText = txtAfterReplace.Text.Trim();
                }

                DialogResult = DialogResult.OK;
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
                DialogResult = DialogResult.Cancel;
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
