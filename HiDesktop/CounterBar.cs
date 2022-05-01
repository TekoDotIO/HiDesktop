using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HiDesktop
{
    public partial class CounterBar : Form
    {

        DateTime Latest;
        readonly DateTime Target;
        readonly string Path;
        readonly Hashtable AppConfig;
        public CounterBar(string Path)
        {
            TopMost = false;
            InitializeComponent();
            this.Path = Path;
            Hashtable htStandard = new Hashtable()
            {
                { "type", "CounterBar" },
                { "font", "auto" },
                { "fontSize", "30" },
                { "opacity", "1" },
                { "topMost", "true" },
                { "date", "2023.1.1" },
                { "event", "Configue your countBar in properties file.." },
                { "location", "auto" },
                { "enabled","true" }
            };
            if (!File.Exists(Path))
            {
                Hashtable Config = htStandard;
                Properties.Save(Path, Config);
            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                Properties.Save(Path, Config);
            }
            Properties.FixProperties(htStandard, Path);
            AppConfig = Properties.Load(Path);

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
            if ((string)AppConfig["topMost"] == "true")
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
            string[] targetStr = ((string)AppConfig["date"]).Split(".");
            EventText.Text = (string)AppConfig["event"];
            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            CheckForIllegalCrossThreadCalls = false;
            Latest = DateTime.Now;
            //MessageBox.Show(Latest.DayOfWeek.ToString());

            if ((string)AppConfig["font"] != "auto")
            {
                Setfont((string)AppConfig["font"]);
            }


            Target = new DateTime(Convert.ToInt32(targetStr[0]), Convert.ToInt32(targetStr[1]), Convert.ToInt32(targetStr[2]));
            LabelNo1.Font = new Font(LabelNo1.Font.Name, fontSize);
            LabelNo2.Font = new Font(LabelNo2.Font.Name, fontSize);
            EventText.Font = new Font(EventText.Font.Name, fontSize);
            NumText.Font = new Font(NumText.Font.Name, fontSize);



            LabelNo1.ForeColor = Color.White;
            LabelNo2.ForeColor = Color.White;
            EventText.ForeColor = Color.Red;
            NumText.ForeColor = Color.Red;
            LabelNo1.Location = new Point(0, 0);
            EventText.Location = new Point(LabelNo1.Location.X + LabelNo1.Size.Width, EventText.Location.Y);

            LabelNo2.Location = new Point(EventText.Location.X + EventText.Size.Width, LabelNo2.Location.Y);
            UpdateTimeOnce();
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

            Thread thread = new Thread(new ThreadStart(UpdateTime));
            thread.Start();

        }
        private void UpdateTime()
        {
            while (true)
            {
                var span = Target - Latest;
                NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({GetDays(Target, Latest)}个工作日).";
                Thread.Sleep(500);
                Latest = DateTime.Now;

            }
        }
        private void UpdateTimeOnce()
        {
            var span = Target - Latest;
            NumText.Text = $"{Math.Floor(span.TotalDays)}天 {Math.Floor(span.TotalHours) - Math.Floor(span.TotalDays) * 24}小时 {Math.Floor(span.TotalMinutes) - Math.Floor(span.TotalHours) * 60}分钟 {Math.Floor(span.TotalSeconds) - Math.Floor(span.TotalMinutes) * 60}秒({GetDays(Target, Latest)}个工作日).";
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
        public void Setfont(string path)
        {
            string AppPath = Application.StartupPath;
            try
            {
                //从外部文件加载字体文件
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(AppPath + path);

                //定义成新的字体对象
                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                Font myFont = new Font(myFontFamily, 56F, FontStyle.Regular);

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
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
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
            Properties.Save(Path, AppConfig);
        }
        private void LabelNo1_Click(object sender, EventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            Properties.Save(Path, AppConfig);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            Properties.Save(Path, AppConfig);
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }
    }
}
