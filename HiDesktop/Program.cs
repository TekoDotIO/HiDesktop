using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiDesktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TextBar textBar = new TextBar();
            textBar.Opacity = 0.5;
            textBar.BackColor = Color.SkyBlue;
            textBar.TransparencyKey = Color.SkyBlue;
            textBar.ShowDialog();

        }
    }
}
