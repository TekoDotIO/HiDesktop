using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;
using Windows.Devices.PointOfService;

namespace Widgets.MVP.WidgetModels
{
    public partial class CountdownV2 : Form
    {
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        private string countdown_frontText = "To", count_frontText = "From", event_text = "EVENT!";
        private string days = "d", hours = "hrs", minutes = "mins", seconds = "s";
        Color mainColor, sub_Color;
        WorkStyle workStyle;
        public string Path;
        int refreshTime = 900;
        public DateTime dtTarget;
        Hashtable htStandard = new Hashtable()
            {
                { "type", "CountdownV2" },
                { "font", "auto" },
                { "opacity", "1" },
                { "radius", "20" },
                { "size", "432,304" },
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


        //Part from CSDN.
        //Ori: https://blog.csdn.net/qq_27524749/article/details/102501450
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        //用来拉伸界面中的组件
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        #region 圆角矩形实现
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
        #endregion




        private void CountdownV2_Load(object sender, EventArgs e)
        {
            Log.SaveLog($"{Path} Start initializing...", "CountdownV2");
            Hashtable AppConfig = PropertiesHelper.AutoCheck(htStandard, Path);
            //topmost
            if ((string)AppConfig["topMost"] == "true") 
            {
                TopMost = true;
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

                    Location = new Point(Convert.ToInt32(((string)AppConfig["location"]).Split(",")[0]), Convert.ToInt32(((string)AppConfig["location"]).Split(",")[1]));
                }
            }
            catch (Exception ex)
            {
                Location = new Point(0, 0);
                Log.SaveLog($"{Path} Err when applying location: {ex}", "CountdownV2");
            }
            //date&time
            try
            {
                dtTarget = DateTime.Parse($"{(string)AppConfig["date"]} {(string)AppConfig["time"]}");
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying date or time: {ex}", "CountdownV2");
                //throw;
            }

            //fonts
            try
            {
                if ((string)AppConfig["date"] != "auto") 
                {
                    

                    //将字体显示到控件
                    foreach (Control item in this.Controls)
                    {
                        //从外部文件加载字体文件
                        PrivateFontCollection font = new PrivateFontCollection();
                        font.AddFontFile((string)AppConfig["font"]);

                        //定义成新的字体对象
                        FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                        Font myFont = new Font(myFontFamily, item.Font.Size, FontStyle.Regular);

                        item.Font = myFont;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying font: {ex}", "CountdownV2");
            }



            //tagsFloating
            if ((string)AppConfig["tagsFloating"] != "true")
            {
                DayLabel.Location = new Point(DayLabel.Location.X, DayDisplay.Location.Y + DayDisplay.Height - DayLabel.Height);
                HourLabel.Location = new Point(HourLabel.Location.X, HourDisplay.Location.Y + HourLabel.Height - DayLabel.Height);
                MinLabel.Location = new Point(MinLabel.Location.X, MinDisplay.Location.Y + MinDisplay.Height - MinLabel.Height);
                SecDisplay.Location = new Point(SecDisplay.Location.X, MinDisplay.Location.Y);            
            }

            var wsStr = (string)AppConfig["timeCalcLevel"];
            switch (wsStr)
            {
                case "DHMS":
                    workStyle = WorkStyle.DayHourMinSec;
                    break;
                case "DHM":
                    workStyle = WorkStyle.DayHourMin;
                    break;
                case "DH":
                    workStyle = WorkStyle.DayHour;
                    break;
                case "D":
                    workStyle = WorkStyle.Day;
                    break;
                case "HMS":
                    workStyle = WorkStyle.HourMinSec;
                    break;
                case "MS":
                    workStyle = WorkStyle.MinSec;
                    break;
                case "S":
                    workStyle = WorkStyle.Sec;
                    break;
                case "H":
                    workStyle = WorkStyle.Hour;
                    break;
                case "M":
                    workStyle = WorkStyle.Min;
                    break;
                default:
                    Log.SaveLog("Workstyle configued wrongly! Will use DHMS style.", "CounterBar", false);
                    workStyle = WorkStyle.DayHourMinSec;
                    break;
            }
            //initialize workstyle=>tags


            if ((string)AppConfig["enableGrowing"] == "true")
            {
                AutoSize = true;
                //sides????
                int border = EventLabel.Location.X;
                bool autoSizeFlag = false;
                int minWidth = 0;
                int currentTemp = 0;
                currentTemp = FrontTipLabel.Location.X + FrontTipLabel.Size.Width + border;
                if (currentTemp > Width) 
                {
                    autoSizeFlag = true;
                    if (minWidth < currentTemp) minWidth = FrontTipLabel.Location.X + FrontTipLabel.Size.Width + border;
                }
                currentTemp = EventLabel.Location.X + EventLabel.Size.Width + border;
                if (currentTemp > Width)
                {
                    autoSizeFlag = true;

                    if (minWidth < currentTemp) minWidth = EventLabel.Location.X + EventLabel.Size.Width + border;

                }
                Control[] controls = { DayLabel, HourLabel, MinLabel, DayDisplay, HourDisplay, MinDisplay, SecDisplay };

                // 遍历所有控件
                foreach (var control in controls)
                {
                    currentTemp = control.Location.X + control.Size.Width + border;
                    if (currentTemp > Width)
                    {
                        autoSizeFlag = true;
                        if (minWidth < currentTemp)
                        {
                            minWidth = currentTemp;
                        }
                    }
                }

                if (autoSizeFlag)
                {
                    Width = minWidth;
                }
            }




            days = (string)AppConfig["days"];
            hours = (string)AppConfig["hours"];
            minutes = (string)AppConfig["minutes"];
            seconds = (string)AppConfig["seconds"];
            DayLabel.Hide();
            DayDisplay.Hide();
            HourLabel.Hide();
            HourDisplay.Hide();
            MinLabel.Hide();
            MinDisplay.Hide();
            SecDisplay.Hide();
            switch (workStyle)
            {
                //case WorkStyle.DayHourMinSec:
                //    DayLabel.Text = days;
                //    HourLabel.Text = hours;
                //    MinLabel.Text = minutes;
                //    SecDisplay.Text += seconds;
                //    break;
                //case WorkStyle.DayHourMin:
                //    DayLabel.Text = days;
                //    HourLabel.Text = hours;
                //    MinLabel.Text = minutes;
                //    SecDisplay.Hide();
                //    break;
                case WorkStyle.DayHourMinSec:
                    DayLabel.Text = days;
                    DayDisplay.Text = days;
                    HourLabel.Text = hours;
                    HourDisplay.Text = hours;
                    MinLabel.Text = minutes;
                    MinDisplay.Text = minutes;
                    SecDisplay.Text = seconds;

                    // 显示所有控件
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Show(); HourDisplay.Show();
                    MinLabel.Show(); MinDisplay.Show();
                    SecDisplay.Show();
                    break;

                case WorkStyle.DayHourMin:
                    DayLabel.Text = days;
                    DayDisplay.Text = days;
                    HourLabel.Text = hours;
                    HourDisplay.Text = hours;
                    MinLabel.Text = minutes;
                    MinDisplay.Text = minutes;

                    // 显示高顺位控件，隐藏低顺位
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Show(); HourDisplay.Show();
                    MinLabel.Show(); MinDisplay.Show();
                    SecDisplay.Hide();
                    break;

                case WorkStyle.HourMinSec:
                    DayLabel.Text = hours; // Hour 占用 Day 的位置
                    DayDisplay.Text = hours;
                    HourLabel.Text = minutes; // Min 占用 Hour 的位置
                    HourDisplay.Text = minutes;
                    MinLabel.Text = seconds; // Sec 占用 Min 的位置
                    MinDisplay.Text = seconds;

                    // 显示高顺位控件，隐藏最低顺位
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Show(); HourDisplay.Show();
                    MinLabel.Show(); MinDisplay.Show();
                    SecDisplay.Hide();
                    break;

                case WorkStyle.MinSec:
                    DayLabel.Text = minutes; // Min 占用 Day 的位置
                    DayDisplay.Text = minutes;
                    HourLabel.Text = seconds; // Sec 占用 Hour 的位置
                    HourDisplay.Text = seconds;

                    // 显示高顺位控件，隐藏最低顺位
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Show(); HourDisplay.Show();
                    MinLabel.Hide(); MinDisplay.Hide();
                    SecDisplay.Hide();
                    break;

                case WorkStyle.Sec:
                    DayLabel.Text = seconds; // Sec 占用 Day 的位置
                    DayDisplay.Text = seconds;

                    // 显示最高顺位控件，隐藏其余
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Hide(); HourDisplay.Hide();
                    MinLabel.Hide(); MinDisplay.Hide();
                    SecDisplay.Hide();
                    break;

                case WorkStyle.Hour:
                    DayLabel.Text = hours; // Hour 占用 Day 的位置
                    DayDisplay.Text = hours;

                    // 显示最高顺位控件，隐藏其余
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Hide(); HourDisplay.Hide();
                    MinLabel.Hide(); MinDisplay.Hide();
                    SecDisplay.Hide();
                    break;

                case WorkStyle.Min:
                    DayLabel.Text = minutes; // Min 占用 Day 的位置
                    DayDisplay.Text = minutes;

                    // 显示最高顺位控件，隐藏其余
                    DayLabel.Show(); DayDisplay.Show();
                    HourLabel.Hide(); HourDisplay.Hide();
                    MinLabel.Hide(); MinDisplay.Hide();
                    SecDisplay.Hide();
                    break;
            }

            



            //size
            try
            {
                setTag(this);
                x = this.Width;//初始化时候的界面宽度
                y = this.Height;//初始化时候的界面高度

                if ((string)AppConfig["size"] == "auto")
                {
                    //Size = new Size(432, 304);
                }
                else
                {

                    Size targetSize = new Size(Convert.ToInt32(((string)AppConfig["size"]).Split(",")[0]), Convert.ToInt32(((string)AppConfig["size"]).Split(",")[1]));
                    Size = targetSize;
                }

                float newx = (this.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
                float newy = (this.Height) / y;//拖动界面之后的高度与之前界面的高度之比
                setControls(newx, newy, this);//进行控件大小的伸缩变换
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying size: {ex}", "CountdownV2");
                //throw;
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

            //colors
            try
            {
                var bkColor = ColorTranslator.FromHtml((string)AppConfig["back_Color"]);
                BackColor = bkColor;
                mainColor = ColorTranslator.FromHtml((string)AppConfig["main_Color"]);
                sub_Color = ColorTranslator.FromHtml((string)AppConfig["sub_Color"]);
                foreach (Control item in this.Controls)
                {
                    item.BackColor = bkColor;
                }
                EventLabel.ForeColor = ColorTranslator.FromHtml((string)AppConfig["event_Color"]);
                FrontTipLabel.ForeColor = ColorTranslator.FromHtml((string)AppConfig["frontText_Color"]);
                DayDisplay.ForeColor = mainColor;
                DayLabel.ForeColor = sub_Color;
                HourDisplay.ForeColor = mainColor;
                HourLabel.ForeColor = sub_Color;
                MinDisplay.ForeColor = mainColor;
                MinLabel.ForeColor = sub_Color;
                SecDisplay.ForeColor = mainColor;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying colors: {ex}", "CountdownV2");
                //throw;
            }

            //refreshTime
            try
            {
                refreshTime = (Convert.ToInt32((string)AppConfig["radius"]));
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{Path} Err when applying refreshTime: {ex}", "CountdownV2");
                //throw;
            }


            Thread updateThread = new(new ThreadStart(Updater));
            updateThread.Start();
        }


        public enum WorkStyle
        {
            DayHourMinSec,
            DayHourMin,
            DayHour,
            Day,
            HourMinSec,
            MinSec,
            Sec,
            Hour,
            Min,
        }

        private void Updater()
        {
            int LenDay = 0, LenHour = 0, LenMin = 0, LenSec = 0;
            bool isCountdown = true;
            if (dtTarget > DateTime.Now)
            {
                isCountdown = true;
                FrontTipLabel.Text = countdown_frontText;
            }
            else
            {
                isCountdown = false;
                FrontTipLabel.Text = count_frontText;
            }
            while (true)
            {
                //var span = DateTime.Now - DateTime.Parse("2024.10.03");
                //DayDisplay.Text = Math.Floor(span.TotalDays).ToString();
                //HourDisplay.Text = (Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24).ToString();
                //MinDisplay.Text = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();
                //SecDisplay.Text = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();
                //Thread.Sleep(900);
                if (isCountdown)
                {
                    if (dtTarget < DateTime.Now)
                    {
                        isCountdown = false;
                        FrontTipLabel.Text = count_frontText;
                    }
                }
                TimeSpan span;
                if (isCountdown)
                {
                    span = DateTime.Now - dtTarget;

                }
                else
                {
                    span = dtTarget - DateTime.Now ;

                }
                string day = "", hour = "", min = "", sec = "";
                //switch (workStyle)
                //{
                //    case WorkStyle.DayHourMinSec:
                //        day = Math.Floor(span.TotalDays).ToString();
                //        hour = (Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24).ToString();
                //        min = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();
                //        sec = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();
                //        DayDisplay.Text = day;
                //        HourDisplay.Text = hour;
                //        MinDisplay.Text = min;
                //        SecDisplay.Text = sec;
                //        break;
                //    case WorkStyle.DayHourMin:
                //        break;
                //    case WorkStyle.DayHour:
                //        break;
                //    case WorkStyle.Day:
                //        break;
                //    case WorkStyle.HourMinSec:
                //        break;
                //    case WorkStyle.MinSec:
                //        break;
                //    case WorkStyle.Sec:
                //        break;
                //    case WorkStyle.Hour:
                //        break;
                //    case WorkStyle.Min:
                //        break;
                //}

                switch (workStyle)
                {
                    case WorkStyle.DayHourMinSec:
                        day = Math.Floor(span.TotalDays).ToString();
                        hour = (Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24).ToString();
                        min = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();
                        sec = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();

                        DayDisplay.Text = day;
                        HourDisplay.Text = hour;
                        MinDisplay.Text = min;
                        SecDisplay.Text = sec;
                        break;

                    case WorkStyle.DayHourMin:
                        day = Math.Floor(span.TotalDays).ToString();
                        hour = (Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24).ToString();
                        min = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();

                        DayDisplay.Text = day;
                        HourDisplay.Text = hour;
                        MinDisplay.Text = min;

                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.DayHour:
                        day = Math.Floor(span.TotalDays).ToString();
                        hour = Math.Floor(span.TotalHours).ToString(); // 包括天数的小时总计

                        DayDisplay.Text = day;
                        HourDisplay.Text = hour;

                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.Day:
                        day = Math.Floor(span.TotalDays).ToString(); // 累计天数

                        DayDisplay.Text = day;

                        //HourDisplay.Hide();
                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.HourMinSec:
                        hour = Math.Floor(span.TotalHours).ToString();
                        min = (Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60).ToString();
                        sec = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();

                        DayDisplay.Text = hour;  // 小时作为首位
                        HourDisplay.Text = min;  // 分钟作为第二位
                        MinDisplay.Text = sec;   // 秒作为第三位

                        //SecDisplay.Hide();       // 隐藏最小单位
                        break;

                    case WorkStyle.MinSec:
                        min = Math.Floor(span.TotalMinutes).ToString(); // 累计分钟数
                        sec = (Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60).ToString();

                        DayDisplay.Text = min; // 分钟作为首位
                        HourDisplay.Text = sec; // 秒作为第二位

                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.Sec:
                        sec = Math.Floor(span.TotalSeconds).ToString(); // 累计秒数

                        DayDisplay.Text = sec;

                        //HourDisplay.Hide();
                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.Hour:
                        hour = Math.Floor(span.TotalHours).ToString(); // 累计小时数

                        DayDisplay.Text = hour;

                        //HourDisplay.Hide();
                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                    case WorkStyle.Min:
                        min = Math.Floor(span.TotalMinutes).ToString(); // 累计分钟数

                        DayDisplay.Text = min;

                        //HourDisplay.Hide();
                        //MinDisplay.Hide();
                        //SecDisplay.Hide();
                        break;

                }



                if (day.Length != LenDay || hour.Length != LenHour || min.Length != LenMin || sec.Length != LenSec)
                {
                    RelocateCompoments();
                }
                LenDay = day.Length;
                LenHour = hour.Length;
                LenMin = min.Length;
                LenSec = sec.Length;

                Thread.Sleep(refreshTime);


            }

        }



        void RelocateCompoments()
        {
            int spare = Height / 15; // 控件之间的间距
            int currentX = spare;   // 从左侧边距开始布局

            // DayDisplay 定位
            DayDisplay.Location = new Point(currentX, DayDisplay.Location.Y);
            currentX += DayDisplay.Width + spare;

            // DayLabel 定位
            DayLabel.Location = new Point(currentX, DayLabel.Location.Y);
            currentX += DayLabel.Width + spare;

            // HourDisplay 定位
            HourDisplay.Location = new Point(currentX, HourDisplay.Location.Y);
            currentX += HourDisplay.Width + spare;

            // HourLabel 定位
            HourLabel.Location = new Point(currentX, HourLabel.Location.Y);
            currentX += HourLabel.Width + spare;

            // MinDisplay 定位
            MinDisplay.Location = new Point(currentX, MinDisplay.Location.Y);
            currentX += MinDisplay.Width + spare;

            // MinLabel 定位
            MinLabel.Location = new Point(currentX, MinLabel.Location.Y);
            SecDisplay.Location = new Point(currentX, MinLabel.Location.Y);
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
