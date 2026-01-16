using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widgets.MVP;
using System.Windows.Forms;
using System.Drawing;

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
                
                contextMenuStrip.Show(Cursor.Position);
            });
            ni.Visible = true;
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
        }
    }
}
