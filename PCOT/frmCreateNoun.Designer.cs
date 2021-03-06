namespace PCOT
{
    partial class frmCreateNoun
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
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtAfterReplace = new System.Windows.Forms.TextBox();
            this.txtBeforeReplace = new System.Windows.Forms.TextBox();
            this.lblAfterReplace = new System.Windows.Forms.Label();
            this.lblBeforeRepalce = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlBody.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(12, 12);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(60, 16);
            this.chkEnabled.TabIndex = 0;
            this.chkEnabled.Text = "有効化";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // txtAfterReplace
            // 
            this.txtAfterReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAfterReplace.Location = new System.Drawing.Point(63, 52);
            this.txtAfterReplace.MaxLength = 10000;
            this.txtAfterReplace.Name = "txtAfterReplace";
            this.txtAfterReplace.Size = new System.Drawing.Size(192, 19);
            this.txtAfterReplace.TabIndex = 2;
            // 
            // txtBeforeReplace
            // 
            this.txtBeforeReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeforeReplace.Location = new System.Drawing.Point(63, 30);
            this.txtBeforeReplace.MaxLength = 10000;
            this.txtBeforeReplace.Name = "txtBeforeReplace";
            this.txtBeforeReplace.Size = new System.Drawing.Size(192, 19);
            this.txtBeforeReplace.TabIndex = 1;
            // 
            // lblAfterReplace
            // 
            this.lblAfterReplace.AutoSize = true;
            this.lblAfterReplace.Location = new System.Drawing.Point(10, 56);
            this.lblAfterReplace.Name = "lblAfterReplace";
            this.lblAfterReplace.Size = new System.Drawing.Size(47, 12);
            this.lblAfterReplace.TabIndex = 5;
            this.lblAfterReplace.Text = "置換後：";
            // 
            // lblBeforeRepalce
            // 
            this.lblBeforeRepalce.AutoSize = true;
            this.lblBeforeRepalce.Location = new System.Drawing.Point(10, 34);
            this.lblBeforeRepalce.Name = "lblBeforeRepalce";
            this.lblBeforeRepalce.Size = new System.Drawing.Size(47, 12);
            this.lblBeforeRepalce.TabIndex = 6;
            this.lblBeforeRepalce.Text = "置換前：";
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(99, 1);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "確定";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(180, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.chkEnabled);
            this.pnlBody.Controls.Add(this.lblBeforeRepalce);
            this.pnlBody.Controls.Add(this.lblAfterReplace);
            this.pnlBody.Controls.Add(this.txtBeforeReplace);
            this.pnlBody.Controls.Add(this.txtAfterReplace);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(264, 76);
            this.pnlBody.TabIndex = 7;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnReturn);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 81);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(264, 35);
            this.pnlFooter.TabIndex = 8;
            // 
            // frmCreateNoun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(264, 116);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(10000, 155);
            this.MinimumSize = new System.Drawing.Size(280, 155);
            this.Name = "frmCreateNoun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "名詞登録";
            this.Load += new System.EventHandler(this.frmCreateNoun_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TextBox txtAfterReplace;
        private System.Windows.Forms.TextBox txtBeforeReplace;
        private System.Windows.Forms.Label lblAfterReplace;
        private System.Windows.Forms.Label lblBeforeRepalce;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlFooter;
    }
}