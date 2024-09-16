using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP.WidgetModels
{
    public partial class OneQuoteText : Form
    {
        readonly string Path;
        readonly Hashtable AppConfig;
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public int defaultWidth = 500;
        public int defaultHeight = 250;
        public bool allowMove = false;
        public DataSourceType SrcType;
        public string DataSrc;
        public bool random = false;
        public UpdateMode updateMode;
        public enum UpdateMode
        {
            Day,
            Manual,
            Boot
        }
        public enum DataSourceType
        {
            Excel
        }
        public Hashtable htStandard = new()
        {
            { "type", "OneQuote" },
            { "enabled", "true" },
            { "size", "auto" },
            { "location", "auto" },
            { "dataSrcType", "Excel" },
            { "dataSrc", "./OneQuoteText.xlsx" },
            { "asMainQuote", "true" },
            { "random", "false" },
            { "allowMove", "true" },
            { "updateBy", "day" },//day/manual/boot
            { "opacity", "1" },
            { "topMost", "false" }
        };
        public OneQuoteText(string path)
        {
            InitializeComponent();
            this.Path = path;
            x = this.Width;//初始化时候的界面宽度
            y = this.Height;//初始化时候的界面高度
            //Log.SaveLog("Start initializing...");
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
            PropertiesHelper.AutoCheck(htStandard, Path);
            Log.SaveLog($"[{Path}]Repaired properties");
            AppConfig = PropertiesHelper.Load(Path);
            Log.SaveLog($"[{Path}]Loaded properties");
            if ((string)AppConfig["enabled"] != "true")
            {
                Log.SaveLog($"[{Path}]{Path} is not enabled.");
                this.Close();
                return;

            }

            if ((string)AppConfig["type"] != "OneQuote")
            {
                Log.SaveLog($"[{Path}]{Path} is not a properties file of OneQuote.");
                this.Close();
                return;
            }

            //size
            if ((string)AppConfig["size"] == "auto") 
            {
                var h = SystemInformation.PrimaryMonitorSize.Height;
                this.Size = new Size(Convert.ToInt32(h / 3.5), h / 7);
            }
            else
            {
                var wh = Convert.ToInt32((string)AppConfig["size"]);
                Size = new Size(wh * 2, wh);
            }

            //location
            if ((string)AppConfig["location"] == "auto") 
            {
                StartPosition = FormStartPosition.CenterScreen;
                AppConfig["location"] = $"{Location.X},{Location.Y}";
                PropertiesHelper.Save(path, AppConfig);
            }
            else
            {
                try
                {
                    StartPosition = FormStartPosition.Manual;
                    var temp = ((string)AppConfig["location"]).Split(",");
                    Location = new Point(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]));
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"[{path}]Error when loading location:{ex}", "OneQuote");
                    StartPosition = FormStartPosition.CenterScreen;
                    AppConfig["location"] = $"{Location.X},{Location.Y}";
                    PropertiesHelper.Save(path, AppConfig);
                }

            }

            //dataScrType
            switch ((string)AppConfig["dataSrcType"])
            {
                case "Excel":
                    SrcType = DataSourceType.Excel;
                    break;
                default:
                    Log.SaveLog($"[{path}]Not suppoorted src type.", "OneQuote");
                    return;
            }

            //allowMove
            if ((string)AppConfig["allowMove"] == "true")
            {
                allowMove = true;
            }

            //topMost
            if ((string)AppConfig["topMost"] == "true") 
            {
                TopMost = true;
            }

            //random
            if ((string)AppConfig["random"] == "true")
            {
                random = true;
            }

            //opacity
            Opacity = Convert.ToDouble((string)AppConfig["opacity"]);

            //dataSrc
            DataSrc = (string)AppConfig["dataSrc"];

            //updateBy
            switch ((string)AppConfig["updateBy"])
            {
                case "day":
                    updateMode = UpdateMode.Day;
                    break;
                case "manual":
                    updateMode = UpdateMode.Manual;
                    break;
                case "boot":
                    updateMode = UpdateMode.Boot;
                    break;
                default:
                    Log.SaveLog($"[{path}]Error when loading updateBy: Invaild arg.", "OneQuote");
                    updateMode = UpdateMode.Manual;
                    break;
            }

            //asMainQuote
            if ((string)AppConfig["asMainQuote"] == "true")
            {
                Program.MainQuote = this;
            }

            Log.SaveLog($"[{path}]Initialization accomplished.", "OneQuote");





        }

        private void OneQuoteText_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
            float newy = (this.Height) / y;//拖动界面之后的高度与之前界面的高度之比
            setControls(newx, newy, this);//进行控件大小的伸缩变换
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
    }
}
