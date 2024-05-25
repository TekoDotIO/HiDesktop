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
            this.LabelNo1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelNo1
            // 
            this.LabelNo1.AutoSize = true;
            this.LabelNo1.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelNo1.Location = new System.Drawing.Point(0, 0);
            this.LabelNo1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LabelNo1.Name = "LabelNo1";
            this.LabelNo1.Size = new System.Drawing.Size(69, 35);
            this.LabelNo1.TabIndex = 0;
            this.LabelNo1.Text = "Text";
            this.LabelNo1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.LabelNo1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.LabelNo1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // TextBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(100, 48);
            this.ControlBox = false;
            this.Controls.Add(this.LabelNo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TextBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TextBar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelNo1;
    }
}