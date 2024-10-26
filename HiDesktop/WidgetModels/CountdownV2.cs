using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP.WidgetModels
{
    public partial class CountdownV2 : Form
    {
        public string Path;
        Hashtable htStandard = new Hashtable()
            {
                { "type", "CountdownV2" },
                { "font", "auto" },
                { "opacity", "1" },
                { "radius", "20" },
                { "size", "304" },
                { "enableGrowing", "false" },
                { "topMost", "false" },
                { "tagsFloating", "true" },
                { "date", "2025.1.1" },
                { "time","00:00:00" },
                { "event", "EVENT!" },
                { "location", "auto" },
                { "enabled","true" },
                { "countdown_frontText","To" },
                { "count_frontText","From" },
                { "days","d" },
                { "hours","hr" },
                { "minutes","min" },
                { "seconds","" },
                { "refreshTime","500" },
                { "frontText_Color","#000000" },
                { "main_Color","#000000" },
                { "sub_Color","#696969" },
                { "event_Color","#faadac" },
                { "back_Color","#fcdfe5" },
                { "allowMove","true" },
                { "timeCalcLevel", "DHMS" }
            };
        public CountdownV2(string Path)
        {
            InitializeComponent();
            this.Path = Path;
            StartPosition = FormStartPosition.Manual;
        }

        //From https://www.cnblogs.com/darkic/p/16256294.html
        /// <summary>
        /// 设置窗体的Region
        /// </summary>
        public void SetWindowRegion(int radius)
        {
            GraphicsPath FormPath;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, radius);
            this.Region = new Region(FormPath);

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        private void CountdownV2_Load(object sender, EventArgs e)
        {
            Log.SaveLog($"{Path} Start initializing...", "CountdownV2");
            Hashtable AppConfig = PropertiesHelper.AutoCheck(htStandard, Path);
            //topmost
            if ((string)AppConfig["topMost"] == "true") 
            {
                TopMost = true;
            }
            //radius
            try
            {
                SetWindowRegion(Convert.ToInt32((string)AppConfig["radius"]));
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying radius: {ex}", "CountdownV2");
                //throw;
            }
            //opacity
            try
            {
                Opacity = Convert.ToDouble((string)AppConfig["opacity"]);
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying opacity: {ex}", "CountdownV2");
                throw;
            }
            //location
            try
            {
                if ((string)AppConfig["location"] == "auto")
                {
                    Location = new Point(0, 0);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Thread updateThread = new(new ThreadStart(Updater));
            updateThread.Start();
        }

        private void Updater()
        {
            
            while (true)
            {
                var span = DateTime.Now - DateTime.Parse("2024.10.03");
                DayDisplay.Text = Math.Floor(span.TotalDays).ToString();
                HourDisplay.Text = (Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24).ToString();
                MinDisplay.Text = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();
                SecDisplay.Text = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();
                Thread.Sleep(900);
            }
            
        }







        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                cp.ExStyle |= 0x02000000;//解决闪屏问题，来自 https://blog.csdn.net/weixin_38211198/article/details/90724952

                return cp;
            }
        }
        //From https://www.cnblogs.com/darkic/p/16256294.html
    }
}
