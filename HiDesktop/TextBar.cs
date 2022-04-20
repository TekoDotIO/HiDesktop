using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;

namespace HiDesktop
{
    public partial class TextBar : Form
    {
        DateTime Latest;
        DateTime Target;
        public TextBar()
        {
            InitializeComponent();
            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            CheckForIllegalCrossThreadCalls = false;
            Latest = DateTime.Now;
            Target = new DateTime(2022, 6, 18);
            LabelNo1.ForeColor = Color.White;
            LabelNo2.ForeColor = Color.White;
            EventText.ForeColor = Color.Red;
            NumText.ForeColor = Color.Red;
            LabelNo1.Location = new Point(0, 0);
            EventText.Location = new Point(LabelNo1.Location.X + LabelNo1.Size.Width, EventText.Location.Y);
            EventText.Text = "2022年初中学业水平考试";
            LabelNo2.Location = new Point(EventText.Location.X + EventText.Size.Width, LabelNo2.Location.Y);
            UpdateTimeOnce();
            NumText.Location = new Point(LabelNo2.Location.X + LabelNo2.Size.Width, NumText.Location.Y);
            Size = new Size(NumText.Location.X + NumText.Size.Width, NumText.Size.Height + 10);
            int startX = LabelNo1.Location.X;
            int endX = NumText.Location.X + NumText.Size.Width;
            int deltaX = endX - startX;
            Location = new Point(w / 2 - (deltaX) / 2, Location.Y);
            Thread thread = new Thread(new ThreadStart(UpdateTime));
            thread.Start();

        }
        private void UpdateTime()
        {
            while (true)
            {
                var span = Target - Latest;
                NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({getDays(Target, Latest)}个工作日).";
                Thread.Sleep(500);
                Latest = DateTime.Now;
            }
        }
        private void UpdateTimeOnce()
        {
            var span = Target - Latest;
            NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({getDays(Target, Latest)}个工作日).";
            Thread.Sleep(500);
            Latest = DateTime.Now;
        }
        public int getDays(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts1 = dt1.Subtract(dt2);//TimeSpan得到dt1和dt2的时间间隔
            int countday = ts1.Days;//获取两个日期间的总天数
            int weekday = 0;//工作日
                            //循环用来扣除总天数中的双休日
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = dt1.Date.AddDays(i);
                if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
                {
                    weekday++;
                }
            }
            return weekday;
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        private void TextBar_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void changeLocation(object sender, EventArgs e)
        {
            

            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            frmMain_MouseDown(this, e);
        }
        private void LabelNo1_Click(object sender, EventArgs e)
        {

        }
    }
}
