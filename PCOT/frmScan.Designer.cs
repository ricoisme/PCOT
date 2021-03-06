namespace PCOT
{
    partial class frmScan
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Black;
            this.lblMessage.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(94, 21);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Tag = "";
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(598, 447);
            this.Controls.Add(this.lblMessage);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(100, 240);
            this.Name = "frmScan";
            this.Opacity = 0.6D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmScan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScan_FormClosing);
            this.Load += new System.EventHandler(this.frmScan_Load);
            this.Shown += new System.EventHandler(this.frmScan_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmScan_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmScan_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmScan_MouseDown);
            this.MouseEnter += new System.EventHandler(this.frmScan_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmScan_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmScan_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
    }
}