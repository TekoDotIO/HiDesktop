using HiDesktop.Widgets.MVP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HiDesktop
{
    public partial class TextBar : Form
    {
        readonly string Path;
        readonly Hashtable AppConfig;
        public TextBar(string Path)
        {
            InitializeComponent();
            this.Path = Path;
            Hashtable htStandard = new Hashtable()
            {
                { "type", "TextBar" },
                { "font", "auto" },
                { "fontSize", "30" },
                { "opacity", "1" },
                { "topMost", "true" },
                { "text", "Configue your textBar in properties file.." },
                { "location", "auto" },
                { "enabled","true" },
                { "color","#FFFFFF" },
                { "allowMove","true" }
            };
            if (!File.Exists(Path))
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
                Log.SaveLog($"[{Path}]Created properties file.");

            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
                
            }
            PropertiesHelper.FixProperties(htStandard, Path);
            Log.SaveLog($"[{Path}]Repaired properties");
            AppConfig = PropertiesHelper.Load(Path);
            Log.SaveLog($"[{Path}]Loaded properties");
            if ((string)AppConfig["enabled"] != "true")
            {
                Log.SaveLog($"[{Path}]{Path} is not enabled.");
                this.Close();
                return;

            }

            if ((string)AppConfig["type"] != "TextBar")
            {
                Log.SaveLog($"[{Path}]{Path} is not a properties file of TextBar.");
                this.Close();
                return;
            }

            float fontSize = Convert.ToInt32(AppConfig["fontSize"]);
            Log.SaveLog($"[{Path}]FontSize set.");
            Opacity = Convert.ToDouble(AppConfig["opacity"]);
            Log.SaveLog($"[{Path}]Opacity set.");
            if ((string)AppConfig["topMost"] == "true")
            {
                TopMost = true;
                Log.SaveLog($"[{Path}]TopMost.");
            }
            else
            {
                TopMost = false;
                Log.SaveLog($"[{Path}]Not TopMost.");
            }
            if ((string)AppConfig["font"] != "auto")
            {
                Setfont((string)AppConfig["font"]);
            }
            LabelNo1.ForeColor = ColorTranslator.FromHtml((string)AppConfig["color"]);
            LabelNo1.Font = new Font(LabelNo1.Font.Name, fontSize);
            LabelNo1.Text = (string)AppConfig["text"];


            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            LabelNo1.Location = new Point(0, 0);
            Size = new Size(LabelNo1.Location.X + LabelNo1.Size.Width, LabelNo1.Size.Height);
            if ((string)AppConfig["location"] == "auto")
            {
                int startX = LabelNo1.Location.X;
                int endX = LabelNo1.Location.X + LabelNo1.Size.Width;
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
                        int endX = LabelNo1.Location.X + LabelNo1.Size.Width;
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
                    int endX = LabelNo1.Location.X + LabelNo1.Size.Width;
                    int deltaX = endX - startX;
                    Location = new Point(w / 2 - (deltaX) / 2, Location.Y);
                }
            }

        }

        public void Setfont(string path)
        {
            try
            {
                //从外部文件加载字体文件
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(path);

                //定义成新的字体对象
                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                Font myFont = new Font(myFontFamily, 56F, FontStyle.Regular);

                //将字体显示到控件
                LabelNo1.Font = myFont;

            }
            catch (InvalidCastException e)
            {
                MessageBox.Show(e.Message.ToString(), "异常：", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

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
    }
}
