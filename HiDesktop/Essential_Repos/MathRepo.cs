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
        public static void MoveWindowSmoothly_MethodA(Form f, int targetX, int targetY, double tm, int timeDelay)
        {
            //f.TopMost = true;
            double t = 0;
            double xm = targetX - f.Location.X;
            double ym = targetY - f.Location.Y;
            ArrayList lx = new();
            ArrayList ly = new();
            var startX = f.Location.X;
            var startY = f.Location.Y;
            while (t < tm) 
            {
                //var x = -xm * (tm / 2 - t) / (2 * Math.Abs(tm / 2 - t)) + xm / 2 + (tm / 2 - t) * 4 * xm * Math.Pow(-Math.Abs(t - tm / 2) + tm / 2, 2) / (Math.Abs(tm / 2 - t) * 2 * Math.Pow(tm, 2));
                
                lx.Add(SmoothMovePhysicFormula(startX, targetX, tm, t));
                //var y = -ym * (tm / 2 - t) / (2 * Math.Abs(tm / 2 - t)) + ym / 2 + (tm / 2 - t) * 4 * ym * Math.Pow(-Math.Abs(t - tm / 2) + tm / 2, 2) / (Math.Abs(tm / 2 - t) * 2 * Math.Pow(tm, 2));
                ly.Add(SmoothMovePhysicFormula(startY, targetY, tm, t));
                t += Convert.ToDouble(timeDelay) / 1000;
            }
            for (int i = 0; i < ly.Count; i++)
            {
                f.Location = new Point(Convert.ToInt32(lx[i]), Convert.ToInt32(ly[i]));
                Thread.Sleep(timeDelay);
            }
        }
        public static double SmoothMovePhysicFormula(double start, double end, double maxt, double t)
        {
            var delta = end - start;
            var x = -delta * (maxt / 2 - t) / (2 * Math.Abs(maxt / 2 - t)) + delta / 2 + (maxt / 2 - t) * 4 * delta * Math.Pow(-Math.Abs(t - maxt / 2) + maxt / 2, 2) / (Math.Abs(maxt / 2 - t) * 2 * Math.Pow(maxt, 2)); 
            return x + start;
        }
        public static ArrayList CreatePhysicalSmoothMovePointsSet(double start, double end, double max, double step)
        {
            double t = 0;
            ArrayList pointsSet = new();
            while (t < max) 
            {
                pointsSet.Add(SmoothMovePhysicFormula(start, end, max, t));
                t += step;
            }
            return pointsSet;
        }
    }
}
