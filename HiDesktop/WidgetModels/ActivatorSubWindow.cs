﻿using System;
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
using System.Runtime.InteropServices;
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
        /// <summary>
        /// 添加元素图标
        /// </summary>
        Image EmptyElementIcon;
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
        public ActivatorDbContext dataScr;
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
        /// 预加载提示
        /// </summary>
        string preloadTips;
        /// <summary>
        /// 预加载后窗口关闭提示
        /// </summary>
        string closingTips;
        /// <summary>
        /// 空项文本
        /// </summary>
        string emptyItemTxt;
        /// <summary>
        /// 空项图标
        /// </summary>
        string emptyItemIcon;

        /// <summary>
        /// 空项操作
        /// </summary>
        string emptyItemAction;
        /// <summary>
        /// 当前页码
        /// </summary>
        int currentPageNum = 1;
        /// <summary>
        /// 是否已创建句柄
        /// </summary>
        public bool hasBooted = false;
        /// <summary>
        /// 假如占满是否多页
        /// </summary>
        bool additionalPageIfFull = true;
        bool enableAutoClose = true;
        #endregion

        #region Controls

        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
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



        //[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        //public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //public const uint SWP_NOMOVE = 0x2;
        //public const uint SWP_NOSIZE = 0x1;
        //public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        //public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

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
            x = this.Width;//初始化时候的界面宽度
            y = this.Height;//初始化时候的界面高度
            setTag(this);
            AppConfig = appConfig;
            parentActivator = activator;
            radius = Convert.ToInt32((string)AppConfig["windowRadius"]);
            opacity = Convert.ToDouble((string)AppConfig["opacity"]);
            additionalPageIfFull = (string)AppConfig["additionalPageIfFull"] == "true";
            //Opacity = opacity;
            //CheckForIllegalCrossThreadCalls = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = (string)AppConfig["topMost"] == "true";
            //if (TopMost)
            //{
            //    SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);//窗口置顶
            //}
            windowForeColor = ColorTranslator.FromHtml((string)AppConfig["windowForeColor"]);
            showTimeBar = (string)AppConfig["showTimeBar"] == "true";
            try
            {
                Image bm = Bitmap.FromFile((string)AppConfig["windowBackground"]);
                BackgroundImage = bm;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Unable to load back img:\n{ex}\n Will use color instead.", "ActivatorSubWindow", false);
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
            Size = new Size(windowSize, showTimeBar ? windowSize / 7 * 8 : windowSize);
            Log.SaveLog("StartInitialize : loadingLabel", "ActivatorSubWindow", output: false);
            SetWindowRegion(radius);

            preloadTips = (string)AppConfig["preloadTips"];
            closingTips = (string)AppConfig["closingTips"];
            emptyItemTxt = (string)AppConfig["emptyItemTxt"];
            emptyItemIcon = (string)AppConfig["emptyItemIcon"];
            emptyItemAction = (string)AppConfig["emptyItemAction"];

            loadingLabel = new();
            loadingLabel.Parent = this;
            loadingLabel.ForeColor = windowForeColor;
            loadingLabel.Text = preloadTips;
            // loadingDb.Text = "Ciallo~";
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
                if (emptyItemIcon!="default")
                {
                    EmptyElementIcon = Bitmap.FromFile(emptyItemIcon);
                }
                else
                {
                    EmptyElementIcon = Bitmap.FromFile("./Resources/AddElement.png");
                }
                
            }
            catch (Exception ex)
            {
                
                try
                {
                    EmptyElementIcon = Bitmap.FromFile("./Resources/AddElement.png");

                }
                catch (Exception ex2)
                {
                    Log.SaveLog($"App package has been illegaly modified! Cannot load default activator icon:\n{ex2}", "ActivatorSubWindow", false);
                }
                Log.SaveLog($"Error when loading emptyItemIcon:\n{ex}", "ActivatorSubWindow", false);
            }
            try
            {
                lstPage = Bitmap.FromFile("./Resources/lstPg.png");
                nxtPage = Bitmap.FromFile("./Resources/nxtPg.png");
                lstPage_Disabled = Bitmap.FromFile("./Resources/lstPg_Disabled.png");
                nxtPage_Disabled = Bitmap.FromFile("./Resources/nxtPg_Disabled.png");

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
            lastPage.Location = new Point(Convert.ToInt32(Width - Width * 0.618 - lastPage.Width), Width / 40);
            nextPage.Location = new Point(Convert.ToInt32(Width * 0.618), Width / 40);
            lastPage.Click += (object sender, EventArgs e) =>
            {
                LastPage();
            };
            nextPage.Click += (object sender, EventArgs e) =>
            {
                NextPage();
            };

            //Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
            //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);

            //lastPage.Visible = false;
            //nextPage.Visible = false;
            //lastPage.Hide();
            //nextPage.Hide();
            Log.SaveLog("StartInitialize : pageDisplay", "ActivatorSubWindow", output: false);


            //float newx = (this.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
            //float newy = (this.Height) / y;//拖动界面之后的高度与之前界面的高度之比


            pageDisplay = new Label()
            {
                ForeColor = windowForeColor,
                Parent = this,
                //AutoSize = true,
                //Visible = false,
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
            int i = 0;
            foreach (var item in itemIcons)
            {
                item.Parent = this;
                item.SizeMode = PictureBoxSizeMode.StretchImage;
                //item.Hide();
                item.Size = new Size(windowSize / 7 * 2, windowSize / 7 * 2);
                item.Image = EmptyElementIcon;
                OperationHandler oh = new(i);
                item.Click += oh.ExecuteOperation;
                i += 1;
            }

            itemIcon1.Location = new Point(windowSize / 7, windowSize / 7);
            itemIcon2.Location = new Point(windowSize / 7 * 4, windowSize / 7);
            itemIcon3.Location = new Point(windowSize / 7, windowSize / 7 * 4);
            itemIcon4.Location = new Point(windowSize / 7 * 4, windowSize / 7 * 4);

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
                item.Font = new Font(Font.Name, Font.Size * (this.Width) / x, Font.Style, Font.Unit);
                item.Text = emptyItemTxt;
                item.Size = new Size(itemIcon1.Width, lastPage.Width / 5 * 4);
                //item.Hide();
            }
            itemTxt1.Location = new Point(windowSize / 7, windowSize / 7 + itemIcon1.Height + windowSize / 50);
            itemTxt2.Location = new Point(windowSize / 7 * 4, windowSize / 7 + itemIcon2.Height + windowSize / 50);
            itemTxt3.Location = new Point(windowSize / 7, windowSize / 7 * 4 + itemIcon3.Height + windowSize / 50);
            itemTxt4.Location = new Point(windowSize / 7 * 4, windowSize / 7 * 4 + itemIcon4.Height + windowSize / 50);

            itemIcon1.Click += ElementNo1Click;
            itemIcon2.Click += ElementNo2Click;
            itemIcon3.Click += ElementNo3Click;
            itemIcon4.Click += ElementNo4Click;

            //if (showTimeBar)
            //{
            //    timeBar = new Label()
            //    {
            //        ForeColor = windowForeColor,
            //        Parent = this,
            //        AutoSize = true,
            //        //Visible = false,
            //        //TextAlign = ContentAlignment.MiddleCenter,
            //        Font = new Font(Font.FontFamily, lastPage.Width / 5),
            //        Text = DateTime.Now.ToString("HH:mm")
            //    };
            //    timeBar.AutoSize = false;
            //    timeBar.Location = new Point(windowSize / 7 * 6 + windowSize / 7 / 2 + timeBar.Size.Height / 2, windowSize / 20);
            //    Thread timeUpdateThread = new(new ThreadStart(() =>
            //    {
            //        this.Invoke((MethodInvoker)delegate ()
            //        {
            //            this.Refresh();
            //            timeBar.Text = DateTime.Now.ToString("HH:mm");
            //            Thread.Sleep(1000);
            //        });
            //    }));
            //    timeUpdateThread.Start();
            //}



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
            if (!enableAutoClose) return;
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
            hasBooted = true;
        }

        void InitializeControls()
        {
            if (showTimeBar)
            {
                timeBar.Show();
            }
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
            var c = dataScr.Repo.Count();
            if (c == 0)
            {
                foreach (var item in itemIcons)
                {
                    item.Image = EmptyElementIcon;
                }
                foreach (var item in itemTxts)
                {
                    item.Text = emptyItemTxt;
                }
                nextPage.Image = nxtPage_Disabled;
                lastPage.Image = lstPage_Disabled;
                pageDisplay.Text = "0 / 0";
            }
            else
            {
                LoadElements();

            }
            


        }

        void ElementNo1Click(object sender, EventArgs e)
        {
            BootElementAction(1);
        }

        void ElementNo2Click(object sender, EventArgs e)
        {
            BootElementAction(2);
        }
        void ElementNo3Click(object sender, EventArgs e)
        {
            BootElementAction(3);
        }
        void ElementNo4Click(object sender, EventArgs e)
        {
            BootElementAction(4);
        }

        void BootElementAction(int num)
        {
            enableAutoClose = false;
            int ID = currentPageNum * 4 - 4 + num;
            var elementEfObj = dataScr.Repo.FirstOrDefault(p => p.ID == ID);
            string action = emptyItemAction;
            if (elementEfObj != null)
            {
                action = elementEfObj.Action;
            }
            else
            {
                action = emptyItemAction + $"&id={ID}";
            }
            try
            {
                ActivatorUriProcessor a = new(action);
                a.Process();
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Error when processing element action from db. At Loc:Page={currentPageNum},ElementID={num}.(Db ID={ID}),\nEx:\n{ex}","ActivatorSubWindow");
                MessageBox.Show($"Error when processing element action from db. At Loc:Page={currentPageNum},ElementID={num}.(Db ID={ID}),\nEx:\n{ex}", "ActivatorSubWindow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
            enableAutoClose = true;
        }

        void LoadElements()
        {
            var maxID = dataScr.Repo.Max(p => p.ID);
            var maxPageID = Math.Ceiling(((Convert.ToDouble(maxID) + (additionalPageIfFull ? 1 : 0)) / 4));
            if (currentPageNum == 1)
            {
                lastPage.Image = lstPage_Disabled;
            }
            else
            {
                lastPage.Image = lstPage;
            }
            if (currentPageNum == maxPageID)
            {
                nextPage.Image = nxtPage_Disabled;
            }
            else
            {
                nextPage.Image = nxtPage;
            }
            pageDisplay.Text = $"{currentPageNum} / {maxPageID}";
            for (int i = 0; i < 4; i++)
            {
                var obj = dataScr.Repo.FirstOrDefault(p => p.ID == currentPageNum * 4 - 3 + i);
                if (obj == null) 
                {
                    itemTxts[i].Text = emptyItemTxt;
                    itemIcons[i].Image = EmptyElementIcon;
                }
                else
                {
                    try
                    {
                        Image image = Bitmap.FromFile(obj.Icon);
                        string description = obj.Description;
                        itemTxts[i].Text = description;
                        itemIcons[i].Image = image;
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog($"Error when loading element at page {currentPageNum} obj {i}.Ex:\n{ex}", "ActivatorSubWindow");
                        //throw;
                    }
                }
            }
        }

        void NextPage()
        {
            if (nextPage.Image != nxtPage_Disabled)
            {
                currentPageNum += 1;
                
            }
            LoadElements();
        }

        void LastPage()
        {
            if (lastPage.Image != lstPage_Disabled)
            {
                currentPageNum -= 1;
            }
            LoadElements();
        }

        class OperationHandler
        {
            public int ID { get; set; }
            public OperationHandler(int id)
            {
                ID = id;
            }
            public void ExecuteOperation(object sender, EventArgs e)
            {

            }
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
                else
                {
                    dataScr = new((string)AppConfig["dataSource"]);
                    Log.SaveLog("Start database preload...", "ActivatorSubWindow", output: false);
                    dataScr.PreloadDb();
                    Log.SaveLog("Database preloaded.", "ActivatorSubWindow", output: false);
                }

                
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
            if (!enableAutoClose) return;
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
            if (showTimeBar)
            {
                timeBar = new Label()
                {
                    ForeColor = windowForeColor,
                    Parent = this,
                    AutoSize = true,
                    ////Visible = false,
                    ////TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(Font.FontFamily, pageDisplay.Font.Size),
                    Text = DateTime.Now.ToString("HH:mm")
                };
                setTag(timeBar);
                //timeBar.Hide();
                //timeBar.AutoSize = false;
                timeBar.Location = new Point(windowSize / 10, windowSize + windowSize / 7 / 2 - timeBar.Size.Height);
                //在启用全局缩放的情况下无需对timeBar的FontSize和Location进行设置。

                float newx = (Size.Width) / x;//拖动界面之后的宽度与之前界面的宽度之比
                float newy = (Size.Height) / y;//拖动界面之后的高度与之前界面的高度之比
                setControls(newx, newy, this);//进行控件大小的伸缩变换

                Thread timeUpdateThread = new(new ThreadStart(() =>
                {
                    while (true)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            this.Refresh();
                            timeBar.Text = DateTime.Now.ToString("HH:mm");
                            

                        });
                        Thread.Sleep(1000);
                    }
                   
                }));
                timeUpdateThread.IsBackground = true;
                timeUpdateThread.Start();
            }
        }

        void LoadDatabasePreload()
        {
            LoadDatabase();
            loadingLabel.Text = closingTips;
            loadingLabel.Location = new Point(windowSize / 2 - loadingLabel.Size.Width / 2, windowSize / 2 - loadingLabel.Height / 2);
            Refresh();
            Thread.Sleep(1000);
            loadingDb = false;

            Hide();
            //SleepForm();



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
