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
using System.Windows.Forms.VisualStyles;

namespace PCOT
{
    public partial class frmSelectFilter : Form
    {
        #region 変数
        /// <summary>フィルター切替用データテーブル</summary>
        public DataTable FilterDt = new DataTable();
        #endregion

        #region コンストラクタ 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmSelectFilter()
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
        private void frmSelectFilter_Load(object sender, EventArgs e)
        {
            try
            {
                if (FilterDt.Rows.Count == 0)
                {
                    return;
                }

                dgvLabelList.DataSource = FilterDt;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 全てチェックON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckOnAll_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLabelList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[Checked.Name].Value = true).ToList();
                FilterDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 全てチェックOFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckOffAll_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLabelList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[Checked.Name].Value = false).ToList();
                FilterDt.AcceptChanges();
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
                // 入力チェック
                if(dgvLabelList.Rows.Cast<DataGridViewRow>()
                    .All(a => a.Cells[HistoryFilterColumns.Checked].Value.Equals(false)))
                {
                    CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                        "全てのラベルを非表示にすることはできません。");
                    return;
                }

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
