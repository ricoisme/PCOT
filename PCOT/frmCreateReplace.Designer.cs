namespace PCOT
{
    partial class frmCreateReplace
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
            this.lblBeforeRepalce = new System.Windows.Forms.Label();
            this.txtBeforeReplace = new System.Windows.Forms.TextBox();
            this.lblAfterReplace = new System.Windows.Forms.Label();
            this.txtAfterReplace = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.cboDicType = new System.Windows.Forms.ComboBox();
            this.lblDicType = new System.Windows.Forms.Label();
            this.chkWordUnit = new System.Windows.Forms.CheckBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkUpperAndLower = new System.Windows.Forms.CheckBox();
            this.lblRegistTo = new System.Windows.Forms.Label();
            this.rdoCommon = new System.Windows.Forms.RadioButton();
            this.rdoTargetProcess = new System.Windows.Forms.RadioButton();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBeforeRepalce
            // 
            this.lblBeforeRepalce.AutoSize = true;
            this.lblBeforeRepalce.Location = new System.Drawing.Point(25, 39);
            this.lblBeforeRepalce.Name = "lblBeforeRepalce";
            this.lblBeforeRepalce.Size = new System.Drawing.Size(47, 12);
            this.lblBeforeRepalce.TabIndex = 0;
            this.lblBeforeRepalce.Text = "置換前：";
            // 
            // txtBeforeReplace
            // 
            this.txtBeforeReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeforeReplace.Location = new System.Drawing.Point(78, 36);
            this.txtBeforeReplace.MaxLength = 10000;
            this.txtBeforeReplace.Name = "txtBeforeReplace";
            this.txtBeforeReplace.Size = new System.Drawing.Size(289, 19);
            this.txtBeforeReplace.TabIndex = 3;
            // 
            // lblAfterReplace
            // 
            this.lblAfterReplace.AutoSize = true;
            this.lblAfterReplace.Location = new System.Drawing.Point(25, 61);
            this.lblAfterReplace.Name = "lblAfterReplace";
            this.lblAfterReplace.Size = new System.Drawing.Size(47, 12);
            this.lblAfterReplace.TabIndex = 0;
            this.lblAfterReplace.Text = "置換後：";
            // 
            // txtAfterReplace
            // 
            this.txtAfterReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAfterReplace.Location = new System.Drawing.Point(78, 58);
            this.txtAfterReplace.MaxLength = 10000;
            this.txtAfterReplace.Name = "txtAfterReplace";
            this.txtAfterReplace.Size = new System.Drawing.Size(289, 19);
            this.txtAfterReplace.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(295, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(214, 0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 8;
            this.btnReturn.Text = "確定";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // cboDicType
            // 
            this.cboDicType.DisplayMember = "DicTypeName";
            this.cboDicType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDicType.FormattingEnabled = true;
            this.cboDicType.Location = new System.Drawing.Point(78, 10);
            this.cboDicType.Name = "cboDicType";
            this.cboDicType.Size = new System.Drawing.Size(102, 20);
            this.cboDicType.TabIndex = 0;
            this.cboDicType.ValueMember = "DicTypeCd";
            this.cboDicType.SelectedIndexChanged += new System.EventHandler(this.cboDicType_SelectedIndexChanged);
            // 
            // lblDicType
            // 
            this.lblDicType.AutoSize = true;
            this.lblDicType.Location = new System.Drawing.Point(13, 13);
            this.lblDicType.Name = "lblDicType";
            this.lblDicType.Size = new System.Drawing.Size(59, 12);
            this.lblDicType.TabIndex = 0;
            this.lblDicType.Text = "辞書区分：";
            // 
            // chkWordUnit
            // 
            this.chkWordUnit.AutoSize = true;
            this.chkWordUnit.Location = new System.Drawing.Point(81, 86);
            this.chkWordUnit.Name = "chkWordUnit";
            this.chkWordUnit.Size = new System.Drawing.Size(106, 16);
            this.chkWordUnit.TabIndex = 6;
            this.chkWordUnit.Text = "単語単位で置換";
            this.chkWordUnit.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(15, 86);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(60, 16);
            this.chkEnabled.TabIndex = 5;
            this.chkEnabled.Text = "有効化";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // chkUpperAndLower
            // 
            this.chkUpperAndLower.AutoSize = true;
            this.chkUpperAndLower.Location = new System.Drawing.Point(193, 86);
            this.chkUpperAndLower.Name = "chkUpperAndLower";
            this.chkUpperAndLower.Size = new System.Drawing.Size(129, 16);
            this.chkUpperAndLower.TabIndex = 7;
            this.chkUpperAndLower.Text = "大文字小文字を区別";
            this.chkUpperAndLower.UseVisualStyleBackColor = true;
            // 
            // lblRegistTo
            // 
            this.lblRegistTo.AutoSize = true;
            this.lblRegistTo.Location = new System.Drawing.Point(186, 13);
            this.lblRegistTo.Name = "lblRegistTo";
            this.lblRegistTo.Size = new System.Drawing.Size(47, 12);
            this.lblRegistTo.TabIndex = 8;
            this.lblRegistTo.Text = "登録先：";
            // 
            // rdoCommon
            // 
            this.rdoCommon.AutoSize = true;
            this.rdoCommon.Location = new System.Drawing.Point(239, 11);
            this.rdoCommon.Name = "rdoCommon";
            this.rdoCommon.Size = new System.Drawing.Size(47, 16);
            this.rdoCommon.TabIndex = 1;
            this.rdoCommon.Text = "共通";
            this.rdoCommon.UseVisualStyleBackColor = true;
            // 
            // rdoTargetProcess
            // 
            this.rdoTargetProcess.AutoSize = true;
            this.rdoTargetProcess.Location = new System.Drawing.Point(292, 11);
            this.rdoTargetProcess.Name = "rdoTargetProcess";
            this.rdoTargetProcess.Size = new System.Drawing.Size(84, 16);
            this.rdoTargetProcess.TabIndex = 2;
            this.rdoTargetProcess.Text = "対象プロセス";
            this.rdoTargetProcess.UseVisualStyleBackColor = true;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.lblDicType);
            this.pnlBody.Controls.Add(this.lblBeforeRepalce);
            this.pnlBody.Controls.Add(this.lblAfterReplace);
            this.pnlBody.Controls.Add(this.rdoTargetProcess);
            this.pnlBody.Controls.Add(this.txtBeforeReplace);
            this.pnlBody.Controls.Add(this.rdoCommon);
            this.pnlBody.Controls.Add(this.txtAfterReplace);
            this.pnlBody.Controls.Add(this.lblRegistTo);
            this.pnlBody.Controls.Add(this.cboDicType);
            this.pnlBody.Controls.Add(this.chkUpperAndLower);
            this.pnlBody.Controls.Add(this.chkWordUnit);
            this.pnlBody.Controls.Add(this.chkEnabled);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(384, 107);
            this.pnlBody.TabIndex = 10;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnReturn);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 108);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(384, 35);
            this.pnlFooter.TabIndex = 11;
            // 
            // frmCreateReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 143);
            this.ControlBox = false;
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlBody);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(10000, 182);
            this.MinimumSize = new System.Drawing.Size(400, 182);
            this.Name = "frmCreateReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "辞書登録";
            this.Load += new System.EventHandler(this.frmEditReplace_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBeforeRepalce;
        private System.Windows.Forms.TextBox txtBeforeReplace;
        private System.Windows.Forms.Label lblAfterReplace;
        private System.Windows.Forms.TextBox txtAfterReplace;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ComboBox cboDicType;
        private System.Windows.Forms.Label lblDicType;
        private System.Windows.Forms.CheckBox chkWordUnit;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkUpperAndLower;
        private System.Windows.Forms.Label lblRegistTo;
        private System.Windows.Forms.RadioButton rdoCommon;
        private System.Windows.Forms.RadioButton rdoTargetProcess;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlFooter;
    }
}