namespace PCOT
{
    partial class frmProtection
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
            this.SuspendLayout();
            // 
            // frmProtection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(582, 429);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProtection";
            this.Opacity = 0.6D;
            this.Text = "frmProtection";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmProtection_Load);
            this.Shown += new System.EventHandler(this.frmProtection_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmProtection_MouseDown);
            this.MouseEnter += new System.EventHandler(this.frmProtection_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.frmProtection_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}