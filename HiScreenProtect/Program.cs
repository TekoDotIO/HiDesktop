using HiDesktop.HiScreenProtect.MVP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenProtect.MVP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Thread.Sleep(500);
                    if (CommandRepo.IsMultiProcess("ScreenProtect.MVP"))
                    {
                        Log.SaveLog("One or more HiDesktop.Widgets.MVP is already running.Exiting...");
                        return;
                    }
                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ScreenProtector());
                    break;
                case 1:
                    switch (args[0])
                    {
                        case "/S":
                        case "/s":
                        case "/p":
                        case "/P":
                            Application.SetHighDpiMode(HighDpiMode.SystemAware);
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new ScreenProtector());
                            break;
                        case "/c":
                        case "/C":
                            Process.Start("./config.properties");
                            break;
                        default:
                            MessageBox.Show(args[0]);
                            break;
                    }
                    
                    break;
                default:
                    
                    switch (args[0])
                    {
                        case "/S":
                        case "/s":
                        case "/p":
                        case "/P":
                            Application.SetHighDpiMode(HighDpiMode.SystemAware);
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new ScreenProtector());
                            break;
                        case "/c":
                        case "/C":
                            Process.Start("./config.properties");
                            break;
                        default:
                            if (args[0].Contains("/p") || args[0].Contains("/P")) 
                            {
                                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                                Application.EnableVisualStyles();
                                Application.SetCompatibleTextRenderingDefault(false);
                                Application.Run(new ScreenProtector());
                            }
                            else
                            {
                                if (args[0].Contains("/c") || args[0].Contains("/C"))
                                {
                                    Process.Start("./config.properties");
                                }
                                else
                                {
                                    string arg = "";
                                    foreach (string argText in args)
                                    {
                                        arg += $" {argText}";
                                    }
                                    MessageBox.Show(arg);
                                }
                                
                            }
                            
                            break;
                    }

                    break;
            }
            
        }
    }
}
