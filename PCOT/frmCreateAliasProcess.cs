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
    public partial class frmCreateAliasProcess : Form
    {
        #region 変数
        /// <summary>プロセス名</summary>
        public string ProcessName = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmCreateAliasProcess()
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
        private void CreateAliasProcess_Load(object sender, EventArgs e)
        {
            try
            {
                // ベースのプロセス名を設定
                lblProcessName.Text = ProcessName + "_";
                int lblSizeW = lblProcessName.Size.Width;

                // サイズをプロセス名の長さに合わせる
                Size = new Size(Size.Width + lblSizeW, Size.Height);
                txtAliasProcessName.Location = new Point(
                    txtAliasProcessName.Location.X + lblSizeW, txtAliasProcessName.Location.Y);
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
                // 先にトリムしておく
                txtAliasProcessName.Text = txtAliasProcessName.Text.Trim();

                // ブランクチェック
                if (string.IsNullOrEmpty(txtAliasProcessName.Text))
                {
                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Error, "プロセスの別名は必須入力です。");
                    return;
                }

                // 全角チェック
                var byteCount = CommonUtil.GetByteCount(txtAliasProcessName.Text);
                if (txtAliasProcessName.Text.Length != byteCount)
                {
                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Error, "プロセスの別名に全角文字は使用できません。");
                    return;
                }

                // 不正ファイル名チェック
                try
                {
                    Path.GetFullPath(txtAliasProcessName.Text);
                }
                catch
                {
                    CommonUtil.PutMessage(
                        CommonEnums.MessageType.Error, "禁則文字が含まれています。");
                    return;
                }

                // 重複チェック
                var aliasProcessName = lblProcessName.Text + txtAliasProcessName.Text;
                if (ProcessNameInfo.ProcessNameDt.AsEnumerable()
                    .Any(a => a[ProcessNameColumns.ProcessAliasName].Equals(aliasProcessName)))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error, "その別名は既に登録されています。");
                    return;
                }

                // 登録処理
                var dr = ProcessNameInfo.ProcessNameDt.NewRow();
                dr[ProcessNameColumns.ProcessName] = ProcessName;
                dr[ProcessNameColumns.ProcessAliasName] = aliasProcessName;
                dr[ProcessNameColumns.IsAliasProcess] = true;
                ProcessNameInfo.ProcessNameDt.Rows.Add(dr);

                Util.WriteXml(ProcessNameInfo.ProcessNameDt, ProcessNameInfo.ProcessManagerFilePath);

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
