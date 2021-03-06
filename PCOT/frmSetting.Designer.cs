namespace PCOT
{
    partial class frmSetting
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBody = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSpeechSetting = new System.Windows.Forms.Button();
            this.chkSpeechAuto = new System.Windows.Forms.CheckBox();
            this.chkThreadStop = new System.Windows.Forms.CheckBox();
            this.rdoUseShortcut = new System.Windows.Forms.RadioButton();
            this.btnShortcutSetting = new System.Windows.Forms.Button();
            this.rdoUseSimpleCmd = new System.Windows.Forms.RadioButton();
            this.lblTransparency = new System.Windows.Forms.Label();
            this.lblTree = new System.Windows.Forms.Label();
            this.nudTransparency = new System.Windows.Forms.NumericUpDown();
            this.chkProcessActivate = new System.Windows.Forms.CheckBox();
            this.grpDefault = new System.Windows.Forms.GroupBox();
            this.chkIgnoreReturn = new System.Windows.Forms.CheckBox();
            this.chkTargetReturn = new System.Windows.Forms.CheckBox();
            this.lblSelectOcrEngine = new System.Windows.Forms.Label();
            this.cboSelectOcrEngine = new System.Windows.Forms.ComboBox();
            this.grpFontSetting = new System.Windows.Forms.GroupBox();
            this.grpFontSample = new System.Windows.Forms.GroupBox();
            this.lblRstFontSample = new System.Windows.Forms.Label();
            this.lblOrgFontSample = new System.Windows.Forms.Label();
            this.btnSetRstFont = new System.Windows.Forms.Button();
            this.btnSetOrgFont = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.pnlBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransparency)).BeginInit();
            this.grpDefault.SuspendLayout();
            this.grpFontSetting.SuspendLayout();
            this.grpFontSample.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.groupBox1);
            this.pnlBody.Controls.Add(this.grpDefault);
            this.pnlBody.Controls.Add(this.grpFontSetting);
            this.pnlBody.Controls.Add(this.btnReset);
            this.pnlBody.Controls.Add(this.btnCancel);
            this.pnlBody.Controls.Add(this.btnReturn);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(288, 414);
            this.pnlBody.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSpeechSetting);
            this.groupBox1.Controls.Add(this.chkSpeechAuto);
            this.groupBox1.Controls.Add(this.chkThreadStop);
            this.groupBox1.Controls.Add(this.rdoUseShortcut);
            this.groupBox1.Controls.Add(this.btnShortcutSetting);
            this.groupBox1.Controls.Add(this.rdoUseSimpleCmd);
            this.groupBox1.Controls.Add(this.lblTransparency);
            this.groupBox1.Controls.Add(this.lblTree);
            this.groupBox1.Controls.Add(this.nudTransparency);
            this.groupBox1.Controls.Add(this.chkProcessActivate);
            this.groupBox1.Location = new System.Drawing.Point(12, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 135);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共通動作設定";
            // 
            // btnSpeechSetting
            // 
            this.btnSpeechSetting.Location = new System.Drawing.Point(150, 14);
            this.btnSpeechSetting.Name = "btnSpeechSetting";
            this.btnSpeechSetting.Size = new System.Drawing.Size(106, 23);
            this.btnSpeechSetting.TabIndex = 1;
            this.btnSpeechSetting.Text = "音声設定";
            this.btnSpeechSetting.UseVisualStyleBackColor = true;
            this.btnSpeechSetting.Click += new System.EventHandler(this.btnSpeechSetting_Click);
            // 
            // chkSpeechAuto
            // 
            this.chkSpeechAuto.AutoSize = true;
            this.chkSpeechAuto.Location = new System.Drawing.Point(6, 18);
            this.chkSpeechAuto.Name = "chkSpeechAuto";
            this.chkSpeechAuto.Size = new System.Drawing.Size(117, 16);
            this.chkSpeechAuto.TabIndex = 0;
            this.chkSpeechAuto.Text = "翻訳後に音声出力";
            this.chkSpeechAuto.UseVisualStyleBackColor = true;
            // 
            // chkThreadStop
            // 
            this.chkThreadStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkThreadStop.AutoSize = true;
            this.chkThreadStop.Location = new System.Drawing.Point(6, 42);
            this.chkThreadStop.Name = "chkThreadStop";
            this.chkThreadStop.Size = new System.Drawing.Size(231, 16);
            this.chkThreadStop.TabIndex = 2;
            this.chkThreadStop.Text = "範囲選択時に対象プロセスのスレッドを停止";
            this.chkThreadStop.UseVisualStyleBackColor = true;
            // 
            // rdoUseShortcut
            // 
            this.rdoUseShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoUseShortcut.AutoSize = true;
            this.rdoUseShortcut.Checked = true;
            this.rdoUseShortcut.Location = new System.Drawing.Point(6, 66);
            this.rdoUseShortcut.Name = "rdoUseShortcut";
            this.rdoUseShortcut.Size = new System.Drawing.Size(105, 16);
            this.rdoUseShortcut.TabIndex = 3;
            this.rdoUseShortcut.TabStop = true;
            this.rdoUseShortcut.Text = "ショートカット使用";
            this.rdoUseShortcut.UseVisualStyleBackColor = true;
            this.rdoUseShortcut.CheckedChanged += new System.EventHandler(this.rdoUseShortcut_CheckedChanged);
            // 
            // btnShortcutSetting
            // 
            this.btnShortcutSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShortcutSetting.Location = new System.Drawing.Point(150, 62);
            this.btnShortcutSetting.Name = "btnShortcutSetting";
            this.btnShortcutSetting.Size = new System.Drawing.Size(106, 23);
            this.btnShortcutSetting.TabIndex = 4;
            this.btnShortcutSetting.Text = "ショートカット設定";
            this.btnShortcutSetting.UseVisualStyleBackColor = true;
            this.btnShortcutSetting.Click += new System.EventHandler(this.btnShortcutSetting_Click);
            // 
            // rdoUseSimpleCmd
            // 
            this.rdoUseSimpleCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoUseSimpleCmd.AutoSize = true;
            this.rdoUseSimpleCmd.Location = new System.Drawing.Point(6, 110);
            this.rdoUseSimpleCmd.Name = "rdoUseSimpleCmd";
            this.rdoUseSimpleCmd.Size = new System.Drawing.Size(130, 16);
            this.rdoUseSimpleCmd.TabIndex = 6;
            this.rdoUseSimpleCmd.Text = "簡易コマンド画面使用";
            this.rdoUseSimpleCmd.UseVisualStyleBackColor = true;
            this.rdoUseSimpleCmd.CheckedChanged += new System.EventHandler(this.rdoUseSimpleCmd_CheckedChanged);
            // 
            // lblTransparency
            // 
            this.lblTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTransparency.AutoSize = true;
            this.lblTransparency.Location = new System.Drawing.Point(143, 112);
            this.lblTransparency.Name = "lblTransparency";
            this.lblTransparency.Size = new System.Drawing.Size(59, 12);
            this.lblTransparency.TabIndex = 16;
            this.lblTransparency.Text = "不透明度：";
            // 
            // lblTree
            // 
            this.lblTree.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTree.AutoSize = true;
            this.lblTree.Location = new System.Drawing.Point(6, 90);
            this.lblTree.Name = "lblTree";
            this.lblTree.Size = new System.Drawing.Size(17, 12);
            this.lblTree.TabIndex = 19;
            this.lblTree.Text = "└";
            // 
            // nudTransparency
            // 
            this.nudTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudTransparency.Location = new System.Drawing.Point(203, 110);
            this.nudTransparency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTransparency.Name = "nudTransparency";
            this.nudTransparency.Size = new System.Drawing.Size(53, 19);
            this.nudTransparency.TabIndex = 7;
            this.nudTransparency.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // chkProcessActivate
            // 
            this.chkProcessActivate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkProcessActivate.AutoSize = true;
            this.chkProcessActivate.Location = new System.Drawing.Point(24, 88);
            this.chkProcessActivate.Name = "chkProcessActivate";
            this.chkProcessActivate.Size = new System.Drawing.Size(214, 16);
            this.chkProcessActivate.TabIndex = 5;
            this.chkProcessActivate.Text = "翻訳後にフォーカスを対象プロセスに戻す";
            this.chkProcessActivate.UseVisualStyleBackColor = true;
            // 
            // grpDefault
            // 
            this.grpDefault.Controls.Add(this.chkIgnoreReturn);
            this.grpDefault.Controls.Add(this.chkTargetReturn);
            this.grpDefault.Controls.Add(this.lblSelectOcrEngine);
            this.grpDefault.Controls.Add(this.cboSelectOcrEngine);
            this.grpDefault.Location = new System.Drawing.Point(12, 167);
            this.grpDefault.Name = "grpDefault";
            this.grpDefault.Size = new System.Drawing.Size(262, 67);
            this.grpDefault.TabIndex = 1;
            this.grpDefault.TabStop = false;
            this.grpDefault.Text = "フリー選択時のデフォルト動作設定";
            // 
            // chkIgnoreReturn
            // 
            this.chkIgnoreReturn.AutoSize = true;
            this.chkIgnoreReturn.Location = new System.Drawing.Point(119, 18);
            this.chkIgnoreReturn.Name = "chkIgnoreReturn";
            this.chkIgnoreReturn.Size = new System.Drawing.Size(81, 16);
            this.chkIgnoreReturn.TabIndex = 1;
            this.chkIgnoreReturn.Text = "改行を無視";
            this.chkIgnoreReturn.UseVisualStyleBackColor = true;
            this.chkIgnoreReturn.CheckedChanged += new System.EventHandler(this.chkIgnoreReturn_CheckedChanged);
            // 
            // chkTargetReturn
            // 
            this.chkTargetReturn.AutoSize = true;
            this.chkTargetReturn.Location = new System.Drawing.Point(12, 18);
            this.chkTargetReturn.Name = "chkTargetReturn";
            this.chkTargetReturn.Size = new System.Drawing.Size(101, 16);
            this.chkTargetReturn.TabIndex = 0;
            this.chkTargetReturn.Text = "対象通りに改行";
            this.chkTargetReturn.UseVisualStyleBackColor = true;
            this.chkTargetReturn.CheckedChanged += new System.EventHandler(this.chkTargetReturn_CheckedChanged);
            // 
            // lblSelectOcrEngine
            // 
            this.lblSelectOcrEngine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSelectOcrEngine.AutoSize = true;
            this.lblSelectOcrEngine.Location = new System.Drawing.Point(10, 41);
            this.lblSelectOcrEngine.Name = "lblSelectOcrEngine";
            this.lblSelectOcrEngine.Size = new System.Drawing.Size(96, 12);
            this.lblSelectOcrEngine.TabIndex = 17;
            this.lblSelectOcrEngine.Text = "使用OCRエンジン：";
            // 
            // cboSelectOcrEngine
            // 
            this.cboSelectOcrEngine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboSelectOcrEngine.DisplayMember = "Text";
            this.cboSelectOcrEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectOcrEngine.FormattingEnabled = true;
            this.cboSelectOcrEngine.Location = new System.Drawing.Point(112, 38);
            this.cboSelectOcrEngine.Name = "cboSelectOcrEngine";
            this.cboSelectOcrEngine.Size = new System.Drawing.Size(144, 20);
            this.cboSelectOcrEngine.TabIndex = 2;
            this.cboSelectOcrEngine.ValueMember = "Value";
            // 
            // grpFontSetting
            // 
            this.grpFontSetting.Controls.Add(this.grpFontSample);
            this.grpFontSetting.Controls.Add(this.btnSetRstFont);
            this.grpFontSetting.Controls.Add(this.btnSetOrgFont);
            this.grpFontSetting.Location = new System.Drawing.Point(12, 12);
            this.grpFontSetting.Name = "grpFontSetting";
            this.grpFontSetting.Size = new System.Drawing.Size(262, 149);
            this.grpFontSetting.TabIndex = 0;
            this.grpFontSetting.TabStop = false;
            this.grpFontSetting.Text = "フォント設定";
            // 
            // grpFontSample
            // 
            this.grpFontSample.Controls.Add(this.lblRstFontSample);
            this.grpFontSample.Controls.Add(this.lblOrgFontSample);
            this.grpFontSample.Location = new System.Drawing.Point(6, 47);
            this.grpFontSample.Name = "grpFontSample";
            this.grpFontSample.Size = new System.Drawing.Size(250, 95);
            this.grpFontSample.TabIndex = 1;
            this.grpFontSample.TabStop = false;
            this.grpFontSample.Text = "フォントサンプル";
            // 
            // lblRstFontSample
            // 
            this.lblRstFontSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRstFontSample.Location = new System.Drawing.Point(139, 17);
            this.lblRstFontSample.Name = "lblRstFontSample";
            this.lblRstFontSample.Size = new System.Drawing.Size(100, 69);
            this.lblRstFontSample.TabIndex = 0;
            this.lblRstFontSample.Text = "Aaあぁ\r\nアァ亜宇";
            this.lblRstFontSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrgFontSample
            // 
            this.lblOrgFontSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrgFontSample.Location = new System.Drawing.Point(12, 17);
            this.lblOrgFontSample.Name = "lblOrgFontSample";
            this.lblOrgFontSample.Size = new System.Drawing.Size(100, 69);
            this.lblOrgFontSample.TabIndex = 0;
            this.lblOrgFontSample.Text = "AaBb\r\nYyZz";
            this.lblOrgFontSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetRstFont
            // 
            this.btnSetRstFont.Location = new System.Drawing.Point(134, 18);
            this.btnSetRstFont.Name = "btnSetRstFont";
            this.btnSetRstFont.Size = new System.Drawing.Size(122, 23);
            this.btnSetRstFont.TabIndex = 1;
            this.btnSetRstFont.Text = "訳文フォント設定";
            this.btnSetRstFont.UseVisualStyleBackColor = true;
            this.btnSetRstFont.Click += new System.EventHandler(this.btnSetRstFont_Click);
            // 
            // btnSetOrgFont
            // 
            this.btnSetOrgFont.Location = new System.Drawing.Point(6, 18);
            this.btnSetOrgFont.Name = "btnSetOrgFont";
            this.btnSetOrgFont.Size = new System.Drawing.Size(122, 23);
            this.btnSetOrgFont.TabIndex = 0;
            this.btnSetOrgFont.Text = "原文フォント設定";
            this.btnSetOrgFont.UseVisualStyleBackColor = true;
            this.btnSetOrgFont.Click += new System.EventHandler(this.btnSetOrgFont_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(15, 381);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "リセット";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(199, 381);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(118, 381);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "確定";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(288, 414);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PCOTシステム設定";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.pnlBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransparency)).EndInit();
            this.grpDefault.ResumeLayout(false);
            this.grpDefault.PerformLayout();
            this.grpFontSetting.ResumeLayout(false);
            this.grpFontSample.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblTransparency;
        private System.Windows.Forms.NumericUpDown nudTransparency;
        private System.Windows.Forms.RadioButton rdoUseShortcut;
        private System.Windows.Forms.Button btnShortcutSetting;
        private System.Windows.Forms.RadioButton rdoUseSimpleCmd;
        private System.Windows.Forms.ComboBox cboSelectOcrEngine;
        private System.Windows.Forms.Label lblSelectOcrEngine;
        private System.Windows.Forms.CheckBox chkThreadStop;
        private System.Windows.Forms.CheckBox chkProcessActivate;
        private System.Windows.Forms.Label lblTree;
        private System.Windows.Forms.GroupBox grpFontSetting;
        private System.Windows.Forms.GroupBox grpFontSample;
        private System.Windows.Forms.Label lblRstFontSample;
        private System.Windows.Forms.Label lblOrgFontSample;
        private System.Windows.Forms.Button btnSetRstFont;
        private System.Windows.Forms.Button btnSetOrgFont;
        private System.Windows.Forms.GroupBox grpDefault;
        private System.Windows.Forms.CheckBox chkTargetReturn;
        private System.Windows.Forms.CheckBox chkIgnoreReturn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSpeechSetting;
        private System.Windows.Forms.CheckBox chkSpeechAuto;
    }
}

