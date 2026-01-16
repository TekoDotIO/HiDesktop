using System;
using System.Collections.Generic;
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
        public static void BuildNotifyController()
        {
            NotifyIcon ni = new();
            ni.Icon = new Icon(@"./Resources/app.ico");
            
            //ni.ShowBalloonTip(3, "NotifyController started", "HiDesktop launching...!", ToolTipIcon.Info);
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            MenuItems mi = new(contextMenuStrip);
            ni.ContextMenuStrip = contextMenuStrip;
            ni.Text = "HiDesktop - Main process\nRunning...";
            ni.Click += new EventHandler((object sender, EventArgs e) =>
            {
                mi.RefreshWidgetsList();
                contextMenuStrip.Show(Cursor.Position);
            });
            ni.Visible = true;
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
                    
                    CommandRepo.ExitSelf(Program.productName);
                    CommandRepo.ExitSelf("Widgets.MVP");
                });
                ExitSingleMenuItem = new ToolStripMenuItem();
                ExitSingleMenuItem.Text = "退出指定小组件...";
                EnterManagePageMenuItem = new ToolStripMenuItem();
                EnterManagePageMenuItem.Text = "打开特定组件设置...";
                OpenPanelMenuItem = new ToolStripMenuItem();
                OpenPanelMenuItem.Text = "打开管理面板";
                RestartMenuItem = new ToolStripMenuItem();
                RestartMenuItem.Text = "重新启动程序";
                
                contextMenuStrip.Items.AddRange(
                [
                    ExitAllMenuItem,
                    ExitSingleMenuItem, 
                    EnterManagePageMenuItem,
                    OpenPanelMenuItem, 
                    RestartMenuItem,
                ]);
            }
            public void RefreshWidgetsList()
            {
                ExitSingleMenuItem.DropDownItems.Clear();
                EnterManagePageMenuItem.DropDownItems.Clear();
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
                                Program.ActivatedProgram.ActivatedWidgets.Remove(item);
                                break;
                            default:
                                break;
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
                EnterManagePageMenuItem.DropDownItems.AddRange(widgetsListForManager);
            }
        }
    }
}
