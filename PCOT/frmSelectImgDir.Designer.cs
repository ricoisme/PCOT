namespace PCOT
{
    partial class frmSelectImgDir
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
            this.dgvImgDirList = new System.Windows.Forms.DataGridView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ImageDirName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImgDirList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvImgDirList
            // 
            this.dgvImgDirList.AllowUserToAddRows = false;
            this.dgvImgDirList.AllowUserToDeleteRows = false;
            this.dgvImgDirList.AllowUserToResizeColumns = false;
            this.dgvImgDirList.AllowUserToResizeRows = false;
            this.dgvImgDirList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImgDirList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImgDirList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageDirName});
            this.dgvImgDirList.Location = new System.Drawing.Point(12, 12);
            this.dgvImgDirList.MultiSelect = false;
            this.dgvImgDirList.Name = "dgvImgDirList";
            this.dgvImgDirList.ReadOnly = true;
            this.dgvImgDirList.RowHeadersVisible = false;
            this.dgvImgDirList.RowTemplate.Height = 21;
            this.dgvImgDirList.Size = new System.Drawing.Size(390, 228);
            this.dgvImgDirList.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(246, 251);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "選択";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(327, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ImageDirName
            // 
            this.ImageDirName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ImageDirName.DataPropertyName = "ImageDirName";
            this.ImageDirName.HeaderText = "画像フォルダ";
            this.ImageDirName.Name = "ImageDirName";
            this.ImageDirName.ReadOnly = true;
            // 
            // frmSelectImgDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(414, 286);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dgvImgDirList);
            this.Name = "frmSelectImgDir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "画像フォルダ選択";
            this.Load += new System.EventHandler(this.frmSelectImgDir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImgDirList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvImgDirList;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageDirName;
    }
}