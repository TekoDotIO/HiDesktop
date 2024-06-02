using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Widgets.MVP;

namespace HiDesktop
{
    public partial class LaunchPage : Form
    {
        public LaunchPage()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Thread thread = new Thread(new ThreadStart(Initialize));
            thread.Start();
        }

        public static Hashtable htStandard = new Hashtable()
        {
            { "type" , "launchPage" },
            { "enableFontInstall" , "true"},
            { "waitForEffects" , "true"},
            { "showBootWindow" , "true"},
            { "topMost" , "true"}
        };

        void Initialize()
        {
            var ht = PropertiesHelper.AutoCheck(htStandard, @"./Properties/LaunchPage.properties");
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
            Close();
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
    }
}
