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
        #region Resources
        /// <summary>
        /// 数据库版本信息
        /// </summary>
        string version = "a.db.20240731.1119";
        /// <summary>
        /// 下一页图标
        /// </summary>
        Image nxtPage;
        /// <summary>
        /// 上一页图标
        /// </summary>
        Image lstPage;
        /// <summary>
        /// 下一页（已禁用）
        /// </summary>
        Image nxtPage_Disabled;
        /// <summary>
        /// 上一页（已禁用）
        /// </summary>
        Image lstPage_Disabled;
        #endregion

        #region Config
        public bool stopNextAwake = false;
        /// <summary>
        /// 应用配置文件
        /// </summary>
        Hashtable AppConfig;
        /// <summary>
        /// 窗体圆弧角度记忆值
        /// </summary>
        int radius;
        /// <summary>
        /// 窗体透明度记忆值
        /// </summary>
        double opacity;
        /// <summary>
        /// 窗体大小记忆值
        /// </summary>
        int windowSize;
        /// <summary>
        /// 是否显示时间
        /// </summary>
        bool showTimeBar;
        /// <summary>
        /// （状态机）窗体是否被唤醒
        /// </summary>
        public bool isAwake = false;
        /// <summary>
        /// 窗体背景颜色
        /// </summary>
        Color windowBackColor;
        /// <summary>
        /// 窗体内容颜色
        /// </summary>
        Color windowForeColor;
        /// <summary>
        /// 用户数据库上下文
        /// </summary>
        ActivatorDbContext dataScr;
        /// <summary>
        /// （状态机）是否正在加载数据库
        /// </summary>
        bool loadingDb = true;
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        int scrW = SystemInformation.PrimaryMonitorSize.Width;
        /// <summary>
        /// 屏幕高度
        /// </summary>
        int scrH = SystemInformation.PrimaryMonitorSize.Height;
        /// <summary>
        /// 窗体最大高度
        /// </summary>
        int maxH = SystemInformation.PrimaryMonitorMaximizedWindowSize.Height;
        /// <summary>
        /// 添加元素图标
        /// </summary>
        Image AddElementIcon;
        #endregion

        #region Controls
        /// <summary>
        /// 绑定的Activator
        /// </summary>
        Activator parentActivator;
        /// <summary>
        /// 加载文本
        /// </summary>
        Label loadingLabel;
        /// <summary>
        /// 组件图标
        /// </summary>
        PictureBox itemIcon1, itemIcon2, itemIcon3, itemIcon4;

        PictureBox[] itemIcons = new PictureBox[4];

        /// <summary>
        /// 组件描述
        /// </summary>
        Label itemTxt1, itemTxt2, itemTxt3, itemTxt4;

        Label[] itemTxts = new Label[4];

        /// <summary>
        /// 时间显示
        /// </summary>
        Label timeBar;
        /// <summary>
        /// 页码显示
        /// </summary>
        Label pageDisplay;
        /// <summary>
        /// 翻页操作图标
        /// </summary>
        PictureBox lastPage, nextPage;
        #endregion





        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

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
            //CheckForIllegalCrossThreadCalls = false;
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
            Log.SaveLog("StartInitialize : loadingLabel", "ActivatorSubWindow", output: false);
            SetWindowRegion(radius);
            loadingLabel = new();
            loadingLabel.Parent = this;
            loadingLabel.ForeColor = windowForeColor;
            loadingLabel.Text = "Pre-Loading database...";
            loadingLabel.Font = new Font(loadingLabel.Font.FontFamily, 10);
            loadingLabel.AutoSize = true;
            loadingLabel.Location = new Point(windowSize / 2 - loadingLabel.Size.Width / 2, windowSize / 2 - loadingLabel.Height / 2);
            //loadingLabel.TextAlign = ContentAlignment.MiddleCenter;
            loadingLabel.Visible = false;
            Log.SaveLog("Initialized : loadingLabel", "ActivatorSubWindow", output: false);
            Thread t = new(new ThreadStart(LoadDatabasePreload));
            Log.SaveLog("StartInitialize : Database EF construction", "ActivatorSubWindow", output: false);
            t.Start();
            //Log.SaveLog("StartInitialize : ", "ActivatorSubWindow", output: false);
            //Thread checkStatu = new(new ThreadStart(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(100);
            //        if (!loadingDb)
            //        {
            //            InitializeControls();
            //            return;
            //        }
            //    }
            //}));
            //checkStatu.Start();

            Log.SaveLog("StartInitialize : Resources", "ActivatorSubWindow", output: false);
            try
            {
                lstPage = Bitmap.FromFile("./Resources/lstPg.png");
                nxtPage = Bitmap.FromFile("./Resources/nxtPg.png");
                lstPage_Disabled = Bitmap.FromFile("./Resources/lstPg_Disabled.png");
                nxtPage_Disabled = Bitmap.FromFile("./Resources/nxtPg_Disabled.png");
                AddElementIcon = Bitmap.FromFile("./Resources/AddElement.png");
            }
            catch (Exception ex)
            {
                Log.SaveLog($"App package has been illegaly modified! Cannot load default activator icon:\n{ex}", "ActivatorSubWindow", false);
            }
            Log.SaveLog("StartInitialize : PageIcons", "ActivatorSubWindow", output: false);
            lastPage = new PictureBox();
            nextPage = new PictureBox();
            lastPage.Image = lstPage_Disabled;
            nextPage.Image = nxtPage;
            lastPage.Parent = this;
            nextPage.Parent = this;
            lastPage.Size = new Size(Width / 9, Width / 9);
            nextPage.Size = new Size(Width / 9, Width / 9);
            lastPage.SizeMode = PictureBoxSizeMode.StretchImage;
            nextPage.SizeMode = PictureBoxSizeMode.StretchImage;
            lastPage.Location = new Point(Convert.ToInt32(Width - Width * 0.618 - lastPage.Width), Width / 20);
            nextPage.Location = new Point(Convert.ToInt32(Width * 0.618), Width / 20);
            lastPage.Click += (object sender, EventArgs e) =>
            {
                lastPage.Hide();
            };
            lastPage.Visible = false;
            nextPage.Visible = false;
            lastPage.Hide();
            nextPage.Hide();
            Log.SaveLog("StartInitialize : pageDisplay", "ActivatorSubWindow", output: false);
            pageDisplay = new Label()
            {
                ForeColor = windowForeColor,
                Parent = this,
                //AutoSize = true,
                Visible = false,
                Size = new Size(nextPage.Location.X - (lastPage.Location.X + lastPage.Width), lastPage.Height),
                Location = new Point(lastPage.Location.X + lastPage.Width, lastPage.Location.Y),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font.FontFamily, lastPage.Width / 5),
                Text = "Page / Page"
            };

            itemIcon1 = new();
            itemIcon2 = new();
            itemIcon3 = new();
            itemIcon4 = new();
            itemIcons[0] = itemIcon1;
            itemIcons[1] = itemIcon2;
            itemIcons[2] = itemIcon3;
            itemIcons[3] = itemIcon4;
            foreach (var item in itemIcons)
            {
                item.Parent = this;
                item.SizeMode = PictureBoxSizeMode.StretchImage;
                item.Hide();
                item.Size = new Size(windowSize / 7 * 2, windowSize / 7 * 2);
                item.Image = AddElementIcon;
            }

            itemIcon1.Location = new Point(windowSize / 7, windowSize / 7);
            itemIcon2.Location = new Point(windowSize / 7 * 5, windowSize / 7);
            itemIcon3.Location = new Point(windowSize / 7, windowSize / 7 * 5);
            itemIcon4.Location = new Point(windowSize / 7 * 5, windowSize / 7 * 5);

            itemTxt1 = new();
            itemTxt2 = new();
            itemTxt3 = new();
            itemTxt4 = new();
            itemTxts[0] = itemTxt1;
            itemTxts[1] = itemTxt2;
            itemTxts[2] = itemTxt3;
            itemTxts[3] = itemTxt4;
            foreach (var item in itemTxts)
            {
                item.Parent = this;
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.ForeColor = windowForeColor;
                item.Font = new Font(Font.FontFamily, lastPage.Width / 5);
                item.Text = "新增...";
                item.Size = new Size(itemIcon1.Width, lastPage.Width / 5);
                item.Hide();
            }
            itemTxt1.Location = new Point(windowSize / 7, windowSize / 7 + itemIcon1.Height + windowSize / 50);
            itemTxt2.Location = new Point(windowSize / 7 * 5, windowSize / 7 + itemIcon2.Height + windowSize / 50);
            itemTxt3.Location = new Point(windowSize / 7, windowSize / 7 * 5 + itemIcon3.Height + windowSize / 50);
            itemTxt4.Location = new Point(windowSize / 7 * 5, windowSize / 7 * 5 + itemIcon4.Height + windowSize / 50);


            Log.SaveLog("SubWindow Initialization completed...", "ActivatorSubWindow", false);




            //this.DoubleBuffered = true;//设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //UpdateStyles();
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

        public void CallUpForm()
        {
            if (stopNextAwake)
            {
                stopNextAwake = false;
                return;
            }
            Hide();
            loadingLabel.Visible = false;
            foreach (Control control in Controls)
            {
                control.Hide();
            }
            Refresh();
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
                InitializeControls();
            }
            else
            {
                loadingLabel.Visible = true;
            }
            isAwake = true;
        }

        void InitializeControls()
        {
            //RecreateHandle();
            //Initialize Controls...
            nextPage.Visible = true;
            lastPage.Visible = true;
            nextPage.Show();
            lastPage.Show();
            //nextPage.Refresh();
            //lastPage.Refresh();
            nextPage.BringToFront();
            lastPage.BringToFront();
            pageDisplay.Visible = true;
            pageDisplay.Show();
            pageDisplay.BringToFront();

            foreach (var item in itemIcons)
            {
                item.Show();
                item.Visible = true;
                item.BringToFront();
            }
            foreach (var item in itemTxts)
            {
                item.Show();
                item.Visible = true;
                item.BringToFront();
            }

            Refresh();
            //Log.SaveLog("Test.");

        }

        void LoadDatabase()
        {
            try
            {
                DbProviderFactories.RegisterFactory("System.Data.SQLite.EF6", SQLiteFactory.Instance);
                if (!File.Exists((string)AppConfig["dataSource"]))
                {
                    Log.SaveLog("Database not exists. Creating...", "ActivatorSubWindow", output: false);
                    dataScr = new((string)AppConfig["dataSource"]);
                    dataScr.Initialize();
                    Log.SaveLog("Start database preload...", "ActivatorSubWindow", output: false);
                    dataScr.PreloadDb();
                    Log.SaveLog("Database preloaded.", "ActivatorSubWindow", output: false);
                    dataScr.Config.Add(new ActivatorDataModel.Models.Config
                    {
                        Key = "version",
                        Value = $"{version}"
                    });
                    dataScr.SaveChanges();
                }
                dataScr = new((string)AppConfig["dataSource"]);
                Log.SaveLog("Start database preload...", "ActivatorSubWindow", output: false);
                dataScr.PreloadDb();
                Log.SaveLog("Database preloaded.", "ActivatorSubWindow", output: false);
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Fatal error: Unable to load activator database.Disabling Activator...\n{ex}", "ActivatorSubWindow", false);
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
            var p = Control.MousePosition;
            if (!((Location.X <= p.X && p.X <= Location.X + Size.Width) && (Location.Y <= p.Y && p.Y <= Location.Y + Size.Height)))
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

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")] //导入user32.dll函数库
        public static extern bool GetCursorPos(out System.Drawing.Point lpPoint);//获取鼠标坐标

        private Point GetMousePos()
        {
            System.Drawing.Point mp = new System.Drawing.Point();
            GetCursorPos(out mp);
            int mousex = mp.X; //鼠标当前X坐标
            int mousey = mp.Y; //鼠标当前Y坐标
            return new Point(mousex, mousey);
        }

        private void ActivatorSubWindow_Load(object sender, EventArgs e)
        {
            loadingLabel.Show();
            Refresh();
            //LoadDatabaseAsync();

        }

        void LoadDatabasePreload()
        {
            LoadDatabase();
            loadingLabel.Text = "Please re-open form!";
            loadingLabel.Location = new Point(windowSize / 2 - loadingLabel.Size.Width / 2, windowSize / 2 - loadingLabel.Height / 2);
            Refresh();
            Thread.Sleep(1000);
            loadingDb = false;

            Hide();
            //CallUpForm();
            //loadingLabel.Visible = false;
            //InitializeControls();
        }

        private void ActivatorSubWindow_Deactivate(object sender, EventArgs e)
        {
            //if (!isAwake)
            //{
            //    return;
            //}
            //var p = Control.MousePosition;
            //MessageBox.Show($"{p.X},{p.Y},{Location.X},{Location.X + Size.Width},{Location.Y},{Location.Y + Size.Height}");
            //if (!((Location.X <= p.X && p.X <= Location.X + Size.Width) && (Location.Y <= p.Y && p.Y <= Location.Y + Size.Height)))
            //{
            //    isAwake = false;
            //    for (int i = 12; i > 0; i--)
            //    {
            //        Opacity -= 0.08;
            //        Thread.Sleep(1);
            //    }
            //    Hide();
            //    Opacity = 1;

            //}

            SleepForm();
            stopNextAwake = true;
        }

        private void ActivatorSubWindow_Layout(object sender, LayoutEventArgs e)
        {

        }
    }
}
