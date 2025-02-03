using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.WidgetModels.OneQuoteDataModel;

namespace Widgets.MVP.WidgetModels
{
    public partial class OneQuoteText : Form
    {
        readonly string Path;
        Hashtable AppConfig;
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public int defaultWidth = 500;
        public int defaultHeight = 250;
        public bool allowMove = false;
        public bool formatIfStyleIncorrect = true;
        public DataSourceType SrcType;
        public string DataSrc;
        public string ColorSrcIfNotExcelSrc;
        public bool random = false;
        DbDataRowObj quote;
        public UpdateMode updateMode;
        OneQuoteDbExcelProcessor dp;
        Font defaultQuoteFont;
        Font defaultAuthorFont;
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
            { "topMost", "false" },
            { "colorSrcIfNotExcelSrc", "" },
            { "radius", "auto" },
            { "formatIfStyleIncorrect", "true" },
            //{ "fontPath", "auto" } // Add this line for font path
        };


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


        public OneQuoteText(string path)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            this.Path = path;
            x = this.Width;//初始化时候的界面宽度
            y = this.Height;//初始化时候的界面高度
            setTag(this);
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
            
            // Load and apply fonts
            //ApplyFonts();

        }


        //private void ApplyFonts()
        //{
        //    try
        //    {
        //        if ((string)AppConfig["fontPath"] != "auto")
        //        {
        //            //将字体显示到控件
        //            foreach (Control item in this.Controls)
        //            {
        //                //从外部文件加载字体文件
        //                PrivateFontCollection font = new PrivateFontCollection();
        //                font.AddFontFile((string)AppConfig["fontPath"]);

        //                //定义成新的字体对象
        //                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
        //                Font myFont = new Font(myFontFamily, item.Font.Size, FontStyle.Regular);


        //                loadedFont = myFont;

        //                item.Invoke((MethodInvoker)delegate { item.Font = loadedFont; });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.SaveLog($"[{Path}]Error when applying fonts: {ex}", "OneQuote");
        //    }
        //}

        void LoadFromExcel(bool skipDate)
        {
            dp = new(DataSrc, true);

            dp.Initialize();
            var datas = dp.GetDatasFromDatas();
            for (int i = 0; i < datas.Count(); i++)
            {
                var data = datas[i];
                if (data == null)
                {
                    continue;
                }
                if (data.Ahead == "true")
                {
                    if (!skipDate)
                    {
                        if (data.Date != "" && data.Date != DateTime.Today.ToString("yyyy.MM.dd"))
                        {
                            data.Ahead = "false";
                            dp.ModifyToDataByID(data);
                        }
                        quote = data;
                    }

                }
            }

            if (quote == null)
            {
                for (int i = 0; i < datas.Count(); i++)
                {

                    var data = datas[i];
                    if (data == null)
                    {
                        continue;
                    }
                    if (data.Date == DateTime.Today.ToString("yyyy.MM.dd"))
                    {
                        if (updateMode != UpdateMode.Boot && !skipDate)
                        {

                            quote = data;
                        }

                    }
                }
            }

            CorrectFormat();



            if (quote == null)
            {
                if (random)
                {
                    List<DbDataRowObj> objs = new();
                    for (int i = 0; i < datas.Count(); i++)
                    {
                        var data = datas[i];
                        if (data == null)
                        {
                            continue;
                        }
                        if (data.Date == "")
                        {
                            objs.Add(data);
                        }
                    }
                    if (objs.Count == 0)
                    {
                        Log.SaveLog($"[{Path}]No enough quotes!", "OneQuote");
                        return;
                    }
                    Random r = new();
                    quote = objs[r.Next(0, objs.Count)];
                    if (updateMode == UpdateMode.Day && !skipDate)
                    {
                        quote.Date = DateTime.Today.ToString("yyyy.MM.dd");
                    }
                    else if (updateMode == UpdateMode.Boot && !skipDate)
                    {
                        quote.Date = DateTime.Today.AddDays(-1).ToString("yyyy.MM.dd");
                    }

                    dp.ModifyToDataByID(quote);


                    CorrectFormat();
                }
                else
                {
                    for (int i = 0; i < datas.Count(); i++)
                    {
                        var data = datas[i];
                        if (data == null)
                        {
                            continue;
                        }
                        if (data.Date == "")
                        {
                            quote = data;
                            CorrectFormat();
                            if (updateMode == UpdateMode.Day && !skipDate)
                            {
                                quote.Date = DateTime.Today.ToString("yyyy.MM.dd");
                            }
                            else if (updateMode == UpdateMode.Boot && !skipDate)
                            {
                                quote.Date = DateTime.Today.AddDays(-1).ToString("yyyy.MM.dd");
                            }
                            dp.ModifyToDataByID(quote);
                            break;
                        }
                    }
                    if (quote == null)
                    {
                        Log.SaveLog($"[{Path}]No enough quotes!", "OneQuote");
                        return;
                    }
                }
            }


            QuoteText.Font = defaultQuoteFont;
            AuthorText.Font = defaultAuthorFont;


            //Apply quote
            QuoteText.Invoke((MethodInvoker)delegate {
                QuoteText.Text = quote.Text;
            });
            //QuoteText.Text = quote.Text;
            Refresh();
            AuthorText.Invoke((MethodInvoker)delegate {
                AuthorText.Text = quote.Author;
            });
            //AuthorText.Text = quote.Author;


            //Apply fonts
            try
            {
                if (quote.TextFont != "")
                {
                    PrivateFontCollection fcText = new();
                    fcText.AddFontFile(quote.TextFont);

                    //定义成新的字体对象
                    FontFamily myFontFamily = new FontFamily(fcText.Families[0].Name, fcText);
                    Font myFont = new Font(myFontFamily, QuoteText.Font.Size, FontStyle.Regular);

                    QuoteText.Invoke((MethodInvoker)delegate {
                        QuoteText.Font = myFont;
                    });

                    //QuoteText.Font = myFont;
                }
                if (quote.AuthorFont != "")
                {
                    PrivateFontCollection atText = new();
                    atText.AddFontFile(quote.AuthorFont);
                    //定义成新的字体对象
                    FontFamily myFontFamily = new FontFamily(atText.Families[0].Name, atText);
                    Font myFont = new Font(myFontFamily, AuthorText.Font.Size, FontStyle.Regular);

                    AuthorText.Invoke((MethodInvoker)delegate {
                        AuthorText.Font = myFont;
                    });

                    //AuthorText.Font = myFont;
                }

            }
            catch (Exception ex)
            {
                Log.SaveLog($"[{Path}]Error when loading fonts:{ex}", "OneQuote");
            }

            //Apply colors
            if (quote.ColorID != "")
            {
                try
                {
                    var c = dp.GetRowFromColorByID(quote.ColorID);
                    QuoteText.ForeColor = ColorTranslator.FromHtml(c.TextColor);
                    AuthorText.ForeColor = ColorTranslator.FromHtml(c.AuthorColor);
                    BackColor = ColorTranslator.FromHtml(c.BackColor);
                    QuoteText.BackColor = Color.Transparent;
                    AuthorText.BackColor = Color.Transparent;
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"[{Path}]Error when loading colors:{ex}", "OneQuote");
                }

            }
            else
            {
                try
                {
                    var colors = dp.GetDatasFromColor();
                    Random r = new();
                    var c = colors[r.Next(0, colors.Count())];
                    QuoteText.ForeColor = ColorTranslator.FromHtml(c.TextColor);
                    AuthorText.ForeColor = ColorTranslator.FromHtml(c.AuthorColor);
                    BackColor = ColorTranslator.FromHtml(c.BackColor);
                    QuoteText.BackColor = Color.Transparent;
                    AuthorText.BackColor = Color.Transparent;
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"[{Path}]Error when loading colors:{ex}", "OneQuote");
                }
            }


        }


        private void CorrectFormat()
        {
            if (quote != null)
            {
                if (quote.Text[0] != '\"' || quote.Text[quote.Text.Length - 1] != '\"' || quote.Author[0] != '-' || quote.Author[quote.Author.Length - 1] != '-')
                {
                    if (formatIfStyleIncorrect)
                    {
                        quote.Text = "\"" + quote.Text + "\"";
                        quote.Author = "- " + quote.Author + " -";
                    }
                }
            }
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




        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (allowMove)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x0112, 0xF012, 0);
            }

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (allowMove)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x0112, 0xF012, 0);
            }
        }


        public void ReloadOneQuote()
        {
            
            AppConfig = PropertiesHelper.Load(Path);
            Log.SaveLog($"[{Path}]Loaded properties");

            if ((string)AppConfig["formatIfStyleIncorrect"] != "true")
            {
                formatIfStyleIncorrect = false;
                //return;

            }
            else
            {
                formatIfStyleIncorrect = true;
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
            float newx = (Size.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
            float newy = (Size.Height) / y;//拖动界面之后的高度与之前界面的高度之比
            setControls(newx, newy, this);//进行控件大小的伸缩变换
            defaultQuoteFont = QuoteText.Font;
            defaultAuthorFont = AuthorText.Font;

            //location
            if ((string)AppConfig["location"] == "auto")
            {
                StartPosition = FormStartPosition.CenterScreen;
                AppConfig["location"] = $"{Location.X},{Location.Y}";
                PropertiesHelper.Save(Path, AppConfig);
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
                    Log.SaveLog($"[{Path}]Error when loading location:{ex}", "OneQuote");
                    StartPosition = FormStartPosition.CenterScreen;
                    AppConfig["location"] = $"{Location.X},{Location.Y}";
                    PropertiesHelper.Save(Path, AppConfig);
                }

            }

            //dataScrType
            switch ((string)AppConfig["dataSrcType"])
            {
                case "Excel":
                    SrcType = DataSourceType.Excel;
                    break;
                default:
                    Log.SaveLog($"[{Path}]Not suppoorted src type.", "OneQuote");
                    return;
            }

            //allowMove
            if ((string)AppConfig["allowMove"] == "true")
            {
                allowMove = true;
            }
            else
            {
                allowMove = false;
            }

            //topMost
            if ((string)AppConfig["topMost"] == "true")
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }

            //random
            if ((string)AppConfig["random"] == "true")
            {
                random = true;
            }
            else
            {
                random = false;
            }

            //opacity
            Opacity = Convert.ToDouble((string)AppConfig["opacity"]);

            //dataSrc
            DataSrc = (string)AppConfig["dataSrc"];

            //colorSrcIfNotExcelSrc
            ColorSrcIfNotExcelSrc = (string)AppConfig["colorSrcIfNotExcelSrc"];

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
                    Log.SaveLog($"[{Path}]Error when loading updateBy: Invaild arg.", "OneQuote");
                    updateMode = UpdateMode.Manual;
                    break;
            }

            //asMainQuote
            if ((string)AppConfig["asMainQuote"] == "true")
            {
                if (Program.MainQuote != null)
                {
                    Log.SaveLog($"[{Path}]Threr is already a main quote registered. Skipping...", "OneQuote");
                }
                Program.MainQuote = this;
            }


            //radius
            if ((string)AppConfig["radius"] == "auto")
            {
                SetWindowRegion(Height / 7);
            }
            else
            {
                try
                {
                    SetWindowRegion(Convert.ToInt32((string)AppConfig["radius"]));
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"[{Path}]Error when loading radius:{ex}", "OneQuote");
                    SetWindowRegion(Height / 7);
                    //throw;
                }
            }

            

            Log.SaveLog($"[{Path}]Initialization accomplished.", "OneQuote");

            LoadText(false);


        }


        private void LoadText(bool skipDate)
        {
            Thread t = new(new ThreadStart(() =>
            {
                //ApplyDefaultFont();
                //Start loading: quote
                switch (SrcType)
                {
                    case DataSourceType.Excel:
                        LoadFromExcel(skipDate);

                        break;
                    default:
                        break;
                }
                // Apply fonts
                //ApplyFonts();
            }));
            t.Start();
        }


        private void OneQuoteText_Load(object sender, EventArgs e)
        {
            
            ReloadOneQuote();
            if (updateMode == UpdateMode.Day)
            {
                Thread dawnUpdater = new(new ThreadStart(() =>
                {
                    DateTime dtRecord = DateTime.Now.Date;
                    while (true)
                    {
                        if (DateTime.Now.Date != dtRecord)
                        {

                            dtRecord = DateTime.Now.Date;
                            QuoteText.Text = "是新的一天了！";
                            AuthorText.Text = "- HiDesktop V2 -";
                            quote = null;
                            ReloadOneQuote();
                        }
                        Thread.Sleep(800);
                    }
                }));
                dawnUpdater.Start();
            }

        }





        private void QuoteText_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }

        private void QuoteText_MouseUp(object sender, MouseEventArgs e)
        {
            //AppConfig["location"] = $"{Location.X},{Location.Y}";
            //PropertiesHelper.Save(Path, AppConfig);
        }

        private void AuthorText_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
        }

        private void AuthorText_MouseUp(object sender, MouseEventArgs e)
        {
            //AppConfig["location"] = $"{Location.X},{Location.Y}";
            //PropertiesHelper.Save(Path, AppConfig);
        }

        private void ReloadOneQuoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //重新加载小组件
            quote = null;
            ReloadOneQuote();

        }

        private void Menu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ChangeTextManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //换一条（临时）
            quote = null;
            this.random = true;
            LoadText(true);
            Thread.Sleep(2000);
            if (quote == null)
            {
                //MessageBox.Show("更换失败！请检查是否是所剩项目不足！", "OneQuote - HiDesktop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveLog("更换可能失败！请检查是否是所剩项目不足！", $"[OneQuote]{Path}");
            }
        }

        private void SkipTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quote != null)
            {
                quote.Date = DateTime.Today.AddDays(-1).ToString("yyyy.MM.dd");
                dp.ModifyToDataByID(quote);
            }

            quote = null;
            LoadText(true);
            Thread.Sleep(2000);
            if (quote == null)
            {
                //MessageBox.Show("更换失败！请检查是否是所剩项目不足！", "OneQuote - HiDesktop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveLog("更换可能失败！请检查是否是所剩项目不足！", $"[OneQuote]{Path}");
            }
        }

        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var colors = dp.GetDatasFromColor();
                Random r = new();
                var c = colors[r.Next(0, colors.Count())];
                QuoteText.ForeColor = ColorTranslator.FromHtml(c.TextColor);
                AuthorText.ForeColor = ColorTranslator.FromHtml(c.AuthorColor);
                BackColor = ColorTranslator.FromHtml(c.BackColor);
                QuoteText.BackColor = Color.Transparent;
                AuthorText.BackColor = Color.Transparent;
                Refresh();
            }
            catch (Exception ex)
            {
                Log.SaveLog($"[{Path}]Error when loading colors:{ex}", "OneQuote");
            }
        }


        private void SavePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppConfig["location"] = $"{Location.X},{Location.Y}";
            PropertiesHelper.Save(Path, AppConfig);
            MessageBox.Show("新的位置已保存到配置文件", "OneQuote - HiDesktop", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AuthorText_Click(object sender, EventArgs e)
        {

        }

        private void AuthorText_Click_1(object sender, EventArgs e)
        {

        }

        private void openPropertiesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filepath = System.IO.Path.GetFullPath(Path);
            Process p = new();
            p.StartInfo = new()
            {
                FileName = filepath,
                UseShellExecute = true
            };
            p.Start();
        }

        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filepath = System.IO.Path.GetFullPath(DataSrc);
            Process p = new();
            p.StartInfo = new()
            {
                FileName = filepath,
                UseShellExecute = true
            };
            p.Start();
        }
    }
}

