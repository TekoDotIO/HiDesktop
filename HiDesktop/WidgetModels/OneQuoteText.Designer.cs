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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            label1.Location = new System.Drawing.Point(36, 78);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(432, 86);
            label1.TabIndex = 0;
            label1.Text = "只是那生命的大雨纷飞，总不济于这片虚幻到不真实的万里晴空就是了。";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.FromArgb(119, 140, 204);
            label2.Location = new System.Drawing.Point(36, 188);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(432, 23);
            label2.TabIndex = 1;
            label2.Text = "- 幻愿Recovery -";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OneQuoteText
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(194, 215, 243);
            ClientSize = new System.Drawing.Size(500, 250);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "OneQuoteText";
            Text = "Hi一言";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}