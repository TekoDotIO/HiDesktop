using ScreenProtect.MVP;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HiDesktop.HiScreenProtect.MVP
{
    public partial class ScreenProtector : Form
    {
        string Path;
        Hashtable AppConfig;
        Thread refreshThread;
        int w = SystemInformation.PrimaryMonitorSize.Width;
        int h = SystemInformation.PrimaryMonitorSize.Height;
        public ScreenProtector()
        {
            Opacity = 0;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            TopMost = false;
            this.Path = "./config.properties";
            Hashtable htStandard = new Hashtable()
            {
                { "type", "screenProtector" },
                { "font", "auto" },
                { "time_fontSize", "200" },
                { "tips_fontSize", "20" },
                { "opacity", "1" },
                { "topMost", "true" },
                { "enableSmoothStart", "true" },
                { "enabled","true" },
                { "refreshTime","50" },
                { "timeType","HH:mm:ss" },
                { "tips","Click any place to exit..." },
                { "time_Color","#FFFFFF" },
                { "tips_Color","#FFFFFF" }
            };
            if (!File.Exists(Path))
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
            }
            PropertiesHelper.FixProperties(htStandard, Path);
            AppConfig = PropertiesHelper.Load(Path);
            if (!((string)AppConfig["enabled"] == "true")) 
            {
                Log.SaveLog("Screen protector disabled...");
                return;
            }
        }

        private void CloseSlowly(object sender, FormClosingEventArgs e)
        {
            //this.Opacity = 1;
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1);
            //    Opacity -= 0.01;
            //}
            if ((string)AppConfig["enableSmoothStart"] == "true") 
            {
                AnimateWindow(Handle, 500, AW_BLEND | AW_HIDE);
            }
            
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
            if ((string)AppConfig["topMost"]=="true")
            {
                TopMost = true;
            }
            if ((string)AppConfig["enableSmoothStart"] == "true")
            {
                Opacity = 0;
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1);
                    Opacity += 0.01;
                }
            }
            else
            {
                Opacity = 1;
            }
            Opacity = Convert.ToDouble((string)AppConfig["opacity"]);
            timeBox.Text = DateTime.Now.ToString((string)AppConfig["timeType"]);
            Font f;
            if ((string)AppConfig["font"] == "auto")
            {
                f = new Font(timeBox.Font.Name, 200);
            }
            else
            {
                try
                {
                    //从外部文件加载字体文件
                    PrivateFontCollection font = new PrivateFontCollection();
                    font.AddFontFile((string)AppConfig["font"]);

                    //定义成新的字体对象
                    FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                    f = new Font(myFontFamily, 56F, FontStyle.Regular);
                }
                catch (Exception ex)
                {
                    Log.SaveLog(ex.ToString());
                    f = new Font(timeBox.Font.Name, 200);
                }
                f = new Font(timeBox.Font.Name, 200);
            }

            f = new Font(f.FontFamily, Convert.ToInt32((string)AppConfig["time_fontSize"]));
            timeBox.Font = f;
            f = new Font(f.FontFamily, Convert.ToInt32((string)AppConfig["tips_fontSize"]));
            Tips.Font = f;

            timeBox.ForeColor = ColorTranslator.FromHtml((string)AppConfig["time_Color"]);
            Point timeLocation = new Point
            {
                X = w / 2 - timeBox.Width / 2,
                Y = h / 2 - timeBox.Height / 2
            };
            timeBox.Location = timeLocation;

            Tips.ForeColor = ColorTranslator.FromHtml((string)AppConfig["tips_Color"]);
            Tips.Text = (string)AppConfig["tips"];
            Point tipsLocation = new Point
            {
                X = w / 2 - Tips.Width / 2,
                Y = h - 50
            };
            Tips.Location = tipsLocation;

            refreshThread = new Thread(new ThreadStart(RefreshTime));
            refreshThread.Start();

            //Thread shiningThread = new Thread(new ThreadStart(TimeShining));
            //shiningThread.Start();

        }

        void RefreshTime()
        {
            int refreshTime = Convert.ToInt32((string)AppConfig["refreshTime"]);
            string timeType = (string)AppConfig["timeType"];
            while (true)
            {
                Thread.Sleep(refreshTime);
                timeBox.Text = DateTime.Now.ToString(timeType);
            }
        }
        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
        [DllImport("user32.dll")]//导入dll
        private static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        //dwflag的取值如下
        public const int AW_HOR_POSITIVE = 0x00000001;
        //从左到右显示
        public const int AW_HOR_NEGATIVE = 0x00000002;
        //从右到左显示
        public const int AW_VER_POSITIVE = 0x00000004;
        //从上到下显示
        public const int AW_VER_NEGATIVE = 0x00000008;
        //从下到上显示
        public const int AW_CENTER = 0x00000010;
        //若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        public const int AW_HIDE = 0x00010000;
        //隐藏窗口，缺省则显示窗口
        public const int AW_ACTIVATE = 0x00020000;
        //激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        public const int AW_SLIDE = 0x00040000;
        //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        public const int AW_BLEND = 0x00080000;

        private void label1_Click(object sender, EventArgs e)
        {
            if ((string)AppConfig["enableSmoothStart"] == "true")
            {
                AnimateWindow(Handle, 500, AW_BLEND | AW_HIDE);
            }

            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void timeBox_Click(object sender, EventArgs e)
        {
            if ((string)AppConfig["enableSmoothStart"] == "true")
            {
                AnimateWindow(Handle, 500, AW_BLEND | AW_HIDE);
            }

            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
        //透明度从高到低
    }
}
