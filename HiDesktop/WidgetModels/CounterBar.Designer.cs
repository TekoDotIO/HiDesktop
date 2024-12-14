namespace HiDesktop
{
    partial class CounterBar
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
            LabelNo1 = new System.Windows.Forms.Label();
            EventText = new System.Windows.Forms.Label();
            LabelNo2 = new System.Windows.Forms.Label();
            NumText = new System.Windows.Forms.Label();
            Menu = new System.Windows.Forms.ContextMenuStrip(components);
            ReloadWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SaveLocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openPropertiesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // LabelNo1
            // 
            LabelNo1.AutoSize = true;
            LabelNo1.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            LabelNo1.Location = new System.Drawing.Point(0, 0);
            LabelNo1.Margin = new System.Windows.Forms.Padding(3);
            LabelNo1.Name = "LabelNo1";
            LabelNo1.Size = new System.Drawing.Size(69, 35);
            LabelNo1.TabIndex = 0;
            LabelNo1.Text = "距离";
            LabelNo1.Click += LabelNo1_Click;
            LabelNo1.MouseDoubleClick += OnMouseUp;
            LabelNo1.MouseDown += OnMouseDown;
            LabelNo1.MouseUp += OnMouseUp;
            LabelNo1.Move += ChangeLocation;
            // 
            // EventText
            // 
            EventText.AutoSize = true;
            EventText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            EventText.Location = new System.Drawing.Point(75, 0);
            EventText.Margin = new System.Windows.Forms.Padding(3);
            EventText.Name = "EventText";
            EventText.Size = new System.Drawing.Size(86, 35);
            EventText.TabIndex = 1;
            EventText.Text = "Event";
            EventText.Click += Label1_Click;
            EventText.MouseDown += OnMouseDown;
            EventText.MouseUp += OnMouseUp;
            // 
            // LabelNo2
            // 
            LabelNo2.AutoSize = true;
            LabelNo2.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            LabelNo2.Location = new System.Drawing.Point(150, 0);
            LabelNo2.Margin = new System.Windows.Forms.Padding(3);
            LabelNo2.Name = "LabelNo2";
            LabelNo2.Size = new System.Drawing.Size(69, 35);
            LabelNo2.TabIndex = 2;
            LabelNo2.Text = "还有";
            LabelNo2.MouseDown += OnMouseDown;
            LabelNo2.MouseUp += OnMouseUp;
            // 
            // NumText
            // 
            NumText.AutoSize = true;
            NumText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F);
            NumText.Location = new System.Drawing.Point(225, 0);
            NumText.Margin = new System.Windows.Forms.Padding(3);
            NumText.Name = "NumText";
            NumText.Size = new System.Drawing.Size(74, 35);
            NumText.TabIndex = 3;
            NumText.Text = "num";
            NumText.Click += NumText_Click;
            NumText.MouseDown += OnMouseDown;
            NumText.MouseUp += OnMouseUp;
            // 
            // Menu
            // 
            Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ReloadWidgetToolStripMenuItem, SaveLocToolStripMenuItem, openPropertiesFileToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new System.Drawing.Size(181, 92);
            // 
            // ReloadWidgetToolStripMenuItem
            // 
            ReloadWidgetToolStripMenuItem.Name = "ReloadWidgetToolStripMenuItem";
            ReloadWidgetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ReloadWidgetToolStripMenuItem.Text = "重新加载小组件";
            ReloadWidgetToolStripMenuItem.Click += ReloadWidgetToolStripMenuItem_Click;
            // 
            // SaveLocToolStripMenuItem
            // 
            SaveLocToolStripMenuItem.Name = "SaveLocToolStripMenuItem";
            SaveLocToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            SaveLocToolStripMenuItem.Text = "保存当前位置";
            SaveLocToolStripMenuItem.Click += SaveLocToolStripMenuItem_Click;
            // 
            // openPropertiesFileToolStripMenuItem
            // 
            openPropertiesFileToolStripMenuItem.Name = "openPropertiesFileToolStripMenuItem";
            openPropertiesFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            openPropertiesFileToolStripMenuItem.Text = "打开配置文件";
            openPropertiesFileToolStripMenuItem.Click += openPropertiesFileToolStripMenuItem_Click;
            // 
            // CounterBar
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(584, 41);
            ControlBox = false;
            Controls.Add(NumText);
            Controls.Add(LabelNo2);
            Controls.Add(EventText);
            Controls.Add(LabelNo1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ImeMode = System.Windows.Forms.ImeMode.Disable;
            Name = "CounterBar";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "TextBar";
            Activated += CounterBar_Activated;
            Load += CountBar_Load;
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelNo1;
        private System.Windows.Forms.Label EventText;
        private System.Windows.Forms.Label LabelNo2;
        private System.Windows.Forms.Label NumText;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem ReloadWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveLocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPropertiesFileToolStripMenuItem;
    }
}