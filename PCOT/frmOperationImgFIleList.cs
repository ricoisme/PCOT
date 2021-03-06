using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmOperationImgFIleList : Form
    {
        #region 変数
        /// <summary>画像リストデータテーブル</summary>
        public DataTable ImgListDt = new DataTable();
        /// <summary>選択中の最終行</summary>
        public int ParentSelectedIdx = 0;
        /// <summary>削除ファイルリスト</summary>
        public List<string> DeleteFileList = new List<string>();
        /// <summary>変更フラグ</summary>
        public bool IsChanged = false;

        /// <summary>オリジナルのデータテーブル</summary>
        private DataTable orgDt = new DataTable();
        /// <summary>一つ前の選択行</summary>
        private int baseSingleRowIdx = 0;
        /// <summary>選択中の最終行</summary>
        private int selectedLastRowIdx = 0;
        /// <summary>選択変更フラグ</summary>
        private bool isSelectedChanged = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmOperationImgFIleList()
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
        private void frmOperationImgFIleList_Load(object sender, EventArgs e)
        {
            try
            {
                timUpdateImg.Start();

                // オリジナルに退避
                orgDt = ImgListDt.Copy();

                // ヘッダーの改行を無効
                dgvImageList.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

                // 初期表示時の画像名を設定
                lblImgFileName.Text =
                    ImgListDt.Rows[ParentSelectedIdx][OperationImgColumns.ImgFileName].ToString();

                // バインド
                dgvImageList.DataSource = ImgListDt;
                dgvImageList.Select();
                dgvImageList.CurrentCell = dgvImageList[0, ParentSelectedIdx];

                // 初期選択位置に移動
                dgvImageList.ClearSelection();
                dgvImageList.Rows[ParentSelectedIdx].Selected = true;

                // 初期値は親画面で選択されているインデックス
                baseSingleRowIdx = ParentSelectedIdx;

                // スクロール追従
                dgvImageList.FirstDisplayedScrollingRowIndex = ParentSelectedIdx;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 行追加後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvImageList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                // コンテキストメニューを紐づける
                dgvImageList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.ContextMenuStrip = cmsMenu).ToList();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 選択行変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvImageList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvImageList.Rows.Count == 0)
                {
                    return;
                }

                // このイベントに入った時点で変更は必ずある
                isSelectedChanged = true;

                // 選択インデックスの最小値
                var firstRowIdx = dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Index).DefaultIfEmpty(-1).Min();

                // 選択インデックスの最大値
                var lastRowIdx = dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Index).DefaultIfEmpty(-1).Max();

                if (firstRowIdx == -1 || lastRowIdx == -1)
                {
                    // インデックスが取れない場合は中断
                    return;
                }

                // 単行選択時
                if (dgvImageList.SelectedRows.Count == 1)
                {
                    baseSingleRowIdx = dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                        .Select(s => s.Index).FirstOrDefault();
                    selectedLastRowIdx = baseSingleRowIdx;
                    return;
                }

                if (baseSingleRowIdx > firstRowIdx)
                {
                    selectedLastRowIdx = firstRowIdx;
                }
                else
                {
                    selectedLastRowIdx = lastRowIdx;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// セルの変更を即時反映
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvImageList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                var dgv = (DataGridView)sender;
                if (dgv.IsCurrentCellDirty)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 削除選択ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smiDeleteSelectOn_Click(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[Delete.Name].Value = true).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 削除選択OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smiDeleteSelectOff_Click(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[Delete.Name].Value = false).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 表示選択ON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smiShowSelectOn_Click(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[ShowImg.Name].Value = true).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 表示選択OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smiShowSelectOff_Click(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.SelectedRows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[ShowImg.Name].Value = false).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 削除チェックON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDeleteAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[Delete.Name].Value = chkDeleteAll.Checked).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 表示チェックON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dgvImageList.Rows.Cast<DataGridViewRow>()
                    .Select(s => s.Cells[ShowImg.Name].Value = chkShowAll.Checked).ToList();
                ImgListDt.AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// チェック行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvImageList.Rows.Cast<DataGridViewRow>().ToArray())
                {
                    if ((bool)row.Cells[Delete.Name].Value)
                    {
                        // 削除ファイルリストにパスを追加
                        DeleteFileList.Add(row.Cells[ImgFilePath.Name].Value.ToString());
                        // グリッドから削除
                        dgvImageList.Rows.Remove(row);
                    }
                }

                ImgListDt.AcceptChanges();
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
                // 先に入力チェック
                if (dgvImageList.Rows.Count > 0)
                {
                    if (dgvImageList.Rows.Cast<DataGridViewRow>()
                        .All(a => !(bool)a.Cells[ShowImg.Name].Value))
                    {
                        CommonUtil.PutMessage(CommonEnums.MessageType.Error,
                            "全ての画像を非表示にすることはできません。");
                        return;
                    }
                }

                // 何も編集が無い場合はそのまま閉じる
                if (Util.IsSameValue(orgDt, ImgListDt))
                {
                    Close();
                    return;
                }

                // 編集の確定確認
                var result = CommonUtil.PutMessage(
                    CommonEnums.MessageType.Confirm, "ファイル操作を確定しますか？", true);

                switch (result)
                {
                    case DialogResult.Yes:
                        IsChanged = true;
                        Close();
                        break;
                    case DialogResult.No:
                        ImgListDt = orgDt.Copy();
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 更新間隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timUpdateImg_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dgvImageList.Rows.Cast<DataGridViewRow>().All(a => (bool)a.Cells[Delete.Name].Value))
                {
                    chkDeleteAll.Checked = true;
                }

                if (dgvImageList.Rows.Cast<DataGridViewRow>().All(a => !(bool)a.Cells[Delete.Name].Value))
                {
                    chkDeleteAll.Checked = false;
                }

                if (dgvImageList.Rows.Cast<DataGridViewRow>().All(a => (bool)a.Cells[ShowImg.Name].Value))
                {
                    chkShowAll.Checked = true;
                }

                if (dgvImageList.Rows.Cast<DataGridViewRow>().All(a => !(bool)a.Cells[ShowImg.Name].Value))
                {
                    chkShowAll.Checked = false;
                }

                if (dgvImageList.Rows.Cast<DataGridViewRow>().Any(a => (bool)a.Cells[Delete.Name].Value))
                {
                    btnChkDelete.Enabled = true;
                }
                else
                {
                    btnChkDelete.Enabled = false;
                }

                if (dgvImageList.Rows.Count == 0 || dgvImageList.SelectedRows.Count == 0)
                {
                    lblImgFileName.Text = "表示する画像がありません";
                    picImage.SetImageFilePath(string.Empty);
                    picImage.Image = null;
                    isSelectedChanged = false;
                    return;
                }

                if (isSelectedChanged)
                {
                    picImage.SetImageFilePath(
                        dgvImageList[ImgFilePath.Name, selectedLastRowIdx].Value.ToString());

                    picImage.InitDrawImage(false);

                    isSelectedChanged = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        #endregion
    }
}
