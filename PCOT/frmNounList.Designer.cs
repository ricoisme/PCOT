namespace PCOT
{
    partial class frmNounList
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
            this.dgvNounList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.BeforeNoun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfterNoun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNounList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNounList
            // 
            this.dgvNounList.AllowUserToAddRows = false;
            this.dgvNounList.AllowUserToDeleteRows = false;
            this.dgvNounList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNounList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNounList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IsEnabled,
            this.Delete,
            this.BeforeNoun,
            this.AfterNoun});
            this.dgvNounList.Location = new System.Drawing.Point(12, 12);
            this.dgvNounList.Name = "dgvNounList";
            this.dgvNounList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvNounList.RowTemplate.Height = 21;
            this.dgvNounList.Size = new System.Drawing.Size(601, 350);
            this.dgvNounList.TabIndex = 0;
            this.dgvNounList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNounList_CellContentClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(538, 368);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 41;
            // 
            // IsEnabled
            // 
            this.IsEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsEnabled.DataPropertyName = "IsEnabled";
            this.IsEnabled.HeaderText = "有効";
            this.IsEnabled.Name = "IsEnabled";
            this.IsEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsEnabled.Width = 54;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "削除";
            this.Delete.Name = "Delete";
            this.Delete.Width = 35;
            // 
            // BeforeNoun
            // 
            this.BeforeNoun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BeforeNoun.DataPropertyName = "BeforeNoun";
            this.BeforeNoun.HeaderText = "置換前名詞";
            this.BeforeNoun.MaxInputLength = 10000;
            this.BeforeNoun.Name = "BeforeNoun";
            // 
            // AfterNoun
            // 
            this.AfterNoun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AfterNoun.DataPropertyName = "AfterNoun";
            this.AfterNoun.HeaderText = "置換後名詞";
            this.AfterNoun.MaxInputLength = 10000;
            this.AfterNoun.Name = "AfterNoun";
            // 
            // frmNounList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 403);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvNounList);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "frmNounList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "名詞一覧";
            this.Load += new System.EventHandler(this.frmNounList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNounList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNounList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnabled;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeforeNoun;
        private System.Windows.Forms.DataGridViewTextBoxColumn AfterNoun;
    }
}