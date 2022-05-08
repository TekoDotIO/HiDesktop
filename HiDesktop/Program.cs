using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HiDesktop.Widgets.MVP
{
    internal class Program
    {
        const string productName = "HiDesktop";
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
            foreach (string localFile in properties)
            {
                if (localFile.Contains(".properties"))
                {
                    Hashtable config = PropertiesHelper.Load(localFile);
                    switch ((string)config["type"])
                    {
                        case "CounterBar":
                            Program p = new Program();
                            if ((string)config["enabled"] == "true")
                            {
                                CounterBar textBar = new CounterBar(localFile)
                                {
                                    BackColor = Color.SkyBlue,
                                    TransparencyKey = Color.SkyBlue
                                };
                                p.widgets.Add(localFile, textBar);
                                p.nowFile = localFile;
                                Thread View = new Thread(new ThreadStart(p.StartView));
                                View.Start();
                                Log.SaveLog($"Lanunched {localFile}");
                            }
                            else
                            {
                                Log.SaveLog($"Program:\"{localFile}\" is not enabled.");
                            }
                            break;
                        case "TextBar":
                            p = new Program();
                            if ((string)config["enabled"] == "true")
                            {
                                TextBar textBar = new TextBar(localFile)
                                {
                                    BackColor = Color.SkyBlue,
                                    TransparencyKey = Color.SkyBlue
                                };
                                p.widgets.Add(localFile, textBar);
                                p.nowFile = localFile;
                                Thread Counter = new Thread(new ThreadStart(p.StartView));
                                Counter.Start();
                                Log.SaveLog($"Lanunched {localFile}");
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
            Log.SaveLog("Lanunched all.");
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {


            switch (args.Length)//读取传入的参数
            {
                case 0:
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
                            Application.Run(new LaunchPage());
                            break;
                        case "--ExitAll":
                            CommandRepo.ExitAll(productName);
                            Log.SaveLog($"Killed all {productName} process.");
                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
                            break;
                        case "--Install":
                            CommandRepo.CreateStartUpScript();
                            Log.SaveLog("Each. Tech. 相互科技 2022 All Right Reserved.");
                            break;
                        case "--Uninstall":
                            CommandRepo.Uninstall(productName);
                            break;
                    }
                    break;

            }


        }
    }
}
