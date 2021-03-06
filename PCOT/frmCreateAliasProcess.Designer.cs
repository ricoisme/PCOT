namespace PCOT
{
    partial class frmCreateAliasProcess
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
            this.lblProcessName = new System.Windows.Forms.Label();
            this.txtAliasProcessName = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProcessName
            // 
            this.lblProcessName.AutoSize = true;
            this.lblProcessName.Location = new System.Drawing.Point(12, 9);
            this.lblProcessName.Name = "lblProcessName";
            this.lblProcessName.Size = new System.Drawing.Size(0, 12);
            this.lblProcessName.TabIndex = 0;
            // 
            // txtAliasProcessName
            // 
            this.txtAliasProcessName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtAliasProcessName.Location = new System.Drawing.Point(18, 6);
            this.txtAliasProcessName.MaxLength = 20;
            this.txtAliasProcessName.Name = "txtAliasProcessName";
            this.txtAliasProcessName.Size = new System.Drawing.Size(175, 19);
            this.txtAliasProcessName.TabIndex = 1;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(38, 31);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "確定";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(119, 31);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCreateAliasProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(206, 65);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtAliasProcessName);
            this.Controls.Add(this.lblProcessName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateAliasProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "プロセス別名設定";
            this.Load += new System.EventHandler(this.CreateAliasProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcessName;
        private System.Windows.Forms.TextBox txtAliasProcessName;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCancel;
    }
}