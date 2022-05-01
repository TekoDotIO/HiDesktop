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
            switch (args.Length)//��ȡ����Ĳ���
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
                //������Ϊ1��,���ڶ������
                case 1:
                    //��ȡ��һ������
                    switch (args[0])
                    {
                        case "--ExitAll":
                            var Running = Process.GetProcessesByName("AutoFileBAK");
                            //��ȡ������ΪAutoFileBAK�Ľ���
                            var ThisID = Process.GetCurrentProcess().Id;
                            //��ȡ��ǰ����ID,��ֹ�Լ������Լ����½������򲻳���
                            foreach (Process process in Running)//Ϊÿ��ʶ�𵽵Ľ����ظ�
                            {
                                if (ThisID != process.Id)//��ֹ���н���
                                {
                                    process.Kill();//�����˽���
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
