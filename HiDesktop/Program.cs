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
            CounterBar textBar = new CounterBar("./JuniorExam.properties")
            {
                BackColor = Color.SkyBlue,
                TransparencyKey = Color.SkyBlue
            };
            textBar.ShowDialog();

        }
    }
}
