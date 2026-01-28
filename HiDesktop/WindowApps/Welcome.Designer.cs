namespace Widgets.MVP
{
    partial class Welcome
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
            materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            label2 = new System.Windows.Forms.Label();
            AgreedCB = new ReaLTaiizor.Controls.MaterialCheckBox();
            StartConfigBtn = new ReaLTaiizor.Controls.MaterialButton();
            label1 = new System.Windows.Forms.Label();
            WelcomeTitle = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            materialTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            materialTabControl1.Controls.Add(tabPage1);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Depth = 0;
            materialTabControl1.Location = new System.Drawing.Point(6, 67);
            materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new System.Drawing.Size(788, 377);
            materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(AgreedCB);
            tabPage1.Controls.Add(StartConfigBtn);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(WelcomeTitle);
            tabPage1.Location = new System.Drawing.Point(4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(780, 347);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label2.Location = new System.Drawing.Point(229, 207);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(360, 19);
            label2.TabIndex = 4;
            label2.Text = "我已知悉此软件为开源软件，从未售卖，不得用于非法用途。";
            // 
            // AgreedCB
            // 
            AgreedCB.AutoSize = true;
            AgreedCB.Depth = 0;
            AgreedCB.Location = new System.Drawing.Point(198, 199);
            AgreedCB.Margin = new System.Windows.Forms.Padding(0);
            AgreedCB.MouseLocation = new System.Drawing.Point(-1, -1);
            AgreedCB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            AgreedCB.Name = "AgreedCB";
            AgreedCB.ReadOnly = false;
            AgreedCB.Ripple = true;
            AgreedCB.Size = new System.Drawing.Size(35, 37);
            AgreedCB.TabIndex = 3;
            AgreedCB.UseAccentColor = false;
            AgreedCB.UseVisualStyleBackColor = true;
            AgreedCB.CheckedChanged += materialCheckBox1_CheckedChanged;
            // 
            // StartConfigBtn
            // 
            StartConfigBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            StartConfigBtn.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            StartConfigBtn.Depth = 0;
            StartConfigBtn.Enabled = false;
            StartConfigBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            StartConfigBtn.HighEmphasis = true;
            StartConfigBtn.Icon = null;
            StartConfigBtn.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            StartConfigBtn.Location = new System.Drawing.Point(61, 198);
            StartConfigBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            StartConfigBtn.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            StartConfigBtn.Name = "StartConfigBtn";
            StartConfigBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            StartConfigBtn.Size = new System.Drawing.Size(117, 36);
            StartConfigBtn.TabIndex = 2;
            StartConfigBtn.Text = "让我们开始吧";
            StartConfigBtn.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            StartConfigBtn.UseAccentColor = false;
            StartConfigBtn.UseVisualStyleBackColor = true;
            StartConfigBtn.Click += StartConfigBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label1.Location = new System.Drawing.Point(61, 163);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(334, 19);
            label1.TabIndex = 1;
            label1.Text = "在准备完成后，我们随时可以开始配置您的桌面小组件。";
            // 
            // WelcomeTitle
            // 
            WelcomeTitle.AutoSize = true;
            WelcomeTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            WelcomeTitle.Location = new System.Drawing.Point(58, 119);
            WelcomeTitle.Name = "WelcomeTitle";
            WelcomeTitle.Size = new System.Drawing.Size(152, 36);
            WelcomeTitle.TabIndex = 0;
            WelcomeTitle.Text = "Welcome!";
            // 
            // tabPage2
            // 
            tabPage2.Location = new System.Drawing.Point(4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(780, 347);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Welcome
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(materialTabControl1);
            Name = "Welcome";
            Text = "HiDesktop Welcome";
            materialTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label WelcomeTitle;
        private ReaLTaiizor.Controls.MaterialButton StartConfigBtn;
        private ReaLTaiizor.Controls.MaterialCheckBox AgreedCB;
        private System.Windows.Forms.Label label2;
    }
}