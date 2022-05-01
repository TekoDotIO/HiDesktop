using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;

namespace HiDesktop
{
    internal class Program
    {
        const string productName = "HiDesktop";
        CounterBar counterBar;
        void StartView()
        {
            if (!counterBar.IsDisposed)
            {
                counterBar.ShowDialog();

            }
        }
        static void MainProcess()
        {
            Directory.CreateDirectory("./Properties/");
            string[] properties = Directory.GetFiles("./Properties/");
            foreach (string localFile in properties)
            {
                if (localFile.Contains(".properties"))
                {
                    CounterBar textBar = new CounterBar(localFile)
                    {
                        BackColor = Color.SkyBlue,
                        TransparencyKey = Color.SkyBlue
                    };
                    Program p = new Program
                    {
                        counterBar = textBar
                    };
                    Thread Counter = new Thread(new ThreadStart(p.StartView));
                    Counter.Start();
                    Log.SaveLog($"Lanunched {localFile}");
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
            switch (args.Length)//��ȡ����Ĳ���
            {
                case 0:
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
                            Log.SaveLog("Each. Tech. �໥�Ƽ� 2022 All Right Reserved.");
                            MainProcess();
                            break;
                        case "--ExitAll":
                            CommandRepo.ExitAll(productName);
                            Log.SaveLog($"Killed all {productName} process.");
                            Log.SaveLog("Each. Tech. �໥�Ƽ� 2022 All Right Reserved.");
                            break;
                        case "--Install":
                            CommandRepo.CreateStartUpScript();
                            Log.SaveLog("Each. Tech. �໥�Ƽ� 2022 All Right Reserved.");
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
