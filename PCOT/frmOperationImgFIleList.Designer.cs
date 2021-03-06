namespace PCOT
{
    partial class frmOperationImgFIleList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperationImgFIleList));
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvImageList = new System.Windows.Forms.DataGridView();
            this.Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ShowImg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ImgFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImgFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDeleteSelectOn = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDeleteSelectOff = new System.Windows.Forms.ToolStripMenuItem();
            this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiShowSelectOn = new System.Windows.Forms.ToolStripMenuItem();
            this.smiShowSelectOff = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChkDelete = new System.Windows.Forms.Button();
            this.spcImgInfo = new System.Windows.Forms.SplitContainer();
            this.lblImgFileName = new System.Windows.Forms.Label();
            this.timUpdateImg = new System.Windows.Forms.Timer(this.components);
            this.chkDeleteAll = new System.Windows.Forms.CheckBox();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.picImage = new PCOT.ImageViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImageList)).BeginInit();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcImgInfo)).BeginInit();
            this.spcImgInfo.Panel1.SuspendLayout();
            this.spcImgInfo.Panel2.SuspendLayout();
            this.spcImgInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(727, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvImageList
            // 
            this.dgvImageList.AllowUserToAddRows = false;
            this.dgvImageList.AllowUserToDeleteRows = false;
            this.dgvImageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.ShowImg,
            this.ImgFileName,
            this.ImgFilePath});
            this.dgvImageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImageList.Location = new System.Drawing.Point(0, 0);
            this.dgvImageList.Name = "dgvImageList";
            this.dgvImageList.RowTemplate.Height = 21;
            this.dgvImageList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImageList.Size = new System.Drawing.Size(370, 389);
            this.dgvImageList.TabIndex = 0;
            this.dgvImageList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvImageList_CurrentCellDirtyStateChanged);
            this.dgvImageList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvImageList_RowsAdded);
            this.dgvImageList.SelectionChanged += new System.EventHandler(this.dgvImageList_SelectionChanged);
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "削除";
            this.Delete.Name = "Delete";
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Width = 54;
            // 
            // ShowImg
            // 
            this.ShowImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ShowImg.DataPropertyName = "ShowImg";
            this.ShowImg.HeaderText = "表示";
            this.ShowImg.Name = "ShowImg";
            this.ShowImg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ShowImg.Width = 54;
            // 
            // ImgFileName
            // 
            this.ImgFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ImgFileName.DataPropertyName = "ImgFileName";
            this.ImgFileName.HeaderText = "画像ファイル名";
            this.ImgFileName.Name = "ImgFileName";
            this.ImgFileName.ReadOnly = true;
            // 
            // ImgFilePath
            // 
            this.ImgFilePath.DataPropertyName = "ImgFilePath";
            this.ImgFilePath.HeaderText = "画像ファイルパス";
            this.ImgFilePath.Name = "ImgFilePath";
            this.ImgFilePath.Visible = false;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.削除ToolStripMenuItem,
            this.表示ToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(99, 48);
            // 
            // 削除ToolStripMenuItem
            // 
            this.削除ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiDeleteSelectOn,
            this.smiDeleteSelectOff});
            this.削除ToolStripMenuItem.Name = "削除ToolStripMenuItem";
            this.削除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.削除ToolStripMenuItem.Text = "削除";
            // 
            // smiDeleteSelectOn
            // 
            this.smiDeleteSelectOn.Name = "smiDeleteSelectOn";
            this.smiDeleteSelectOn.Size = new System.Drawing.Size(204, 22);
            this.smiDeleteSelectOn.Text = "選択行のチェックを全てON";
            this.smiDeleteSelectOn.Click += new System.EventHandler(this.smiDeleteSelectOn_Click);
            // 
            // smiDeleteSelectOff
            // 
            this.smiDeleteSelectOff.Name = "smiDeleteSelectOff";
            this.smiDeleteSelectOff.Size = new System.Drawing.Size(204, 22);
            this.smiDeleteSelectOff.Text = "選択行のチェックを全てOFF";
            this.smiDeleteSelectOff.Click += new System.EventHandler(this.smiDeleteSelectOff_Click);
            // 
            // 表示ToolStripMenuItem
            // 
            this.表示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiShowSelectOn,
            this.smiShowSelectOff});
            this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
            this.表示ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.表示ToolStripMenuItem.Text = "表示";
            // 
            // smiShowSelectOn
            // 
            this.smiShowSelectOn.Name = "smiShowSelectOn";
            this.smiShowSelectOn.Size = new System.Drawing.Size(204, 22);
            this.smiShowSelectOn.Text = "選択行のチェックを全てON";
            this.smiShowSelectOn.Click += new System.EventHandler(this.smiShowSelectOn_Click);
            // 
            // smiShowSelectOff
            // 
            this.smiShowSelectOff.Name = "smiShowSelectOff";
            this.smiShowSelectOff.Size = new System.Drawing.Size(204, 22);
            this.smiShowSelectOff.Text = "選択行のチェックを全てOFF";
            this.smiShowSelectOff.Click += new System.EventHandler(this.smiShowSelectOff_Click);
            // 
            // btnChkDelete
            // 
            this.btnChkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChkDelete.Location = new System.Drawing.Point(182, 419);
            this.btnChkDelete.Name = "btnChkDelete";
            this.btnChkDelete.Size = new System.Drawing.Size(229, 23);
            this.btnChkDelete.TabIndex = 4;
            this.btnChkDelete.Text = "削除チェックされた画像ファイルを全て削除";
            this.btnChkDelete.UseVisualStyleBackColor = true;
            this.btnChkDelete.Click += new System.EventHandler(this.btnChkDelete_Click);
            // 
            // spcImgInfo
            // 
            this.spcImgInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcImgInfo.Location = new System.Drawing.Point(12, 24);
            this.spcImgInfo.Name = "spcImgInfo";
            // 
            // spcImgInfo.Panel1
            // 
            this.spcImgInfo.Panel1.Controls.Add(this.dgvImageList);
            // 
            // spcImgInfo.Panel2
            // 
            this.spcImgInfo.Panel2.Controls.Add(this.picImage);
            this.spcImgInfo.Size = new System.Drawing.Size(790, 389);
            this.spcImgInfo.SplitterDistance = 370;
            this.spcImgInfo.TabIndex = 6;
            // 
            // lblImgFileName
            // 
            this.lblImgFileName.AutoSize = true;
            this.lblImgFileName.Location = new System.Drawing.Point(10, 9);
            this.lblImgFileName.Name = "lblImgFileName";
            this.lblImgFileName.Size = new System.Drawing.Size(75, 12);
            this.lblImgFileName.TabIndex = 7;
            this.lblImgFileName.Text = "画像ファイル名";
            this.lblImgFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timUpdateImg
            // 
            this.timUpdateImg.Tick += new System.EventHandler(this.timUpdateImg_Tick);
            // 
            // chkDeleteAll
            // 
            this.chkDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDeleteAll.AutoSize = true;
            this.chkDeleteAll.Location = new System.Drawing.Point(12, 423);
            this.chkDeleteAll.Name = "chkDeleteAll";
            this.chkDeleteAll.Size = new System.Drawing.Size(79, 16);
            this.chkDeleteAll.TabIndex = 8;
            this.chkDeleteAll.Text = "削除チェック";
            this.chkDeleteAll.UseVisualStyleBackColor = true;
            this.chkDeleteAll.CheckedChanged += new System.EventHandler(this.chkDeleteAll_CheckedChanged);
            // 
            // chkShowAll
            // 
            this.chkShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new System.Drawing.Point(97, 423);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(79, 16);
            this.chkShowAll.TabIndex = 8;
            this.chkShowAll.Text = "表示チェック";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Image = ((System.Drawing.Image)(resources.GetObject("picImage.Image")));
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(416, 389);
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // frmOperationImgFIleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(814, 454);
            this.ControlBox = false;
            this.Controls.Add(this.chkShowAll);
            this.Controls.Add(this.chkDeleteAll);
            this.Controls.Add(this.lblImgFileName);
            this.Controls.Add(this.spcImgInfo);
            this.Controls.Add(this.btnChkDelete);
            this.Controls.Add(this.btnClose);
            this.Name = "frmOperationImgFIleList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "画像ファイル操作";
            this.Load += new System.EventHandler(this.frmOperationImgFIleList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImageList)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.spcImgInfo.Panel1.ResumeLayout(false);
            this.spcImgInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcImgInfo)).EndInit();
            this.spcImgInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvImageList;
        private System.Windows.Forms.Button btnChkDelete;
        private System.Windows.Forms.SplitContainer spcImgInfo;
        private System.Windows.Forms.Label lblImgFileName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ShowImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImgFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImgFilePath;
        private System.Windows.Forms.Timer timUpdateImg;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiDeleteSelectOn;
        private System.Windows.Forms.ToolStripMenuItem smiDeleteSelectOff;
        private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiShowSelectOn;
        private System.Windows.Forms.ToolStripMenuItem smiShowSelectOff;
        private System.Windows.Forms.CheckBox chkDeleteAll;
        private System.Windows.Forms.CheckBox chkShowAll;
        private ImageViewer picImage;
    }
}