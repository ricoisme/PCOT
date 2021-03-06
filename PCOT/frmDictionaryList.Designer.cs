namespace PCOT
{
    partial class frmDictionaryList
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
            this.dgvDicList = new System.Windows.Forms.DataGridView();
            this.cmsProcessNames = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDicTypeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuReplaceOn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIgnoreOn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.btnEnabled = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RegistProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DicTypeCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DicTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordUnit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsUpperAndLower = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BeforeText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfterText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).BeginInit();
            this.cmsDicTypeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDicList
            // 
            this.dgvDicList.AllowUserToAddRows = false;
            this.dgvDicList.AllowUserToDeleteRows = false;
            this.dgvDicList.AllowUserToResizeRows = false;
            this.dgvDicList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDicList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDicList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IsEnabled,
            this.Delete,
            this.RegistProcess,
            this.DicTypeCd,
            this.DicTypeName,
            this.WordUnit,
            this.IsUpperAndLower,
            this.BeforeText,
            this.AfterText});
            this.dgvDicList.Location = new System.Drawing.Point(12, 12);
            this.dgvDicList.Name = "dgvDicList";
            this.dgvDicList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDicList.RowTemplate.Height = 21;
            this.dgvDicList.Size = new System.Drawing.Size(784, 350);
            this.dgvDicList.TabIndex = 0;
            this.dgvDicList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDicList_CellContentClick);
            this.dgvDicList.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvDicList_CellContextMenuStripNeeded);
            this.dgvDicList.Sorted += new System.EventHandler(this.dgvDicList_Sorted);
            // 
            // cmsProcessNames
            // 
            this.cmsProcessNames.Name = "cmsProcessNames";
            this.cmsProcessNames.Size = new System.Drawing.Size(61, 4);
            // 
            // cmsDicTypeMenu
            // 
            this.cmsDicTypeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReplaceOn,
            this.mnuIgnoreOn});
            this.cmsDicTypeMenu.Name = "cmsDicTypeMenu";
            this.cmsDicTypeMenu.Size = new System.Drawing.Size(162, 48);
            // 
            // mnuReplaceOn
            // 
            this.mnuReplaceOn.Name = "mnuReplaceOn";
            this.mnuReplaceOn.Size = new System.Drawing.Size(161, 22);
            this.mnuReplaceOn.Text = "処理タイプ：置換";
            this.mnuReplaceOn.Click += new System.EventHandler(this.mnuReplaceOn_Click);
            // 
            // mnuIgnoreOn
            // 
            this.mnuIgnoreOn.Name = "mnuIgnoreOn";
            this.mnuIgnoreOn.Size = new System.Drawing.Size(161, 22);
            this.mnuIgnoreOn.Text = "処理タイプ：無視";
            this.mnuIgnoreOn.Click += new System.EventHandler(this.mnuIgnoreOn_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(721, 368);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboFilter
            // 
            this.cboFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboFilter.DisplayMember = "Text";
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(123, 370);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(121, 20);
            this.cboFilter.TabIndex = 2;
            this.cboFilter.ValueMember = "Value";
            // 
            // btnEnabled
            // 
            this.btnEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnabled.Location = new System.Drawing.Point(12, 368);
            this.btnEnabled.Name = "btnEnabled";
            this.btnEnabled.Size = new System.Drawing.Size(105, 23);
            this.btnEnabled.TabIndex = 1;
            this.btnEnabled.Text = "有効/無効切替";
            this.btnEnabled.UseVisualStyleBackColor = true;
            this.btnEnabled.Click += new System.EventHandler(this.btnEnabled_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.Location = new System.Drawing.Point(250, 368);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(90, 23);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "フィルター";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
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
            // RegistProcess
            // 
            this.RegistProcess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RegistProcess.ContextMenuStrip = this.cmsProcessNames;
            this.RegistProcess.DataPropertyName = "RegistProcess";
            this.RegistProcess.HeaderText = "登録プロセス";
            this.RegistProcess.Name = "RegistProcess";
            this.RegistProcess.ReadOnly = true;
            this.RegistProcess.Width = 91;
            // 
            // DicTypeCd
            // 
            this.DicTypeCd.DataPropertyName = "DicTypeCd";
            this.DicTypeCd.HeaderText = "辞書タイプコード";
            this.DicTypeCd.Name = "DicTypeCd";
            this.DicTypeCd.Visible = false;
            // 
            // DicTypeName
            // 
            this.DicTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DicTypeName.ContextMenuStrip = this.cmsDicTypeMenu;
            this.DicTypeName.DataPropertyName = "DicTypeName";
            this.DicTypeName.HeaderText = "処理タイプ";
            this.DicTypeName.Name = "DicTypeName";
            this.DicTypeName.ReadOnly = true;
            this.DicTypeName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DicTypeName.Width = 80;
            // 
            // WordUnit
            // 
            this.WordUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.WordUnit.DataPropertyName = "IsWordUnit";
            this.WordUnit.HeaderText = "単語単位";
            this.WordUnit.Name = "WordUnit";
            this.WordUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.WordUnit.Width = 78;
            // 
            // IsUpperAndLower
            // 
            this.IsUpperAndLower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsUpperAndLower.DataPropertyName = "IsUpperAndLower";
            this.IsUpperAndLower.HeaderText = "大文字小文字";
            this.IsUpperAndLower.Name = "IsUpperAndLower";
            this.IsUpperAndLower.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsUpperAndLower.Width = 72;
            // 
            // BeforeText
            // 
            this.BeforeText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BeforeText.DataPropertyName = "BeforeText";
            this.BeforeText.HeaderText = "対象テキスト";
            this.BeforeText.MaxInputLength = 10000;
            this.BeforeText.Name = "BeforeText";
            // 
            // AfterText
            // 
            this.AfterText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AfterText.DataPropertyName = "AfterText";
            this.AfterText.HeaderText = "置換後テキスト";
            this.AfterText.MaxInputLength = 10000;
            this.AfterText.Name = "AfterText";
            // 
            // frmDictionaryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 403);
            this.ControlBox = false;
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnEnabled);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvDicList);
            this.Name = "frmDictionaryList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "辞書一覧";
            this.Load += new System.EventHandler(this.frmDictionary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDicList)).EndInit();
            this.cmsDicTypeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDicList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsDicTypeMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuReplaceOn;
        private System.Windows.Forms.ToolStripMenuItem mnuIgnoreOn;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Button btnEnabled;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ContextMenuStrip cmsProcessNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnabled;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegistProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn DicTypeCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DicTypeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn WordUnit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsUpperAndLower;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeforeText;
        private System.Windows.Forms.DataGridViewTextBoxColumn AfterText;
    }
}