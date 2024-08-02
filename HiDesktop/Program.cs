using HiDesktop;
using System;
using System.Collections;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Widgets.MVP.WidgetModels;
using Widgets.MVP.WidgetModels.ActivatorDataModel;

namespace Widgets.MVP
{
    internal class Program
    {
        static bool activatorExists = false;
        public static LaunchPage launchPage;
        public static Hashtable htStandard = new Hashtable()
        {
            { "type" , "launchPage" },
            { "enableFontInstall" , "true"},
            { "waitForEffects" , "false"},
            { "showBootWindow" , "true"},
            { "topMost" , "true"}
        };
        const string productName = "HiDesktop";
        static bool showBootWindow = false;
        //static bool enableFontInstall = false;
        static bool waitForEffects = false;
        readonly Hashtable widgets = new Hashtable();
        string nowFile;
        void StartView()
        {
            try
            {
                switch (widgets[nowFile])
                {
                    case CounterBar _:
                        if (!((CounterBar)widgets[nowFile]).IsDisposed)
                        {
                            //throw new Exception("This is a simulating FATAL exception...");
                            ((CounterBar)widgets[nowFile]).ShowDialog();
                            
                        }
                        break;
                    case TextBar _:
                        ((TextBar)widgets[nowFile]).ShowDialog();
                        break;
                    case WidgetModels.Activator _:
                        ((WidgetModels.Activator)widgets[nowFile]).ShowDialog();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception e)
            {
                var item = (Form)widgets[nowFile];
                if (item != null)
                {
                    if (!item.IsDisposed)
                    {
                        item.TopMost = false;
                    }
                }

                Log.SaveLog($"Main program fatal exception:\n{e}", $"HiDesktopMain: {nowFile}", false);
                var r = MessageBox.Show($"主程序执行时出现致命错误！\n出错的程序模块已终止，我们对造成的不便表示歉意。\n\n如果您是用户，请将此信息和日志信息反馈给开发者：\n\n异常文件：{nowFile}\n异常信息：{e}\n\n如果您想亲自调试程序，请点击“是”，如果您想退出当前模块，请点击“否”。", $"HiDesktop - 异常捕获：{nowFile}", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (r == DialogResult.Yes)
                {
                    throw;
                }
                else
                {
                    return;
                }
                
            }
        }
        public static void MainProcess()
        {

            Directory.CreateDirectory("./Properties/");
            string[] properties = Directory.GetFiles("./Properties/");
            Program p = new Program();
            
            bool enableHook = false;//允许接入启动页面
            if (launchPage != null) 
            {
                enableHook = true;
            }
            foreach (string localFile in properties)
            {
                if (localFile.Contains(".properties"))
                {
                    Hashtable config = PropertiesHelper.Load(localFile);
                    
                    switch ((string)config["type"])
                    {
                        case "CounterBar":
                            
                            if ((string)config["enabled"] == "true")
                            {
                                CounterBar textBar = new CounterBar(localFile);
                                p.widgets.Add(localFile, textBar);
                                p.nowFile = localFile;
                                Thread View = new Thread(new ThreadStart(p.StartView));
                                View.Start();
                                string s = $"Launched {localFile}";
                                Log.SaveLog(s);
                                if (enableHook)
                                {
                                    launchPage.ProcessText.Text = $"对象成功构建:{localFile}";
                                }
                            }
                            else
                            {
                                Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                            }
                            break;
                        case "TextBar":
                            if ((string)config["enabled"] == "true")
                            {
                                TextBar textBar = new TextBar(localFile);
                                p.widgets.Add(localFile, textBar);
                                p.nowFile = localFile;
                                Thread Counter = new Thread(new ThreadStart(p.StartView));
                                Counter.Start();
                                Log.SaveLog($"Launched {localFile}");
                                if (enableHook)
                                {
                                    launchPage.ProcessText.Text = $"对象成功构建:{localFile}";
                                }
                            }
                            else
                            {
                                Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                            }
                            break;
                        case "Activator":
                            if (activatorExists)
                            {
                                Log.SaveLog("Already exists one activated Activator. This widget can only exist 1 per OS.");
                                break;
                            }
                            if ((string)config["enabled"] == "true")
                            {
                                WidgetModels.Activator activator = new(localFile);
                                p.widgets.Add(localFile, activator);
                                p.nowFile = localFile;
                                Thread a = new Thread(new ThreadStart(p.StartView));
                                a.Start();
                                Log.SaveLog($"Launched {localFile}");
                                activatorExists = true;
                                if (enableHook)
                                {
                                    launchPage.ProcessText.Text = $"Activator对象成功构建:{localFile}";
                                }
                            }
                            else
                            {
                                Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                            }
                            break;
                        default:
                            Log.SaveLog($"Unknown program type:{localFile}");
                            break;
                    }

                }

            }
            Log.SaveLog("Launched all.");
        }



        static void Main2(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            //CountdownV2 cv2 = new CountdownV2();
            //cv2.ShowDialog();
            //var actvt = new WidgetModels.Activator();
            //actvt.ShowDialog();
            DbProviderFactories.RegisterFactory("System.Data.SQLite.EF6", SQLiteFactory.Instance);
            ActivatorDbContext adc = new("./example.db");
            adc.Initialize();
        }




        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var ht = PropertiesHelper.AutoCheck(htStandard, @"./Properties/LaunchPage.properties");


            //if ((string)ht["enableFontInstall"] == "true") enableFontInstall = true;
            if ((string)ht["waitForEffects"] == "true") waitForEffects = true;
            if ((string)ht["showBootWindow"] == "true") Program.showBootWindow = true;

            switch (args.Length)//读取传入的参数
            {
                case 0:
                    Thread.Sleep(500);
                    if (CommandRepo.IsMultiProcess("Widgets.MVP"))
                    {
                        Log.SaveLog("One or more HiDesktop.Widgets.MVP is already running.Exiting...");
                        return;
                    }
                    Process p = new Process();
                    p.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                    p.StartInfo.Arguments = "--MainProcess";
                    //p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    break;
                //当参数为1个,存在多种情况
                case 1:
                    //读取第一个参数
                    switch (args[0])
                    {
                        case "--MainProcess":

                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            Application.EnableVisualStyles();
                            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                            if (showBootWindow)
                            {
                                if (waitForEffects)
                                {
                                    Thread.Sleep(500);//等待字体响应
                                }
                                launchPage = new();
                                //launchPage.ShowDialog();
                                Application.Run(launchPage);
                            }
                            else
                            {
                                Thread t = new(new ThreadStart(MainProcess));
                                t.Start();
                                Log.SaveLog("Program started with no launch pages.");
                            }
                            break;
                        case "--SkipLaunchPage":
                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            Application.EnableVisualStyles();
                            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                            Thread t2 = new(new ThreadStart(MainProcess));
                            t2.Start();
                            Log.SaveLog("Program started with no launch pages.");
                            break;
                        case "--ExitAll":
                            CommandRepo.ExitAll(productName);
                            CommandRepo.ExitAll("Widgets.MVP");
                            Log.SaveLog($"Killed all {productName} process.");
                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            break;
                        case "--Install":
                            CommandRepo.CreateStartUpScript();
                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            break;
                        case "--Uninstall":
                            CommandRepo.Uninstall("Widgets.MVP");
                            CommandRepo.Uninstall(productName);
                            break;
                    }
                    break;

            }


        }
    }
}
