namespace Widgets.MVP.WidgetModels
{
    partial class OneQuoteText
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
            QuoteText = new System.Windows.Forms.Label();
            Menu = new System.Windows.Forms.ContextMenuStrip(components);
            ReloadOneQuoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            OneQuoteControlMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ChangeTextManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SkipTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ChangeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            AuthorText = new System.Windows.Forms.Label();
            savePositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // QuoteText
            // 
            QuoteText.ContextMenuStrip = Menu;
            QuoteText.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            QuoteText.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            QuoteText.Location = new System.Drawing.Point(36, 49);
            QuoteText.Name = "QuoteText";
            QuoteText.Size = new System.Drawing.Size(432, 159);
            QuoteText.TabIndex = 0;
            QuoteText.Text = "正在从数据源加载数据...";
            QuoteText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            QuoteText.MouseDown += QuoteText_MouseDown;
            QuoteText.MouseUp += QuoteText_MouseUp;
            // 
            // Menu
            // 
            Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ReloadOneQuoteToolStripMenuItem, OneQuoteControlMenuToolStripMenuItem, savePositionToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new System.Drawing.Size(181, 92);
            Menu.Opening += Menu_Opening;
            // 
            // ReloadOneQuoteToolStripMenuItem
            // 
            ReloadOneQuoteToolStripMenuItem.Name = "ReloadOneQuoteToolStripMenuItem";
            ReloadOneQuoteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ReloadOneQuoteToolStripMenuItem.Text = "重新加载小组件";
            ReloadOneQuoteToolStripMenuItem.Click += ReloadOneQuoteToolStripMenuItem_Click;
            // 
            // OneQuoteControlMenuToolStripMenuItem
            // 
            OneQuoteControlMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ChangeTextManualToolStripMenuItem, SkipTextToolStripMenuItem, ChangeColorToolStripMenuItem });
            OneQuoteControlMenuToolStripMenuItem.Name = "OneQuoteControlMenuToolStripMenuItem";
            OneQuoteControlMenuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            OneQuoteControlMenuToolStripMenuItem.Text = "控制选项";
            // 
            // ChangeTextManualToolStripMenuItem
            // 
            ChangeTextManualToolStripMenuItem.Name = "ChangeTextManualToolStripMenuItem";
            ChangeTextManualToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ChangeTextManualToolStripMenuItem.Text = "换一条（临时）";
            ChangeTextManualToolStripMenuItem.Click += ChangeTextManualToolStripMenuItem_Click;
            // 
            // SkipTextToolStripMenuItem
            // 
            SkipTextToolStripMenuItem.Name = "SkipTextToolStripMenuItem";
            SkipTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            SkipTextToolStripMenuItem.Text = "跳过当前条目";
            SkipTextToolStripMenuItem.Click += SkipTextToolStripMenuItem_Click;
            // 
            // ChangeColorToolStripMenuItem
            // 
            ChangeColorToolStripMenuItem.Name = "ChangeColorToolStripMenuItem";
            ChangeColorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            ChangeColorToolStripMenuItem.Text = "换个颜色";
            ChangeColorToolStripMenuItem.Click += ChangeColorToolStripMenuItem_Click;
            // 
            // AuthorText
            // 
            AuthorText.BackColor = System.Drawing.Color.Transparent;
            AuthorText.ContextMenuStrip = Menu;
            AuthorText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            AuthorText.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            AuthorText.Location = new System.Drawing.Point(36, 208);
            AuthorText.Name = "AuthorText";
            AuthorText.Size = new System.Drawing.Size(432, 23);
            AuthorText.TabIndex = 1;
            AuthorText.Text = "- HiDesktop 一言小组件 -";
            AuthorText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            AuthorText.MouseDown += AuthorText_MouseDown;
            AuthorText.MouseUp += AuthorText_MouseUp;
            // 
            // savePositionToolStripMenuItem
            // 
            savePositionToolStripMenuItem.Name = "savePositionToolStripMenuItem";
            savePositionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            savePositionToolStripMenuItem.Text = "保存位置";
            savePositionToolStripMenuItem.Click += SavePositionToolStripMenuItem_Click;
            // 
            // OneQuoteText
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(194, 215, 243);
            ClientSize = new System.Drawing.Size(500, 250);
            ContextMenuStrip = Menu;
            Controls.Add(AuthorText);
            Controls.Add(QuoteText);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "OneQuoteText";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Hi一言";
            Load += OneQuoteText_Load;
            Resize += OneQuoteText_Resize;
            Menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label QuoteText;
        private System.Windows.Forms.Label AuthorText;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem ReloadOneQuoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OneQuoteControlMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeTextManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SkipTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePositionToolStripMenuItem;
    }
}