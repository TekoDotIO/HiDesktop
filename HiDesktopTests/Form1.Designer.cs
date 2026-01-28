namespace HiDesktopTests
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            poisonProgressSpinner1 = new ReaLTaiizor.Controls.PoisonProgressSpinner();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            poisonProgressBar1 = new ReaLTaiizor.Controls.PoisonProgressBar();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            materialCheckBox1 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialMultiLineTextBoxEdit1 = new ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit();
            SuspendLayout();
            // 
            // poisonProgressSpinner1
            // 
            poisonProgressSpinner1.BackColor = Color.White;
            poisonProgressSpinner1.LineWidthRatio = 0;
            poisonProgressSpinner1.Location = new Point(189, 165);
            poisonProgressSpinner1.Maximum = 100;
            poisonProgressSpinner1.Name = "poisonProgressSpinner1";
            poisonProgressSpinner1.Size = new Size(102, 99);
            poisonProgressSpinner1.TabIndex = 15;
            poisonProgressSpinner1.Text = "poisonProgressSpinner1";
            poisonProgressSpinner1.UseCustomBackColor = true;
            poisonProgressSpinner1.UseSelectable = true;
            poisonProgressSpinner1.Value = 6;
            poisonProgressSpinner1.Visible = false;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.Font = new Font("MiSans Medium", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 134);
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton1.Location = new Point(353, 334);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(77, 36);
            materialButton1.TabIndex = 16;
            materialButton1.Text = "cancel";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // poisonProgressBar1
            // 
            poisonProgressBar1.Location = new Point(31, 408);
            poisonProgressBar1.Name = "poisonProgressBar1";
            poisonProgressBar1.ProgressBarMarqueeWidth = 247;
            poisonProgressBar1.ProgressBarStyle = ProgressBarStyle.Marquee;
            poisonProgressBar1.Size = new Size(741, 5);
            poisonProgressBar1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Custom;
            poisonProgressBar1.TabIndex = 17;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(353, 208);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(93, 19);
            materialLabel1.TabIndex = 18;
            materialLabel1.Text = "Connecting...";
            // 
            // materialCheckBox1
            // 
            materialCheckBox1.AutoSize = true;
            materialCheckBox1.Depth = 0;
            materialCheckBox1.Location = new Point(31, 85);
            materialCheckBox1.Margin = new Padding(0);
            materialCheckBox1.MouseLocation = new Point(-1, -1);
            materialCheckBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox1.Name = "materialCheckBox1";
            materialCheckBox1.ReadOnly = false;
            materialCheckBox1.Ripple = true;
            materialCheckBox1.Size = new Size(172, 37);
            materialCheckBox1.TabIndex = 19;
            materialCheckBox1.Text = "materialCheckBox1";
            materialCheckBox1.UseAccentColor = false;
            materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // materialMultiLineTextBoxEdit1
            // 
            materialMultiLineTextBoxEdit1.AnimateReadOnly = false;
            materialMultiLineTextBoxEdit1.BackgroundImageLayout = ImageLayout.None;
            materialMultiLineTextBoxEdit1.CharacterCasing = CharacterCasing.Normal;
            materialMultiLineTextBoxEdit1.Depth = 0;
            materialMultiLineTextBoxEdit1.HideSelection = true;
            materialMultiLineTextBoxEdit1.Hint = "text";
            materialMultiLineTextBoxEdit1.Location = new Point(201, 125);
            materialMultiLineTextBoxEdit1.MaxLength = 32767;
            materialMultiLineTextBoxEdit1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialMultiLineTextBoxEdit1.Name = "materialMultiLineTextBoxEdit1";
            materialMultiLineTextBoxEdit1.PasswordChar = '\0';
            materialMultiLineTextBoxEdit1.ReadOnly = false;
            materialMultiLineTextBoxEdit1.ScrollBars = ScrollBars.None;
            materialMultiLineTextBoxEdit1.SelectedText = "";
            materialMultiLineTextBoxEdit1.SelectionLength = 0;
            materialMultiLineTextBoxEdit1.SelectionStart = 0;
            materialMultiLineTextBoxEdit1.ShortcutsEnabled = true;
            materialMultiLineTextBoxEdit1.Size = new Size(500, 200);
            materialMultiLineTextBoxEdit1.TabIndex = 20;
            materialMultiLineTextBoxEdit1.TabStop = false;
            materialMultiLineTextBoxEdit1.Text = "materialMultiLineTextBoxEdit1";
            materialMultiLineTextBoxEdit1.TextAlign = HorizontalAlignment.Left;
            materialMultiLineTextBoxEdit1.UseSystemPasswordChar = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 424);
            Controls.Add(materialMultiLineTextBoxEdit1);
            Controls.Add(materialCheckBox1);
            Controls.Add(materialLabel1);
            Controls.Add(poisonProgressBar1);
            Controls.Add(materialButton1);
            Controls.Add(poisonProgressSpinner1);
            Font = new Font("OPPO Sans 4.0 Medium", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 134);
            Name = "Form1";
            Padding = new Padding(3, 60, 3, 3);
            Text = "Loading Config...";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.PoisonProgressSpinner poisonProgressSpinner1;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.PoisonProgressBar poisonProgressBar1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox1;
        private ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit materialMultiLineTextBoxEdit1;
    }
}
