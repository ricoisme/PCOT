namespace PCOT
{
    partial class frmSpeechSetting
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
            this.nudVolume = new System.Windows.Forms.NumericUpDown();
            this.nudRate = new System.Windows.Forms.NumericUpDown();
            this.lblSpeechVolume = new System.Windows.Forms.Label();
            this.lblSpeechRate = new System.Windows.Forms.Label();
            this.txtSpeechTest = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).BeginInit();
            this.SuspendLayout();
            // 
            // nudVolume
            // 
            this.nudVolume.Location = new System.Drawing.Point(95, 7);
            this.nudVolume.Name = "nudVolume";
            this.nudVolume.Size = new System.Drawing.Size(48, 19);
            this.nudVolume.TabIndex = 0;
            // 
            // nudRate
            // 
            this.nudRate.Location = new System.Drawing.Point(240, 7);
            this.nudRate.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRate.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudRate.Name = "nudRate";
            this.nudRate.Size = new System.Drawing.Size(48, 19);
            this.nudRate.TabIndex = 1;
            // 
            // lblSpeechVolume
            // 
            this.lblSpeechVolume.AutoSize = true;
            this.lblSpeechVolume.Location = new System.Drawing.Point(10, 9);
            this.lblSpeechVolume.Name = "lblSpeechVolume";
            this.lblSpeechVolume.Size = new System.Drawing.Size(79, 12);
            this.lblSpeechVolume.TabIndex = 1;
            this.lblSpeechVolume.Text = "音量(0～100)：";
            // 
            // lblSpeechRate
            // 
            this.lblSpeechRate.AutoSize = true;
            this.lblSpeechRate.Location = new System.Drawing.Point(149, 9);
            this.lblSpeechRate.Name = "lblSpeechRate";
            this.lblSpeechRate.Size = new System.Drawing.Size(85, 12);
            this.lblSpeechRate.TabIndex = 1;
            this.lblSpeechRate.Text = "速度(-10～10)：";
            // 
            // txtSpeechTest
            // 
            this.txtSpeechTest.Location = new System.Drawing.Point(12, 32);
            this.txtSpeechTest.Name = "txtSpeechTest";
            this.txtSpeechTest.Size = new System.Drawing.Size(276, 19);
            this.txtSpeechTest.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 57);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "音声テスト";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(213, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(132, 57);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "確定";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // frmSpeechSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(301, 87);
            this.ControlBox = false;
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtSpeechTest);
            this.Controls.Add(this.lblSpeechRate);
            this.Controls.Add(this.lblSpeechVolume);
            this.Controls.Add(this.nudRate);
            this.Controls.Add(this.nudVolume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpeechSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "音声設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpeechSetting_FormClosing);
            this.Load += new System.EventHandler(this.frmSpeechSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudVolume;
        private System.Windows.Forms.NumericUpDown nudRate;
        private System.Windows.Forms.Label lblSpeechVolume;
        private System.Windows.Forms.Label lblSpeechRate;
        private System.Windows.Forms.TextBox txtSpeechTest;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReturn;
    }
}