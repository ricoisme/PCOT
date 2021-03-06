using GameUtil;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCOT
{
    public partial class frmSetting : Form
    {
        #region 定数
        /// <summary>親ノード名</summary>
        private const string ROOT_NODE_NAME = "DeepLと連携";
        /// <summary>子ノード名</summary>
        private const string CHILD_NODE_NAME = "自動的にDeepLを起動";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmSetting()
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
        private void frmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                // 使用OCRエンジンコンボボックスアイテム設定
                SetSelectOcrEngineComboBox();

                // フォント設定読み込み
                SystemSettingInfo.FontSettingDt.Clear();
                SystemSettingInfo.FontSettingDt.ReadXml(SystemSettingInfo.FontSettingFilePath);

                // 設定ファイル読み込み
                SystemSettingInfo.SystemSettingDt.Clear();
                SystemSettingInfo.SystemSettingDt.ReadXml(SystemSettingInfo.SystemSettingFilePath);

                SetSetting();
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 原文フォント設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOrgFont_Click(object sender, EventArgs e)
        {
            try
            {
                Font font = lblOrgFontSample.Font;
                Color color = lblOrgFontSample.ForeColor;
                Util.OpenFontDialog(font, color, ref font, ref color);
                lblOrgFontSample.Font = font;
                lblOrgFontSample.ForeColor = color;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 訳文フォント設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetRstFont_Click(object sender, EventArgs e)
        {
            try
            {
                Font font = lblRstFontSample.Font;
                Color color = lblRstFontSample.ForeColor;
                Util.OpenFontDialog(font, color, ref font, ref color);
                lblRstFontSample.Font = font;
                lblRstFontSample.ForeColor = color;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 対象通りに改行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTargetReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTargetReturn.Checked)
                {
                    chkIgnoreReturn.Checked = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 改行を無視
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIgnoreReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIgnoreReturn.Checked)
                {
                    chkTargetReturn.Checked = false;
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 音声設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpeechSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using(var form = new frmSpeechSetting())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ショートカット使用チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoUseShortcut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnShortcutSetting.Enabled = rdoUseShortcut.Checked;
                chkProcessActivate.Enabled = rdoUseShortcut.Checked;
                nudTransparency.Enabled = rdoUseSimpleCmd.Checked;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// ショートカット設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShortcutSetting_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new frmShortcutSetting())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// 簡易コマンド画面使用チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoUseSimpleCmd_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnShortcutSetting.Enabled = rdoUseShortcut.Checked;
                chkProcessActivate.Enabled = rdoUseShortcut.Checked;
                nudTransparency.Enabled = rdoUseSimpleCmd.Checked;
            }
            catch (Exception ex)
            {
                CommonUtil.PutMessage(CommonEnums.MessageType.Error, ex.Message);
            }
        }

        /// <summary>
        /// リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // フォント設定
                SystemSettingInfo.FontSettingDt = Util.CreateFontSettingTable();

                // システム設定
                SystemSettingInfo.SystemSettingDt = Util.CreatePcotSettingTable();
                var dr = SystemSettingInfo.SystemSettingDt.NewRow();
                SystemSettingInfo.SystemSettingDt.Rows.Add(dr);

                SetSetting();
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
                // フォント設定
                {
                    var orgFontDr = SystemSettingInfo.FontSettingDt.Rows[0];
                    orgFontDr[FontColumns.FontName] = lblOrgFontSample.Font.Name;
                    orgFontDr[FontColumns.FontSize] = (int)lblOrgFontSample.Font.Size;
                    orgFontDr[FontColumns.FontStyle] = (int)lblOrgFontSample.Font.Style;
                    Color orgForeColor = lblOrgFontSample.ForeColor;
                    orgFontDr[FontColumns.FontColor] =
                        $"0x{orgForeColor.R:X2}{orgForeColor.G:X2}{orgForeColor.B:X2}";

                    var rstFontDr = SystemSettingInfo.FontSettingDt.Rows[1];
                    rstFontDr[FontColumns.FontName] = lblRstFontSample.Font.Name;
                    rstFontDr[FontColumns.FontSize] = (int)lblRstFontSample.Font.Size;
                    rstFontDr[FontColumns.FontStyle] = (int)lblRstFontSample.Font.Style;
                    Color rstForeColor = lblRstFontSample.ForeColor;
                    rstFontDr[FontColumns.FontColor] =
                        $"0x{rstForeColor.R:X2}{rstForeColor.G:X2}{rstForeColor.B:X2}";

                    Util.WriteXml(SystemSettingInfo.FontSettingDt, SystemSettingInfo.FontSettingFilePath);
                }

                // システム設定
                {
                    var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
                    dr[SettingColumns.TargetReturn] = chkTargetReturn.Checked;
                    dr[SettingColumns.IgnoreReturn] = chkIgnoreReturn.Checked;
                    dr[SettingColumns.UsingOcrEngine] = (int)cboSelectOcrEngine.SelectedValue;
                    dr[SettingColumns.SpeechAuto] = chkSpeechAuto.Checked;
                    dr[SettingColumns.StopTargetProcess] = chkThreadStop.Checked;
                    dr[SettingColumns.UseShortcut] = rdoUseShortcut.Checked;
                    dr[SettingColumns.ProcessActivate] = chkProcessActivate.Checked;
                    dr[SettingColumns.UseSimpleCmd] = rdoUseSimpleCmd.Checked;
                    dr[SettingColumns.Transparency] = Convert.ToInt32(nudTransparency.Value);

                    if ((int)dr[SettingColumns.RelationDeepLState] == 0)
                    {
                        dr[SettingColumns.CopyResult] = false;
                    }

                    Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
                }

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
                // フォント設定
                SystemSettingInfo.FontSettingDt.Clear();
                SystemSettingInfo.FontSettingDt.ReadXml(SystemSettingInfo.FontSettingFilePath);

                // システム設定
                SystemSettingInfo.SystemSettingDt.Clear();
                SystemSettingInfo.SystemSettingDt.ReadXml(SystemSettingInfo.SystemSettingFilePath);

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
        /// 使用OCRエンジンコンボボックスアイテム設定
        /// </summary>
        private void SetSelectOcrEngineComboBox()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(cboSelectOcrEngine.ValueMember, typeof(int));
            dt.Columns.Add(cboSelectOcrEngine.DisplayMember, typeof(string));

            DataRow dr;

            var osVer = Environment.OSVersion;

            if (Util.IsEnabledWindows10OcrWithWindowsVersion())
            {
                if (Util.IsEnabledWindows10Ocr())
                {
                    dr = dt.NewRow();
                    dr[cboSelectOcrEngine.ValueMember] = 0;
                    dr[cboSelectOcrEngine.DisplayMember] = "Windows10 OCR";
                    dt.Rows.Add(dr);
                }
            }

            dr = dt.NewRow();
            dr[cboSelectOcrEngine.ValueMember] = 1;
            dr[cboSelectOcrEngine.DisplayMember] = "Tesseract OCR";
            dt.Rows.Add(dr);

            cboSelectOcrEngine.DataSource = dt;

            // Tesseractを初期選択
            if (cboSelectOcrEngine.Items.Count > 1)
            {
                cboSelectOcrEngine.SelectedIndex = 1;
            }
            else
            {
                cboSelectOcrEngine.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 設定ファイルの値をセット
        /// </summary>
        private void SetSetting()
        {
            var dr = SystemSettingInfo.SystemSettingDt.Rows[0];
            Color orgColor = Color.Empty;
            Font orgFont = Util.GetFont(0, ref orgColor);
            Color rstColor = Color.Empty;
            Font rstFont = Util.GetFont(1, ref rstColor);

            lblOrgFontSample.Font = orgFont;
            lblOrgFontSample.ForeColor = orgColor;
            lblRstFontSample.Font = rstFont;
            lblRstFontSample.ForeColor = rstColor;

            if (!Util.IsEnabledWindows10Ocr())
            {
                if ((int)dr[SettingColumns.UsingOcrEngine] == 0)
                {
                    // 言語パックがインストールされていない場合は一度Tesseractで設定ファイルを更新
                    dr[SettingColumns.UsingOcrEngine] = 1;
                    Util.WriteXml(SystemSettingInfo.SystemSettingDt, SystemSettingInfo.SystemSettingFilePath);
                }
            }

            chkSpeechAuto.Checked= (bool)dr[SettingColumns.SpeechAuto];
            chkTargetReturn.Checked = (bool)dr[SettingColumns.TargetReturn];
            chkIgnoreReturn.Checked = (bool)dr[SettingColumns.IgnoreReturn];
            cboSelectOcrEngine.SelectedValue = (int)dr[SettingColumns.UsingOcrEngine];
            chkThreadStop.Checked = (bool)dr[SettingColumns.StopTargetProcess];
            rdoUseShortcut.Checked = (bool)dr[SettingColumns.UseShortcut];
            chkProcessActivate.Checked = (bool)dr[SettingColumns.ProcessActivate];
            rdoUseSimpleCmd.Checked = (bool)dr[SettingColumns.UseSimpleCmd];
            btnShortcutSetting.Enabled = rdoUseShortcut.Checked;
            nudTransparency.Value = Convert.ToDecimal(dr[SettingColumns.Transparency]);
            nudTransparency.Enabled = rdoUseSimpleCmd.Checked;
        }
        #endregion
    }
}
