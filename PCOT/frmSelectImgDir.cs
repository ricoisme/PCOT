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
    public partial class frmSelectImgDir : Form
    {
        #region 変数
        /// <summary>画像フォルダリスト</summary>
        public List<string> ImageDirNames = new List<string>();
        /// <summary>選択画像フォルダ</summary>
        public string SelectedImgDir = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmSelectImgDir()
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
        private void frmSelectImgDir_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(ImageDirName.Name, typeof(string));

                foreach (var item in ImageDirNames.ToArray())
                {
                    if (Directory.GetFileSystemEntries(item, "*.png").Length == 0)
                    {
                        // フォルダ内に画像がない場合は削除
                        ImageDirNames.Remove(item);
                    }
                }

                foreach (var item in ImageDirNames)
                {
                    var dr = dt.NewRow();
                    dr[ImageDirName.Name] = Path.GetFileName(item);
                    dt.Rows.Add(dr);
                }

                dgvImgDirList.DataSource = dt.AsEnumerable()
                    .OrderBy(o => o[ImageDirName.Name].ToString()).ToArray().CopyToDataTable();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 画像フォルダ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedCell = dgvImgDirList.SelectedCells[0];
                SelectedImgDir = selectedCell.Value.ToString();
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
