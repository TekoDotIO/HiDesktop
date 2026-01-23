using HiDesktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP.WidgetModels
{
    public class NotifyController
    {
        public static NotifyIcon ActivatedNotifyController;
        public static void BuildNotifyController()
        {
            ActivatedNotifyController = new();
            ActivatedNotifyController.Icon = new Icon(@"./Resources/app.ico");
            
            //ni.ShowBalloonTip(3, "NotifyController started", "HiDesktop launching...!", ToolTipIcon.Info);
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            MenuItems mi = new(contextMenuStrip);
            ActivatedNotifyController.ContextMenuStrip = contextMenuStrip;
            ActivatedNotifyController.Text = "HiDesktop - Main process\nRunning...";
            ActivatedNotifyController.Click += new EventHandler((object sender, EventArgs e) =>
            {
                mi.RefreshWidgetsList();
                contextMenuStrip.Show(Cursor.Position);
            });
            ActivatedNotifyController.Visible = true;
            mi.RefreshWidgetsList();
            Application.Run();
        }


        public class MenuItems
        {
            ToolStripMenuItem ExitAllMenuItem;
            ToolStripMenuItem ExitSingleMenuItem;
            ToolStripMenuItem EnterManagePageMenuItem;
            ToolStripMenuItem OpenPanelMenuItem;
            ToolStripMenuItem RestartMenuItem;
            public MenuItems(ContextMenuStrip contextMenuStrip)
            {
                ExitAllMenuItem = new ToolStripMenuItem();
                ExitAllMenuItem.Text = "退出程序";
                ExitAllMenuItem.Click += new EventHandler((sender, e) =>
                {
                    Log.SaveLog("Exiting Application after notifyController calling.");
                    CommandRepo.ExitSelf(Program.productName);
                    CommandRepo.ExitSelf("Widgets.MVP");
                    
                });
                ExitSingleMenuItem = new ToolStripMenuItem();
                ExitSingleMenuItem.Text = "退出指定小组件...";
                //EnterManagePageMenuItem = new ToolStripMenuItem();
                //EnterManagePageMenuItem.Text = "打开特定组件设置...";
                //OpenPanelMenuItem = new ToolStripMenuItem();
                //OpenPanelMenuItem.Text = "打开管理面板";
                RestartMenuItem = new ToolStripMenuItem();
                RestartMenuItem.Text = "重新启动程序";
                RestartMenuItem.Click += new EventHandler((sender, e) =>
                {
                    Process newProcess = new();
                    string CdPath = Directory.GetCurrentDirectory();
                    string ThisFile = Process.GetCurrentProcess().MainModule.FileName;
                    string path = ThisFile;
                    newProcess.StartInfo.FileName = path;
                    newProcess.StartInfo.WorkingDirectory = CdPath;
                    newProcess.Start();
                    Log.SaveLog("Exiting to restart...");
                    CommandRepo.ExitSelf(Program.productName);
                    CommandRepo.ExitSelf("Widgets.MVP");
                });

                contextMenuStrip.Items.AddRange(
                [
                    ExitAllMenuItem,
                    ExitSingleMenuItem, 
                    //EnterManagePageMenuItem,
                    //OpenPanelMenuItem, 
                    RestartMenuItem,
                ]);
            }
            public void RefreshWidgetsList()
            {
                ExitSingleMenuItem.DropDownItems.Clear();
                //EnterManagePageMenuItem.DropDownItems.Clear();
                var widgetsListForExiting = new ToolStripMenuItem[Program.ActivatedProgram.ActivatedWidgets.Count];
                int i = 0;
                foreach (string item in Program.ActivatedProgram.ActivatedWidgets.Keys)
                {
                    string shortName = Path.GetFileNameWithoutExtension(item);
                    widgetsListForExiting[i] = new ToolStripMenuItem(shortName);
                    widgetsListForExiting[i].Click += new EventHandler((sender, e) =>
                    {
                        switch (Program.ActivatedProgram.ActivatedWidgetsTypeRepo[item])
                        {
                            case "Activator":
                                ((Activator)Program.ActivatedProgram.ActivatedWidgets[item]).Exit();
                                Log.SaveLog($"Activator widget \"{item}\" is exiting after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            case "CountdownV2":
                                ((CountdownV2)Program.ActivatedProgram.ActivatedWidgets[item]).Exit();
                                Log.SaveLog($"CountdownV2 widget \"{item}\" is exiting after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            case "CounterBar":
                                ((CounterBar)Program.ActivatedProgram.ActivatedWidgets[item]).Exit();
                                Log.SaveLog($"CounterBar widget \"{item}\" is exiting after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            case "TextBar":
                                
                                ((TextBar)Program.ActivatedProgram.ActivatedWidgets[item]).Exit();
                                Log.SaveLog($"TextBar widget \"{item}\" is exiting after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            case "OneQuote":
                                ((OneQuoteText)Program.ActivatedProgram.ActivatedWidgets[item]).Exit();
                                Log.SaveLog($"OneQuote widget \"{item}\" is exiting after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            default:
                                MessageBox.Show("Invalid action.无效小组件。出错的组件将被移除。", "运行时错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Log.SaveLog($"Error: invalid widget {item} removed after notifyController call.");
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                        }
                        if (Program.ActivatedProgram.ActivatedWidgets.Count == 0) 
                        {
                            if ((string)AppInfo.ApplicationConfig["notifyIcon.sendNotifyAfterAllDisposed"] == "true") 
                            {
                                if ((string)AppInfo.ApplicationConfig["notifyIcon.autoExitApp"] == "true")
                                {
                                    ActivatedNotifyController.ShowBalloonTip(5, "HiDesktop - 运行时正在退出", "小组件已全部关闭，正在退出应用程序...", ToolTipIcon.Info);
                                    ActivatedNotifyController.Dispose();
                                    Log.SaveLog("Exiting app after disposing final widget.");
                                    CommandRepo.ExitSelf(Program.productName);
                                    CommandRepo.ExitSelf("Widgets.MVP");
                                }
                                else
                                {
                                    Log.SaveLog("Disposed final widget. AppInfo instructed to keep running...");
                                    ActivatedNotifyController.ShowBalloonTip(5, "HiDesktop - 运行时警告", "可供显示的小组件已全部关闭。请注意，按照您的设置，程序仍在后台继续运行。", ToolTipIcon.Warning);
                                }
                            }
                            else if ((string)AppInfo.ApplicationConfig["notifyIcon.autoExitApp"] == "true")
                            {
                                Log.SaveLog("Disposed final widget. AppInfo instructed to exit mutely...");
                                CommandRepo.ExitSelf(Program.productName);
                                CommandRepo.ExitSelf("Widgets.MVP");
                            }
                           
                        }
                    });
                    i++;
                }
                ExitSingleMenuItem.DropDownItems.AddRange(widgetsListForExiting);

                var widgetsListForManager = new ToolStripMenuItem[Program.ActivatedProgram.ActivatedWidgets.Count];
                i = 0;
                foreach (string item in Program.ActivatedProgram.ActivatedWidgets.Keys)
                {
                    string shortName = Path.GetFileNameWithoutExtension(item);
                    widgetsListForManager[i] = new ToolStripMenuItem(shortName);
                    i++;
                }
                //EnterManagePageMenuItem.DropDownItems.AddRange(widgetsListForManager);
            }
        }
    }
}
