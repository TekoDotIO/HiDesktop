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
            LabelNo1 = new System.Windows.Forms.Label();
            EventText = new System.Windows.Forms.Label();
            LabelNo2 = new System.Windows.Forms.Label();
            NumText = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // LabelNo1
            // 
            LabelNo1.AutoSize = true;
            LabelNo1.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LabelNo1.Location = new System.Drawing.Point(0, 0);
            LabelNo1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            LabelNo1.Name = "LabelNo1";
            LabelNo1.Size = new System.Drawing.Size(88, 45);
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
            EventText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            EventText.Location = new System.Drawing.Point(96, 0);
            EventText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            EventText.Name = "EventText";
            EventText.Size = new System.Drawing.Size(110, 45);
            EventText.TabIndex = 1;
            EventText.Text = "Event";
            EventText.Click += Label1_Click;
            EventText.MouseDown += OnMouseDown;
            EventText.MouseUp += OnMouseUp;
            // 
            // LabelNo2
            // 
            LabelNo2.AutoSize = true;
            LabelNo2.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LabelNo2.Location = new System.Drawing.Point(193, 0);
            LabelNo2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            LabelNo2.Name = "LabelNo2";
            LabelNo2.Size = new System.Drawing.Size(88, 45);
            LabelNo2.TabIndex = 2;
            LabelNo2.Text = "还有";
            LabelNo2.MouseDown += OnMouseDown;
            LabelNo2.MouseUp += OnMouseUp;
            // 
            // NumText
            // 
            NumText.AutoSize = true;
            NumText.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            NumText.Location = new System.Drawing.Point(289, 0);
            NumText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            NumText.Name = "NumText";
            NumText.Size = new System.Drawing.Size(94, 45);
            NumText.TabIndex = 3;
            NumText.Text = "num";
            NumText.MouseDown += OnMouseDown;
            NumText.MouseUp += OnMouseUp;
            // 
            // CounterBar
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(751, 48);
            ControlBox = false;
            Controls.Add(NumText);
            Controls.Add(LabelNo2);
            Controls.Add(EventText);
            Controls.Add(LabelNo1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ImeMode = System.Windows.Forms.ImeMode.Disable;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "CounterBar";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "TextBar";
            Activated += CounterBar_Activated;
            Load += TextBar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelNo1;
        private System.Windows.Forms.Label EventText;
        private System.Windows.Forms.Label LabelNo2;
        private System.Windows.Forms.Label NumText;
    }
}