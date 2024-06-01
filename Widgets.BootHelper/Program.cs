using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using Microsoft.Win32;

namespace Widgets.BootHelper
{
    class Program
    {
        public static Hashtable htStandard = new()
        {
            { "type" , "BootHelper" },
            { "enableScrSettings", "true" },
            { "installFonts" , "true" },
            { "skipLaunchPage" , "true" },
            { "filePath" , "D:/HiDesktop/Widgets.MVP.exe" }
        };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:验证平台兼容性", Justification = "<挂起>")]
        static void Main()
        {
            bool enableScrSettings = false;
            bool installFonts = false;
            bool skipLaunchPage = false;
            string filePath = "";
            if (!File.Exists("./Properties/BootHelper.properties"))
            {
                Directory.CreateDirectory("./Properties/");
                Directory.CreateDirectory("./Fonts/");
                PropertiesHelper.Save("./Properties/BootHelper.properties", htStandard);

            }
            var ht = PropertiesHelper.AutoCheck(htStandard, "./Properties/BootHelper.properties");
            if ((string)ht["enableScrSettings"] == "true") enableScrSettings = true;
            if ((string)ht["installFonts"] == "true") installFonts = true;
            if ((string)ht["skipLaunchPage"] == "true") skipLaunchPage = true;
            filePath = (string)ht["filePath"];
            if (filePath == null) filePath = "";

            if (installFonts)
            {
                try
                {
                    foreach (string file in Directory.GetFiles("./Fonts/"))
                    {
                        //ProcessText.Text = $"正在安装字体{file}-Installing font{file}";
                        InstallFont(file);
                        //progressBar.Value = progressBar.Value + 5;
                        Console.WriteLine($"[Helper]Installed font {file}");
                        Log.SaveLog($"[Helper]Installed font {file}");
                    }
                    //ProcessText.Text = $"字体安装完成-Fonts installed..";
                    Console.WriteLine($"[Helper]Fonts installed.");
                    Log.SaveLog($"[Helper]Fonts installed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Helper]Exception while installing font:{ex}");
                    Log.SaveLog($"[Helper]Exception while installing font:{ex}");
                    //ProcessText.Text = $"字体安装异常-Fonts installed with an exception..";
                }
                Log.SaveLog("[Helper]Font setting completed.Booting Main Process...");
            }
            else
            {
                Log.SaveLog("[Helper]Font installing is not enabled.Booting Main Process...");
            }

            

            if (skipLaunchPage)
            {
                try
                {
                    Process p = new();
                    p.StartInfo.WorkingDirectory = Path.GetDirectoryName(filePath);
                    p.StartInfo.Arguments = "--SkipLaunchPage";
                    p.StartInfo.FileName = filePath;
                    p.Start();
                    //Process.Start(filePath, "--SkipLaunchPage");
                    Log.SaveLog("[Helper]Launched main process with launchpage skipped.");
                }
                catch (Exception ex)
                {
                    Log.SaveLog("[Helper]Exception occured when booting Widgets.MVP with argument \"--SkipLaunchPage\".");
                    Log.SaveLog($"[Helper][ExceptionDetails]{ex}");
                }
            }
            else
            {
                try
                {
                    Process p = new();
                    p.StartInfo.WorkingDirectory = Path.GetDirectoryName(filePath);
                    //p.StartInfo.Arguments = "--SkipLaunchPage";
                    p.StartInfo.FileName = filePath;
                    p.Start();
                    //Process.Start(filePath);
                    Log.SaveLog("[Helper]Launched main process with launchpage not skipped.");
                }
                catch (Exception ex)
                {

                    Log.SaveLog("[Helper]Exception occured when booting Widgets.MVP without arguments.");
                    Log.SaveLog($"[Helper][ExceptionDetails]{ex}");
                }
            }

            /*
            版权声明：本代码为CSDN博主「无熵~」的原创，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
            原文链接：https://blog.csdn.net/lvxingzhe3/article/details/135595441
            */


            if (enableScrSettings)
            {
                Log.SaveLog("[Helper]ScrSettings is enabled.Doing extra settings...");
                string scrPath = @"./ScreenProtect.MVP.scr";
                if (File.Exists(scrPath))
                {
                    try
                    {
                        //获取user根项
                        RegistryKey user = Registry.CurrentUser;
                        //打开desktop项
                        RegistryKey desktop = user.OpenSubKey("Control Panel\\Desktop", true);
                        if (desktop != null)
                        {
                            //设置屏保程序位置
                            desktop.SetValue("SCRNSAVE.EXE", scrPath);
                            //是否启动屏保 0:不启动 1:启动
                            desktop.SetValue("ScreenSaveActive", "1");
                            //退出屏保后是否需要登录 0：不需要 1：需要
                            desktop.SetValue("ScreenSaverIsSecure", "0");
                            //电脑无操作后启动屏保时间（秒）
                            desktop.SetValue("ScreenSaveTimeout", "1800");
                        }


                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog($"[Helper]Exception occured when installing scr:\n{ex}");
                    }
                }
                else
                {
                    Log.SaveLog("[Helper]Could not find the scr fil.Skipping...");
                }
                

            }
            else
            {
                Log.SaveLog("[Helper]ScrSettings is disabled.Exiting...");
            }
            
            
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
                string fontPath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "fonts", Path.GetFileName(fontFilePath));
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
    }
}
