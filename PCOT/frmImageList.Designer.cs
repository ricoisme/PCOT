namespace PCOT
{
    partial class frmImageList
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlAutoPlay = new System.Windows.Forms.Panel();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblAutoPlaying = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnOpeImgFile = new System.Windows.Forms.Button();
            this.chkTargetReturn = new System.Windows.Forms.CheckBox();
            this.btnFreeSelect = new System.Windows.Forms.Button();
            this.chkIgnoreReturn = new System.Windows.Forms.CheckBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.lblImgCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.picImg = new System.Windows.Forms.PictureBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.trkImgListBar = new System.Windows.Forms.TrackBar();
            this.pnlTrackBar = new System.Windows.Forms.Panel();
            this.pnlFooter.SuspendLayout();
            this.pnlAutoPlay.SuspendLayout();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).BeginInit();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkImgListBar)).BeginInit();
            this.pnlTrackBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.pnlAutoPlay);
            this.pnlFooter.Controls.Add(this.pnlControl);
            this.pnlFooter.Controls.Add(this.lblImgCount);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 556);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(846, 37);
            this.pnlFooter.TabIndex = 1;
            // 
            // pnlAutoPlay
            // 
            this.pnlAutoPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlAutoPlay.Controls.Add(this.lblSpeed);
            this.pnlAutoPlay.Controls.Add(this.lblAutoPlaying);
            this.pnlAutoPlay.Location = new System.Drawing.Point(8, 16);
            this.pnlAutoPlay.Name = "pnlAutoPlay";
            this.pnlAutoPlay.Size = new System.Drawing.Size(149, 18);
            this.pnlAutoPlay.TabIndex = 10;
            this.pnlAutoPlay.Visible = false;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblSpeed.Location = new System.Drawing.Point(86, 4);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(54, 12);
            this.lblSpeed.TabIndex = 10;
            this.lblSpeed.Text = "XXXXms";
            // 
            // lblAutoPlaying
            // 
            this.lblAutoPlaying.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAutoPlaying.AutoSize = true;
            this.lblAutoPlaying.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAutoPlaying.ForeColor = System.Drawing.Color.Blue;
            this.lblAutoPlaying.Location = new System.Drawing.Point(3, 4);
            this.lblAutoPlaying.Name = "lblAutoPlaying";
            this.lblAutoPlaying.Size = new System.Drawing.Size(77, 12);
            this.lblAutoPlaying.TabIndex = 10;
            this.lblAutoPlaying.Text = "自動再生中：";
            // 
            // pnlControl
            // 
            this.pnlControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlControl.Controls.Add(this.btnOpeImgFile);
            this.pnlControl.Controls.Add(this.chkTargetReturn);
            this.pnlControl.Controls.Add(this.btnFreeSelect);
            this.pnlControl.Controls.Add(this.chkIgnoreReturn);
            this.pnlControl.Controls.Add(this.btnTranslate);
            this.pnlControl.Location = new System.Drawing.Point(305, 3);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(449, 29);
            this.pnlControl.TabIndex = 9;
            // 
            // btnOpeImgFile
            // 
            this.btnOpeImgFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpeImgFile.Location = new System.Drawing.Point(2, 3);
            this.btnOpeImgFile.Name = "btnOpeImgFile";
            this.btnOpeImgFile.Size = new System.Drawing.Size(88, 23);
            this.btnOpeImgFile.TabIndex = 0;
            this.btnOpeImgFile.TabStop = false;
            this.btnOpeImgFile.Text = "ファイル操作";
            this.btnOpeImgFile.UseVisualStyleBackColor = true;
            this.btnOpeImgFile.Click += new System.EventHandler(this.btnOpeImgFile_Click);
            // 
            // chkTargetReturn
            // 
            this.chkTargetReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTargetReturn.AutoSize = true;
            this.chkTargetReturn.Location = new System.Drawing.Point(96, 7);
            this.chkTargetReturn.Name = "chkTargetReturn";
            this.chkTargetReturn.Size = new System.Drawing.Size(101, 16);
            this.chkTargetReturn.TabIndex = 7;
            this.chkTargetReturn.TabStop = false;
            this.chkTargetReturn.Text = "対象通りに改行";
            this.chkTargetReturn.UseVisualStyleBackColor = true;
            this.chkTargetReturn.CheckedChanged += new System.EventHandler(this.chkTargetReturn_CheckedChanged);
            // 
            // btnFreeSelect
            // 
            this.btnFreeSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFreeSelect.Location = new System.Drawing.Point(290, 3);
            this.btnFreeSelect.Name = "btnFreeSelect";
            this.btnFreeSelect.Size = new System.Drawing.Size(75, 23);
            this.btnFreeSelect.TabIndex = 0;
            this.btnFreeSelect.TabStop = false;
            this.btnFreeSelect.Text = "フリー選択";
            this.btnFreeSelect.UseVisualStyleBackColor = true;
            this.btnFreeSelect.Click += new System.EventHandler(this.btnFreeSelect_Click);
            // 
            // chkIgnoreReturn
            // 
            this.chkIgnoreReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIgnoreReturn.AutoSize = true;
            this.chkIgnoreReturn.Location = new System.Drawing.Point(203, 7);
            this.chkIgnoreReturn.Name = "chkIgnoreReturn";
            this.chkIgnoreReturn.Size = new System.Drawing.Size(81, 16);
            this.chkIgnoreReturn.TabIndex = 8;
            this.chkIgnoreReturn.TabStop = false;
            this.chkIgnoreReturn.Text = "改行を無視";
            this.chkIgnoreReturn.UseVisualStyleBackColor = true;
            this.chkIgnoreReturn.CheckedChanged += new System.EventHandler(this.chkIgnoreReturn_CheckedChanged);
            // 
            // btnTranslate
            // 
            this.btnTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTranslate.Location = new System.Drawing.Point(371, 3);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(75, 23);
            this.btnTranslate.TabIndex = 0;
            this.btnTranslate.TabStop = false;
            this.btnTranslate.Text = "翻訳";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // lblImgCount
            // 
            this.lblImgCount.AutoSize = true;
            this.lblImgCount.Location = new System.Drawing.Point(12, 2);
            this.lblImgCount.Name = "lblImgCount";
            this.lblImgCount.Size = new System.Drawing.Size(31, 12);
            this.lblImgCount.TabIndex = 1;
            this.lblImgCount.Text = "0 / 0";
            this.lblImgCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(757, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picImg
            // 
            this.picImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picImg.BackColor = System.Drawing.Color.SlateGray;
            this.picImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImg.Location = new System.Drawing.Point(0, 0);
            this.picImg.Name = "picImg";
            this.picImg.Size = new System.Drawing.Size(846, 521);
            this.picImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImg.TabIndex = 0;
            this.picImg.TabStop = false;
            this.picImg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picImg_MouseDoubleClick);
            this.picImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImg_MouseDown);
            this.picImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picImg_MouseMove);
            this.picImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picImg_MouseUp);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.picImg);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(846, 593);
            this.pnlBody.TabIndex = 2;
            // 
            // trkImgListBar
            // 
            this.trkImgListBar.AutoSize = false;
            this.trkImgListBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkImgListBar.LargeChange = 1;
            this.trkImgListBar.Location = new System.Drawing.Point(0, 0);
            this.trkImgListBar.Minimum = 1;
            this.trkImgListBar.Name = "trkImgListBar";
            this.trkImgListBar.Size = new System.Drawing.Size(846, 29);
            this.trkImgListBar.TabIndex = 1;
            this.trkImgListBar.TabStop = false;
            this.trkImgListBar.Value = 1;
            this.trkImgListBar.Scroll += new System.EventHandler(this.trkImgListBar_Scroll);
            // 
            // pnlTrackBar
            // 
            this.pnlTrackBar.Controls.Add(this.trkImgListBar);
            this.pnlTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTrackBar.Location = new System.Drawing.Point(0, 527);
            this.pnlTrackBar.Name = "pnlTrackBar";
            this.pnlTrackBar.Size = new System.Drawing.Size(846, 29);
            this.pnlTrackBar.TabIndex = 3;
            // 
            // frmImageList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(846, 593);
            this.Controls.Add(this.pnlTrackBar);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlBody);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "frmImageList";
            this.Text = "画像翻訳";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImageList_FormClosing);
            this.Load += new System.EventHandler(this.frmImageList_Load);
            this.Shown += new System.EventHandler(this.frmImageList_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImageList_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmImageList_KeyUp);
            this.Resize += new System.EventHandler(this.frmImageList_Resize);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlAutoPlay.ResumeLayout(false);
            this.pnlAutoPlay.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).EndInit();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trkImgListBar)).EndInit();
            this.pnlTrackBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFreeSelect;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblImgCount;
        private System.Windows.Forms.PictureBox picImg;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.CheckBox chkTargetReturn;
        private System.Windows.Forms.CheckBox chkIgnoreReturn;
        private System.Windows.Forms.TrackBar trkImgListBar;
        private System.Windows.Forms.Button btnOpeImgFile;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlAutoPlay;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblAutoPlaying;
        private System.Windows.Forms.Panel pnlTrackBar;
    }
}