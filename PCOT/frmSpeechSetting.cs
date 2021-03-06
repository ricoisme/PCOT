using GameUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmSpeechSetting : Form
    {
        #region 変数
        /// <summary>音声出力</summary>
        SpeechSynthesizer speech = new SpeechSynthesizer();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmSpeechSetting()
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
        private void frmSpeechSetting_Load(object sender, EventArgs e)
        {
            try
            {
                // 設定ファイルから初期値を設定
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                nudVolume.Value = (int)dr[SettingColumns.SpeechVolume];
                nudRate.Value = (int)dr[SettingColumns.SpeechRate];

                speech.SetOutputToDefaultAudioDevice();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 音声テスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSpeechTest.Text))
                {
                    // 空文字の場合は中断
                    return;
                }

                if (speech.State == SynthesizerState.Ready)
                {
                    speech.Volume = Convert.ToInt32(nudVolume.Value);
                    speech.Rate = Convert.ToInt32(nudRate.Value);
                    speech.SpeakAsync(txtSpeechTest.Text);
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
                var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                dr[SettingColumns.SpeechVolume] = Convert.ToInt32(nudVolume.Value);
                dr[SettingColumns.SpeechRate] = Convert.ToInt32(nudRate.Value);
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

        /// <summary>
        /// フォームクロージング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSpeechSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                speech.Dispose();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }
        #endregion
    }
}
