namespace Widgets.MVP.WindowApps
{
    partial class RandomPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomPicker));
            Subtitle = new System.Windows.Forms.Label();
            Title = new System.Windows.Forms.Label();
            CopyrightLabel = new System.Windows.Forms.Label();
            AppPages = new System.Windows.Forms.TabControl();
            RanNumPage = new System.Windows.Forms.TabPage();
            RanNumMemClear = new System.Windows.Forms.Button();
            RanNumDisplay = new System.Windows.Forms.Label();
            RanNumDisplayFontSizeApply = new System.Windows.Forms.Button();
            RanNumHistoryBtn = new System.Windows.Forms.Button();
            RanNumDisplayFontSizeBox = new System.Windows.Forms.TextBox();
            RanNumGenerateBtn = new System.Windows.Forms.Button();
            RanNumDisplayFontSize = new System.Windows.Forms.Label();
            RanNumAnimate = new System.Windows.Forms.CheckBox();
            RanNumNumBox = new System.Windows.Forms.TextBox();
            RanNumNum = new System.Windows.Forms.Label();
            RanNumExceptBox = new System.Windows.Forms.TextBox();
            RanNumExcept = new System.Windows.Forms.Label();
            RanNumMaxBox = new System.Windows.Forms.TextBox();
            RanNumMax = new System.Windows.Forms.Label();
            RanNumMinBox = new System.Windows.Forms.TextBox();
            RanNumMin = new System.Windows.Forms.Label();
            RanNumAddToExcept = new System.Windows.Forms.CheckBox();
            RanNumResultDisplayLabel = new System.Windows.Forms.Label();
            RanDbPage = new System.Windows.Forms.TabPage();
            RanDbEnableWeight = new System.Windows.Forms.CheckBox();
            RanDbSettingBtn = new System.Windows.Forms.Button();
            RanDbExceptTagBox = new System.Windows.Forms.TextBox();
            RanDbExceptTag = new System.Windows.Forms.Label();
            aRanDbDisplayFontSizeApplyBtn = new System.Windows.Forms.Button();
            RanDbHistory = new System.Windows.Forms.Button();
            RanDbGenerateBtn = new System.Windows.Forms.Button();
            RanDbDisplayFontSize = new System.Windows.Forms.Label();
            RanDbAnimate = new System.Windows.Forms.CheckBox();
            RanDbNumBox = new System.Windows.Forms.TextBox();
            RanDbNum = new System.Windows.Forms.Label();
            RanDbExceptNameBox = new System.Windows.Forms.TextBox();
            RanDbExceptName = new System.Windows.Forms.Label();
            RanDbDisplayFontSizeBox = new System.Windows.Forms.TextBox();
            RanDbSettingText = new System.Windows.Forms.Label();
            RanDbAddToExcept = new System.Windows.Forms.CheckBox();
            RanDbDisplay = new System.Windows.Forms.Label();
            RanDbResultDisplayText = new System.Windows.Forms.Label();
            fullScreenBox = new System.Windows.Forms.CheckBox();
            darkModeBox = new System.Windows.Forms.CheckBox();
            ExitBtn = new System.Windows.Forms.Button();
            RanDbMemClear = new System.Windows.Forms.Button();
            AppPages.SuspendLayout();
            RanNumPage.SuspendLayout();
            RanDbPage.SuspendLayout();
            SuspendLayout();
            // 
            // Subtitle
            // 
            resources.ApplyResources(Subtitle, "Subtitle");
            Subtitle.Name = "Subtitle";
            // 
            // Title
            // 
            resources.ApplyResources(Title, "Title");
            Title.Name = "Title";
            // 
            // CopyrightLabel
            // 
            resources.ApplyResources(CopyrightLabel, "CopyrightLabel");
            CopyrightLabel.Name = "CopyrightLabel";
            // 
            // AppPages
            // 
            AppPages.Controls.Add(RanNumPage);
            AppPages.Controls.Add(RanDbPage);
            resources.ApplyResources(AppPages, "AppPages");
            AppPages.Name = "AppPages";
            AppPages.SelectedIndex = 0;
            // 
            // RanNumPage
            // 
            RanNumPage.Controls.Add(RanNumMemClear);
            RanNumPage.Controls.Add(RanNumDisplay);
            RanNumPage.Controls.Add(RanNumDisplayFontSizeApply);
            RanNumPage.Controls.Add(RanNumHistoryBtn);
            RanNumPage.Controls.Add(RanNumDisplayFontSizeBox);
            RanNumPage.Controls.Add(RanNumGenerateBtn);
            RanNumPage.Controls.Add(RanNumDisplayFontSize);
            RanNumPage.Controls.Add(RanNumAnimate);
            RanNumPage.Controls.Add(RanNumNumBox);
            RanNumPage.Controls.Add(RanNumNum);
            RanNumPage.Controls.Add(RanNumExceptBox);
            RanNumPage.Controls.Add(RanNumExcept);
            RanNumPage.Controls.Add(RanNumMaxBox);
            RanNumPage.Controls.Add(RanNumMax);
            RanNumPage.Controls.Add(RanNumMinBox);
            RanNumPage.Controls.Add(RanNumMin);
            RanNumPage.Controls.Add(RanNumAddToExcept);
            RanNumPage.Controls.Add(RanNumResultDisplayLabel);
            resources.ApplyResources(RanNumPage, "RanNumPage");
            RanNumPage.Name = "RanNumPage";
            RanNumPage.UseVisualStyleBackColor = true;
            RanNumPage.Click += RanNumPage_Click;
            // 
            // RanNumMemClear
            // 
            RanNumMemClear.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(RanNumMemClear, "RanNumMemClear");
            RanNumMemClear.Name = "RanNumMemClear";
            RanNumMemClear.UseVisualStyleBackColor = false;
            RanNumMemClear.Click += RanNumMemClear_Click;
            // 
            // RanNumDisplay
            // 
            resources.ApplyResources(RanNumDisplay, "RanNumDisplay");
            RanNumDisplay.Name = "RanNumDisplay";
            RanNumDisplay.Click += label1_Click;
            // 
            // RanNumDisplayFontSizeApply
            // 
            RanNumDisplayFontSizeApply.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(RanNumDisplayFontSizeApply, "RanNumDisplayFontSizeApply");
            RanNumDisplayFontSizeApply.Name = "RanNumDisplayFontSizeApply";
            RanNumDisplayFontSizeApply.UseVisualStyleBackColor = false;
            RanNumDisplayFontSizeApply.Click += RanNumDisplayFontSizeApply_Click_1;
            // 
            // RanNumHistoryBtn
            // 
            RanNumHistoryBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(RanNumHistoryBtn, "RanNumHistoryBtn");
            RanNumHistoryBtn.Name = "RanNumHistoryBtn";
            RanNumHistoryBtn.UseVisualStyleBackColor = false;
            RanNumHistoryBtn.Click += RanNumHistoryBtn_Click;
            // 
            // RanNumDisplayFontSizeBox
            // 
            resources.ApplyResources(RanNumDisplayFontSizeBox, "RanNumDisplayFontSizeBox");
            RanNumDisplayFontSizeBox.Name = "RanNumDisplayFontSizeBox";
            // 
            // RanNumGenerateBtn
            // 
            RanNumGenerateBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(RanNumGenerateBtn, "RanNumGenerateBtn");
            RanNumGenerateBtn.Name = "RanNumGenerateBtn";
            RanNumGenerateBtn.UseVisualStyleBackColor = false;
            RanNumGenerateBtn.Click += RanNumGenerateBtn_Click;
            // 
            // RanNumDisplayFontSize
            // 
            resources.ApplyResources(RanNumDisplayFontSize, "RanNumDisplayFontSize");
            RanNumDisplayFontSize.Name = "RanNumDisplayFontSize";
            // 
            // RanNumAnimate
            // 
            resources.ApplyResources(RanNumAnimate, "RanNumAnimate");
            RanNumAnimate.Checked = true;
            RanNumAnimate.CheckState = System.Windows.Forms.CheckState.Checked;
            RanNumAnimate.Name = "RanNumAnimate";
            RanNumAnimate.UseVisualStyleBackColor = true;
            // 
            // RanNumNumBox
            // 
            resources.ApplyResources(RanNumNumBox, "RanNumNumBox");
            RanNumNumBox.Name = "RanNumNumBox";
            // 
            // RanNumNum
            // 
            resources.ApplyResources(RanNumNum, "RanNumNum");
            RanNumNum.Name = "RanNumNum";
            // 
            // RanNumExceptBox
            // 
            resources.ApplyResources(RanNumExceptBox, "RanNumExceptBox");
            RanNumExceptBox.Name = "RanNumExceptBox";
            // 
            // RanNumExcept
            // 
            resources.ApplyResources(RanNumExcept, "RanNumExcept");
            RanNumExcept.Name = "RanNumExcept";
            // 
            // RanNumMaxBox
            // 
            resources.ApplyResources(RanNumMaxBox, "RanNumMaxBox");
            RanNumMaxBox.Name = "RanNumMaxBox";
            // 
            // RanNumMax
            // 
            resources.ApplyResources(RanNumMax, "RanNumMax");
            RanNumMax.Name = "RanNumMax";
            // 
            // RanNumMinBox
            // 
            resources.ApplyResources(RanNumMinBox, "RanNumMinBox");
            RanNumMinBox.Name = "RanNumMinBox";
            // 
            // RanNumMin
            // 
            resources.ApplyResources(RanNumMin, "RanNumMin");
            RanNumMin.Name = "RanNumMin";
            // 
            // RanNumAddToExcept
            // 
            resources.ApplyResources(RanNumAddToExcept, "RanNumAddToExcept");
            RanNumAddToExcept.Checked = true;
            RanNumAddToExcept.CheckState = System.Windows.Forms.CheckState.Checked;
            RanNumAddToExcept.Name = "RanNumAddToExcept";
            RanNumAddToExcept.UseVisualStyleBackColor = true;
            // 
            // RanNumResultDisplayLabel
            // 
            resources.ApplyResources(RanNumResultDisplayLabel, "RanNumResultDisplayLabel");
            RanNumResultDisplayLabel.Name = "RanNumResultDisplayLabel";
            // 
            // RanDbPage
            // 
            RanDbPage.Controls.Add(RanDbMemClear);
            RanDbPage.Controls.Add(RanDbEnableWeight);
            RanDbPage.Controls.Add(RanDbSettingBtn);
            RanDbPage.Controls.Add(RanDbExceptTagBox);
            RanDbPage.Controls.Add(RanDbExceptTag);
            RanDbPage.Controls.Add(aRanDbDisplayFontSizeApplyBtn);
            RanDbPage.Controls.Add(RanDbHistory);
            RanDbPage.Controls.Add(RanDbGenerateBtn);
            RanDbPage.Controls.Add(RanDbDisplayFontSize);
            RanDbPage.Controls.Add(RanDbAnimate);
            RanDbPage.Controls.Add(RanDbNumBox);
            RanDbPage.Controls.Add(RanDbNum);
            RanDbPage.Controls.Add(RanDbExceptNameBox);
            RanDbPage.Controls.Add(RanDbExceptName);
            RanDbPage.Controls.Add(RanDbDisplayFontSizeBox);
            RanDbPage.Controls.Add(RanDbSettingText);
            RanDbPage.Controls.Add(RanDbAddToExcept);
            RanDbPage.Controls.Add(RanDbDisplay);
            RanDbPage.Controls.Add(RanDbResultDisplayText);
            resources.ApplyResources(RanDbPage, "RanDbPage");
            RanDbPage.Name = "RanDbPage";
            RanDbPage.UseVisualStyleBackColor = true;
            // 
            // RanDbEnableWeight
            // 
            resources.ApplyResources(RanDbEnableWeight, "RanDbEnableWeight");
            RanDbEnableWeight.Checked = true;
            RanDbEnableWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            RanDbEnableWeight.Name = "RanDbEnableWeight";
            RanDbEnableWeight.UseVisualStyleBackColor = true;
            // 
            // RanDbSettingBtn
            // 
            RanDbSettingBtn.BackColor = System.Drawing.SystemColors.Control;
            RanDbSettingBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(RanDbSettingBtn, "RanDbSettingBtn");
            RanDbSettingBtn.Name = "RanDbSettingBtn";
            RanDbSettingBtn.UseVisualStyleBackColor = false;
            // 
            // RanDbExceptTagBox
            // 
            resources.ApplyResources(RanDbExceptTagBox, "RanDbExceptTagBox");
            RanDbExceptTagBox.Name = "RanDbExceptTagBox";
            // 
            // RanDbExceptTag
            // 
            resources.ApplyResources(RanDbExceptTag, "RanDbExceptTag");
            RanDbExceptTag.Name = "RanDbExceptTag";
            // 
            // aRanDbDisplayFontSizeApplyBtn
            // 
            aRanDbDisplayFontSizeApplyBtn.BackColor = System.Drawing.SystemColors.Control;
            aRanDbDisplayFontSizeApplyBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(aRanDbDisplayFontSizeApplyBtn, "aRanDbDisplayFontSizeApplyBtn");
            aRanDbDisplayFontSizeApplyBtn.Name = "aRanDbDisplayFontSizeApplyBtn";
            aRanDbDisplayFontSizeApplyBtn.UseVisualStyleBackColor = false;
            // 
            // RanDbHistory
            // 
            RanDbHistory.BackColor = System.Drawing.SystemColors.Control;
            RanDbHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(RanDbHistory, "RanDbHistory");
            RanDbHistory.Name = "RanDbHistory";
            RanDbHistory.UseVisualStyleBackColor = false;
            // 
            // RanDbGenerateBtn
            // 
            RanDbGenerateBtn.BackColor = System.Drawing.SystemColors.Control;
            RanDbGenerateBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(RanDbGenerateBtn, "RanDbGenerateBtn");
            RanDbGenerateBtn.Name = "RanDbGenerateBtn";
            RanDbGenerateBtn.UseVisualStyleBackColor = false;
            // 
            // RanDbDisplayFontSize
            // 
            resources.ApplyResources(RanDbDisplayFontSize, "RanDbDisplayFontSize");
            RanDbDisplayFontSize.Name = "RanDbDisplayFontSize";
            // 
            // RanDbAnimate
            // 
            resources.ApplyResources(RanDbAnimate, "RanDbAnimate");
            RanDbAnimate.Checked = true;
            RanDbAnimate.CheckState = System.Windows.Forms.CheckState.Checked;
            RanDbAnimate.Name = "RanDbAnimate";
            RanDbAnimate.UseVisualStyleBackColor = true;
            // 
            // RanDbNumBox
            // 
            resources.ApplyResources(RanDbNumBox, "RanDbNumBox");
            RanDbNumBox.Name = "RanDbNumBox";
            // 
            // RanDbNum
            // 
            resources.ApplyResources(RanDbNum, "RanDbNum");
            RanDbNum.Name = "RanDbNum";
            // 
            // RanDbExceptNameBox
            // 
            resources.ApplyResources(RanDbExceptNameBox, "RanDbExceptNameBox");
            RanDbExceptNameBox.Name = "RanDbExceptNameBox";
            // 
            // RanDbExceptName
            // 
            resources.ApplyResources(RanDbExceptName, "RanDbExceptName");
            RanDbExceptName.Name = "RanDbExceptName";
            // 
            // RanDbDisplayFontSizeBox
            // 
            resources.ApplyResources(RanDbDisplayFontSizeBox, "RanDbDisplayFontSizeBox");
            RanDbDisplayFontSizeBox.Name = "RanDbDisplayFontSizeBox";
            // 
            // RanDbSettingText
            // 
            resources.ApplyResources(RanDbSettingText, "RanDbSettingText");
            RanDbSettingText.Name = "RanDbSettingText";
            // 
            // RanDbAddToExcept
            // 
            resources.ApplyResources(RanDbAddToExcept, "RanDbAddToExcept");
            RanDbAddToExcept.Checked = true;
            RanDbAddToExcept.CheckState = System.Windows.Forms.CheckState.Checked;
            RanDbAddToExcept.Name = "RanDbAddToExcept";
            RanDbAddToExcept.UseVisualStyleBackColor = true;
            RanDbAddToExcept.CheckedChanged += RanDbAddToExcept_CheckedChanged;
            // 
            // RanDbDisplay
            // 
            resources.ApplyResources(RanDbDisplay, "RanDbDisplay");
            RanDbDisplay.Name = "RanDbDisplay";
            // 
            // RanDbResultDisplayText
            // 
            resources.ApplyResources(RanDbResultDisplayText, "RanDbResultDisplayText");
            RanDbResultDisplayText.Name = "RanDbResultDisplayText";
            // 
            // fullScreenBox
            // 
            resources.ApplyResources(fullScreenBox, "fullScreenBox");
            fullScreenBox.Name = "fullScreenBox";
            fullScreenBox.UseVisualStyleBackColor = true;
            fullScreenBox.CheckedChanged += fullScreenBox_CheckedChanged;
            // 
            // darkModeBox
            // 
            resources.ApplyResources(darkModeBox, "darkModeBox");
            darkModeBox.Name = "darkModeBox";
            darkModeBox.UseVisualStyleBackColor = true;
            darkModeBox.CheckedChanged += DarkModeBox_CheckedChanged;
            // 
            // ExitBtn
            // 
            resources.ApplyResources(ExitBtn, "ExitBtn");
            ExitBtn.Name = "ExitBtn";
            ExitBtn.UseVisualStyleBackColor = true;
            ExitBtn.Click += ExitBtn_Click_1;
            // 
            // RanDbMemClear
            // 
            RanDbMemClear.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(RanDbMemClear, "RanDbMemClear");
            RanDbMemClear.Name = "RanDbMemClear";
            RanDbMemClear.UseVisualStyleBackColor = false;
            // 
            // RandomPicker
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(ExitBtn);
            Controls.Add(darkModeBox);
            Controls.Add(fullScreenBox);
            Controls.Add(AppPages);
            Controls.Add(CopyrightLabel);
            Controls.Add(Subtitle);
            Controls.Add(Title);
            Name = "RandomPicker";
            Load += RandomPicker_Load;
            Resize += RandomPicker_Resize;
            AppPages.ResumeLayout(false);
            RanNumPage.ResumeLayout(false);
            RanNumPage.PerformLayout();
            RanDbPage.ResumeLayout(false);
            RanDbPage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Subtitle;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.TabPage RanNumPage;
        private System.Windows.Forms.TabPage RanDbPage;
        private System.Windows.Forms.TextBox RanNumMinBox;
        private System.Windows.Forms.Label RanNumMin;
        private System.Windows.Forms.CheckBox RanNumAddToExcept;
        private System.Windows.Forms.Label RanNumResultDisplayLabel;
        private System.Windows.Forms.TextBox RanNumMaxBox;
        private System.Windows.Forms.Label RanNumMax;
        private System.Windows.Forms.CheckBox RanNumAnimate;
        private System.Windows.Forms.TextBox RanNumNumBox;
        private System.Windows.Forms.Label RanNumNum;
        private System.Windows.Forms.TextBox RanNumExceptBox;
        private System.Windows.Forms.Label RanNumExcept;
        private System.Windows.Forms.Button RanNumHistoryBtn;
        private System.Windows.Forms.Button RanNumGenerateBtn;
        private System.Windows.Forms.CheckBox fullScreenBox;
        private System.Windows.Forms.CheckBox darkModeBox;
        private System.Windows.Forms.Button RanNumDisplayFontSizeApply;
        private System.Windows.Forms.TextBox RanNumDisplayFontSizeBox;
        private System.Windows.Forms.Label RanNumDisplayFontSize;
        private System.Windows.Forms.TextBox RanDbExceptTagBox;
        private System.Windows.Forms.Label RanDbExceptTag;
        private System.Windows.Forms.Button aRanDbDisplayFontSizeApplyBtn;
        private System.Windows.Forms.Button RanDbHistory;
        private System.Windows.Forms.Button RanDbGenerateBtn;
        private System.Windows.Forms.Label RanDbDisplayFontSize;
        private System.Windows.Forms.CheckBox RanDbAnimate;
        private System.Windows.Forms.TextBox RanDbNumBox;
        private System.Windows.Forms.Label RanDbNum;
        private System.Windows.Forms.TextBox RanDbExceptNameBox;
        private System.Windows.Forms.Label RanDbExceptName;
        private System.Windows.Forms.TextBox RanDbDisplayFontSizeBox;
        private System.Windows.Forms.Label RanDbSettingText;
        private System.Windows.Forms.CheckBox RanDbAddToExcept;
        private System.Windows.Forms.Label RanDbDisplay;
        private System.Windows.Forms.Label RanDbResultDisplayText;
        private System.Windows.Forms.Button RanDbSettingBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.CheckBox RanDbEnableWeight;
        public System.Windows.Forms.TabControl AppPages;
        private System.Windows.Forms.Label RanNumDisplay;
        private System.Windows.Forms.Button RanNumMemClear;
        private System.Windows.Forms.Button RanDbMemClear;
    }
}