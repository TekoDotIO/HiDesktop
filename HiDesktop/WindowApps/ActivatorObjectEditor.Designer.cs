namespace Widgets.MVP.WidgetModels
{
    partial class ActivatorObjectEditor
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
            idBox = new System.Windows.Forms.TextBox();
            descriptionBox = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            iconBox = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            actionBox = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            tips = new System.Windows.Forms.Label();
            saveBtn = new System.Windows.Forms.Button();
            cancelBtn = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            iconDisplay = new System.Windows.Forms.PictureBox();
            loadFromDbBtn = new System.Windows.Forms.Button();
            DelWithID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)iconDisplay).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 19.8000011F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(23, 20);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(150, 36);
            label1.TabIndex = 0;
            label1.Text = "项目编辑器";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(30, 65);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(188, 17);
            label2.TabIndex = 1;
            label2.Text = "键入项目属性以编辑数据库内容。";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(30, 107);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(21, 17);
            label3.TabIndex = 2;
            label3.Text = "ID";
            // 
            // idBox
            // 
            idBox.Location = new System.Drawing.Point(65, 105);
            idBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            idBox.Name = "idBox";
            idBox.Size = new System.Drawing.Size(108, 23);
            idBox.TabIndex = 3;
            // 
            // descriptionBox
            // 
            descriptionBox.Location = new System.Drawing.Point(388, 105);
            descriptionBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new System.Drawing.Size(198, 23);
            descriptionBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(353, 107);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(32, 17);
            label4.TabIndex = 4;
            label4.Text = "描述";
            // 
            // iconBox
            // 
            iconBox.Location = new System.Drawing.Point(65, 130);
            iconBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            iconBox.Name = "iconBox";
            iconBox.Size = new System.Drawing.Size(520, 23);
            iconBox.TabIndex = 7;
            iconBox.KeyDown += iconBox_KeyDown;
            iconBox.KeyPress += iconBox_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(30, 133);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(32, 17);
            label5.TabIndex = 6;
            label5.Text = "图标";
            // 
            // actionBox
            // 
            actionBox.Location = new System.Drawing.Point(65, 158);
            actionBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            actionBox.Multiline = true;
            actionBox.Name = "actionBox";
            actionBox.Size = new System.Drawing.Size(522, 134);
            actionBox.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(30, 161);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(32, 17);
            label6.TabIndex = 8;
            label6.Text = "动作";
            // 
            // tips
            // 
            tips.AutoSize = true;
            tips.Location = new System.Drawing.Point(30, 304);
            tips.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            tips.Name = "tips";
            tips.Size = new System.Drawing.Size(80, 17);
            tips.TabIndex = 10;
            tips.Text = "数据库状态：";
            // 
            // saveBtn
            // 
            saveBtn.Location = new System.Drawing.Point(65, 334);
            saveBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new System.Drawing.Size(73, 25);
            saveBtn.TabIndex = 12;
            saveBtn.Text = "保存";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new System.Drawing.Point(142, 334);
            cancelBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new System.Drawing.Size(73, 25);
            cancelBtn.TabIndex = 13;
            cancelBtn.Text = "关闭";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(228, 337);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(309, 17);
            label8.TabIndex = 14;
            label8.Text = "此程序是HiDesktop应用的一部分 - teko.IO 版权所有。";
            // 
            // iconDisplay
            // 
            iconDisplay.Location = new System.Drawing.Point(522, 20);
            iconDisplay.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            iconDisplay.Name = "iconDisplay";
            iconDisplay.Size = new System.Drawing.Size(62, 68);
            iconDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            iconDisplay.TabIndex = 15;
            iconDisplay.TabStop = false;
            // 
            // loadFromDbBtn
            // 
            loadFromDbBtn.Location = new System.Drawing.Point(177, 105);
            loadFromDbBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            loadFromDbBtn.Name = "loadFromDbBtn";
            loadFromDbBtn.Size = new System.Drawing.Size(97, 25);
            loadFromDbBtn.TabIndex = 16;
            loadFromDbBtn.Text = "从数据库加载";
            loadFromDbBtn.UseVisualStyleBackColor = true;
            loadFromDbBtn.Click += loadFromDbBtn_Click;
            // 
            // DelWithID
            // 
            DelWithID.Location = new System.Drawing.Point(278, 105);
            DelWithID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            DelWithID.Name = "DelWithID";
            DelWithID.Size = new System.Drawing.Size(71, 25);
            DelWithID.TabIndex = 17;
            DelWithID.Text = "删除";
            DelWithID.UseVisualStyleBackColor = true;
            DelWithID.Click += DelWithID_Click;
            // 
            // ActivatorObjectEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(622, 382);
            Controls.Add(DelWithID);
            Controls.Add(loadFromDbBtn);
            Controls.Add(iconDisplay);
            Controls.Add(label8);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(tips);
            Controls.Add(actionBox);
            Controls.Add(label6);
            Controls.Add(iconBox);
            Controls.Add(label5);
            Controls.Add(descriptionBox);
            Controls.Add(label4);
            Controls.Add(idBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "ActivatorObjectEditor";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "HiDesktop  - Activator Object Editor";
            FormClosing += ActivatorObjectEditor_FormClosing;
            Load += ActivatorObjectEditor_Load;
            ((System.ComponentModel.ISupportInitialize)iconDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox iconBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox actionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label tips;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox iconDisplay;
        private System.Windows.Forms.Button loadFromDbBtn;
        private System.Windows.Forms.Button DelWithID;
    }
}