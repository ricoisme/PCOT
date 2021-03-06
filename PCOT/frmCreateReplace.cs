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
    public partial class frmCreateReplace : Form
    {
        #region 定数
        /// <summary>辞書タイプ：置換</summary>
        private const string DIC_TYPE_REPLACE = "0";
        #endregion

        #region 変数
        /// <summary>プロセス名</summary>
        public string ProcessName = string.Empty;
        /// <summary>入力テキスト</summary>
        public string InputText = string.Empty;
        /// <summary>出力テキスト</summary>
        public string OutputText = string.Empty;
        /// <summary>辞書ファイルパス</summary>
        private string dicFilePath = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmCreateReplace()
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
        private void frmEditReplace_Load(object sender, EventArgs e)
        {
            try
            {
                SetComboBoxItem();

                txtBeforeReplace.Text = InputText;
                txtAfterReplace.Text = InputText;
                cboDicType.SelectedIndex = 0;
                chkWordUnit.Checked = true;
                chkEnabled.Checked = true;
                rdoTargetProcess.Checked = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 辞書区分変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDicType.SelectedValue.ToString() == DIC_TYPE_REPLACE)
                {
                    // 置換
                    txtAfterReplace.Enabled = true;
                    chkWordUnit.Enabled = true;
                    chkUpperAndLower.Enabled = true;
                }
                else
                {
                    // 無視
                    txtAfterReplace.Text = string.Empty;
                    txtAfterReplace.Enabled = false;
                    chkWordUnit.Checked = false;
                    chkWordUnit.Enabled = false;
                    chkUpperAndLower.Checked = false;
                    chkUpperAndLower.Enabled = false;
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
                // 置換前テキスト入力チェック
                if (string.IsNullOrEmpty(txtBeforeReplace.Text))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換前テキストが未入力です。");
                    return;
                }

                // 置換後テキスト入力チェック
                if (cboDicType.SelectedValue.ToString() == DIC_TYPE_REPLACE)
                {
                    if (string.IsNullOrEmpty(txtAfterReplace.Text))
                    {
                        CommonUtil.PutMessage(CommonEnums.MessageType.Error, "置換後テキストが未入力です。");
                        return;
                    }
                }

                var dt = Util.CreateDicDataTable();

                // 辞書ファイルパス取得
                dicFilePath = Util.GetDictionaryFilePath();

                if (File.Exists(dicFilePath))
                {
                    dt.ReadXml(dicFilePath);
                }

                // 新規作成
                int id = 1;

                if (dt.Rows.Count > 0)
                {
                    id = dt.AsEnumerable().Select(s => (int)s[DicColumns.Id]).Max() + 1;
                }

                var dr = dt.NewRow();
                dr[DicColumns.Id] = id;
                dr[DicColumns.IsEnabled] = chkEnabled.Checked;
                dr[DicColumns.RegistProcess] = rdoCommon.Checked ? "共通" : ProcessName;
                dr[DicColumns.DicTypeCd] = (int)cboDicType.SelectedValue;
                dr[DicColumns.IsWordUnit] = chkWordUnit.Checked;
                dr[DicColumns.IsUpperAndLower] = chkUpperAndLower.Checked;
                dr[DicColumns.BeforeText] = txtBeforeReplace.Text.Trim();
                dr[DicColumns.AfterText] = txtAfterReplace.Text.Trim();
                dt.Rows.Add(dr);

                // 出力
                Util.WriteXml(dt, dicFilePath);

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

        #region メソッド
        /// <summary>
        /// コンボボックス作成
        /// </summary>
        private void SetComboBoxItem()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(DicDispColumns.DicTypeCd, typeof(int));
            dt.Columns.Add(DicDispColumns.DicTypeName, typeof(string));

            DataRow dr;
            dr = dt.NewRow();
            dr[cboDicType.ValueMember] = 0;
            dr[cboDicType.DisplayMember] = "置換";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[cboDicType.ValueMember] = 1;
            dr[cboDicType.DisplayMember] = "無視";
            dt.Rows.Add(dr);

            cboDicType.DataSource = dt;
        }
        #endregion
    }
}
