namespace PCOT
{
    partial class frmStart
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDummy = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dgvProcessList = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.prcsTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prcsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prcsAdd = new System.Windows.Forms.DataGridViewButtonColumn();
            this.prcsDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.hasSettingData = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.noneActivePrcs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isAliasPrcs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDummy
            // 
            this.lblDummy.AutoSize = true;
            this.lblDummy.Location = new System.Drawing.Point(624, 9);
            this.lblDummy.Name = "lblDummy";
            this.lblDummy.Size = new System.Drawing.Size(0, 12);
            this.lblDummy.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(549, 244);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "選択";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dgvProcessList
            // 
            this.dgvProcessList.AllowUserToAddRows = false;
            this.dgvProcessList.AllowUserToDeleteRows = false;
            this.dgvProcessList.AllowUserToResizeRows = false;
            this.dgvProcessList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prcsTitle,
            this.prcsName,
            this.prcsAdd,
            this.prcsDel,
            this.hasSettingData,
            this.noneActivePrcs,
            this.isAliasPrcs});
            this.dgvProcessList.Location = new System.Drawing.Point(12, 9);
            this.dgvProcessList.MultiSelect = false;
            this.dgvProcessList.Name = "dgvProcessList";
            this.dgvProcessList.RowHeadersVisible = false;
            this.dgvProcessList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvProcessList.RowTemplate.Height = 21;
            this.dgvProcessList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProcessList.Size = new System.Drawing.Size(612, 229);
            this.dgvProcessList.TabIndex = 0;
            this.dgvProcessList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcessList_CellContentClick);
            this.dgvProcessList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvProcessList_CellPainting);
            this.dgvProcessList.SelectionChanged += new System.EventHandler(this.dgvProcessList_SelectionChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(12, 244);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "リスト更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetting.Location = new System.Drawing.Point(93, 244);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(84, 23);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "システム設定";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // chkShowAll
            // 
            this.chkShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new System.Drawing.Point(183, 248);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(178, 16);
            this.chkShowAll.TabIndex = 3;
            this.chkShowAll.Text = "設定のある全てのプロセスを表示";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // prcsTitle
            // 
            this.prcsTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prcsTitle.DataPropertyName = "prcsTitle";
            this.prcsTitle.HeaderText = "タイトル";
            this.prcsTitle.Name = "prcsTitle";
            this.prcsTitle.ReadOnly = true;
            this.prcsTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // prcsName
            // 
            this.prcsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prcsName.DataPropertyName = "prcsName";
            this.prcsName.HeaderText = "プロセス名";
            this.prcsName.Name = "prcsName";
            this.prcsName.ReadOnly = true;
            this.prcsName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // prcsAdd
            // 
            this.prcsAdd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.prcsAdd.DataPropertyName = "prcsAdd";
            this.prcsAdd.HeaderText = "追加";
            this.prcsAdd.Name = "prcsAdd";
            this.prcsAdd.Width = 35;
            // 
            // prcsDel
            // 
            this.prcsDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.prcsDel.DataPropertyName = "prcsDel";
            this.prcsDel.HeaderText = "削除";
            this.prcsDel.Name = "prcsDel";
            this.prcsDel.Width = 35;
            // 
            // hasSettingData
            // 
            this.hasSettingData.DataPropertyName = "hasSettingData";
            this.hasSettingData.HeaderText = "データ保持";
            this.hasSettingData.Name = "hasSettingData";
            this.hasSettingData.Visible = false;
            // 
            // noneActivePrcs
            // 
            this.noneActivePrcs.DataPropertyName = "noneActivePrcs";
            this.noneActivePrcs.HeaderText = "非アクティブプロセス";
            this.noneActivePrcs.Name = "noneActivePrcs";
            this.noneActivePrcs.Visible = false;
            // 
            // isAliasPrcs
            // 
            this.isAliasPrcs.DataPropertyName = "isAliasPrcs";
            this.isAliasPrcs.HeaderText = "別名プロセス";
            this.isAliasPrcs.Name = "isAliasPrcs";
            this.isAliasPrcs.Visible = false;
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 279);
            this.Controls.Add(this.chkShowAll);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dgvProcessList);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblDummy);
            this.Name = "frmStart";
            this.Text = "プログラム一覧";
            this.Load += new System.EventHandler(this.frmStart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDummy;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridView dgvProcessList;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.CheckBox chkShowAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn prcsTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn prcsName;
        private System.Windows.Forms.DataGridViewButtonColumn prcsAdd;
        private System.Windows.Forms.DataGridViewButtonColumn prcsDel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasSettingData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn noneActivePrcs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAliasPrcs;
    }
}

