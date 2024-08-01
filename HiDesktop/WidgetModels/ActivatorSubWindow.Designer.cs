namespace Widgets.MVP.WidgetModels
{
    partial class ActivatorSubWindow
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
            SuspendLayout();
            // 
            // ActivatorSubWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(382, 353);
            ForeColor = System.Drawing.Color.White;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "ActivatorSubWindow";
            ShowInTaskbar = false;
            Text = "ActivatorSubWindow";
            TopMost = true;
            Deactivate += ActivatorSubWindow_Deactivate;
            Load += ActivatorSubWindow_Load;
            Layout += ActivatorSubWindow_Layout;
            MouseLeave += ActivatorSubWindow_MouseLeave;
            ResumeLayout(false);
        }

        #endregion
    }
}