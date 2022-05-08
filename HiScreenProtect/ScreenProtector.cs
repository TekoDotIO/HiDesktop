using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiScreenProtect
{
    public partial class ScreenProtector : Form
    {
        Thread refreshThread;
        int w = SystemInformation.PrimaryMonitorSize.Width;
        int h = SystemInformation.PrimaryMonitorSize.Height;
        public ScreenProtector()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void CloseSlowly(object sender, FormClosingEventArgs e)
        {
            //this.Opacity = 1;
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1);
            //    Opacity -= 0.01;
            //}
            AnimateWindow(this.Handle, 500, AW_BLEND | AW_HIDE);
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
        //void TimeShining()
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Thread.Sleep(1);
        //        timeBox.Opacity += 0.01;
        //    }
        //}

        private void ScreenProtector_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1);
                Opacity += 0.01;
            }
            timeBox.Text = DateTime.Now.ToString("HH:mm:ss");
            Font f = new Font(timeBox.Font.Name, 200);
            timeBox.Font = f;
            timeBox.ForeColor = Color.White;
            Point timeLocation = new Point();
            timeLocation.X = w / 2 - timeBox.Width / 2;
            timeLocation.Y = h / 2 - timeBox.Height / 2;
            timeBox.Location = timeLocation;

            refreshThread = new Thread(new ThreadStart(RefreshTime));
            refreshThread.Start();

            //Thread shiningThread = new Thread(new ThreadStart(TimeShining));
            //shiningThread.Start();

        }

        void RefreshTime()
        {
            while (true)
            {
                Thread.Sleep(200);
                timeBox.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }
        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
        [DllImport("user32.dll")]//导入dll
        private static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        //dwflag的取值如下
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        //从左到右显示
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        //从右到左显示
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        //从上到下显示
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        //从下到上显示
        public const Int32 AW_CENTER = 0x00000010;
        //若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        public const Int32 AW_HIDE = 0x00010000;
        //隐藏窗口，缺省则显示窗口
        public const Int32 AW_ACTIVATE = 0x00020000;
        //激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        public const Int32 AW_SLIDE = 0x00040000;
        //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        public const Int32 AW_BLEND = 0x00080000;
        //透明度从高到低
    }
}
