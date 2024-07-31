using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.WidgetModels.ActivatorDataModel;


namespace Widgets.MVP.WidgetModels
{
    public partial class ActivatorSubWindow : Form
    {
        string version = "a.db.20240731.1119";
        Hashtable AppConfig;
        Activator parentActivator;
        int radius;
        double opacity;
        int windowSize;
        bool showTimeBar;
        public bool isAwake = false;
        Color windowBackColor;
        Color windowForeColor;
        ActivatorDbContext dataScr;
        bool loadingDb = true;
        int scrW = SystemInformation.PrimaryMonitorSize.Width;
        int scrH = SystemInformation.PrimaryMonitorSize.Height;
        int maxH = SystemInformation.PrimaryMonitorMaximizedWindowSize.Height;
        Label loadingLabel;
        public ActivatorSubWindow(Hashtable appConfig, Activator activator)
        {
            //Hashtable htStandard = new()
            //{
            //    { "type" ,"Activator" },//
            //    { "enabled" ,"true" },//
            //    { "size" ,"100" },//
            //    { "topMost" ,"true" },//
            //    { "location" ,"auto" },//
            //    { "dataSource" ,"./activator.db" },
            //    { "icon" , "default" },
            //    { "windowBackColor" , "#000000" },//
            //    { "windowSize","auto" },
            //    { "allowMove","true"},//
            //    { "windowBackground","" },//
            //    { "iconSet","default"},
            //    { "rightClick",""},
            //    { "showTimeBar","true"},
            //    { "showControl","false"},
            //    { "allowHiding","false"},
            //    { "opacity","1"},//
            //    { "radius","40"},
            //    { "edge","20"},
            //    { "enableSideHide","true"},
            //    { "scrEdgeSize","50"}

            //};

            InitializeComponent();
            AppConfig = appConfig;
            parentActivator = activator;
            radius = Convert.ToInt32((string)AppConfig["windowRadius"]);
            opacity = Convert.ToDouble((string)AppConfig["opacity"]);
            Opacity = opacity;
            CheckForIllegalCrossThreadCalls = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = (string)AppConfig["topMost"] == "true";
            windowForeColor = ColorTranslator.FromHtml((string)AppConfig["windowForeColor"]);
            try
            {
                Image bm = Bitmap.FromFile((string)AppConfig["windowBackground"]);
                BackgroundImage = bm;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Unable to load back img:\n{ex}\n Will use color instead.", "Activator", false);
                windowBackColor = ColorTranslator.FromHtml((string)AppConfig["windowBackColor"]);

                BackColor = windowBackColor;
                //var defaultIcon = Properties.Resources.DefaultActivatorIcon;
                //icon = ByteToBitmap(defaultIcon);
                //throw;
            }
            if ((string)AppConfig["windowSize"] == "auto")
            {
                windowSize = scrH / 3;
            }
            else
            {
                try
                {
                    windowSize = Convert.ToInt32((string)AppConfig["windowSize"]);
                }
                catch
                {
                    windowSize = scrH / 3;
                }
            }
            Size = new Size(windowSize, showTimeBar ? windowSize / 4 * 5 : windowSize);
            SetWindowRegion(radius);
            loadingLabel = new();
            loadingLabel.Parent = this;
            loadingLabel.ForeColor = windowForeColor;
            loadingLabel.Text = "Pre-Loading database...";
            loadingLabel.Font = new Font(loadingLabel.Font.FontFamily, 10);
            loadingLabel.AutoSize = true;
            loadingLabel.Location = new Point(windowSize / 2 - loadingLabel.Size.Width / 2, windowSize / 2 - loadingLabel.Height / 2);
            loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
            Thread t = new(new ThreadStart(LoadDatabaseAsync));
            t.Start();

            //this.DoubleBuffered = true;//设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
                                                        //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //UpdateStyles();
        }

        public void CallUpForm()
        {
            Hide();
            if (parentActivator.Location.Y + Size.Height + 20 >= maxH)
            {
                Location = new Point(Location.X, parentActivator.Location.Y - Size.Height + parentActivator.Height);
            }
            else
            {
                Location = new Point(Location.X, parentActivator.Location.Y);
            }
            if (parentActivator.Location.X < scrW / 2)
            {
                Location = new Point(-Size.Width, Location.Y);
            }
            else
            {
                Location = new Point(scrW, Location.Y);
            }
            Show();
            if (parentActivator.Location.X < scrW / 2)
            {
                MathRepo.MoveWindowSmoothly_MethodA(this, parentActivator.Location.X + parentActivator.Size.Width + 10, Location.Y, 0.3, 20);
            }
            else
            {
                MathRepo.MoveWindowSmoothly_MethodA(this, parentActivator.Location.X - 10 - Size.Width, Location.Y, 0.3, 20);
            }

            if (!loadingDb)
            {
                loadingLabel.Visible = false;
            }
            isAwake = true;
        }

        async Task LoadDatabase()
        {
            try
            {
                DbProviderFactories.RegisterFactory("System.Data.SQLite.EF6", SQLiteFactory.Instance);
                if (!File.Exists((string)AppConfig["dataSource"]))
                {
                    dataScr = new((string)AppConfig["dataSource"]);
                    dataScr.Initialize();
                    await dataScr.PreloadDb();
                    dataScr.Config.Add(new ActivatorDataModel.Models.Config
                    {
                        Key = "version",
                        Value = $"{version}"
                    });
                    dataScr.SaveChanges();
                }
                dataScr = new((string)AppConfig["dataSource"]);
                await dataScr.PreloadDb();
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Fatal error: Unable to load activator database.Disabling Activator...\n{ex}", "Activator", false);
                parentActivator.Close();
                parentActivator = null;
                Close();

                throw;
            }
        }
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

        private void ActivatorSubWindow_MouseLeave(object sender, EventArgs e)
        {
            for (int i = 12; i > 0; i--)
            {
                Opacity -= 0.08;
                Thread.Sleep(1);
            }
            Hide();
            Opacity = 1;
            isAwake = false;
        }

        public void SleepForm()
        {
            for (int i = 12; i > 0; i--)
            {
                Opacity -= 0.08;
                Thread.Sleep(1);
            }
            Hide();
            Opacity = 1;
            isAwake = false;
        }

        private void ActivatorSubWindow_Load(object sender, EventArgs e)
        {
            loadingLabel.Show();
            Refresh();
            //LoadDatabaseAsync();
            
        }

        async void LoadDatabaseAsync()
        {
            await LoadDatabase();
            loadingLabel.Text = "Complete!";
            Refresh();
            Thread.Sleep(500);
            loadingDb = false;
            loadingLabel.Visible = false;
        }
    }
}
