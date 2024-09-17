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
            QuoteText = new System.Windows.Forms.Label();
            AuthorText = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // QuoteText
            // 
            QuoteText.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            QuoteText.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            QuoteText.Location = new System.Drawing.Point(36, 49);
            QuoteText.Name = "QuoteText";
            QuoteText.Size = new System.Drawing.Size(432, 159);
            QuoteText.TabIndex = 0;
            QuoteText.Text = "只是那生命的大雨纷飞，​总不济于这片虚幻到不真实的万里晴空就是了。";
            QuoteText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            QuoteText.MouseDown += QuoteText_MouseDown;
            QuoteText.MouseUp += QuoteText_MouseUp;
            // 
            // AuthorText
            // 
            AuthorText.BackColor = System.Drawing.Color.Transparent;
            AuthorText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            AuthorText.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            AuthorText.Location = new System.Drawing.Point(36, 208);
            AuthorText.Name = "AuthorText";
            AuthorText.Size = new System.Drawing.Size(432, 23);
            AuthorText.TabIndex = 1;
            AuthorText.Text = "- 幻愿Recovery -";
            AuthorText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            AuthorText.MouseDown += AuthorText_MouseDown;
            AuthorText.MouseUp += AuthorText_MouseUp;
            // 
            // OneQuoteText
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(194, 215, 243);
            ClientSize = new System.Drawing.Size(500, 250);
            Controls.Add(AuthorText);
            Controls.Add(QuoteText);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "OneQuoteText";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Hi一言";
            Load += OneQuoteText_Load;
            Resize += OneQuoteText_Resize;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label QuoteText;
        private System.Windows.Forms.Label AuthorText;
    }
}