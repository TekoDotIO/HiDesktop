namespace Widgets.MVP.WindowApps
{
    partial class RandomItemsDbHelper
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
            label3 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            label4 = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(28, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(220, 39);
            label1.TabIndex = 0;
            label1.Text = "数据库连接选项";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(33, 67);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(314, 21);
            label2.TabIndex = 1;
            label2.Text = "选择一个随机项数据库，然后编辑或启用。";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(33, 122);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(68, 17);
            label3.TabIndex = 2;
            label3.Text = "数据库路径";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(107, 119);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(588, 23);
            textBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(469, 533);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(226, 23);
            button1.TabIndex = 4;
            button1.Text = "应用数据库到随机选取程序";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(33, 148);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(105, 23);
            button2.TabIndex = 5;
            button2.Text = "测试连接";
            button2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(33, 177);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(662, 350);
            tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Location = new System.Drawing.Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(654, 320);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "数据预览";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new System.Drawing.Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(654, 320);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "快速编辑";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(144, 151);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(409, 17);
            label4.TabIndex = 7;
            label4.Text = "尚未连接到数据库！输入绝对/相对路径或拖放文件至输入框以读取数据库。";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(457, 70);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(225, 21);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "启用Excel模拟数据库，而不是SQLite";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // RandomItemsDbHelper
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(723, 568);
            Controls.Add(checkBox1);
            Controls.Add(label4);
            Controls.Add(tabControl1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "RandomItemsDbHelper";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "随机选取器 - 数据库助手";
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}