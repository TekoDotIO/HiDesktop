using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Widgets.MVP.WindowApps
{
    public partial class RandomPicker : Form
    {
        public RandomPicker()
        {
            InitializeComponent();
            //MessageBox.Show("");
        }

        private void DarkModeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (darkModeBox.Checked) 
            {
                //不知道为什么，设计器中给按钮设置透明背景色会导致所有背景色设置失效，因此改为了Control。
                //Dark mode...
                if (BackColor == SystemColors.Control)
                {
                    BackColor = Color.Black;
                }
                AppPages.BackColor = Color.Black;
                AppPages.ForeColor = Color.White;
                foreach (TabPage item in AppPages.TabPages)
                {
                    item.BackColor = Color.Black;
                    item.ForeColor = Color.White;
                    item.UseVisualStyleBackColor = false;
                    foreach (Control item2 in item.Controls)
                    {
                        if (item2.BackColor == SystemColors.Control)
                        {
                            item2.BackColor = Color.Black;
                        }
                        if (item2.BackColor == SystemColors.Window)
                        {
                            item2.BackColor = Color.DarkSlateGray;
                        }
                        if (item2.ForeColor == SystemColors.ControlText)
                        {
                            item2.ForeColor = Color.White;
                        }
                        if (item2.ForeColor == SystemColors.WindowText)
                        {
                            item2.ForeColor = Color.AntiqueWhite;
                        }
                        if ((object)item.GetType() == typeof(Button))
                        {
                            var btn = (Button)item2;
                            btn.BackColor = Color.Black;
                            //btn.UseVisualStyleBackColor = false;
                        }
                        if ((object)item.GetType() == typeof(CheckBox))
                        {
                            var bx = (CheckBox)item2;
                            //bx.UseVisualStyleBackColor = false;
                        }
                    }
                }
                foreach (Control item in this.Controls)
                {
                    if (item.BackColor == SystemColors.Control) 
                    {
                        item.BackColor = Color.Black;
                    }
                    if (item.BackColor == SystemColors.Window)
                    {
                        item.BackColor = Color.DarkSlateGray;
                    }
                    if (item.ForeColor == SystemColors.ControlText)
                    {
                        item.ForeColor = Color.White;
                    }
                    if (item.ForeColor == SystemColors.WindowText)
                    {
                        item.ForeColor = Color.AntiqueWhite;
                    }
                    if ((object)item.GetType() == typeof(Button)) 
                    {
                        var btn = (Button)item;
                        btn.BackColor = Color.Black;
                        //btn.UseVisualStyleBackColor = false;
                    }
                    if ((object)item.GetType() == typeof(CheckBox))
                    {
                        var bx = (CheckBox)item;
                        //bx.UseVisualStyleBackColor = false;
                    }
                }
            }
            else
            {
                //Light mode...
                AppPages.BackColor = Color.White;
                AppPages.ForeColor = Color.Black;
                foreach (TabPage item in AppPages.TabPages)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                    foreach (Control item2 in item.Controls)
                    {
                        if (item2.BackColor == Color.Black)
                        {
                            item2.BackColor = SystemColors.Control;
                        }
                        if (item2.BackColor == Color.DarkSlateGray)
                        {
                            item2.BackColor = SystemColors.Window;
                        }
                        if (item2.ForeColor == Color.White)
                        {
                            item2.ForeColor = SystemColors.ControlText;
                        }
                        if (item2.ForeColor == Color.AntiqueWhite)
                        {
                            item2.ForeColor = SystemColors.WindowText;
                        }
                        if ((object)item.GetType() == typeof(Button))
                        {
                            var btn = (Button)item2;
                            btn.BackColor = SystemColors.Control;
                            //btn.UseVisualStyleBackColor = true;
                        }
                        if ((object)item.GetType() == typeof(CheckBox))
                        {
                            var bx = (CheckBox)item2;
                            //bx.UseVisualStyleBackColor = true;
                        }
                    }
                }
                if (BackColor == Color.Black)
                {
                    BackColor = SystemColors.Control;
                }

                foreach (Control item in this.Controls)
                {
                    if (item.BackColor == Color.Black)
                    {
                        item.BackColor = SystemColors.Control;
                    }
                    if (item.BackColor == Color.DarkSlateGray)
                    {
                        item.BackColor = SystemColors.Window;
                    }
                    if (item.ForeColor == Color.White)
                    {
                        item.ForeColor = SystemColors.ControlText;
                    }
                    if (item.ForeColor == Color.AntiqueWhite)
                    {
                        item.ForeColor = SystemColors.WindowText;
                    }
                    if ((object)item.GetType() == typeof(Button))
                    {
                        var btn = (Button)item;
                        btn.BackColor = SystemColors.Control;
                        //btn.UseVisualStyleBackColor = true;
                    }
                    if ((object)item.GetType() == typeof(CheckBox))
                    {
                        var bx = (CheckBox)item;
                        //bx.UseVisualStyleBackColor = true;
                    }
                }
            }
        }

        private void RanDbAddToExcept_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
