﻿namespace Widgets.MVP.WidgetModels
{
    partial class Activator
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
            // Activator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(100, 100);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Activator";
            Text = "Activator";
            TopMost = true;
            Load += Activator_Load;
            Click += Activator_Click;
            MouseClick += Activator_MouseClick;
            MouseDown += Activator_MouseDown;
            MouseUp += Activator_MouseUp;
            ResumeLayout(false);
        }

        #endregion
    }
}