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
    public partial class frmProtection : Form
    {
        #region プロパティ
        /// <summary>対象画面の位置座標サイズ</summary>
        public Rectangle Rect { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmProtection()
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
        private void frmProtection_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = true;
                Location = new Point(Rect.X, Rect.Y);
                Size = new Size(Rect.Width - Rect.X, Rect.Height - Rect.Y);
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void frmProtection_Shown(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(1000);
                TopMost = false;
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
        private void frmProtection_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // 翻訳表示画面をアクティブにする
                var translatedText = Application.OpenForms.Cast<Form>()
                    .Where(w => w.Name == nameof(frmShowTranslatedText))
                    .Select(s => (frmShowTranslatedText)s).FirstOrDefault();

                translatedText.Activate();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProtection_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Opacity = 0.01;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// マウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProtection_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (TranslateInfo.ShowScanForm)
                {
                    Opacity = 0.01;
                    return;
                }

                Opacity = 0.6;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion
    }
}
