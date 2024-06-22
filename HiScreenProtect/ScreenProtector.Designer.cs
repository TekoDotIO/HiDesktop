namespace HiDesktop.HiScreenProtect.MVP
{
    partial class ScreenProtector
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
            this.timeBox = new System.Windows.Forms.Label();
            this.Tips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeBox
            // 
            this.timeBox.AutoSize = true;
            this.timeBox.Font = new System.Drawing.Font("极影毁片圆 Medium", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.timeBox.Location = new System.Drawing.Point(111, 169);
            this.timeBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(346, 46);
            this.timeBox.TabIndex = 0;
            this.timeBox.Text = "HiScreenProtector";
            this.timeBox.Click += new System.EventHandler(this.timeBox_Click);
            // 
            // Tips
            // 
            this.Tips.AutoSize = true;
            this.Tips.Location = new System.Drawing.Point(187, 348);
            this.Tips.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(178, 17);
            this.Tips.TabIndex = 1;
            this.Tips.Text = "teko.IO all right reserved.";
            this.Tips.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Tips.Click += new System.EventHandler(this.label1_Click);
            // 
            // ScreenProtector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(560, 382);
            this.Controls.Add(this.Tips);
            this.Controls.Add(this.timeBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ScreenProtector";
            this.Text = "ScreenProtector";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseSlowly);
            this.Load += new System.EventHandler(this.ScreenProtector_Load);
            this.Click += new System.EventHandler(this.CloseWindow);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label timeBox;
        private System.Windows.Forms.Label Tips;
    }
}