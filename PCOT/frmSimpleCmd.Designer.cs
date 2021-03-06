namespace PCOT
{
    partial class frmSimpleCmd
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
            this.pnlClient = new System.Windows.Forms.Panel();
            this.btnClipboardTranslate = new System.Windows.Forms.Button();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnFreeSelect = new System.Windows.Forms.Button();
            this.cboLabel = new System.Windows.Forms.ComboBox();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblTransparent = new System.Windows.Forms.Label();
            this.timActive = new System.Windows.Forms.Timer(this.components);
            this.lblClose = new System.Windows.Forms.Label();
            this.pnlClient.SuspendLayout();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlClient
            // 
            this.pnlClient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClient.Controls.Add(this.btnClipboardTranslate);
            this.pnlClient.Controls.Add(this.btnTranslate);
            this.pnlClient.Controls.Add(this.btnFreeSelect);
            this.pnlClient.Controls.Add(this.cboLabel);
            this.pnlClient.Controls.Add(this.pnlTitleBar);
            this.pnlClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClient.Location = new System.Drawing.Point(0, 0);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(149, 122);
            this.pnlClient.TabIndex = 1;
            // 
            // btnClipboardTranslate
            // 
            this.btnClipboardTranslate.Location = new System.Drawing.Point(0, 42);
            this.btnClipboardTranslate.Name = "btnClipboardTranslate";
            this.btnClipboardTranslate.Size = new System.Drawing.Size(147, 24);
            this.btnClipboardTranslate.TabIndex = 3;
            this.btnClipboardTranslate.Text = "クリップボード翻訳";
            this.btnClipboardTranslate.UseVisualStyleBackColor = true;
            this.btnClipboardTranslate.Click += new System.EventHandler(this.btnClipboardTranslate_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(73, 65);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(74, 55);
            this.btnTranslate.TabIndex = 2;
            this.btnTranslate.Text = "翻訳";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnFreeSelect
            // 
            this.btnFreeSelect.Location = new System.Drawing.Point(0, 65);
            this.btnFreeSelect.Name = "btnFreeSelect";
            this.btnFreeSelect.Size = new System.Drawing.Size(74, 55);
            this.btnFreeSelect.TabIndex = 1;
            this.btnFreeSelect.Text = "フリー\r\n選択";
            this.btnFreeSelect.UseVisualStyleBackColor = true;
            this.btnFreeSelect.Click += new System.EventHandler(this.btnFreeSelect_Click);
            // 
            // cboLabel
            // 
            this.cboLabel.DisplayMember = "Title";
            this.cboLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabel.FormattingEnabled = true;
            this.cboLabel.Location = new System.Drawing.Point(0, 22);
            this.cboLabel.Name = "cboLabel";
            this.cboLabel.Size = new System.Drawing.Size(147, 20);
            this.cboLabel.TabIndex = 0;
            this.cboLabel.ValueMember = "Id";
            this.cboLabel.DropDown += new System.EventHandler(this.cboLabel_DropDown);
            this.cboLabel.DropDownClosed += new System.EventHandler(this.cboLabel_DropDownClosed);
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlTitleBar.Controls.Add(this.lblClose);
            this.pnlTitleBar.Controls.Add(this.lblTransparent);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(147, 22);
            this.pnlTitleBar.TabIndex = 0;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseDown);
            this.pnlTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitleBar_MouseMove);
            // 
            // lblTransparent
            // 
            this.lblTransparent.AutoSize = true;
            this.lblTransparent.Location = new System.Drawing.Point(5, 5);
            this.lblTransparent.Name = "lblTransparent";
            this.lblTransparent.Size = new System.Drawing.Size(59, 12);
            this.lblTransparent.TabIndex = 0;
            this.lblTransparent.Text = "不透明度：";
            this.lblTransparent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTransparent_MouseDown);
            this.lblTransparent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTransparent_MouseMove);
            // 
            // timActive
            // 
            this.timActive.Tick += new System.EventHandler(this.timActive_Tick);
            // 
            // lblClose
            // 
            this.lblClose.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblClose.Location = new System.Drawing.Point(120, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(28, 22);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "×";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            this.lblClose.MouseEnter += new System.EventHandler(this.lblClose_MouseEnter);
            this.lblClose.MouseLeave += new System.EventHandler(this.lblClose_MouseLeave);
            // 
            // frmSimpleCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(149, 122);
            this.Controls.Add(this.pnlClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmSimpleCmd";
            this.Opacity = 0.2D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSimpleCmd";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmSimpleCmd_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSimpleCmd_FormClosing);
            this.Load += new System.EventHandler(this.frmSimpleCmd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSimpleCmd_KeyDown);
            this.pnlClient.ResumeLayout(false);
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlClient;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnFreeSelect;
        private System.Windows.Forms.ComboBox cboLabel;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Timer timActive;
        private System.Windows.Forms.Label lblTransparent;
        private System.Windows.Forms.Button btnClipboardTranslate;
        private System.Windows.Forms.Label lblClose;
    }
}