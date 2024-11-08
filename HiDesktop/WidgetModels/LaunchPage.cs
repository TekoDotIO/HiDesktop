using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP;
using Widgets.MVP.Essential_Repos;

namespace HiDesktop
{
    public partial class LaunchPage : Form
    {

        //From https://www.cnblogs.com/darkic/p/16256294.html
        /// <summary>
        /// 设置窗体的Region
        /// </summary>
        public void SetWindowRegion(int radius)
        {
            GraphicsPath FormPath;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, radius);
            this.Region = new Region(FormPath);

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }


        Hashtable ht;
        bool iniFinished = false;
        public LaunchPage()
        {
            Opacity = 0.01;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;



            ht = PropertiesHelper.AutoCheck(htStandard, @"./Properties/LaunchPage.properties");
            Thread thread = new Thread(new ThreadStart(Initialize));
            thread.Start();
            //Thread animeThread = new Thread(new ThreadStart(TryAnime));
            //animeThread.Start();
        }

        /// <summary>
        /// 捕捉窗体事件 
        /// </summary>
        /// <param name="m"></param>
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //}


        /// <summary>
        /// 让程序不显示在alt+Tab视图窗体中
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                //cp.ExStyle |= 0x02000000;//解决闪屏问题，来自 https://blog.csdn.net/weixin_38211198/article/details/90724952
                return cp;
            }
        }
        //From https://www.cnblogs.com/darkic/p/16256294.html

        public static Hashtable htStandard = new Hashtable()
        {
            { "type" , "launchPage" },
            { "enableFontInstall" , "true"},
            { "waitForEffects" , "true"},
            { "showBootWindow" , "true"},
            { "topMost" , "true"},
            { "enableAnime" , "true"},
            { "animeLength", "0.1" }
        };

        void TryAnime()
        {
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            if ((string)ht["enableAnime"] == "true")
            {
                AnimeStart();
            }
            Opacity = 1.00;
            while (!iniFinished)
            {
                Thread.Sleep(200);
            }
            Close();
        }

        void AnimeStart()
        {
            //Hide();

            Refresh();
            //var targetLocation = Location;
            //var targetSize = Size;
            ////var screenHeight = SystemInformation.PrimaryMonitorSize.Height;
            //var screenWidth = SystemInformation.PrimaryMonitorSize.Width;
            //double maxt = Convert.ToDouble((string)ht["animeLength"]);
            ////maxt = 2.5;
            //Size = new Size(1, Size.Height);
            //Location = new Point(screenWidth / 2, Location.Y);
            ////Show();
            //var sizeAl = MathRepo.CreatePhysicalSmoothMovePointsSet(1, targetSize.Width, maxt, 0.01);
            //var xAl = MathRepo.CreatePhysicalSmoothMovePointsSet(screenWidth / 2, targetLocation.X, maxt, 0.01);
            ////var al = MathRepo.CreatePhysicalSmoothMovePointsSet(0, 1, maxt, 0.01);
            //Opacity = 1;
            //for (int i = 0; i < xAl.Count; i++)
            //{
            //    //Refresh();
            //    //var a = Convert.ToDouble(((double)al[i]).ToString("0.00"));
            //    //Opacity = Convert.ToDouble(((double)al[i]).ToString("0.00"));
            //    Size = new Size((int)Math.Round(((double)sizeAl[i])), Size.Height);
            //    Location = new Point((int)Math.Round(((double)xAl[i])), Location.Y);
            //    //Thread.Sleep(5);
            //}

            var al = MathRepo.CreatePhysicalSmoothMovePointsSet(0, 1, 2, 0.05);
            foreach (var item in al)
            {
                var a = Convert.ToDouble(((double)item).ToString("0.00"));
                Opacity = Convert.ToDouble(((double)item).ToString("0.00"));
                Thread.Sleep(20);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Thread animeThread = new Thread(new ThreadStart(TryAnime));
            animeThread.Start();
        }

        void Initialize()
        {
            SetWindowRegion(Height / 20);
            ProcessText.Parent = poster;
            //poster.Controls.Add(ProcessText);
            //ProcessText.Location = new Point(0, 0);
            ProcessText.BackColor = Color.Transparent;
            ////实现文本反馈的透明背景
            VersionDisplay.Parent = poster;
            //poster.Controls.Add(VersionDisplay);
            VersionDisplay.BackColor = Color.Transparent;
            VersionDisplay.Text = AppInfo.Version;

            StartupInfo.Parent = poster;
            StartupInfo.Text = AppInfo.StartupInfo;
            StartupInfo.BackColor = Color.Transparent;

            bool enableFontInstall = false;
            bool waitForEffects = false;
            //bool showBootWindow = false;
            bool topMost = true;
            if ((string)ht["enableFontInstall"] == "true") enableFontInstall = true;
            if ((string)ht["waitForEffects"] == "true") waitForEffects = true;
            //if ((string)ht["showBootWindow"] == "true") showBootWindow = true;
            if ((string)ht["topMost"] == "true") topMost = true;
            if (topMost) this.TopMost = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 10;
            //Thread.Sleep(10000);
            progressBar.Value = 20;
            ProcessText.Text = "程序正在启动-Program loading...";
            Log.SaveLog("[LaunchPage]Window launched.");
            Thread MainThread = new Thread(new ThreadStart(Program.MainProcess));
            if (waitForEffects) Thread.Sleep(1000);
            Log.SaveLog("[LaunchPage]Thread built");
            if (enableFontInstall)
            {
                ProcessText.Text = "安装字体-Install fonts...";
                try
                {
                    int fontNum = Directory.GetFiles("./Fonts/").Length;
                    foreach (string file in Directory.GetFiles("./Fonts/"))
                    {
                        ProcessText.Text = $"正在安装字体{file}-Installing font{file}";
                        InstallFont(file);
                        progressBar.Value += 55 / fontNum;
                        Log.SaveLog($"[LaunchPage]Installed font {file}");
                        if (waitForEffects) Thread.Sleep(100);
                    }
                    ProcessText.Text = $"字体安装完成-Fonts installed..";
                    Log.SaveLog($"[LaunchPage]Fonts installed.");
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"[LaunchPage]Exception while installing font:{ex}");
                    ProcessText.Text = $"字体安装异常-Fonts installed with an exception..";
                }
            }
            else
            {
                Log.SaveLog($"[LaunchPage]Font install is not enabled.");
                ProcessText.Text = $"字体安装已禁用-Font installation is disabled..";
            }



            progressBar.Value = 75;
            if (waitForEffects) Thread.Sleep(1000);
            ProcessText.Text = "请等待字体安装完成-Waiting for OS font... ";
            if (waitForEffects) Thread.Sleep(500);
            MainThread.Start();
            Log.SaveLog($"[LaunchPage]Thread started.");
            ProcessText.Text = "线程构建完成-Thread built... ";

            progressBar.Value = 100;
            if (waitForEffects) Thread.Sleep(1000);
            ProcessText.Text = "启动完成-Finished";
            Log.SaveLog($"[LaunchPage]Launched MainProcess.");
            if (waitForEffects) Thread.Sleep(200);
            //Close();
            iniFinished = true;
        }


        public static bool CheckFont(string fontFilePath)
        {
            bool haveFont = false;
            foreach (string file in Directory.GetFiles(Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "fonts")))
            {
                if (Path.GetFileName(file) == Path.GetFileName(fontFilePath))
                {
                    haveFont = true;
                }
            }
            return haveFont;

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);

        /// <summary>
        /// 安装字体
        /// </summary>
        /// <param name="fontFilePath">字体文件全路径</param>
        /// <returns>是否成功安装字体</returns>
        /// <exception cref="UnauthorizedAccessException">不是管理员运行程序</exception>
        /// <exception cref="Exception">字体安装失败</exception>
        public static bool InstallFont(string fontFilePath)
        {
            try
            {
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();

                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                //判断当前登录用户是否为管理员
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator) == false)
                {
                    throw new UnauthorizedAccessException("当前用户无管理员权限，无法安装字体。");
                }
                //获取Windows字体文件夹路径
                string fontPath = Path.Combine(System.Environment.GetEnvironmentVariable("WINDIR"), "fonts", Path.GetFileName(fontFilePath));
                //检测系统是否已安装该字体
                if (!File.Exists(fontPath))
                {
                    // File.Copy(System.Windows.Forms.Application.StartupPath + "\\font\\" + FontFileName, FontPath); //font是程序目录下放字体的文件夹
                    //将某路径下的字体拷贝到系统字体文件夹下
                    File.Copy(fontFilePath, fontPath); //font是程序目录下放字体的文件夹
                    AddFontResource(fontPath);

                    //Res = SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0); 
                    //WIN7下编译会出错，不清楚什么问题。注释就行了。  
                    //安装字体
                    WriteProfileString("fonts", Path.GetFileNameWithoutExtension(fontFilePath) + "(TrueType)", Path.GetFileName(fontFilePath));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"[{Path.GetFileNameWithoutExtension(fontFilePath)}] 字体安装失败！原因：{ex.Message}"));
            }
            return true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LaunchPage_Load(object sender, EventArgs e)
        {
            SetWindowRegion(Height / 20);
        }
    }
}
