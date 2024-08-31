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
            Title = new System.Windows.Forms.Label();
            Subtitle = new System.Windows.Forms.Label();
            PathLabel = new System.Windows.Forms.Label();
            PathBox = new System.Windows.Forms.TextBox();
            ApplyBtn = new System.Windows.Forms.Button();
            TestConnectBtn = new System.Windows.Forms.Button();
            DataTabs = new System.Windows.Forms.TabControl();
            DataPreviewPage = new System.Windows.Forms.TabPage();
            PreviewBox = new System.Windows.Forms.DataGridView();
            EditorPage = new System.Windows.Forms.TabPage();
            EditorSaveBtn = new System.Windows.Forms.Button();
            EditorReadByNameBtn = new System.Windows.Forms.Button();
            EditorReadByIDBtn = new System.Windows.Forms.Button();
            EditorTipsLabel = new System.Windows.Forms.Label();
            EditorPoolWeight = new System.Windows.Forms.TextBox();
            EditorPoolWeightLabel = new System.Windows.Forms.Label();
            EditorTags = new System.Windows.Forms.TextBox();
            EditorTagsLabel = new System.Windows.Forms.Label();
            EditorName = new System.Windows.Forms.TextBox();
            EditorNameLabel = new System.Windows.Forms.Label();
            EditorID = new System.Windows.Forms.TextBox();
            EditorIDLabel = new System.Windows.Forms.Label();
            TipLabel = new System.Windows.Forms.Label();
            UseExcelBox = new System.Windows.Forms.CheckBox();
            CreateBtn = new System.Windows.Forms.Button();
            SetAsDefaultDbBox = new System.Windows.Forms.CheckBox();
            DataTabs.SuspendLayout();
            DataPreviewPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PreviewBox).BeginInit();
            EditorPage.SuspendLayout();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Title.Location = new System.Drawing.Point(28, 24);
            Title.Name = "Title";
            Title.Size = new System.Drawing.Size(220, 39);
            Title.TabIndex = 0;
            Title.Text = "数据库连接选项";
            // 
            // Subtitle
            // 
            Subtitle.AutoSize = true;
            Subtitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Subtitle.Location = new System.Drawing.Point(33, 67);
            Subtitle.Name = "Subtitle";
            Subtitle.Size = new System.Drawing.Size(314, 21);
            Subtitle.TabIndex = 1;
            Subtitle.Text = "选择一个随机项数据库，然后编辑或启用。";
            // 
            // PathLabel
            // 
            PathLabel.AutoSize = true;
            PathLabel.Location = new System.Drawing.Point(33, 122);
            PathLabel.Name = "PathLabel";
            PathLabel.Size = new System.Drawing.Size(68, 17);
            PathLabel.TabIndex = 2;
            PathLabel.Text = "数据库路径";
            // 
            // PathBox
            // 
            PathBox.Location = new System.Drawing.Point(107, 119);
            PathBox.Name = "PathBox";
            PathBox.Size = new System.Drawing.Size(588, 23);
            PathBox.TabIndex = 3;
            // 
            // ApplyBtn
            // 
            ApplyBtn.Location = new System.Drawing.Point(469, 533);
            ApplyBtn.Name = "ApplyBtn";
            ApplyBtn.Size = new System.Drawing.Size(226, 23);
            ApplyBtn.TabIndex = 4;
            ApplyBtn.Text = "应用数据库到随机选取程序";
            ApplyBtn.UseVisualStyleBackColor = true;
            ApplyBtn.Click += ApplyBtn_Click;
            // 
            // TestConnectBtn
            // 
            TestConnectBtn.Location = new System.Drawing.Point(33, 148);
            TestConnectBtn.Name = "TestConnectBtn";
            TestConnectBtn.Size = new System.Drawing.Size(105, 23);
            TestConnectBtn.TabIndex = 5;
            TestConnectBtn.Text = "测试连接";
            TestConnectBtn.UseVisualStyleBackColor = true;
            TestConnectBtn.Click += TestConnectBtn_Click;
            // 
            // DataTabs
            // 
            DataTabs.Controls.Add(DataPreviewPage);
            DataTabs.Controls.Add(EditorPage);
            DataTabs.Location = new System.Drawing.Point(33, 177);
            DataTabs.Name = "DataTabs";
            DataTabs.SelectedIndex = 0;
            DataTabs.Size = new System.Drawing.Size(662, 350);
            DataTabs.TabIndex = 6;
            // 
            // DataPreviewPage
            // 
            DataPreviewPage.Controls.Add(PreviewBox);
            DataPreviewPage.Location = new System.Drawing.Point(4, 26);
            DataPreviewPage.Name = "DataPreviewPage";
            DataPreviewPage.Padding = new System.Windows.Forms.Padding(3);
            DataPreviewPage.Size = new System.Drawing.Size(654, 320);
            DataPreviewPage.TabIndex = 0;
            DataPreviewPage.Text = "数据预览";
            DataPreviewPage.UseVisualStyleBackColor = true;
            // 
            // PreviewBox
            // 
            PreviewBox.AllowUserToAddRows = false;
            PreviewBox.AllowUserToDeleteRows = false;
            PreviewBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PreviewBox.Location = new System.Drawing.Point(0, 0);
            PreviewBox.Name = "PreviewBox";
            PreviewBox.ReadOnly = true;
            PreviewBox.RowTemplate.Height = 25;
            PreviewBox.Size = new System.Drawing.Size(654, 320);
            PreviewBox.TabIndex = 0;
            // 
            // EditorPage
            // 
            EditorPage.Controls.Add(EditorSaveBtn);
            EditorPage.Controls.Add(EditorReadByNameBtn);
            EditorPage.Controls.Add(EditorReadByIDBtn);
            EditorPage.Controls.Add(EditorTipsLabel);
            EditorPage.Controls.Add(EditorPoolWeight);
            EditorPage.Controls.Add(EditorPoolWeightLabel);
            EditorPage.Controls.Add(EditorTags);
            EditorPage.Controls.Add(EditorTagsLabel);
            EditorPage.Controls.Add(EditorName);
            EditorPage.Controls.Add(EditorNameLabel);
            EditorPage.Controls.Add(EditorID);
            EditorPage.Controls.Add(EditorIDLabel);
            EditorPage.Location = new System.Drawing.Point(4, 26);
            EditorPage.Name = "EditorPage";
            EditorPage.Padding = new System.Windows.Forms.Padding(3);
            EditorPage.Size = new System.Drawing.Size(654, 320);
            EditorPage.TabIndex = 1;
            EditorPage.Text = "快速编辑";
            EditorPage.UseVisualStyleBackColor = true;
            // 
            // EditorSaveBtn
            // 
            EditorSaveBtn.Location = new System.Drawing.Point(230, 262);
            EditorSaveBtn.Name = "EditorSaveBtn";
            EditorSaveBtn.Size = new System.Drawing.Size(178, 40);
            EditorSaveBtn.TabIndex = 11;
            EditorSaveBtn.Text = "写入更改";
            EditorSaveBtn.UseVisualStyleBackColor = true;
            EditorSaveBtn.Click += EditorSaveBtn_Click;
            // 
            // EditorReadByNameBtn
            // 
            EditorReadByNameBtn.Location = new System.Drawing.Point(300, 52);
            EditorReadByNameBtn.Name = "EditorReadByNameBtn";
            EditorReadByNameBtn.Size = new System.Drawing.Size(163, 23);
            EditorReadByNameBtn.TabIndex = 10;
            EditorReadByNameBtn.Text = "以Name为依据读取";
            EditorReadByNameBtn.UseVisualStyleBackColor = true;
            EditorReadByNameBtn.Click += EditorReadByNameBtn_Click;
            // 
            // EditorReadByIDBtn
            // 
            EditorReadByIDBtn.Location = new System.Drawing.Point(300, 23);
            EditorReadByIDBtn.Name = "EditorReadByIDBtn";
            EditorReadByIDBtn.Size = new System.Drawing.Size(163, 23);
            EditorReadByIDBtn.TabIndex = 9;
            EditorReadByIDBtn.Text = "以ID为依据读取";
            EditorReadByIDBtn.UseVisualStyleBackColor = true;
            EditorReadByIDBtn.Click += EditorReadByIDBtn_Click;
            // 
            // EditorTipsLabel
            // 
            EditorTipsLabel.Location = new System.Drawing.Point(43, 198);
            EditorTipsLabel.Name = "EditorTipsLabel";
            EditorTipsLabel.Size = new System.Drawing.Size(547, 44);
            EditorTipsLabel.TabIndex = 8;
            EditorTipsLabel.Text = "说明：ID - 项编号，用于检索与定位项；Name - 项名称，用于结果展示与检索；Tags - 项标签，用于快速定位和排除某些项；PoolWeight - 卡池概率权重，即抽取概率的倍率。";
            // 
            // EditorPoolWeight
            // 
            EditorPoolWeight.Location = new System.Drawing.Point(179, 157);
            EditorPoolWeight.Name = "EditorPoolWeight";
            EditorPoolWeight.Size = new System.Drawing.Size(100, 23);
            EditorPoolWeight.TabIndex = 7;
            // 
            // EditorPoolWeightLabel
            // 
            EditorPoolWeightLabel.AutoSize = true;
            EditorPoolWeightLabel.Location = new System.Drawing.Point(43, 160);
            EditorPoolWeightLabel.Name = "EditorPoolWeightLabel";
            EditorPoolWeightLabel.Size = new System.Drawing.Size(75, 17);
            EditorPoolWeightLabel.TabIndex = 6;
            EditorPoolWeightLabel.Text = "PoolWeight";
            // 
            // EditorTags
            // 
            EditorTags.Location = new System.Drawing.Point(179, 81);
            EditorTags.Multiline = true;
            EditorTags.Name = "EditorTags";
            EditorTags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            EditorTags.Size = new System.Drawing.Size(411, 70);
            EditorTags.TabIndex = 5;
            // 
            // EditorTagsLabel
            // 
            EditorTagsLabel.AutoSize = true;
            EditorTagsLabel.Location = new System.Drawing.Point(43, 84);
            EditorTagsLabel.Name = "EditorTagsLabel";
            EditorTagsLabel.Size = new System.Drawing.Size(36, 17);
            EditorTagsLabel.TabIndex = 4;
            EditorTagsLabel.Text = "Tags";
            // 
            // EditorName
            // 
            EditorName.Location = new System.Drawing.Point(179, 52);
            EditorName.Name = "EditorName";
            EditorName.Size = new System.Drawing.Size(100, 23);
            EditorName.TabIndex = 3;
            // 
            // EditorNameLabel
            // 
            EditorNameLabel.AutoSize = true;
            EditorNameLabel.Location = new System.Drawing.Point(43, 55);
            EditorNameLabel.Name = "EditorNameLabel";
            EditorNameLabel.Size = new System.Drawing.Size(43, 17);
            EditorNameLabel.TabIndex = 2;
            EditorNameLabel.Text = "Name";
            // 
            // EditorID
            // 
            EditorID.Location = new System.Drawing.Point(179, 23);
            EditorID.Name = "EditorID";
            EditorID.Size = new System.Drawing.Size(100, 23);
            EditorID.TabIndex = 1;
            // 
            // EditorIDLabel
            // 
            EditorIDLabel.AutoSize = true;
            EditorIDLabel.Location = new System.Drawing.Point(43, 26);
            EditorIDLabel.Name = "EditorIDLabel";
            EditorIDLabel.Size = new System.Drawing.Size(21, 17);
            EditorIDLabel.TabIndex = 0;
            EditorIDLabel.Text = "ID";
            // 
            // TipLabel
            // 
            TipLabel.AutoSize = true;
            TipLabel.Location = new System.Drawing.Point(282, 151);
            TipLabel.Name = "TipLabel";
            TipLabel.Size = new System.Drawing.Size(409, 17);
            TipLabel.TabIndex = 7;
            TipLabel.Text = "尚未连接到数据库！输入绝对/相对路径或拖放文件至输入框以读取数据库。";
            // 
            // UseExcelBox
            // 
            UseExcelBox.AutoSize = true;
            UseExcelBox.Checked = true;
            UseExcelBox.CheckState = System.Windows.Forms.CheckState.Checked;
            UseExcelBox.Location = new System.Drawing.Point(457, 70);
            UseExcelBox.Name = "UseExcelBox";
            UseExcelBox.Size = new System.Drawing.Size(225, 21);
            UseExcelBox.TabIndex = 8;
            UseExcelBox.Text = "启用Excel模拟数据库，而不是SQLite";
            UseExcelBox.UseVisualStyleBackColor = true;
            // 
            // CreateBtn
            // 
            CreateBtn.Location = new System.Drawing.Point(143, 148);
            CreateBtn.Name = "CreateBtn";
            CreateBtn.Size = new System.Drawing.Size(133, 23);
            CreateBtn.TabIndex = 9;
            CreateBtn.Text = "在此位置新建";
            CreateBtn.UseVisualStyleBackColor = true;
            CreateBtn.Click += CreateBtn_Click;
            // 
            // SetAsDefaultDbBox
            // 
            SetAsDefaultDbBox.AutoSize = true;
            SetAsDefaultDbBox.Checked = true;
            SetAsDefaultDbBox.CheckState = System.Windows.Forms.CheckState.Checked;
            SetAsDefaultDbBox.Location = new System.Drawing.Point(352, 535);
            SetAsDefaultDbBox.Name = "SetAsDefaultDbBox";
            SetAsDefaultDbBox.Size = new System.Drawing.Size(111, 21);
            SetAsDefaultDbBox.TabIndex = 10;
            SetAsDefaultDbBox.Text = "设为默认数据库";
            SetAsDefaultDbBox.UseVisualStyleBackColor = true;
            // 
            // RandomItemsDbHelper
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(723, 568);
            Controls.Add(SetAsDefaultDbBox);
            Controls.Add(CreateBtn);
            Controls.Add(UseExcelBox);
            Controls.Add(TipLabel);
            Controls.Add(DataTabs);
            Controls.Add(TestConnectBtn);
            Controls.Add(ApplyBtn);
            Controls.Add(PathBox);
            Controls.Add(PathLabel);
            Controls.Add(Subtitle);
            Controls.Add(Title);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "RandomItemsDbHelper";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "随机选取器 - 数据库助手";
            DataTabs.ResumeLayout(false);
            DataPreviewPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PreviewBox).EndInit();
            EditorPage.ResumeLayout(false);
            EditorPage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Subtitle;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Button TestConnectBtn;
        private System.Windows.Forms.TabControl DataTabs;
        private System.Windows.Forms.TabPage DataPreviewPage;
        private System.Windows.Forms.TabPage EditorPage;
        private System.Windows.Forms.Label TipLabel;
        private System.Windows.Forms.CheckBox UseExcelBox;
        private System.Windows.Forms.DataGridView PreviewBox;
        private System.Windows.Forms.TextBox EditorID;
        private System.Windows.Forms.Label EditorIDLabel;
        private System.Windows.Forms.TextBox EditorPoolWeight;
        private System.Windows.Forms.Label EditorPoolWeightLabel;
        private System.Windows.Forms.TextBox EditorTags;
        private System.Windows.Forms.Label EditorTagsLabel;
        private System.Windows.Forms.TextBox EditorName;
        private System.Windows.Forms.Label EditorNameLabel;
        private System.Windows.Forms.Label EditorTipsLabel;
        private System.Windows.Forms.Button EditorReadByNameBtn;
        private System.Windows.Forms.Button EditorReadByIDBtn;
        private System.Windows.Forms.Button EditorSaveBtn;
        private System.Windows.Forms.Button CreateBtn;
        private System.Windows.Forms.CheckBox SetAsDefaultDbBox;
    }
}