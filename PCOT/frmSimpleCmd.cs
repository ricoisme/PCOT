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
    public partial class frmSimpleCmd : Form
    {
        #region 定数
        /// <summary>タイトル表示</summary>
        private const string TITLE = "不透明度：{0}%";
        #endregion

        #region プロパティ
        /// <summary>透明度</summary>
        float _CurrentOpacity =
            Convert.ToSingle(SystemSettingInfo.SystemSettingDt.Rows[0][SettingColumns.Transparency]) / 100;

        public float CurrentOpacity
        {
            get
            {
                return _CurrentOpacity;
            }
            set
            {
                _CurrentOpacity = value;
                if (Cursor.Position.X >= Left && Cursor.Position.Y >= Top && Cursor.Position.X < Right && Cursor.Position.Y < Bottom)
                {
                    Opacity = 1;
                }
                else
                {
                    Opacity = _CurrentOpacity;
                }

                lblTransparent.Text = string.Format(TITLE, Convert.ToInt32(_CurrentOpacity * 100).ToString());
            }
        }
        #endregion

        #region 変数
        /// <summary>非表示フラグ</summary>
        public bool IsNotifyHide = false;
        /// <summary>マウス座標</summary>
        private Point mousePoint = new Point();
        /// <summary>コンボボックスアイテム用データテーブル</summary>
        private DataTable comboDt = new DataTable();
        /// <summary>ID</summary>
        public int SelectedId = -1;
        /// <summary>クリップボード翻訳クリック</summary>
        public bool ClickClipboardTranslate = false;
        /// <summary>フリー選択クリック</summary>
        public bool ClickFreeSelect = false;
        /// <summary>翻訳クリック</summary>
        public bool ClickTranslate = false;
        /// <summary>簡易コマンド画面表示フラグ</summary>
        public bool IsShowSimpleCmd = false;
        /// <summary>ドロップダウン展開フラグ</summary>
        private bool dropDownOpenFlg = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmSimpleCmd()
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
        private void frmSimpleCmd_Load(object sender, EventArgs e)
        {
            try
            {
                timActive.Start();
                Opacity = CurrentOpacity;

                BindComboBoxItem();

                if (SelectedId == -1)
                {
                    cboLabel.SelectedIndex = SelectedId;
                }
                else
                {
                    cboLabel.SelectedValue = SelectedId;
                }

                // マウスホイールのイベントを追加
                MouseWheel += frmSimpleCmd_MouseWheel;

                // 現在の不透明度を表示
                lblTransparent.Text = string.Format(TITLE, Convert.ToInt32(CurrentOpacity * 100));
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 簡易コマンド画面アクティブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSimpleCmd_Activated(object sender, EventArgs e)
        {
            try
            {
                if (!Util.IsSameValue(comboDt, TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE]))
                {
                    cboLabel.DataSource = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE];
                }

                if (SelectedId == -1)
                {
                    cboLabel.SelectedIndex = SelectedId;
                }
                else
                {
                    cboLabel.SelectedValue = SelectedId;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 閉じるボタンマウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                lblClose.BackColor = Color.FromArgb(198, 215, 255);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 閉じるボタンマウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                lblClose.BackColor = Color.Empty;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLabel.SelectedIndex == -1)
                {
                    SelectedId = -1;
                }
                else
                {
                    SelectedId = (int)cboLabel.SelectedValue;
                }

                IsNotifyHide = true;

                WriteOpacity();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フォーム上でキーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSimpleCmd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                {
                    if (cboLabel.SelectedIndex == -1)
                    {
                        SelectedId = -1;
                    }
                    else
                    {
                        SelectedId = (int)cboLabel.SelectedValue;
                    }

                    IsNotifyHide = true;

                    WriteOpacity();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    mousePoint = new Point(e.X, e.Y);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Left += e.X - mousePoint.X;
                    Top += e.Y - mousePoint.Y;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTransparent_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    mousePoint = new Point(e.X, e.Y);
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTransparent_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Left += e.X - mousePoint.X;
                    Top += e.Y - mousePoint.Y;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSimpleCmd_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    var value = e.Delta / 120f / 100f;
                    CurrentOpacity = CommonUtil.GetLimitValue(CurrentOpacity + value, 0.01f, 1f);
                    lblTransparent.Text = string.Format(TITLE, Convert.ToInt32(CurrentOpacity * 100));
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ドロップダウンオープン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLabel_DropDown(object sender, EventArgs e)
        {
            try
            {
                dropDownOpenFlg = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ドロップダウンクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLabel_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                dropDownOpenFlg = false;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// クリップボード翻訳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClipboardTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                IsNotifyHide = true;
                WriteOpacity();
                SelectedId = -1;
                ClickClipboardTranslate = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フリー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFreeSelect_Click(object sender, EventArgs e)
        {
            try
            {
                IsNotifyHide = true;
                WriteOpacity();
                SelectedId = -1;
                ClickFreeSelect = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 翻訳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                IsNotifyHide = true;
                WriteOpacity();
                if (cboLabel.SelectedIndex == -1)
                {
                    SelectedId = -1;
                }
                else
                {
                    SelectedId = (int)cboLabel.SelectedValue;
                }

                ClickTranslate = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フォームのアクティブ制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timActive_Tick(object sender, EventArgs e)
        {
            try
            {
                // コンボボックスが展開されていたら評価しない
                if (dropDownOpenFlg)
                {
                    return;
                }

                if (Cursor.Position.X >= Left && Cursor.Position.Y >= Top && Cursor.Position.X < Right && Cursor.Position.Y < Bottom)
                {
                    Opacity = 1;
                    Activate();
                }
                else
                {
                    Opacity = CurrentOpacity;
                    ActiveControl = null;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSimpleCmd_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// コンボボックスアイテムのバインド
        /// </summary>
        public void BindComboBoxItem()
        {
            comboDt = TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Copy();
            if (TranslateInfo.SettingDs.Tables[Util.TITLE_TABLE].Rows.Count > 0)
            {
                cboLabel.DataSource = comboDt;
            }
        }

        /// <summary>
        /// 設定ファイルに不透明度を書き込む
        /// </summary>
        private void WriteOpacity()
        {
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
            dr[SettingColumns.Transparency] = Convert.ToInt32(CurrentOpacity * 100);
            Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
        }
        #endregion
    }
}
