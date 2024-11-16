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
            // poster
            // 
            poster.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            resources.ApplyResources(poster, "poster");
            poster.Name = "poster";
            poster.TabStop = false;
            // 
            // ProcessText
            // 
            resources.ApplyResources(ProcessText, "ProcessText");
            ProcessText.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            ProcessText.ForeColor = System.Drawing.Color.Black;
            ProcessText.Name = "ProcessText";
            ProcessText.Click += label1_Click;
            // 
            // progressBar
            // 
            resources.ApplyResources(progressBar, "progressBar");
            progressBar.Name = "progressBar";
            // 
            // VersionDisplay
            // 
            VersionDisplay.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            resources.ApplyResources(VersionDisplay, "VersionDisplay");
            VersionDisplay.ForeColor = System.Drawing.Color.Black;
            VersionDisplay.Name = "VersionDisplay";
            // 
            // StartupInfo
            // 
            StartupInfo.BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            resources.ApplyResources(StartupInfo, "StartupInfo");
            StartupInfo.ForeColor = System.Drawing.Color.DimGray;
            StartupInfo.Name = "StartupInfo";
            StartupInfo.Click += StartupInfo_Click;
            // 
            // LaunchPage
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 241, 241);
            Controls.Add(StartupInfo);
            Controls.Add(VersionDisplay);
            Controls.Add(progressBar);
            Controls.Add(ProcessText);
            Controls.Add(poster);
            Cursor = System.Windows.Forms.Cursors.AppStarting;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "LaunchPage";
            ShowInTaskbar = false;
            Load += LaunchPage_Load;
            ((System.ComponentModel.ISupportInitialize)poster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox poster;
        public System.Windows.Forms.Label ProcessText;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label VersionDisplay;
        private System.Windows.Forms.Label StartupInfo;
    }
}