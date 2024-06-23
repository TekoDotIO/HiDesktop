using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Widgets.MVP;

namespace HiDesktop
{
    partial class CounterBar : Form
    {

        DateTime Latest;
        readonly DateTime Target;
        readonly string Path;
        readonly Hashtable AppConfig;
        readonly string days;
        readonly string hours;
        readonly string minutes;
        readonly string seconds;
        readonly string weekdays;
        readonly int refreshTime;

        //protected override void SetVisibleCore(bool value)
        //{
        //    base.SetVisibleCore(value);
        //}

        /// <summary>
        /// 让程序不显示在alt+Tab视图窗体中
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }
        //From https://www.cnblogs.com/darkic/p/16256294.html

        public CounterBar(string Path)
        {
            TopMost = false;
            InitializeComponent();
            //SetVisibleCore(false);
            this.ShowInTaskbar = false;
            //SetFormToolWindowStyle(this);
            this.Path = Path;
            Hashtable htStandard = new Hashtable()
            {
                { "type", "CounterBar" },
                { "font", "auto" },
                { "fontSize", "30" },
                { "opacity", "1" },
                { "topMost", "true" },
                { "date", "2023.1.1" },
                { "event", "Config your countBar in properties file.." },
                { "location", "auto" },
                { "enabled","true" },
                { "countdown_frontText","To" },
                { "countdown_middleText",",there are" },
                { "count_frontText","From" },
                { "count_middleText",",there are already" },
                { "days","day(s)" },
                { "hours","hour(s)" },
                { "minutes","minute(s)" },
                { "seconds","second(s)" },
                { "weekdays","weekday(s)" },
                { "countLevel","" },
                { "refreshTime","500" },
                { "frontText_Color","#FFFFFF" },
                { "middleText_Color","#FFFFFF" },
                { "event_Color","#FF0000" },
                { "date_Color","#FF0000" },
                { "allowMove","true" },
                { "TransparencyKey","#000000" }
            };
            if (!File.Exists(Path))
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
            }
            PropertiesHelper.AutoCheck(htStandard, Path);
            AppConfig = PropertiesHelper.Load(Path);

            if ((string)AppConfig["enabled"] != "true")
            {
                Log.SaveLog($"{Path}已被禁用");
                this.Close();
                return;

            }

            if ((string)AppConfig["type"] != "CounterBar")
            {
                Log.SaveLog($"{Path}不是一个倒计时窗口的配置文件,已跳过加载.");
                this.Close();
                return;
            }
            float fontSize = Convert.ToInt32(AppConfig["fontSize"]);
            Opacity = Convert.ToDouble(AppConfig["opacity"]);
            TopMost = (string)AppConfig["topMost"] == "true";
            string[] targetStr = ((string)AppConfig["date"]).Split(".");
            EventText.Text = (string)AppConfig["event"];

            days = (string)AppConfig["days"];
            hours = (string)AppConfig["hours"];
            minutes = (string)AppConfig["minutes"];
            seconds = (string)AppConfig["seconds"];
            weekdays = (string)AppConfig["weekdays"];
            refreshTime = Convert.ToInt32((string)AppConfig["refreshTime"]);
            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            CheckForIllegalCrossThreadCalls = false;
            Latest = DateTime.Now;
            //MessageBox.Show(Latest.DayOfWeek.ToString());


            
            if ((string)AppConfig["font"] != "auto")
            {
                Setfont((string)AppConfig["font"], fontSize);
            }
            LabelNo1.Font = new Font(LabelNo1.Font.Name, fontSize);
            LabelNo2.Font = new Font(LabelNo2.Font.Name, fontSize);
            EventText.Font = new Font(EventText.Font.Name, fontSize);
            NumText.Font = new Font(NumText.Font.Name, fontSize);
            //旧版字体模块，必须安装字体才能顺利调用


            /*
            //新版字体模块，由于读取速度过慢，刷新失败导致内存重复写入。无法正常使用。暂无解决方案。
            string fontPath = (string)AppConfig["font"];
            PrivateFontCollection pfc = new();
            pfc.AddFontFile(fontPath);
            Font f = new(pfc.Families[0], fontSize);
            //将字体显示到控件
            EventText.Font = f;
            LabelNo1.Font = f;
            LabelNo2.Font = f;
            NumText.Font = f;
            */


            Target = new DateTime(Convert.ToInt32(targetStr[0]), Convert.ToInt32(targetStr[1]), Convert.ToInt32(targetStr[2]));
            if (Target > DateTime.Now)
            {
                LabelNo1.Text = (string)AppConfig["countdown_frontText"];
                LabelNo2.Text = (string)AppConfig["countdown_middleText"];
            }
            else
            {
                LabelNo1.Text = (string)AppConfig["count_frontText"];
                LabelNo2.Text = (string)AppConfig["count_middleText"];
            }



            



            LabelNo1.ForeColor = ColorTranslator.FromHtml((string)AppConfig["frontText_Color"]);
            LabelNo2.ForeColor = ColorTranslator.FromHtml((string)AppConfig["middleText_Color"]);
            EventText.ForeColor = ColorTranslator.FromHtml((string)AppConfig["event_Color"]);
            NumText.ForeColor = ColorTranslator.FromHtml((string)AppConfig["date_Color"]);
            //BackColor = Color.Transparent;
            var tColor = ColorTranslator.FromHtml((string)AppConfig["TransparencyKey"]);
            BackColor = tColor;
            TransparencyKey = tColor;


            //
            //var handle = Handle;
            //var exstyle = GetWindowLong(handle, GWL_EXSTYLE);
            //SetWindowLong(handle, GWL_EXSTYLE, new IntPtr(exstyle.ToInt32() | WS_EX_NOACTIVATE));





            LabelNo1.Location = new Point(0, 0);
            EventText.Location = new Point(LabelNo1.Location.X + LabelNo1.Size.Width, EventText.Location.Y);

            LabelNo2.Location = new Point(EventText.Location.X + EventText.Size.Width, LabelNo2.Location.Y);
            if (Target > DateTime.Now)
            {
                Countdown_UpdateTimeOnce();
            }
            else
            {
                Count_UpdateTimeOnce();
            }

            NumText.Location = new Point(LabelNo2.Location.X + LabelNo2.Size.Width, NumText.Location.Y);
            Size = new Size(NumText.Location.X + NumText.Size.Width, NumText.Size.Height);
            if ((string)AppConfig["location"] == "auto")
            {
                int startX = LabelNo1.Location.X;
                int endX = NumText.Location.X + NumText.Size.Width;
                int deltaX = endX - startX;
                Location = new Point(w / 2 - (deltaX) / 2, Location.Y);
            }
            else
            {
                try
                {
                    string Location = (string)AppConfig["location"];
                    if (Location == null)
                    {
                        int startX = LabelNo1.Location.X;
                        int endX = NumText.Location.X + NumText.Size.Width;
                        int deltaX = endX - startX;
                        this.Location = new Point(w / 2 - (deltaX) / 2, this.Location.Y);
                    }
                    else
                    {
                        this.Location = new Point(Convert.ToInt32(Location.Split(",")[0]), Convert.ToInt32(Location.Split(",")[1]));
                    }

                }
                catch
                {
                    int startX = LabelNo1.Location.X;
                    int endX = NumText.Location.X + NumText.Size.Width;
                    int deltaX = endX - startX;
                    Location = new Point(w / 2 - (deltaX) / 2, Location.Y);
                }
            }

            Thread thread;
            thread = Target > DateTime.Now ? new Thread(new ThreadStart(Countdown_UpdateTime)) : new Thread(new ThreadStart(Count_UpdateTime));
            //?:的作用相当于if else 如果?左侧是true 则执行:左侧句 否则执行右侧句
            thread.Start();

        }
        private void Countdown_UpdateTime()
        {
            while (true)
            {
                var span = Target - Latest;
                NumText.Text = $"{Math.Floor(span.TotalDays)}{days} {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}{hours} {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}{minutes} {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}{seconds}({GetDays(Target, Latest)}{weekdays}).";
                Thread.Sleep(refreshTime);
                Latest = DateTime.Now;

            }
        }
        private void Countdown_UpdateTimeOnce()
        {
            var span = Target - Latest;
            NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({GetDays(Target, Latest)}{weekdays}).";
            Thread.Sleep(refreshTime);
            Latest = DateTime.Now;
        }

        private void Count_UpdateTime()
        {
            while (true)
            {
                var span = Latest - Target;
                NumText.Text = $"{Math.Floor(span.TotalDays)}{days} {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}{hours} {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}{minutes} {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}{seconds}({GetDays(Latest, Target)}{weekdays}).";
                Thread.Sleep(refreshTime);
                Latest = DateTime.Now;

            }
        }
        private void Count_UpdateTimeOnce()
        {
            var span = Target - Latest;
            NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({GetDays(Latest, Target)}{weekdays}).";
            Thread.Sleep(500);
            Latest = DateTime.Now;
        }


        public int GetDays(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts1 = dt1 - dt2;//TimeSpan得到dt1和dt2的时间间隔
            int countday = ts1.Days;//获取两个日期间的总天数
            int weekday = 0;//工作日
                            //循环用来扣除总天数中的双休日
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = dt2.Date.AddDays(i);
                if (tempdt.DayOfWeek != DayOfWeek.Saturday && tempdt.DayOfWeek != DayOfWeek.Sunday)
                {

                    weekday++;
                }
            }
            return weekday;
        }
        public void Setfont(string path, float size)
        {
            try
            {
                //从外部文件加载字体文件
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(path);

                //定义成新的字体对象
                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                Font myFont = new Font(myFontFamily, size, FontStyle.Regular);

                //将字体显示到控件
                EventText.Font = myFont;
                LabelNo1.Font = myFont;
                LabelNo2.Font = myFont;
                NumText.Font = myFont;

            }
            catch (InvalidCastException e)
            {
                MessageBox.Show(e.Message.ToString(), "异常：", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //原文链接：https://blog.csdn.net/electricperi/article/details/8630757
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if ((string)AppConfig["allowMove"] == "true")
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x0112, 0xF012, 0);
            }

        }
        private void TextBar_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ChangeLocation(object sender, EventArgs e)
        {



        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            PropertiesHelper.Save(Path, AppConfig);
        }
        private void LabelNo1_Click(object sender, EventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            PropertiesHelper.Save(Path, AppConfig);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            PropertiesHelper.Save(Path, AppConfig);
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }



        //From https://www.cnblogs.com/pcy0/archive/2010/07/11/1775108.html
        //Modified by 幻愿Recovery at 24/06/15. Fixed up the conlict between tool window part & cannot-be-focused part.
        //[DllImport("user32.dll")]
        //public static extern Int32 GetWindowLong(IntPtr hwnd, Int32 index);
        //[DllImport("user32.dll")]
        //public static extern Int32 SetWindowLong(IntPtr hwnd, Int32 index, Int32 newValue);
        public const int GWL_EXSTYLE = (-20);
        public static void AddWindowExStyle(IntPtr hwnd, Int32 val)
        {
            int oldValue = (int)GetWindowLong(hwnd, GWL_EXSTYLE);
            if (oldValue == 0)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            if ((IntPtr)0 == SetWindowLong(hwnd, GWL_EXSTYLE, (IntPtr)(oldValue | val)))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }
        public static int WS_EX_TOOLWINDOW = 0x00000080;
        //我把这个过程封装下：
        public static void SetFormToolWindowStyle(System.Windows.Forms.Form form)
        {
            AddWindowExStyle(form.Handle, WS_EX_TOOLWINDOW);
        }


        //Code from https://www.cnblogs.com/walterlv/p/10236434.html
        #region Native Methods for FocusingLock

        private const int WS_EX_NOACTIVATE = 0x08000000;
        //private const int GWL_EXSTYLE = -20;

        public static IntPtr GetWindowLong(IntPtr hWnd, int nIndex)
        {
            return Environment.Is64BitProcess
                ? GetWindowLong64(hWnd, nIndex)
                : GetWindowLong32(hWnd, nIndex);
        }

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return Environment.Is64BitProcess
                ? SetWindowLong64(hWnd, nIndex, dwNewLong)
                : SetWindowLong32(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLong64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLong64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #endregion

    }




}
