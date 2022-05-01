using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiDesktop
{
    internal class Program
    {
        CounterBar counterBar;
        void StartView()
        {
            if (!counterBar.IsDisposed)
            {
                counterBar.ShowDialog();

            }
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
                    
                    string[] properties = Directory.GetFiles("./");
                    foreach (string localFile in properties)
                    {
                        if (localFile.Contains(".properties"))
                        {
                            CounterBar textBar = new CounterBar(localFile)
                            {
                                BackColor = Color.SkyBlue,
                                TransparencyKey = Color.SkyBlue
                            };
                            Program p = new Program();
                            p.counterBar = textBar;
                            Thread Counter = new Thread(new ThreadStart(p.StartView));
                            Counter.Start();
                            Log.SaveLog($"Lanunched {localFile}");
                        }

                    }
                    Log.SaveLog("Lanunched all.");
                    break;
                //当参数为1个,存在多种情况
                case 1:
                    //读取第一个参数
                    switch (args[0])
                    {
                        case "--ExitAll":
                            var Running = Process.GetProcessesByName("AutoFileBAK");
                            //获取所有名为AutoFileBAK的进程
                            var ThisID = Process.GetCurrentProcess().Id;
                            //获取当前进程ID,防止自己结束自己导致结束程序不彻底
                            foreach (Process process in Running)//为每个识别到的进程重复
                            {
                                if (ThisID != process.Id)//防止自行结束
                                {
                                    process.Kill();//结束此进程
                                    Log.SaveLog("Killed process:" + process.Id);
                                }
                            }
                            Log.SaveLog("Killed all AutoFileBAK process.");
                            break;
                    }
                    break;
            }
            
            
        }
    }
}
