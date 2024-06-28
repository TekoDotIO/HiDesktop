using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Widgets.MVP.Essential_Repos
{
    internal class MathRepo
    {
        public static void MoveWindowSmoothly_MethodA(Form f, int targetX, int targetY, double tm)
        {
            double t = 0;
            double xm = targetX - f.Location.X;
            double ym = targetY - f.Location.Y;
            ArrayList lx = new();
            ArrayList ly = new();
            while (t < tm) 
            {
                var x = -xm * (tm / 2 - t) / (2 * Math.Abs(tm / 2 - t)) + xm / 2 + (tm / 2 - t) * 4 * xm * Math.Pow(-Math.Abs(t - tm / 2) + tm / 2, 2) / (Math.Abs(tm / 2 - t) * 2 * Math.Pow(tm, 2));
                lx.Add(x + f.Location.X);
                var y = -ym * (tm / 2 - t) / (2 * Math.Abs(tm / 2 - t)) + ym / 2 + (tm / 2 - t) * 4 * ym * Math.Pow(-Math.Abs(t - tm / 2) + tm / 2, 2) / (Math.Abs(tm / 2 - t) * 2 * Math.Pow(tm, 2));
                ly.Add(y + f.Location.Y);
                t += 0.02;
            }
            for (int i = 0; i < ly.Count; i++)
            {
                f.Location = new Point(Convert.ToInt32(lx[i]), Convert.ToInt32(ly[i]));
                Thread.Sleep(2);
            }
        }
    }
}
