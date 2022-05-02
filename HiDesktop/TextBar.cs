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
            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
            }
            PropertiesHelper.FixProperties(htStandard, Path);
            AppConfig = PropertiesHelper.Load(Path);

            if ((string)AppConfig["enabled"] != "true")
            {
                Log.SaveLog($"{Path}已被禁用");
                this.Close();
                return;

            }

            if ((string)AppConfig["type"] != "CounterBar")
            {
                Log.SaveLog($"{Path}不是一个文本窗口的配置文件,已跳过加载.");
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
            if ((string)AppConfig["font"] != "auto")
            {
                Setfont((string)AppConfig["font"]);
            }
            LabelNo1.ForeColor = ColorTranslator.FromHtml((string)AppConfig["color"]);
            LabelNo1.Font = new Font(LabelNo1.Font.Name, fontSize);

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
