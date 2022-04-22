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
            this.LabelNo1 = new System.Windows.Forms.Label();
            this.EventText = new System.Windows.Forms.Label();
            this.LabelNo2 = new System.Windows.Forms.Label();
            this.NumText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelNo1
            // 
            this.LabelNo1.AutoSize = true;
            this.LabelNo1.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelNo1.Location = new System.Drawing.Point(0, 0);
            this.LabelNo1.Margin = new System.Windows.Forms.Padding(3);
            this.LabelNo1.Name = "LabelNo1";
            this.LabelNo1.Size = new System.Drawing.Size(69, 35);
            this.LabelNo1.TabIndex = 0;
            this.LabelNo1.Text = "距离";
            this.LabelNo1.Click += new System.EventHandler(this.LabelNo1_Click);
            this.LabelNo1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.LabelNo1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.LabelNo1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.LabelNo1.Move += new System.EventHandler(this.ChangeLocation);
            // 
            // EventText
            // 
            this.EventText.AutoSize = true;
            this.EventText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EventText.Location = new System.Drawing.Point(75, 0);
            this.EventText.Margin = new System.Windows.Forms.Padding(3);
            this.EventText.Name = "EventText";
            this.EventText.Size = new System.Drawing.Size(86, 35);
            this.EventText.TabIndex = 1;
            this.EventText.Text = "Event";
            this.EventText.Click += new System.EventHandler(this.Label1_Click);
            this.EventText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.EventText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // LabelNo2
            // 
            this.LabelNo2.AutoSize = true;
            this.LabelNo2.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelNo2.Location = new System.Drawing.Point(150, 0);
            this.LabelNo2.Margin = new System.Windows.Forms.Padding(3);
            this.LabelNo2.Name = "LabelNo2";
            this.LabelNo2.Size = new System.Drawing.Size(69, 35);
            this.LabelNo2.TabIndex = 2;
            this.LabelNo2.Text = "还有";
            this.LabelNo2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.LabelNo2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // NumText
            // 
            this.NumText.AutoSize = true;
            this.NumText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumText.Location = new System.Drawing.Point(225, 0);
            this.NumText.Margin = new System.Windows.Forms.Padding(3);
            this.NumText.Name = "NumText";
            this.NumText.Size = new System.Drawing.Size(74, 35);
            this.NumText.TabIndex = 3;
            this.NumText.Text = "num";
            this.NumText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.NumText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // TextBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 41);
            this.ControlBox = false;
            this.Controls.Add(this.NumText);
            this.Controls.Add(this.LabelNo2);
            this.Controls.Add(this.EventText);
            this.Controls.Add(this.LabelNo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "TextBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TextBar";
            this.TopMost = false;
            this.Load += new System.EventHandler(this.TextBar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelNo1;
        private System.Windows.Forms.Label EventText;
        private System.Windows.Forms.Label LabelNo2;
        private System.Windows.Forms.Label NumText;
    }
}