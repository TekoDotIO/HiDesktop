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
using Windows.Services.Maps;
using System.IO.Pipes;
using System.Text;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP
{
    internal class Program
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
                var r = MessageBox.Show($"������ִ��ʱ������������\n����ĳ���ģ������ֹ�����Ƕ���ɵĲ����ʾǸ�⡣\n\n��������û����뽫����Ϣ����־��Ϣ�����������ߣ�\n\n�쳣�ļ���{nowFile}\n�쳣��Ϣ��{e}\n\n����������Ե��Գ����������ǡ�����������˳���ǰģ�飬�������񡱡�", $"HiDesktop - �쳣����{nowFile}", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

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
            
            bool enableHook = false;//�����������ҳ��
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
                                    launchPage.ProcessText.Text = $"����ɹ�����:{localFile}";
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
                                    launchPage.ProcessText.Text = $"����ɹ�����:{localFile}";
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
                                    launchPage.ProcessText.Text = $"Activator����ɹ�����:{localFile}";
                                }
                                activatedActivator = activator;
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
            Log.SaveLog("Start building Pipes service...");
            while (true)
            {
                // ���������ܵ�����
                string pipeName = "ActivatorUriTransPipe";

                // ���������ܵ�������
                NamedPipeServerStream pipeServer = new NamedPipeServerStream(
                    pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte);

                Log.SaveLog("Waiting for connection", pipeName, false);

                // �ȴ�����
                pipeServer.WaitForConnection();

                Log.SaveLog("Connected.", pipeName, false);

                try
                {
                    // ��������
                    byte[] buffer = new byte[4096];
                    int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);

                    // �����յ����ֽ�ת��Ϊ�ַ���
                    string receivedString = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    Log.SaveLog("Received URI: " + receivedString, pipeName, false);

                    // ��������Ը��ݽ��յ������ݽ�����Ӧ����
                    // ���磬�������ݣ�������Ӧ�ĺ�����
                    ActivatorUriProcessor a = new();
                    a.Url = receivedString;
                    a.Process();

                    // �ظ���Ϣ��Ӧ�ó���B
                    byte[] responseBuffer = Encoding.UTF8.GetBytes("DONE");
                    pipeServer.Write(responseBuffer, 0, responseBuffer.Length);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"Error occurs at pipe server or command system: \n{ex}");
                    var r = MessageBox.Show($"ͨ��URL���ݵ�����ִ��ʱ������������\n����ĳ���ģ������ֹ�����Ƕ���ɵĲ����ʾǸ�⡣\n\n��������û����뽫����Ϣ����־��Ϣ�����������ߣ�\n\n�쳣�ļ���-\n�쳣��Ϣ��{ex}\n\n����������Ե��Գ����������ǡ�����������˳���ǰģ�飬�������񡱡�", $"HiDesktop - �쳣����URL Schemeģ��", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (r == DialogResult.Yes) 
                    {
                        throw;
                    }
                }
                finally
                {
                    // �رչܵ�
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

            switch (args.Length)//��ȡ����Ĳ���
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
                //������Ϊ1��,���ڶ������
                case 1:
                    //��ȡ��һ������
                    switch (args[0])
                    {
                        case "--MainProcess":

                            Log.SaveLog("teko.IO �໥�Ƽ� 2024 All Right Reserved.");
                            Application.EnableVisualStyles();
                            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                            if (showBootWindow)
                            {
                                if (waitForEffects)
                                {
                                    Thread.Sleep(500);//�ȴ�������Ӧ
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
                            Log.SaveLog("teko.IO �໥�Ƽ� 2024 All Right Reserved.");
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
                            Log.SaveLog("teko.IO �໥�Ƽ� 2024 All Right Reserved.");
                            break;
                        case "--Install":
                            CommandRepo.CreateStartUpScript();
                            Log.SaveLog("׼������URL Scheme...Creating URL Scheme");
                            CommandRepo.RegisterCustomURLScheme(UriName);
                            Log.SaveLog("teko.IO �໥�Ƽ� 2024 All Right Reserved.");
                            break;
                        case "--Uninstall":
                            CommandRepo.Uninstall("Widgets.MVP");
                            Log.SaveLog("׼��ɾ��URL Scheme...Deleting URL Scheme");
                            CommandRepo.RemoveCustomURLScheme(UriName);
                            CommandRepo.Uninstall(productName);
                            break;
                        case "--RegURL":
                            Log.SaveLog("׼������Activator URL Scheme...Creating URL Scheme");
                            CommandRepo.RegisterCustomURLScheme(UriName);
                            Log.SaveLog("teko.IO �໥�Ƽ� 2024 All Right Reserved.");
                            break;
                        case "--UnRegURL":
                            Log.SaveLog("׼��ɾ��Activator URL Scheme...Deleting URL Scheme");
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
