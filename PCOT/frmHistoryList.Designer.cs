namespace PCOT
{
    partial class frmHistoryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvHistoryList = new PCOT.DataGridViewEx();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(677, 440);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvHistoryList
            // 
            this.dgvHistoryList.AllowUserToAddRows = false;
            this.dgvHistoryList.AllowUserToDeleteRows = false;
            this.dgvHistoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistoryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoryList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Delete,
            this.Label,
            this.OriginalText,
            this.ResultText});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistoryList.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvHistoryList.Location = new System.Drawing.Point(12, 12);
            this.dgvHistoryList.Name = "dgvHistoryList";
            this.dgvHistoryList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHistoryList.RowTemplate.Height = 21;
            this.dgvHistoryList.ShowCellToolTips = false;
            this.dgvHistoryList.Size = new System.Drawing.Size(740, 422);
            this.dgvHistoryList.TabIndex = 0;
            this.dgvHistoryList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistoryList_CellContentClick);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 41;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "削除";
            this.Delete.Name = "Delete";
            this.Delete.Width = 35;
            // 
            // Label
            // 
            this.Label.DataPropertyName = "Label";
            this.Label.FillWeight = 21.827F;
            this.Label.HeaderText = "ラベル";
            this.Label.MinimumWidth = 111;
            this.Label.Name = "Label";
            this.Label.Width = 111;
            // 
            // OriginalText
            // 
            this.OriginalText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OriginalText.DataPropertyName = "OriginalText";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.OriginalText.DefaultCellStyle = dataGridViewCellStyle16;
            this.OriginalText.FillWeight = 0.9831982F;
            this.OriginalText.HeaderText = "原文";
            this.OriginalText.Name = "OriginalText";
            // 
            // ResultText
            // 
            this.ResultText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ResultText.DataPropertyName = "ResultText";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.ResultText.DefaultCellStyle = dataGridViewCellStyle17;
            this.ResultText.FillWeight = 0.9831982F;
            this.ResultText.HeaderText = "訳文";
            this.ResultText.Name = "ResultText";
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(12, 440);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(90, 23);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "フィルター";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // frmHistoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(764, 475);
            this.ControlBox = false;
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dgvHistoryList);
            this.Controls.Add(this.btnClose);
            this.Name = "frmHistoryList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "履歴一覧";
            this.Load += new System.EventHandler(this.frmHistoryList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private DataGridViewEx dgvHistoryList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultText;
        private System.Windows.Forms.Button btnFilter;
    }
}