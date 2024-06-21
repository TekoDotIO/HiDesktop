using HiDesktop;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Widgets.MVP.WidgetModels;

namespace Widgets.MVP
{
    internal class Program
    {
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

            switch (widgets[nowFile])
            {
                case CounterBar _:
                    if (!((CounterBar)widgets[nowFile]).IsDisposed)
                    {

                        ((CounterBar)widgets[nowFile]).ShowDialog();

                    }
                    break;
                case TextBar _:
                    ((TextBar)widgets[nowFile]).ShowDialog();
                    break;
                default:
                    break;
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
                        default:
                            Log.SaveLog($"Unknown program type:{localFile}");
                            break;
                    }

                }

            }
            Log.SaveLog("Launched all.");
        }



        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            //CountdownV2 cv2 = new CountdownV2();
            //cv2.ShowDialog();
            var actvt = new WidgetModels.Activator();
            actvt.ShowDialog();
        }




        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main2(string[] args)
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

                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
                            Application.EnableVisualStyles();
                            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                            if (showBootWindow)
                            {
                                if (waitForEffects)
                                {
                                    Thread.Sleep(500);//等待字体响应
                                }
                                launchPage = new();
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
                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
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
                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
                            break;
                        case "--Install":
                            CommandRepo.CreateStartUpScript();
                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
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
