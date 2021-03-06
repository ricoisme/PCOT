namespace PCOT
{
    partial class frmSelectFilter
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
            this.dgvLabelList = new System.Windows.Forms.DataGridView();
            this.LabelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCheckOffAll = new System.Windows.Forms.Button();
            this.lblAllCheck = new System.Windows.Forms.Label();
            this.btnCheckOnAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabelList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabelList
            // 
            this.dgvLabelList.AllowUserToAddRows = false;
            this.dgvLabelList.AllowUserToDeleteRows = false;
            this.dgvLabelList.AllowUserToResizeRows = false;
            this.dgvLabelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LabelName,
            this.Checked});
            this.dgvLabelList.Location = new System.Drawing.Point(12, 12);
            this.dgvLabelList.MultiSelect = false;
            this.dgvLabelList.Name = "dgvLabelList";
            this.dgvLabelList.RowHeadersVisible = false;
            this.dgvLabelList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvLabelList.RowTemplate.Height = 21;
            this.dgvLabelList.Size = new System.Drawing.Size(278, 306);
            this.dgvLabelList.TabIndex = 0;
            // 
            // LabelName
            // 
            this.LabelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LabelName.DataPropertyName = "LabelName";
            this.LabelName.HeaderText = "ラベル名";
            this.LabelName.Name = "LabelName";
            this.LabelName.ReadOnly = true;
            // 
            // Checked
            // 
            this.Checked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Checked.DataPropertyName = "Checked";
            this.Checked.HeaderText = "表示";
            this.Checked.Name = "Checked";
            this.Checked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Checked.Width = 54;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(215, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCheckOffAll
            // 
            this.btnCheckOffAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckOffAll.Location = new System.Drawing.Point(130, 324);
            this.btnCheckOffAll.Name = "btnCheckOffAll";
            this.btnCheckOffAll.Size = new System.Drawing.Size(43, 23);
            this.btnCheckOffAll.TabIndex = 2;
            this.btnCheckOffAll.Text = "OFF";
            this.btnCheckOffAll.UseVisualStyleBackColor = true;
            this.btnCheckOffAll.Click += new System.EventHandler(this.btnCheckOffAll_Click);
            // 
            // lblAllCheck
            // 
            this.lblAllCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAllCheck.AutoSize = true;
            this.lblAllCheck.Location = new System.Drawing.Point(12, 329);
            this.lblAllCheck.Name = "lblAllCheck";
            this.lblAllCheck.Size = new System.Drawing.Size(73, 12);
            this.lblAllCheck.TabIndex = 3;
            this.lblAllCheck.Text = "全てのチェック：";
            // 
            // btnCheckOnAll
            // 
            this.btnCheckOnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckOnAll.Location = new System.Drawing.Point(85, 324);
            this.btnCheckOnAll.Name = "btnCheckOnAll";
            this.btnCheckOnAll.Size = new System.Drawing.Size(43, 23);
            this.btnCheckOnAll.TabIndex = 1;
            this.btnCheckOnAll.Text = "ON";
            this.btnCheckOnAll.UseVisualStyleBackColor = true;
            this.btnCheckOnAll.Click += new System.EventHandler(this.btnCheckOnAll_Click);
            // 
            // frmSelectFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(302, 359);
            this.ControlBox = false;
            this.Controls.Add(this.btnCheckOnAll);
            this.Controls.Add(this.lblAllCheck);
            this.Controls.Add(this.btnCheckOffAll);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvLabelList);
            this.Name = "frmSelectFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "有効/無効一括切替";
            this.Load += new System.EventHandler(this.frmSelectFilter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabelList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLabelList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.Button btnCheckOffAll;
        private System.Windows.Forms.Label lblAllCheck;
        private System.Windows.Forms.Button btnCheckOnAll;
    }
}