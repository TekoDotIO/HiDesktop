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
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public int defaultWidth = 816;
        public int defaultHeight = 489;
        public RandomPicker()
        {
            InitializeComponent();
            //MessageBox.Show("");
            x = this.Width;//初始化时候的界面宽度
            y = this.Height;//初始化时候的界面高度
            setTag(this);
        }


        public void ReloadLocations()
        {
            //AppPages.Size = new Size(width: Size.Width - 60, height: Size.Height - Location.Y - 70);
            //CopyrightLabel.Location = new Point(CopyrightLabel.Location.X, Size.Height - 65);
            //ExitBtn.Location = new Point(Size.Width - 120, CopyrightLabel.Location.Y);
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

        //Part from CSDN.
        //Ori: https://blog.csdn.net/qq_27524749/article/details/102501450
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        //用来拉伸界面中的组件
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void RandomPicker_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
            float newy = (this.Height) / y;//拖动界面之后的高度与之前界面的高度之比
            setControls(newx, newy, this);//进行控件大小的伸缩变换
        }


        private void fullScreenBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fullScreenBox.Checked)
            {
                //FullScreen
                FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                //NormalSize
                FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }
    }


}
