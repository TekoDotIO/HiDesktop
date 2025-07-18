namespace HiDesktop
{
    partial class LaunchPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchPage));
            poster = new System.Windows.Forms.PictureBox();
            ProcessText = new System.Windows.Forms.Label();
            progressBar = new System.Windows.Forms.ProgressBar();
            VersionDisplay = new System.Windows.Forms.Label();
            StartupInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)poster).BeginInit();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(504, 127);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(104, 23);
            progressBar.TabIndex = 2;
            progressBar.Visible = false;
            // 
            // poster
            // 
            poster.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            poster.Image = (System.Drawing.Image)resources.GetObject("poster.Image");
            poster.Location = new System.Drawing.Point(0, 0);
            poster.Name = "poster";
            poster.Size = new System.Drawing.Size(1338, 839);
            poster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            poster.TabIndex = 4;
            poster.TabStop = false;
            // 
            // ProcessText
            // 
            ProcessText.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            ProcessText.ForeColor = System.Drawing.Color.Black;
            ProcessText.Location = new System.Drawing.Point(100, 786);
            ProcessText.Name = "ProcessText";
            ProcessText.Size = new System.Drawing.Size(1150, 39);
            ProcessText.TabIndex = 3;
            ProcessText.Click += label1_Click;
            
            // 
            // VersionDisplay
            // 
            VersionDisplay.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            VersionDisplay.ForeColor = System.Drawing.Color.Black;
            VersionDisplay.Location = new System.Drawing.Point(100, 659);
            VersionDisplay.Name = "VersionDisplay";
            VersionDisplay.Size = new System.Drawing.Size(508, 58);
            VersionDisplay.TabIndex = 1;
            // 
            // StartupInfo
            // 
            StartupInfo.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            StartupInfo.ForeColor = System.Drawing.Color.DimGray;
            StartupInfo.Location = new System.Drawing.Point(100, 237);
            StartupInfo.Name = "StartupInfo";
            StartupInfo.Size = new System.Drawing.Size(508, 402);
            StartupInfo.TabIndex = 0;
            StartupInfo.Click += StartupInfo_Click;
            // 
            // LaunchPage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            ClientSize = new System.Drawing.Size(1336, 834);
            Controls.Add(StartupInfo);
            Controls.Add(VersionDisplay);
            Controls.Add(progressBar);
            Controls.Add(ProcessText);
            Controls.Add(poster);
            Cursor = System.Windows.Forms.Cursors.AppStarting;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "LaunchPage";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Load += LaunchPage_Load;
            ((System.ComponentModel.ISupportInitialize)poster).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox poster;
        public System.Windows.Forms.Label ProcessText;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label VersionDisplay;
        private System.Windows.Forms.Label StartupInfo;
    }
}