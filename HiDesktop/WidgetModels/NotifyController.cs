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
            ni.Visible = true;
            ni.ShowBalloonTip(3, "NotifyController started", "HiDesktop launching...!", ToolTipIcon.Info);
        }
    }
}
