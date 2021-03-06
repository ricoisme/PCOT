namespace PCOT
{
    partial class frmShowTranslatedText
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowTranslatedText));
            this.cboLabel = new System.Windows.Forms.ComboBox();
            this.btnTranSetting = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnFreeSelect = new System.Windows.Forms.Button();
            this.btnSelectPrcs = new System.Windows.Forms.Button();
            this.btnDic = new System.Windows.Forms.Button();
            this.timChkPrcs = new System.Windows.Forms.Timer(this.components);
            this.chkTargetReturn = new System.Windows.Forms.CheckBox();
            this.chkIgnoreReturn = new System.Windows.Forms.CheckBox();
            this.btnNoun = new System.Windows.Forms.Button();
            this.btnRecnctPrcs = new System.Windows.Forms.Button();
            this.pnlProcessContainer = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnChangeCmdMode = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.stsConnectState = new System.Windows.Forms.StatusStrip();
            this.tssConnectStateLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssUsingTranslator = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlTranslateContainer = new System.Windows.Forms.Panel();
            this.spcSplit = new System.Windows.Forms.SplitContainer();
            this.pnlOrgText = new System.Windows.Forms.Panel();
            this.txtOrgText = new System.Windows.Forms.RichTextBox();
            this.pnlResultText = new System.Windows.Forms.Panel();
            this.txtResultText = new System.Windows.Forms.RichTextBox();
            this.pnlMiddleCmd = new System.Windows.Forms.Panel();
            this.btnRegistHistory = new System.Windows.Forms.Button();
            this.btnOutputResult = new System.Windows.Forms.Button();
            this.chkCopyResult = new System.Windows.Forms.CheckBox();
            this.btnImmediateTrans = new System.Windows.Forms.Button();
            this.chkRelationDeepL = new System.Windows.Forms.CheckBox();
            this.btnRegistDic = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRegistNoun = new System.Windows.Forms.Button();
            this.pnlModuleContainer = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnImgTranslate = new System.Windows.Forms.Button();
            this.ntfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmpPcotMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuExitPcot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMenu = new System.Windows.Forms.MenuStrip();
            this.tsmProcessMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProcessSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDisConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowSimple = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExitPcot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSystemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlProcessContainer.SuspendLayout();
            this.stsConnectState.SuspendLayout();
            this.pnlTranslateContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcSplit)).BeginInit();
            this.spcSplit.Panel1.SuspendLayout();
            this.spcSplit.Panel2.SuspendLayout();
            this.spcSplit.SuspendLayout();
            this.pnlOrgText.SuspendLayout();
            this.pnlResultText.SuspendLayout();
            this.pnlMiddleCmd.SuspendLayout();
            this.pnlModuleContainer.SuspendLayout();
            this.cmpPcotMenu.SuspendLayout();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboLabel
            // 
            this.cboLabel.DisplayMember = "Title";
            this.cboLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabel.FormattingEnabled = true;
            this.cboLabel.Location = new System.Drawing.Point(93, 3);
            this.cboLabel.Name = "cboLabel";
            this.cboLabel.Size = new System.Drawing.Size(156, 20);
            this.cboLabel.TabIndex = 1;
            this.cboLabel.ValueMember = "Id";
            this.cboLabel.SelectedIndexChanged += new System.EventHandler(this.cboLabel_SelectedIndexChanged);
            // 
            // btnTranSetting
            // 
            this.btnTranSetting.Location = new System.Drawing.Point(12, 2);
            this.btnTranSetting.Name = "btnTranSetting";
            this.btnTranSetting.Size = new System.Drawing.Size(75, 45);
            this.btnTranSetting.TabIndex = 0;
            this.btnTranSetting.Text = "タイトル設定";
            this.btnTranSetting.UseVisualStyleBackColor = true;
            this.btnTranSetting.Click += new System.EventHandler(this.btnTranSetting_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(255, 2);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(75, 45);
            this.btnTranslate.TabIndex = 3;
            this.btnTranslate.Text = "翻訳";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnFreeSelect
            // 
            this.btnFreeSelect.Location = new System.Drawing.Point(92, 24);
            this.btnFreeSelect.Name = "btnFreeSelect";
            this.btnFreeSelect.Size = new System.Drawing.Size(158, 23);
            this.btnFreeSelect.TabIndex = 2;
            this.btnFreeSelect.Text = "フリー選択";
            this.btnFreeSelect.UseVisualStyleBackColor = true;
            this.btnFreeSelect.Click += new System.EventHandler(this.btnFreeSelect_Click);
            // 
            // btnSelectPrcs
            // 
            this.btnSelectPrcs.Location = new System.Drawing.Point(13, 1);
            this.btnSelectPrcs.Name = "btnSelectPrcs";
            this.btnSelectPrcs.Size = new System.Drawing.Size(110, 23);
            this.btnSelectPrcs.TabIndex = 0;
            this.btnSelectPrcs.Text = "プロセス再選択";
            this.btnSelectPrcs.UseVisualStyleBackColor = true;
            this.btnSelectPrcs.Click += new System.EventHandler(this.btnSelectPrcs_Click);
            // 
            // btnDic
            // 
            this.btnDic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDic.Location = new System.Drawing.Point(606, 2);
            this.btnDic.Name = "btnDic";
            this.btnDic.Size = new System.Drawing.Size(76, 45);
            this.btnDic.TabIndex = 8;
            this.btnDic.Text = "辞書リスト";
            this.btnDic.UseVisualStyleBackColor = true;
            this.btnDic.Click += new System.EventHandler(this.btnDic_Click);
            // 
            // timChkPrcs
            // 
            this.timChkPrcs.Interval = 500;
            this.timChkPrcs.Tick += new System.EventHandler(this.timChkPrcs_Tick);
            // 
            // chkTargetReturn
            // 
            this.chkTargetReturn.AutoSize = true;
            this.chkTargetReturn.Location = new System.Drawing.Point(417, 7);
            this.chkTargetReturn.Name = "chkTargetReturn";
            this.chkTargetReturn.Size = new System.Drawing.Size(101, 16);
            this.chkTargetReturn.TabIndex = 5;
            this.chkTargetReturn.Text = "対象通りに改行";
            this.chkTargetReturn.UseVisualStyleBackColor = true;
            this.chkTargetReturn.CheckedChanged += new System.EventHandler(this.chkTargetReturn_CheckedChanged);
            // 
            // chkIgnoreReturn
            // 
            this.chkIgnoreReturn.Location = new System.Drawing.Point(417, 24);
            this.chkIgnoreReturn.Name = "chkIgnoreReturn";
            this.chkIgnoreReturn.Size = new System.Drawing.Size(100, 20);
            this.chkIgnoreReturn.TabIndex = 6;
            this.chkIgnoreReturn.Text = "改行を無視";
            this.chkIgnoreReturn.UseVisualStyleBackColor = true;
            this.chkIgnoreReturn.CheckedChanged += new System.EventHandler(this.chkIgnoreReturn_CheckedChanged);
            // 
            // btnNoun
            // 
            this.btnNoun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoun.Location = new System.Drawing.Point(688, 2);
            this.btnNoun.Name = "btnNoun";
            this.btnNoun.Size = new System.Drawing.Size(76, 45);
            this.btnNoun.TabIndex = 9;
            this.btnNoun.Text = "名詞リスト";
            this.btnNoun.UseVisualStyleBackColor = true;
            this.btnNoun.Click += new System.EventHandler(this.btnNoun_Click);
            // 
            // btnRecnctPrcs
            // 
            this.btnRecnctPrcs.Location = new System.Drawing.Point(129, 1);
            this.btnRecnctPrcs.Name = "btnRecnctPrcs";
            this.btnRecnctPrcs.Size = new System.Drawing.Size(75, 23);
            this.btnRecnctPrcs.TabIndex = 1;
            this.btnRecnctPrcs.Text = "再接続";
            this.btnRecnctPrcs.UseVisualStyleBackColor = true;
            this.btnRecnctPrcs.Click += new System.EventHandler(this.btnRecnctPrcs_Click);
            // 
            // pnlProcessContainer
            // 
            this.pnlProcessContainer.Controls.Add(this.btnStop);
            this.pnlProcessContainer.Controls.Add(this.btnPause);
            this.pnlProcessContainer.Controls.Add(this.btnPlay);
            this.pnlProcessContainer.Controls.Add(this.btnChangeCmdMode);
            this.pnlProcessContainer.Controls.Add(this.btnExit);
            this.pnlProcessContainer.Controls.Add(this.btnSelectPrcs);
            this.pnlProcessContainer.Controls.Add(this.btnRecnctPrcs);
            this.pnlProcessContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProcessContainer.Location = new System.Drawing.Point(0, 448);
            this.pnlProcessContainer.Name = "pnlProcessContainer";
            this.pnlProcessContainer.Size = new System.Drawing.Size(778, 27);
            this.pnlProcessContainer.TabIndex = 15;
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::PCOT.Properties.Resources.stop;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStop.Location = new System.Drawing.Point(388, 1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::PCOT.Properties.Resources.pause;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPause.Location = new System.Drawing.Point(362, 1);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = global::PCOT.Properties.Resources.run;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPlay.Location = new System.Drawing.Point(336, 1);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(23, 23);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnChangeCmdMode
            // 
            this.btnChangeCmdMode.Location = new System.Drawing.Point(210, 1);
            this.btnChangeCmdMode.Name = "btnChangeCmdMode";
            this.btnChangeCmdMode.Size = new System.Drawing.Size(120, 23);
            this.btnChangeCmdMode.TabIndex = 2;
            this.btnChangeCmdMode.Text = "簡易コマンドに切替";
            this.btnChangeCmdMode.UseVisualStyleBackColor = true;
            this.btnChangeCmdMode.Click += new System.EventHandler(this.btnChangeCmdMode_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(669, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(95, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "PCOTを終了";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // stsConnectState
            // 
            this.stsConnectState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssConnectStateLbl,
            this.tssUsingTranslator});
            this.stsConnectState.Location = new System.Drawing.Point(0, 475);
            this.stsConnectState.Name = "stsConnectState";
            this.stsConnectState.Size = new System.Drawing.Size(778, 25);
            this.stsConnectState.TabIndex = 17;
            this.stsConnectState.Text = "statusStrip1";
            // 
            // tssConnectStateLbl
            // 
            this.tssConnectStateLbl.BackColor = System.Drawing.SystemColors.Control;
            this.tssConnectStateLbl.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssConnectStateLbl.Margin = new System.Windows.Forms.Padding(2, 4, 0, 2);
            this.tssConnectStateLbl.Name = "tssConnectStateLbl";
            this.tssConnectStateLbl.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.tssConnectStateLbl.Size = new System.Drawing.Size(79, 19);
            this.tssConnectStateLbl.Text = "接続中：";
            // 
            // tssUsingTranslator
            // 
            this.tssUsingTranslator.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssUsingTranslator.Margin = new System.Windows.Forms.Padding(2, 4, 0, 2);
            this.tssUsingTranslator.Name = "tssUsingTranslator";
            this.tssUsingTranslator.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tssUsingTranslator.Size = new System.Drawing.Size(83, 19);
            this.tssUsingTranslator.Text = "Google翻訳";
            // 
            // pnlTranslateContainer
            // 
            this.pnlTranslateContainer.Controls.Add(this.spcSplit);
            this.pnlTranslateContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTranslateContainer.Location = new System.Drawing.Point(0, 73);
            this.pnlTranslateContainer.Name = "pnlTranslateContainer";
            this.pnlTranslateContainer.Size = new System.Drawing.Size(778, 375);
            this.pnlTranslateContainer.TabIndex = 1;
            // 
            // spcSplit
            // 
            this.spcSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcSplit.Location = new System.Drawing.Point(13, 3);
            this.spcSplit.Name = "spcSplit";
            this.spcSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcSplit.Panel1
            // 
            this.spcSplit.Panel1.Controls.Add(this.pnlOrgText);
            this.spcSplit.Panel1MinSize = 1;
            // 
            // spcSplit.Panel2
            // 
            this.spcSplit.Panel2.Controls.Add(this.pnlResultText);
            this.spcSplit.Panel2.Controls.Add(this.pnlMiddleCmd);
            this.spcSplit.Size = new System.Drawing.Size(751, 369);
            this.spcSplit.SplitterDistance = 167;
            this.spcSplit.TabIndex = 22;
            // 
            // pnlOrgText
            // 
            this.pnlOrgText.BackColor = System.Drawing.SystemColors.Window;
            this.pnlOrgText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrgText.Controls.Add(this.txtOrgText);
            this.pnlOrgText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrgText.Location = new System.Drawing.Point(0, 0);
            this.pnlOrgText.Margin = new System.Windows.Forms.Padding(0);
            this.pnlOrgText.Name = "pnlOrgText";
            this.pnlOrgText.Padding = new System.Windows.Forms.Padding(5);
            this.pnlOrgText.Size = new System.Drawing.Size(751, 167);
            this.pnlOrgText.TabIndex = 20;
            // 
            // txtOrgText
            // 
            this.txtOrgText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOrgText.DetectUrls = false;
            this.txtOrgText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOrgText.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOrgText.Location = new System.Drawing.Point(5, 5);
            this.txtOrgText.Name = "txtOrgText";
            this.txtOrgText.Size = new System.Drawing.Size(739, 155);
            this.txtOrgText.TabIndex = 0;
            this.txtOrgText.Text = "";
            this.txtOrgText.TextChanged += new System.EventHandler(this.txtOrgText_TextChanged);
            this.txtOrgText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrgText_KeyDown);
            this.txtOrgText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtOrgText_MouseDown);
            // 
            // pnlResultText
            // 
            this.pnlResultText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResultText.Controls.Add(this.txtResultText);
            this.pnlResultText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultText.Location = new System.Drawing.Point(0, 29);
            this.pnlResultText.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultText.Name = "pnlResultText";
            this.pnlResultText.Padding = new System.Windows.Forms.Padding(5);
            this.pnlResultText.Size = new System.Drawing.Size(751, 169);
            this.pnlResultText.TabIndex = 21;
            // 
            // txtResultText
            // 
            this.txtResultText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResultText.DetectUrls = false;
            this.txtResultText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResultText.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtResultText.Location = new System.Drawing.Point(5, 5);
            this.txtResultText.Name = "txtResultText";
            this.txtResultText.ReadOnly = true;
            this.txtResultText.Size = new System.Drawing.Size(739, 157);
            this.txtResultText.TabIndex = 0;
            this.txtResultText.Text = "";
            this.txtResultText.Leave += new System.EventHandler(this.txtResultText_Leave);
            // 
            // pnlMiddleCmd
            // 
            this.pnlMiddleCmd.Controls.Add(this.btnRegistHistory);
            this.pnlMiddleCmd.Controls.Add(this.btnOutputResult);
            this.pnlMiddleCmd.Controls.Add(this.chkCopyResult);
            this.pnlMiddleCmd.Controls.Add(this.btnImmediateTrans);
            this.pnlMiddleCmd.Controls.Add(this.chkRelationDeepL);
            this.pnlMiddleCmd.Controls.Add(this.btnRegistDic);
            this.pnlMiddleCmd.Controls.Add(this.btnClear);
            this.pnlMiddleCmd.Controls.Add(this.btnRegistNoun);
            this.pnlMiddleCmd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMiddleCmd.Location = new System.Drawing.Point(0, 0);
            this.pnlMiddleCmd.Name = "pnlMiddleCmd";
            this.pnlMiddleCmd.Size = new System.Drawing.Size(751, 29);
            this.pnlMiddleCmd.TabIndex = 0;
            // 
            // btnRegistHistory
            // 
            this.btnRegistHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistHistory.Location = new System.Drawing.Point(512, 1);
            this.btnRegistHistory.Name = "btnRegistHistory";
            this.btnRegistHistory.Size = new System.Drawing.Size(75, 23);
            this.btnRegistHistory.TabIndex = 5;
            this.btnRegistHistory.Text = "履歴登録";
            this.btnRegistHistory.UseVisualStyleBackColor = true;
            this.btnRegistHistory.Click += new System.EventHandler(this.btnRegistHistory_Click);
            // 
            // btnOutputResult
            // 
            this.btnOutputResult.Location = new System.Drawing.Point(167, 1);
            this.btnOutputResult.Name = "btnOutputResult";
            this.btnOutputResult.Size = new System.Drawing.Size(90, 23);
            this.btnOutputResult.TabIndex = 2;
            this.btnOutputResult.Text = "翻訳結果出力";
            this.btnOutputResult.UseVisualStyleBackColor = true;
            this.btnOutputResult.Click += new System.EventHandler(this.btnOutputResult_Click);
            // 
            // chkCopyResult
            // 
            this.chkCopyResult.AutoSize = true;
            this.chkCopyResult.Location = new System.Drawing.Point(357, 5);
            this.chkCopyResult.Name = "chkCopyResult";
            this.chkCopyResult.Size = new System.Drawing.Size(108, 16);
            this.chkCopyResult.TabIndex = 4;
            this.chkCopyResult.Text = "翻訳結果をコピー";
            this.chkCopyResult.UseVisualStyleBackColor = true;
            this.chkCopyResult.CheckedChanged += new System.EventHandler(this.chkCopyResult_CheckedChanged);
            // 
            // btnImmediateTrans
            // 
            this.btnImmediateTrans.Location = new System.Drawing.Point(5, 1);
            this.btnImmediateTrans.Name = "btnImmediateTrans";
            this.btnImmediateTrans.Size = new System.Drawing.Size(75, 23);
            this.btnImmediateTrans.TabIndex = 0;
            this.btnImmediateTrans.Text = "即時翻訳";
            this.btnImmediateTrans.UseVisualStyleBackColor = true;
            this.btnImmediateTrans.Click += new System.EventHandler(this.btnImmediateTrans_Click);
            // 
            // chkRelationDeepL
            // 
            this.chkRelationDeepL.AutoSize = true;
            this.chkRelationDeepL.Checked = true;
            this.chkRelationDeepL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRelationDeepL.Location = new System.Drawing.Point(263, 5);
            this.chkRelationDeepL.Name = "chkRelationDeepL";
            this.chkRelationDeepL.Size = new System.Drawing.Size(88, 16);
            this.chkRelationDeepL.TabIndex = 3;
            this.chkRelationDeepL.Text = "DeepLと連携";
            this.chkRelationDeepL.ThreeState = true;
            this.chkRelationDeepL.UseVisualStyleBackColor = true;
            this.chkRelationDeepL.CheckStateChanged += new System.EventHandler(this.chkRelationDeepL_CheckStateChanged);
            // 
            // btnRegistDic
            // 
            this.btnRegistDic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistDic.Location = new System.Drawing.Point(591, 1);
            this.btnRegistDic.Name = "btnRegistDic";
            this.btnRegistDic.Size = new System.Drawing.Size(75, 23);
            this.btnRegistDic.TabIndex = 6;
            this.btnRegistDic.Text = "辞書登録";
            this.btnRegistDic.UseVisualStyleBackColor = true;
            this.btnRegistDic.Click += new System.EventHandler(this.btnRegistDic_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(86, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "クリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRegistNoun
            // 
            this.btnRegistNoun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistNoun.Location = new System.Drawing.Point(671, 1);
            this.btnRegistNoun.Name = "btnRegistNoun";
            this.btnRegistNoun.Size = new System.Drawing.Size(75, 23);
            this.btnRegistNoun.TabIndex = 7;
            this.btnRegistNoun.Text = "名詞登録";
            this.btnRegistNoun.UseVisualStyleBackColor = true;
            this.btnRegistNoun.Click += new System.EventHandler(this.btnRegistNoun_Click);
            // 
            // pnlModuleContainer
            // 
            this.pnlModuleContainer.Controls.Add(this.btnHistory);
            this.pnlModuleContainer.Controls.Add(this.btnImgTranslate);
            this.pnlModuleContainer.Controls.Add(this.chkTargetReturn);
            this.pnlModuleContainer.Controls.Add(this.chkIgnoreReturn);
            this.pnlModuleContainer.Controls.Add(this.btnNoun);
            this.pnlModuleContainer.Controls.Add(this.btnTranSetting);
            this.pnlModuleContainer.Controls.Add(this.cboLabel);
            this.pnlModuleContainer.Controls.Add(this.btnTranslate);
            this.pnlModuleContainer.Controls.Add(this.btnDic);
            this.pnlModuleContainer.Controls.Add(this.btnFreeSelect);
            this.pnlModuleContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlModuleContainer.Location = new System.Drawing.Point(0, 24);
            this.pnlModuleContainer.Name = "pnlModuleContainer";
            this.pnlModuleContainer.Size = new System.Drawing.Size(778, 49);
            this.pnlModuleContainer.TabIndex = 0;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.Location = new System.Drawing.Point(525, 2);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(76, 45);
            this.btnHistory.TabIndex = 7;
            this.btnHistory.Text = "履歴リスト";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnImgTranslate
            // 
            this.btnImgTranslate.Location = new System.Drawing.Point(336, 2);
            this.btnImgTranslate.Name = "btnImgTranslate";
            this.btnImgTranslate.Size = new System.Drawing.Size(75, 45);
            this.btnImgTranslate.TabIndex = 4;
            this.btnImgTranslate.Text = "画像翻訳";
            this.btnImgTranslate.UseVisualStyleBackColor = true;
            this.btnImgTranslate.Click += new System.EventHandler(this.btnImgTranslate_Click);
            // 
            // ntfIcon
            // 
            this.ntfIcon.BalloonTipText = "PCOT";
            this.ntfIcon.ContextMenuStrip = this.cmpPcotMenu;
            this.ntfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfIcon.Icon")));
            this.ntfIcon.Text = "PCOT";
            this.ntfIcon.Visible = true;
            this.ntfIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ntfIcon_MouseDown);
            // 
            // cmpPcotMenu
            // 
            this.cmpPcotMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExitPcot});
            this.cmpPcotMenu.Name = "cmpPcotMenu";
            this.cmpPcotMenu.Size = new System.Drawing.Size(137, 26);
            // 
            // mnuExitPcot
            // 
            this.mnuExitPcot.Name = "mnuExitPcot";
            this.mnuExitPcot.Size = new System.Drawing.Size(136, 22);
            this.mnuExitPcot.Text = "PCOTを終了";
            this.mnuExitPcot.Click += new System.EventHandler(this.mnuExitPcot_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmProcessMenu,
            this.tsmShowModule,
            this.tsmSetting,
            this.tsmExit});
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(778, 24);
            this.mnuMenu.TabIndex = 1;
            this.mnuMenu.Text = "menuStrip1";
            // 
            // tsmProcessMenu
            // 
            this.tsmProcessMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmProcessSelect,
            this.tsmDisConnect,
            this.tsmReConnect});
            this.tsmProcessMenu.Name = "tsmProcessMenu";
            this.tsmProcessMenu.Size = new System.Drawing.Size(56, 20);
            this.tsmProcessMenu.Text = "プロセス";
            // 
            // tsmProcessSelect
            // 
            this.tsmProcessSelect.Name = "tsmProcessSelect";
            this.tsmProcessSelect.Size = new System.Drawing.Size(159, 22);
            this.tsmProcessSelect.Text = "プロセス再選択";
            this.tsmProcessSelect.Click += new System.EventHandler(this.tsmProcessSelect_Click);
            // 
            // tsmDisConnect
            // 
            this.tsmDisConnect.Name = "tsmDisConnect";
            this.tsmDisConnect.Size = new System.Drawing.Size(159, 22);
            this.tsmDisConnect.Text = "プロセス接続解除";
            this.tsmDisConnect.Click += new System.EventHandler(this.tsmDisConnect_Click);
            // 
            // tsmReConnect
            // 
            this.tsmReConnect.Enabled = false;
            this.tsmReConnect.Name = "tsmReConnect";
            this.tsmReConnect.Size = new System.Drawing.Size(159, 22);
            this.tsmReConnect.Text = "プロセス再接続";
            this.tsmReConnect.Click += new System.EventHandler(this.tsmReConnect_Click);
            // 
            // tsmShowModule
            // 
            this.tsmShowModule.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowSimple});
            this.tsmShowModule.Name = "tsmShowModule";
            this.tsmShowModule.Size = new System.Drawing.Size(43, 20);
            this.tsmShowModule.Text = "表示";
            // 
            // tsmShowSimple
            // 
            this.tsmShowSimple.CheckOnClick = true;
            this.tsmShowSimple.Name = "tsmShowSimple";
            this.tsmShowSimple.Size = new System.Drawing.Size(136, 22);
            this.tsmShowSimple.Text = "シンプル表示";
            this.tsmShowSimple.Click += new System.EventHandler(this.tsmShowSimple_Click);
            // 
            // tsmSetting
            // 
            this.tsmSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSystemSetting});
            this.tsmSetting.Name = "tsmSetting";
            this.tsmSetting.Size = new System.Drawing.Size(43, 20);
            this.tsmSetting.Text = "設定";
            // 
            // tsmExit
            // 
            this.tsmExit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmExitPcot});
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(43, 20);
            this.tsmExit.Text = "終了";
            // 
            // tsmExitPcot
            // 
            this.tsmExitPcot.Name = "tsmExitPcot";
            this.tsmExitPcot.Size = new System.Drawing.Size(136, 22);
            this.tsmExitPcot.Text = "PCOTを終了";
            this.tsmExitPcot.Click += new System.EventHandler(this.tsmExitPcot_Click);
            // 
            // tsmSystemSetting
            // 
            this.tsmSystemSetting.Name = "tsmSystemSetting";
            this.tsmSystemSetting.Size = new System.Drawing.Size(180, 22);
            this.tsmSystemSetting.Text = "システム設定";
            this.tsmSystemSetting.Click += new System.EventHandler(this.tsmSystemSetting_Click);
            // 
            // frmShowTranslatedText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(778, 500);
            this.Controls.Add(this.pnlTranslateContainer);
            this.Controls.Add(this.pnlProcessContainer);
            this.Controls.Add(this.stsConnectState);
            this.Controls.Add(this.pnlModuleContainer);
            this.Controls.Add(this.mnuMenu);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(794, 39);
            this.Name = "frmShowTranslatedText";
            this.Text = "翻訳表示";
            this.Activated += new System.EventHandler(this.frmShowTranslatedText_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShowTranslatedText_FormClosing);
            this.Load += new System.EventHandler(this.frmShowTranslatedText_Load);
            this.Shown += new System.EventHandler(this.frmShowTranslatedText_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmShowTranslatedText_KeyDown);
            this.pnlProcessContainer.ResumeLayout(false);
            this.stsConnectState.ResumeLayout(false);
            this.stsConnectState.PerformLayout();
            this.pnlTranslateContainer.ResumeLayout(false);
            this.spcSplit.Panel1.ResumeLayout(false);
            this.spcSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcSplit)).EndInit();
            this.spcSplit.ResumeLayout(false);
            this.pnlOrgText.ResumeLayout(false);
            this.pnlResultText.ResumeLayout(false);
            this.pnlMiddleCmd.ResumeLayout(false);
            this.pnlMiddleCmd.PerformLayout();
            this.pnlModuleContainer.ResumeLayout(false);
            this.pnlModuleContainer.PerformLayout();
            this.cmpPcotMenu.ResumeLayout(false);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboLabel;
        private System.Windows.Forms.Button btnTranSetting;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnFreeSelect;
        private System.Windows.Forms.Button btnSelectPrcs;
        private System.Windows.Forms.Button btnDic;
        private System.Windows.Forms.Timer timChkPrcs;
        private System.Windows.Forms.Button btnNoun;
        private System.Windows.Forms.CheckBox chkIgnoreReturn;
        private System.Windows.Forms.Button btnRecnctPrcs;
        private System.Windows.Forms.CheckBox chkTargetReturn;
        private System.Windows.Forms.Panel pnlProcessContainer;
        private System.Windows.Forms.Panel pnlTranslateContainer;
        private System.Windows.Forms.Panel pnlResultText;
        private System.Windows.Forms.RichTextBox txtResultText;
        private System.Windows.Forms.Panel pnlOrgText;
        private System.Windows.Forms.RichTextBox txtOrgText;
        private System.Windows.Forms.Button btnRegistNoun;
        private System.Windows.Forms.Button btnRegistDic;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnImmediateTrans;
        private System.Windows.Forms.CheckBox chkRelationDeepL;
        private System.Windows.Forms.Panel pnlModuleContainer;
        private System.Windows.Forms.NotifyIcon ntfIcon;
        private System.Windows.Forms.ContextMenuStrip cmpPcotMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuExitPcot;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnImgTranslate;
        private System.Windows.Forms.StatusStrip stsConnectState;
        private System.Windows.Forms.ToolStripStatusLabel tssConnectStateLbl;
        private System.Windows.Forms.SplitContainer spcSplit;
        private System.Windows.Forms.Panel pnlMiddleCmd;
        private System.Windows.Forms.CheckBox chkCopyResult;
        private System.Windows.Forms.ToolStripStatusLabel tssUsingTranslator;
        private System.Windows.Forms.Button btnOutputResult;
        private System.Windows.Forms.Button btnRegistHistory;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnChangeCmdMode;
        private System.Windows.Forms.MenuStrip mnuMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmProcessMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmProcessSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmDisConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmReConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmShowModule;
        private System.Windows.Forms.ToolStripMenuItem tsmShowSimple;
        private System.Windows.Forms.ToolStripMenuItem tsmSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmExitPcot;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ToolStripMenuItem tsmSystemSetting;
    }
}