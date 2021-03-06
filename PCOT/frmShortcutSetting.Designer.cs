namespace PCOT
{
    partial class frmShortcutSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvShortcutList = new System.Windows.Forms.DataGridView();
            this.IsEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ShortcutDispName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnabledCtrl = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EnabledShift = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EnabledAlt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DispNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortcutName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblSelectAll = new System.Windows.Forms.Label();
            this.chkCtrlAll = new System.Windows.Forms.CheckBox();
            this.chkShiftAll = new System.Windows.Forms.CheckBox();
            this.chkAltAll = new System.Windows.Forms.CheckBox();
            this.cmsTriggerKey = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chkIsEnabledAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortcutList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShortcutList
            // 
            this.dgvShortcutList.AllowUserToAddRows = false;
            this.dgvShortcutList.AllowUserToDeleteRows = false;
            this.dgvShortcutList.AllowUserToResizeColumns = false;
            this.dgvShortcutList.AllowUserToResizeRows = false;
            this.dgvShortcutList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShortcutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShortcutList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsEnabled,
            this.ShortcutDispName,
            this.EnabledCtrl,
            this.EnabledShift,
            this.EnabledAlt,
            this.DispNumber,
            this.ShortcutName,
            this.Number});
            this.dgvShortcutList.Location = new System.Drawing.Point(12, 27);
            this.dgvShortcutList.MultiSelect = false;
            this.dgvShortcutList.Name = "dgvShortcutList";
            this.dgvShortcutList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvShortcutList.RowTemplate.Height = 21;
            this.dgvShortcutList.Size = new System.Drawing.Size(363, 337);
            this.dgvShortcutList.TabIndex = 4;
            this.dgvShortcutList.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvShortcutList_CellContextMenuStripNeeded);
            this.dgvShortcutList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShortcutList_CellValueChanged);
            this.dgvShortcutList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvShortcutList_CurrentCellDirtyStateChanged);
            // 
            // IsEnabled
            // 
            this.IsEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsEnabled.DataPropertyName = "IsEnabled";
            this.IsEnabled.HeaderText = "有効";
            this.IsEnabled.Name = "IsEnabled";
            this.IsEnabled.Width = 32;
            // 
            // ShortcutDispName
            // 
            this.ShortcutDispName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ShortcutDispName.DataPropertyName = "ShortcutDispName";
            this.ShortcutDispName.HeaderText = "ショートカット名";
            this.ShortcutDispName.Name = "ShortcutDispName";
            this.ShortcutDispName.ReadOnly = true;
            this.ShortcutDispName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EnabledCtrl
            // 
            this.EnabledCtrl.DataPropertyName = "EnabledCtrl";
            this.EnabledCtrl.HeaderText = "CTRL";
            this.EnabledCtrl.IndeterminateValue = "";
            this.EnabledCtrl.Name = "EnabledCtrl";
            this.EnabledCtrl.Width = 43;
            // 
            // EnabledShift
            // 
            this.EnabledShift.DataPropertyName = "EnabledShift";
            this.EnabledShift.HeaderText = "SHIFT";
            this.EnabledShift.Name = "EnabledShift";
            this.EnabledShift.Width = 43;
            // 
            // EnabledAlt
            // 
            this.EnabledAlt.DataPropertyName = "EnabledAlt";
            this.EnabledAlt.HeaderText = "ALT";
            this.EnabledAlt.Name = "EnabledAlt";
            this.EnabledAlt.Width = 43;
            // 
            // DispNumber
            // 
            this.DispNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DispNumber.DataPropertyName = "DispNumber";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DispNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.DispNumber.HeaderText = "対象";
            this.DispNumber.Name = "DispNumber";
            this.DispNumber.ReadOnly = true;
            this.DispNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DispNumber.Width = 32;
            // 
            // ShortcutName
            // 
            this.ShortcutName.DataPropertyName = "ShortcutName";
            this.ShortcutName.HeaderText = "ShortcutName";
            this.ShortcutName.Name = "ShortcutName";
            this.ShortcutName.ReadOnly = true;
            this.ShortcutName.Visible = false;
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            this.Number.HeaderText = "Number";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(300, 370);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(219, 370);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "リセット";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblSelectAll
            // 
            this.lblSelectAll.AutoSize = true;
            this.lblSelectAll.Location = new System.Drawing.Point(12, 9);
            this.lblSelectAll.Name = "lblSelectAll";
            this.lblSelectAll.Size = new System.Drawing.Size(86, 12);
            this.lblSelectAll.TabIndex = 3;
            this.lblSelectAll.Text = "全て選択/解除：";
            // 
            // chkCtrlAll
            // 
            this.chkCtrlAll.AutoSize = true;
            this.chkCtrlAll.Location = new System.Drawing.Point(209, 8);
            this.chkCtrlAll.Name = "chkCtrlAll";
            this.chkCtrlAll.Size = new System.Drawing.Size(53, 16);
            this.chkCtrlAll.TabIndex = 1;
            this.chkCtrlAll.Text = "CTRL";
            this.chkCtrlAll.UseVisualStyleBackColor = true;
            this.chkCtrlAll.CheckedChanged += new System.EventHandler(this.chkCtrlAll_CheckedChanged);
            // 
            // chkShiftAll
            // 
            this.chkShiftAll.AutoSize = true;
            this.chkShiftAll.Location = new System.Drawing.Point(268, 8);
            this.chkShiftAll.Name = "chkShiftAll";
            this.chkShiftAll.Size = new System.Drawing.Size(56, 16);
            this.chkShiftAll.TabIndex = 2;
            this.chkShiftAll.Text = "SHIFT";
            this.chkShiftAll.UseVisualStyleBackColor = true;
            this.chkShiftAll.CheckedChanged += new System.EventHandler(this.chkShiftAll_CheckedChanged);
            // 
            // chkAltAll
            // 
            this.chkAltAll.AutoSize = true;
            this.chkAltAll.Location = new System.Drawing.Point(330, 8);
            this.chkAltAll.Name = "chkAltAll";
            this.chkAltAll.Size = new System.Drawing.Size(45, 16);
            this.chkAltAll.TabIndex = 3;
            this.chkAltAll.Text = "ALT";
            this.chkAltAll.UseVisualStyleBackColor = true;
            this.chkAltAll.CheckedChanged += new System.EventHandler(this.chkAltAll_CheckedChanged);
            // 
            // cmsTriggerKey
            // 
            this.cmsTriggerKey.Name = "cmsTriggerKey";
            this.cmsTriggerKey.Size = new System.Drawing.Size(61, 4);
            // 
            // chkIsEnabledAll
            // 
            this.chkIsEnabledAll.AutoSize = true;
            this.chkIsEnabledAll.Location = new System.Drawing.Point(104, 8);
            this.chkIsEnabledAll.Name = "chkIsEnabledAll";
            this.chkIsEnabledAll.Size = new System.Drawing.Size(48, 16);
            this.chkIsEnabledAll.TabIndex = 0;
            this.chkIsEnabledAll.Text = "有効";
            this.chkIsEnabledAll.UseVisualStyleBackColor = true;
            this.chkIsEnabledAll.CheckedChanged += new System.EventHandler(this.chkIsEnabledAll_CheckedChanged);
            // 
            // frmShortcutSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(387, 405);
            this.ControlBox = false;
            this.Controls.Add(this.chkIsEnabledAll);
            this.Controls.Add(this.chkAltAll);
            this.Controls.Add(this.chkShiftAll);
            this.Controls.Add(this.chkCtrlAll);
            this.Controls.Add(this.lblSelectAll);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvShortcutList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShortcutSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ショートカット設定";
            this.Load += new System.EventHandler(this.frmShortcutSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortcutList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShortcutList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblSelectAll;
        private System.Windows.Forms.CheckBox chkCtrlAll;
        private System.Windows.Forms.CheckBox chkShiftAll;
        private System.Windows.Forms.CheckBox chkAltAll;
        private System.Windows.Forms.ContextMenuStrip cmsTriggerKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortcutDispName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledCtrl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledShift;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledAlt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortcutName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.CheckBox chkIsEnabledAll;
    }
}