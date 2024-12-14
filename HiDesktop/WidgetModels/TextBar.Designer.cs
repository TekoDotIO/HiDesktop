namespace HiDesktop
{
    partial class TextBar
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
            Menu = new System.Windows.Forms.ContextMenuStrip(components);
            ReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            LabelNo1.Text = "Text";
            LabelNo1.MouseDoubleClick += OnMouseUp;
            LabelNo1.MouseDown += OnMouseDown;
            LabelNo1.MouseUp += OnMouseUp;
            // 
            // Menu
            // 
            Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ReloadToolStripMenuItem, SaveLocToolStripMenuItem, openPropertiesFileToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new System.Drawing.Size(181, 92);
            // 
            // ReloadToolStripMenuItem
            // 
            ReloadToolStripMenuItem.Name = "ReloadToolStripMenuItem";
            ReloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ReloadToolStripMenuItem.Text = "重新加载小组件";
            ReloadToolStripMenuItem.Click += ReloadToolStripMenuItem_Click;
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
            // TextBar
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(70, 41);
            ControlBox = false;
            Controls.Add(LabelNo1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ImeMode = System.Windows.Forms.ImeMode.Disable;
            Name = "TextBar";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "TextBar";
            Load += TextBar_Load;
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelNo1;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem ReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveLocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPropertiesFileToolStripMenuItem;
    }
}