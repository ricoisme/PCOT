namespace PCOT
{
    partial class frmEnabled
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
            this.dgvProcessList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsIndeterminate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabelList
            // 
            this.dgvProcessList.AllowUserToAddRows = false;
            this.dgvProcessList.AllowUserToDeleteRows = false;
            this.dgvProcessList.AllowUserToResizeRows = false;
            this.dgvProcessList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProcessName,
            this.CheckState,
            this.IsIndeterminate});
            this.dgvProcessList.Location = new System.Drawing.Point(12, 12);
            this.dgvProcessList.MultiSelect = false;
            this.dgvProcessList.Name = "dgvLabelList";
            this.dgvProcessList.RowHeadersVisible = false;
            this.dgvProcessList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProcessList.RowTemplate.Height = 21;
            this.dgvProcessList.Size = new System.Drawing.Size(278, 306);
            this.dgvProcessList.TabIndex = 0;
            this.dgvProcessList.Sorted += new System.EventHandler(this.dgvProcessList_Sorted);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(215, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ProcessName
            // 
            this.ProcessName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessName.DataPropertyName = "ProcessName";
            this.ProcessName.HeaderText = "登録プロセス";
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            // 
            // CheckState
            // 
            this.CheckState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CheckState.DataPropertyName = "CheckState";
            this.CheckState.HeaderText = "有効";
            this.CheckState.Name = "CheckState";
            this.CheckState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CheckState.Width = 54;
            // 
            // IsIndeterminate
            // 
            this.IsIndeterminate.DataPropertyName = "IsIndeterminate";
            this.IsIndeterminate.HeaderText = "不確定チェック状態";
            this.IsIndeterminate.Name = "IsIndeterminate";
            this.IsIndeterminate.Visible = false;
            // 
            // frmEnabled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(302, 359);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvProcessList);
            this.Name = "frmEnabled";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "有効/無効一括切替";
            this.Load += new System.EventHandler(this.frmEnabled_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProcessList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckState;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsIndeterminate;
    }
}