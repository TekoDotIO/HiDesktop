using HiDesktop;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Widgets.MVP.WidgetModels;
using Widgets.MVP.WidgetModels.ActivatorDataModel;
using System.IO.Pipes;
using System.Text;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP
{
    public class Program
    {
        static bool activatorExists = false;
        public static WidgetModels.Activator activatedActivator;
        public static OneQuoteText MainQuote;
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
        static string UriName = "HiDesktop";
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
                    case OneQuoteText _:
                        ((OneQuoteText)widgets[nowFile]).ShowDialog();
                        break;
                    case CountdownV2 _:
                        ((CountdownV2)widgets[nowFile]).ShowDialog();
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

                Log.SaveLog($"Widget runtime fatal exception:\n{e}", $"HiDesktopMain: {nowFile}", false);
                var r = MessageBox.Show($"小组件执行时出现致命错误！\n出错的程序模块已终止，我们对造成的不便表示歉意。\n\n如果您是用户，请将此信息和日志信息反馈给开发者：\n\n异常文件：{nowFile}\n异常信息：{e}\n\n如果您想亲自调试程序，请点击“是”，如果您想退出当前模块，请点击“否”。", $"HiDesktop - 异常捕获：{nowFile}", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

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
            //MessageBox.Show(Path.GetFullPath("./Properties/"));
            Program p = new Program();
            
            bool enableHook = false;//允许接入启动页面
            if (launchPage != null) 
            {
                enableHook = true;
            }
            foreach (string localFile in properties)
            {
                try
                {
                    if (localFile.Contains(".properties"))
                    {
                        Hashtable config = PropertiesHelper.Load(localFile);

                        switch ((string)config["type"])
                        {
                            case "CountdownV2":
                                if ((string)config["enabled"] == "true")
                                {
                                    CountdownV2 widget = new(localFile);
                                    p.widgets.Add(localFile, widget);
                                    p.nowFile = localFile;
                                    Thread View = new Thread(new ThreadStart(p.StartView));
                                    View.Start();
                                    string s = $"Launched {localFile}";
                                    Log.SaveLog(s);
                                    if (enableHook && launchPage.isAlive && !launchPage.isBusy)
                                    {
                                        launchPage.ProcessText.Text = $"对象成功构建:{localFile}";
                                    }
                                }
                                else
                                {
                                    Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                                }
                                break;
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
                                    if (enableHook && launchPage.isAlive && !launchPage.isBusy)
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
                                    if (enableHook && launchPage.isAlive && !launchPage.isBusy)
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
                                    if (enableHook && launchPage.isAlive && !launchPage.isBusy)
                                    {
                                        launchPage.ProcessText.Text = $"Activator对象成功构建:{localFile}";
                                    }
                                    activatedActivator = activator;
                                }
                                else
                                {
                                    Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                                }
                                break;
                            case "OneQuote":
                                if ((string)config["enabled"] == "true")
                                {
                                    OneQuoteText obj = new(localFile);
                                    p.widgets.Add(localFile, obj);
                                    p.nowFile = localFile;
                                    Thread Counter = new Thread(new ThreadStart(p.StartView));
                                    Counter.Start();
                                    Log.SaveLog($"Launched {localFile}");
                                    if (enableHook && launchPage.isAlive && !launchPage.isBusy)
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
                catch (Exception ex)
                {

                    Log.SaveLog($"Widget initialization program fatal exception:\n{ex}", $"HiDesktopMain: {localFile}", false);
                    var r = MessageBox.Show($"小组件初始化阶段时出现致命错误！\n出错的程序模块已终止，我们对造成的不便表示歉意。\n\n如果您是用户，请将此信息和日志信息反馈给开发者：\n\n异常文件：{localFile}\n异常信息：{ex}\n\n如果您想亲自调试程序，请点击“是”，如果您想退出当前模块，请点击“否”。", $"HiDesktop - 异常捕获：{localFile}", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

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
            Log.SaveLog("Launched all.");
            Log.SaveLog("Start building notify controller...");
            NotifyController.BuildNotifyController();
            Log.SaveLog("Start building Pipes service...");
            Thread PipesThread = new(new ThreadStart(() =>
            {
                StartPipesSystemBuilding();
            }));
            PipesThread.Start();
            
            
        }

        static void StartPipesSystemBuilding()
        {
            while (true)
            {
                // 定义命名管道名称
                string pipeName = "ActivatorUriTransPipe";

                // 创建命名管道服务器
                NamedPipeServerStream pipeServer = new NamedPipeServerStream(
                    pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte);

                Log.SaveLog("Waiting for connection", pipeName, false);

                // 等待连接
                pipeServer.WaitForConnection();

                Log.SaveLog("Connected.", pipeName, false);

                try
                {
                    // 接收数据
                    byte[] buffer = new byte[4096];
                    int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);

                    // 将接收到的字节转换为字符串
                    string receivedString = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    Log.SaveLog("Received URI: " + receivedString, pipeName, false);

                    // 在这里可以根据接收到的数据进行相应操作
                    // 例如，解析数据，调用相应的函数等
                    ActivatorUriProcessor a = new();
                    a.Url = receivedString;
                    a.Process();

                    // 回复消息给应用程序B
                    byte[] responseBuffer = Encoding.UTF8.GetBytes("DONE");
                    pipeServer.Write(responseBuffer, 0, responseBuffer.Length);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"Error occurs at pipe server or command system: \n{ex}");
                    var r = MessageBox.Show($"通过URL传递的命令执行时出现致命错误！\n出错的程序模块已终止，我们对造成的不便表示歉意。\n\n如果您是用户，请将此信息和日志信息反馈给开发者：\n\n异常文件：-\n异常信息：{ex}\n\n如果您想亲自调试程序，请点击“是”，如果您想退出当前模块，请点击“否”。", $"HiDesktop - 异常捕获：URL Scheme模块", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }
                }
                finally
                {
                    // 关闭管道
                    pipeServer.Close();
                }

            }
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
        public static void Main(string[] args)
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
                                try
                                {
                                    //launchPage.ShowDialog();
                                    Thread.Sleep(500);//Prevent:“Object is currently in use elsewhere.”
                                    Application.Run(launchPage);
                                }
                                catch (Exception ex)
                                {
                                    Log.SaveLog($"Err when running at Program.cs: {ex}", "LaunchPage");
                                }
                                
                            }
                            else
                            {
                                Thread t = new(new ThreadStart(MainProcess));
                                t.Start();
                                Log.SaveLog("Program started with no a launch page.");
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
                            Log.SaveLog("准备创建URL Scheme...Creating URL Scheme");
                            CommandRepo.RegisterCustomURLScheme(UriName);
                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            break;
                        case "--Uninstall":
                            CommandRepo.Uninstall("Widgets.MVP");
                            Log.SaveLog("准备删除URL Scheme...Deleting URL Scheme");
                            CommandRepo.RemoveCustomURLScheme(UriName);
                            CommandRepo.Uninstall(productName);
                            break;
                        case "--RegURL":
                            Log.SaveLog("准备创建Activator URL Scheme...Creating URL Scheme");
                            CommandRepo.RegisterCustomURLScheme(UriName);
                            Log.SaveLog("teko.IO 相互科技 2024 All Right Reserved.");
                            break;
                        case "--UnRegURL":
                            Log.SaveLog("准备删除Activator URL Scheme...Deleting URL Scheme");
                            CommandRepo.RemoveCustomURLScheme(UriName);
                            break;
                        default:
                            Application.EnableVisualStyles();
                            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                            ActivatorUriProcessor a = new();
                            Directory.SetCurrentDirectory(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
                            //Process.GetCurrentProcess().StartInfo.WorkingDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().StartInfo.FileName);
                            a.Url = args[0];
                            a.TransportToMainProgram();
                            break;
                    }
                    break;

            }


        }
        
    }
}
