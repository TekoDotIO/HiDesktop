﻿namespace Widgets.MVP.WidgetModels
{
    partial class CountdownV2
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
            components = new System.ComponentModel.Container();
            EventLabel = new System.Windows.Forms.Label();
            FrontTipLabel = new System.Windows.Forms.Label();
            DayDisplay = new System.Windows.Forms.Label();
            DayLabel = new System.Windows.Forms.Label();
            HourDisplay = new System.Windows.Forms.Label();
            HourLabel = new System.Windows.Forms.Label();
            MinDisplay = new System.Windows.Forms.Label();
            MinLabel = new System.Windows.Forms.Label();
            SecDisplay = new System.Windows.Forms.Label();
            Menu = new System.Windows.Forms.ContextMenuStrip(components);
            SaveLocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openPropertiesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // EventLabel
            // 
            EventLabel.AutoSize = true;
            EventLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 30F, System.Drawing.FontStyle.Bold);
            EventLabel.ForeColor = System.Drawing.Color.FromArgb(250, 173, 172);
            EventLabel.Location = new System.Drawing.Point(34, 80);
            EventLabel.Name = "EventLabel";
            EventLabel.Size = new System.Drawing.Size(307, 52);
            EventLabel.TabIndex = 0;
            EventLabel.Text = "ADULTHOOD!";
            // 
            // FrontTipLabel
            // 
            FrontTipLabel.AutoSize = true;
            FrontTipLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold);
            FrontTipLabel.ForeColor = System.Drawing.Color.Black;
            FrontTipLabel.Location = new System.Drawing.Point(38, 44);
            FrontTipLabel.Name = "FrontTipLabel";
            FrontTipLabel.Size = new System.Drawing.Size(52, 27);
            FrontTipLabel.TabIndex = 1;
            FrontTipLabel.Text = "进入";
            // 
            // DayDisplay
            // 
            DayDisplay.AutoSize = true;
            DayDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 60F, System.Drawing.FontStyle.Bold);
            DayDisplay.ForeColor = System.Drawing.Color.Black;
            DayDisplay.Location = new System.Drawing.Point(21, 144);
            DayDisplay.Name = "DayDisplay";
            DayDisplay.Size = new System.Drawing.Size(94, 106);
            DayDisplay.TabIndex = 2;
            DayDisplay.Text = "1";
            // 
            // DayLabel
            // 
            DayLabel.AutoSize = true;
            DayLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Bold);
            DayLabel.ForeColor = System.Drawing.Color.Black;
            DayLabel.Location = new System.Drawing.Point(82, 160);
            DayLabel.Name = "DayLabel";
            DayLabel.Size = new System.Drawing.Size(33, 36);
            DayLabel.TabIndex = 3;
            DayLabel.Text = "d";
            // 
            // HourDisplay
            // 
            HourDisplay.AutoSize = true;
            HourDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 60F, System.Drawing.FontStyle.Bold);
            HourDisplay.ForeColor = System.Drawing.Color.Black;
            HourDisplay.Location = new System.Drawing.Point(134, 144);
            HourDisplay.Name = "HourDisplay";
            HourDisplay.Size = new System.Drawing.Size(143, 106);
            HourDisplay.TabIndex = 4;
            HourDisplay.Text = "01";
            // 
            // HourLabel
            // 
            HourLabel.AutoSize = true;
            HourLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Bold);
            HourLabel.ForeColor = System.Drawing.Color.Black;
            HourLabel.Location = new System.Drawing.Point(244, 160);
            HourLabel.Name = "HourLabel";
            HourLabel.Size = new System.Drawing.Size(43, 36);
            HourLabel.TabIndex = 5;
            HourLabel.Text = "hr";
            // 
            // MinDisplay
            // 
            MinDisplay.AutoSize = true;
            MinDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 30F);
            MinDisplay.ForeColor = System.Drawing.Color.DimGray;
            MinDisplay.Location = new System.Drawing.Point(298, 186);
            MinDisplay.Name = "MinDisplay";
            MinDisplay.Size = new System.Drawing.Size(68, 52);
            MinDisplay.TabIndex = 6;
            MinDisplay.Text = "00";
            // 
            // MinLabel
            // 
            MinLabel.AutoSize = true;
            MinLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            MinLabel.ForeColor = System.Drawing.Color.DimGray;
            MinLabel.Location = new System.Drawing.Point(359, 193);
            MinLabel.Name = "MinLabel";
            MinLabel.Size = new System.Drawing.Size(36, 19);
            MinLabel.TabIndex = 7;
            MinLabel.Text = "min";
            // 
            // SecDisplay
            // 
            SecDisplay.AutoSize = true;
            SecDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            SecDisplay.ForeColor = System.Drawing.Color.DimGray;
            SecDisplay.Location = new System.Drawing.Point(358, 213);
            SecDisplay.Name = "SecDisplay";
            SecDisplay.Size = new System.Drawing.Size(27, 19);
            SecDisplay.TabIndex = 8;
            SecDisplay.Text = "00";
            // 
            // Menu
            // 
            Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { SaveLocToolStripMenuItem, ReloadToolStripMenuItem, openPropertiesFileToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new System.Drawing.Size(181, 92);
            // 
            // SaveLocToolStripMenuItem
            // 
            SaveLocToolStripMenuItem.Name = "SaveLocToolStripMenuItem";
            SaveLocToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            SaveLocToolStripMenuItem.Text = "保存当前位置";
            SaveLocToolStripMenuItem.Click += SaveLocToolStripMenuItem_Click;
            // 
            // ReloadToolStripMenuItem
            // 
            ReloadToolStripMenuItem.Name = "ReloadToolStripMenuItem";
            ReloadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            ReloadToolStripMenuItem.Text = "重新加载小组件";
            ReloadToolStripMenuItem.Click += ReloadToolStripMenuItem_Click;
            // 
            // openPropertiesFileToolStripMenuItem
            // 
            openPropertiesFileToolStripMenuItem.Name = "openPropertiesFileToolStripMenuItem";
            openPropertiesFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            openPropertiesFileToolStripMenuItem.Text = "打开配置文件";
            openPropertiesFileToolStripMenuItem.Click += openPropertiesFileToolStripMenuItem_Click;
            // 
            // CountdownV2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(252, 223, 229);
            ClientSize = new System.Drawing.Size(432, 304);
            Controls.Add(SecDisplay);
            Controls.Add(MinLabel);
            Controls.Add(MinDisplay);
            Controls.Add(HourLabel);
            Controls.Add(HourDisplay);
            Controls.Add(DayLabel);
            Controls.Add(DayDisplay);
            Controls.Add(FrontTipLabel);
            Controls.Add(EventLabel);
            ForeColor = System.Drawing.Color.FromArgb(250, 173, 172);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "CountdownV2";
            Text = "CountdownV2";
            Load += CountdownV2_Load;
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label EventLabel;
        private System.Windows.Forms.Label FrontTipLabel;
        private System.Windows.Forms.Label DayDisplay;
        private System.Windows.Forms.Label DayLabel;
        private System.Windows.Forms.Label HourDisplay;
        private System.Windows.Forms.Label HourLabel;
        private System.Windows.Forms.Label MinDisplay;
        private System.Windows.Forms.Label MinLabel;
        private System.Windows.Forms.Label SecDisplay;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem SaveLocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPropertiesFileToolStripMenuItem;
    }
}