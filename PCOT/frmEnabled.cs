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
    public partial class frmEnabled : Form
    {
        #region 変数
        /// <summary>有効/無効一括切替用データテーブル</summary>
        public DataTable enabledDt = new DataTable();
        #endregion

        #region コンストラクタ 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmEnabled()
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
        private void frmEnabled_Load(object sender, EventArgs e)
        {
            try
            {
                if (enabledDt.Rows.Count == 0)
                {
                    return;
                }

                dgvProcessList.DataSource = enabledDt;

                SetThreeState();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ソート後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessList_Sorted(object sender, EventArgs e)
        {
            try
            {
                SetThreeState();
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
        /// 不確定状態の設定
        /// </summary>
        private void SetThreeState()
        {
            dgvProcessList.Rows.Cast<DataGridViewRow>()
                .Where(w => (bool)w.Cells[IsIndeterminate.Name].Value)
                .Select(s => ((DataGridViewCheckBoxCell)s.Cells[CheckState.Name]).ThreeState = true)
                .ToList();
        }
        #endregion
    }
}
