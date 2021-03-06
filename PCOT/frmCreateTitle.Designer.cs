namespace PCOT
{
    partial class frmCreateTitle
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
            this.cboLabel = new System.Windows.Forms.ComboBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblReadMultiples = new System.Windows.Forms.Label();
            this.lblXH = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblYH = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblWH = new System.Windows.Forms.Label();
            this.lblW = new System.Windows.Forms.Label();
            this.lblHH = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.cboUseOcrEngine = new System.Windows.Forms.ComboBox();
            this.nudReadMultiples = new PCOT.ExtendedNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadMultiples)).BeginInit();
            this.SuspendLayout();
            // 
            // cboLabel
            // 
            this.cboLabel.DisplayMember = "Title";
            this.cboLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabel.FormattingEnabled = true;
            this.cboLabel.Location = new System.Drawing.Point(12, 12);
            this.cboLabel.Name = "cboLabel";
            this.cboLabel.Size = new System.Drawing.Size(156, 20);
            this.cboLabel.TabIndex = 0;
            this.cboLabel.ValueMember = "Id";
            this.cboLabel.SelectedIndexChanged += new System.EventHandler(this.cboLabel_SelectedIndexChanged);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(174, 13);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(156, 19);
            this.txtLabel.TabIndex = 1;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(12, 91);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "新規作成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(255, 91);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(174, 91);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(93, 91);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblReadMultiples
            // 
            this.lblReadMultiples.AutoSize = true;
            this.lblReadMultiples.Location = new System.Drawing.Point(12, 65);
            this.lblReadMultiples.Name = "lblReadMultiples";
            this.lblReadMultiples.Size = new System.Drawing.Size(59, 12);
            this.lblReadMultiples.TabIndex = 7;
            this.lblReadMultiples.Text = "読取倍率：";
            // 
            // lblXH
            // 
            this.lblXH.AutoSize = true;
            this.lblXH.Location = new System.Drawing.Point(191, 44);
            this.lblXH.Name = "lblXH";
            this.lblXH.Size = new System.Drawing.Size(18, 12);
            this.lblXH.TabIndex = 8;
            this.lblXH.Text = "X：";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(215, 44);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(33, 12);
            this.lblX.TabIndex = 8;
            this.lblX.Text = "XXXX";
            // 
            // lblYH
            // 
            this.lblYH.AutoSize = true;
            this.lblYH.Location = new System.Drawing.Point(258, 44);
            this.lblYH.Name = "lblYH";
            this.lblYH.Size = new System.Drawing.Size(18, 12);
            this.lblYH.TabIndex = 8;
            this.lblYH.Text = "Y：";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(282, 44);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(33, 12);
            this.lblY.TabIndex = 8;
            this.lblY.Text = "XXXX";
            // 
            // lblWH
            // 
            this.lblWH.AutoSize = true;
            this.lblWH.Location = new System.Drawing.Point(189, 65);
            this.lblWH.Name = "lblWH";
            this.lblWH.Size = new System.Drawing.Size(20, 12);
            this.lblWH.TabIndex = 8;
            this.lblWH.Text = "W：";
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(215, 65);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(33, 12);
            this.lblW.TabIndex = 8;
            this.lblW.Text = "XXXX";
            // 
            // lblHH
            // 
            this.lblHH.AutoSize = true;
            this.lblHH.Location = new System.Drawing.Point(257, 65);
            this.lblHH.Name = "lblHH";
            this.lblHH.Size = new System.Drawing.Size(19, 12);
            this.lblHH.TabIndex = 8;
            this.lblHH.Text = "H：";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(282, 65);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(33, 12);
            this.lblH.TabIndex = 8;
            this.lblH.Text = "XXXX";
            // 
            // cboUseOcrEngine
            // 
            this.cboUseOcrEngine.DisplayMember = "Text";
            this.cboUseOcrEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUseOcrEngine.FormattingEnabled = true;
            this.cboUseOcrEngine.Location = new System.Drawing.Point(12, 37);
            this.cboUseOcrEngine.Name = "cboUseOcrEngine";
            this.cboUseOcrEngine.Size = new System.Drawing.Size(156, 20);
            this.cboUseOcrEngine.TabIndex = 2;
            this.cboUseOcrEngine.ValueMember = "Value";
            // 
            // nudReadMultiples
            // 
            this.nudReadMultiples.DecimalPlaces = 1;
            this.nudReadMultiples.Location = new System.Drawing.Point(77, 63);
            this.nudReadMultiples.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudReadMultiples.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudReadMultiples.Name = "nudReadMultiples";
            this.nudReadMultiples.ReadOnly = true;
            this.nudReadMultiples.Size = new System.Drawing.Size(91, 19);
            this.nudReadMultiples.TabIndex = 3;
            this.nudReadMultiples.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudReadMultiples.UpDown += new PCOT.ExtendedNumericUpDown.UpDownEventHandler(this.nudReadMultiples_UpDown);
            // 
            // frmCreateTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(343, 126);
            this.ControlBox = false;
            this.Controls.Add(this.cboUseOcrEngine);
            this.Controls.Add(this.nudReadMultiples);
            this.Controls.Add(this.lblH);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblHH);
            this.Controls.Add(this.lblYH);
            this.Controls.Add(this.lblW);
            this.Controls.Add(this.lblWH);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblXH);
            this.Controls.Add(this.lblReadMultiples);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.cboLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateTitle";
            this.Text = "ラベルタイトル設定";
            this.Load += new System.EventHandler(this.frmCreateTitle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudReadMultiples)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLabel;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblReadMultiples;
        private System.Windows.Forms.Label lblXH;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblYH;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblWH;
        private System.Windows.Forms.Label lblW;
        private System.Windows.Forms.Label lblHH;
        private System.Windows.Forms.Label lblH;
        private ExtendedNumericUpDown nudReadMultiples;
        private System.Windows.Forms.ComboBox cboUseOcrEngine;
    }
}