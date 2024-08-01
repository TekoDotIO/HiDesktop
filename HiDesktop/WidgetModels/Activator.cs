using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.Properties;

namespace Widgets.MVP.WidgetModels
{
    enum ActivatorStatus
    {
        Left, Right, None
    }
    public partial class Activator : Form
    {

        #region Resources
        /// <summary>
        /// 显示的图标
        /// </summary>
        Image definedIcon;
        /// <summary>
        /// 向左箭头图标
        /// </summary>
        Image leftArrowIcon;
        /// <summary>
        /// 向右箭头图标
        /// </summary>
        Image rightArrowIcon;
        #endregion
        #region Config
        /// <summary>
        /// 组件配置
        /// </summary>
        Hashtable AppConfig;
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        int scrW = SystemInformation.PrimaryMonitorSize.Width;
        /// <summary>
        /// 屏幕高度
        /// </summary>
        int scrH = SystemInformation.PrimaryMonitorSize.Height;
        /// <summary>
        /// （状态机）是否允许使用图标
        /// </summary>
        bool enableIcon = false;
        /// <summary>
        /// 图标框大小记忆值
        /// </summary>
        Size pbSize;
        /// <summary>
        /// 图标框位置记忆值
        /// </summary>
        Point pbLoc;
        /// <summary>
        /// 窗口大小
        /// </summary>
        int size = 100;//窗口大小
        /// <summary>
        /// 圆弧角度
        /// </summary>
        int radius = 0;//圆弧角度
        /// <summary>
        /// 窗体贴边状态
        /// </summary>
        ActivatorStatus statu;//状态
        /// <summary>
        /// 是否允许贴边隐藏
        /// </summary>
        bool enableSideHide = false;//允许贴边隐藏
        /// <summary>
        /// 贴边检测值
        /// </summary>
        int scrEdgeSize = 50;//贴边检测值
        #endregion
        #region Controls
        /// <summary>
        /// 图标显示框
        /// </summary>
        PictureBox pb;
        /// <summary>
        /// 绑定的子窗口
        /// </summary>
        ActivatorSubWindow subWindow;
        #endregion
        /// <summary>
        /// 标准示例配置文件
        /// </summary>
        public Hashtable htStandard = new()
        {
            { "type" ,"Activator" },//
            { "enabled" ,"true" },//
            { "size" ,"100" },//
            { "topMost" ,"true" },//
            { "location" ,"auto" },//
            { "dataSource" ,"./activator.db" },
            { "icon" , "default" },//
            { "windowBackColor" , "#1F1F1F" },//
            { "windowSize","auto" },//
            { "allowMove","true"},//
            { "windowBackground","" },//
            { "iconSet","default"},
            { "rightClick",""},
            { "showTimeBar","true"},
            { "showControl","false"},
            { "allowHiding","false"},
            { "opacity","1"},//
            { "windowRadius","40"},//
            { "radius","40"},//
            { "edge","20"},//
            { "enableSideHide","true"},//
            { "scrEdgeSize","50"},//
            { "activatorBackColor","#1F1F1F"},
            { "activatorBackground",""},
            { "windowForeColor","#FFFFFF"}
        };
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
                cp.ExStyle |= 0x02000000;//防止闪屏
                return cp;
            }
        }
        //From https://www.cnblogs.com/darkic/p/16256294.html
        public Activator(string Path)
        {

            InitializeComponent();
            //SetWindowRegion(40);

            this.ShowInTaskbar = false;
            

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

            if ((string)AppConfig["type"] != "Activator")
            {
                Log.SaveLog($"{Path}不是一个Activator的配置文件,已跳过加载.");
                this.Close();
                return;
            }
            Opacity = Convert.ToDouble(AppConfig["opacity"]);
            TopLevel = (string)AppConfig["topMost"] == "true";
            TopMost = (string)AppConfig["topMost"] == "true";
            Size = new Size(Convert.ToInt32((string)AppConfig["size"]), Convert.ToInt32((string)AppConfig["size"]));
            size = Convert.ToInt32((string)AppConfig["size"]);
            enableSideHide = (string)AppConfig["enableSideHide"] == "true";
            scrEdgeSize = Convert.ToInt32((string)AppConfig["scrEdgeSize"]);
            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            CheckForIllegalCrossThreadCalls = false;
            StartPosition = FormStartPosition.Manual;//一定要是手动，否则后文不起作用。



            subWindow = new(AppConfig, this);
            
            
            
            if ((string)AppConfig["location"] == "auto")
            {

                Location = new Point(100, h - 400);
                AppConfig["location"] = $"{this.Location.X},{this.Location.Y}";
                PropertiesHelper.Save(Path, AppConfig);
            }
            else
            {
                try
                {
                    string Location = (string)AppConfig["location"];
                    if (Location == null)
                    {
                        this.Location = new Point(100, h - 400);
                        AppConfig["location"] = $"{this.Location.X},{this.Location.Y}";
                        PropertiesHelper.Save(Path, AppConfig);
                    }
                    else
                    {
                        this.Location = new Point(Convert.ToInt32(Location.Split(",")[0]), Convert.ToInt32(Location.Split(",")[1]));
                        //MessageBox.Show($"{this.Location.X},{this.Location.Y}");
                    }

                }
                catch
                {
                    Location = new Point(100, h - 400);
                    AppConfig["location"] = $"{this.Location.X},{this.Location.Y}";
                    PropertiesHelper.Save(Path, AppConfig);
                }
            }
            //Image icon;


            //Load resources...

            Log.SaveLog("Loading resources...", "Activator", false);
            try
            {
                leftArrowIcon = Bitmap.FromFile("./Resources/leftArrow.png");
                rightArrowIcon = Bitmap.FromFile("./Resources/rightArrow.png");
            }
            catch (Exception ex)
            {
                Log.SaveLog($"App package has been illegaly modified! Cannot load default activator icon:\n{ex}", "Activator", false);
            }
            try
            {
                Image bm = Bitmap.FromFile((string)AppConfig["activatorBackground"]);
                BackgroundImage = bm;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Unable to load back img:\n{ex}\n Will use color and icon instead.", "Activator", false);
                BackColor = ColorTranslator.FromHtml((string)AppConfig["activatorBackColor"]);
                //var defaultIcon = Properties.Resources.DefaultActivatorIcon;
                //icon = ByteToBitmap(defaultIcon);
                enableIcon = true;
                //throw;
            }
            if (enableIcon)
            {
                try
                {
                    definedIcon = Bitmap.FromFile((string)AppConfig["icon"]);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"Unable to load icon:\n{ex}\n Will use default icon.", "Activator", false);
                    //var defaultIcon = Properties.Resources.DefaultActivatorIcon;
                    // defaultIcon = Bitmap.FromFile((string)AppConfig["icon"]);
                    //icon = ByteToBitmap(defaultIcon);
                    try
                    {
                        //definedIcon = Bitmap.FromFile("./Resources/ActivatorDefaultIcon.png");
                        definedIcon = Bitmap.FromFile("./Resources/def2thgen.png");
                    }
                    catch (Exception ex2)
                    {
                        Log.SaveLog($"App package has been illegaly modified! Cannot load default activator icon:\n{ex2}", "Activator", false);
                    }
                    
                }
                int edge = Convert.ToInt32((string)AppConfig["edge"]);
                pb = new();
                pb.Image = definedIcon;
                //pb.Size = new Size(definedIcon.Width, definedIcon.Height);
                pb.Size = new Size(Width - edge * 2, Height - edge * 2);
                pbSize = new Size(Width - edge * 2, Height - edge * 2);
                pb.Location = new Point(edge, edge);
                pbLoc = new Point(edge, edge);
                pb.Parent = this;
                //pb.BackgroundImageLayout = ImageLayout.Stretch;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                //pb.Show();
                pb.MouseDown += Pb_MouseDown;
            }
            radius = Convert.ToInt32((string)AppConfig["radius"]);
            SetWindowRegion(radius);
            statu = ActivatorStatus.None;
            
            



            JudgeHideStatu();
            
        }

        private void SetIcon(Image im)
        {
            if (pb == null) 
            {
                pb = new();
                pb.Parent = this;
                //pb.BackgroundImageLayout = ImageLayout.Stretch;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.MouseDown += Pb_MouseDown;
            }
            pb.Image = im;
            pb.Location = new Point(Size.Width / 6, Size.Height / 3);
            pb.Size=new Size(Size.Width/6*5, Size.Height / 3);
            
            pb.Show();
        }

        private void JudgeHideStatu()
        {
            var p = Location;
            if (p.X <= scrEdgeSize)
            {
                if (statu != ActivatorStatus.Left)
                {
                    SetWindowRegion(20);
                    SetIcon(rightArrowIcon);
                    Refresh();
                    var l = MathRepo.CreatePhysicalSmoothMovePointsSet(size, size / 2, 25, 1);
                    foreach (var item in l)
                    {
                        Size = new Size(Convert.ToInt32(item), Size.Height);
                    }
                    //SetWindowRegion(20);
                    SetIcon(rightArrowIcon);
                    SetWindowRegion(20);
                    statu = ActivatorStatus.Left;
                    
                }
                MathRepo.MoveWindowSmoothly_MethodA(this, 5, Location.Y, 0.2, 30);
            }
            else if (p.X >= scrW - scrEdgeSize - size) 
            {
                if (statu != ActivatorStatus.Right)
                {
                    SetWindowRegion(20);
                    SetIcon(leftArrowIcon);
                    Refresh();
                    var l = MathRepo.CreatePhysicalSmoothMovePointsSet(size, size / 2, 25, 1);
                    foreach (var item in l)
                    {
                        Size = new Size(Convert.ToInt32(item), Size.Height);
                    }
                    SetIcon(leftArrowIcon);
                    //SetWindowRegion(20);
                    statu = ActivatorStatus.Right;
                }
                MathRepo.MoveWindowSmoothly_MethodA(this, scrW - 5 - size / 2, Location.Y, 0.2, 30);
            }
            else
            {
                if (statu != ActivatorStatus.None)
                {
                    var l = MathRepo.CreatePhysicalSmoothMovePointsSet(size / 2, size, 25, 1);
                    //SetWindowRegion(30);
                    //Size = new Size(Size.Width - 10, Size.Height);
                    //Refresh();
                    //SetWindowRegion(radius);
                    //Thread.Sleep(500);
                    Hide();
                    Size = new Size(size, size);
                    SetWindowRegion(20);
                    Size = new Size(size / 2, size);
                    Show();
                    Refresh();
                    foreach (var item in l)
                    {
                        Size = new Size(Convert.ToInt32(item), Size.Height);
                        //SetWindowRegion(20);//由于限定窗口区域，必须SetWindowRegion，否则动画不生效
                        Refresh();
                    }
                    SetWindowRegion(radius);
                    if (definedIcon != null) 
                    {
                        pb.Image = definedIcon;
                        //pb.Size = new Size(definedIcon.Width, definedIcon.Height);
                        pb.Size = pbSize;
                        pb.Location = pbLoc;
                    }
                    else
                    {
                        pb.Hide();
                    }
                    statu = ActivatorStatus.None;
                }
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            var previousPoint = Location;
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
            if (previousPoint == Location)
            {
                //MessageBox.Show("Open sub-window.", "Msgbox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (subWindow == null)
                {
                    subWindow = new(AppConfig, this);

                }
                if (subWindow.isAwake)
                {
                    subWindow.SleepForm();
                    return;
                }
                subWindow.CallUpForm();
            }
            else
            {
                if (subWindow != null)
                {
                    subWindow.SleepForm();
                }
                //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
                if (enableSideHide)
                {
                    JudgeHideStatu();
                }
            }

        }
        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            var previousPoint = Location;
            
            //base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
            if (previousPoint == Location)
            {
                //MessageBox.Show($"Open sub-window.\n{this.Width},{this.Height}", "Msgbox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (subWindow == null) 
                {
                    subWindow = new(AppConfig, this);
                    
                }
                if (subWindow.isAwake)
                {
                    subWindow.SleepForm();
                    return;
                }
                subWindow.CallUpForm();
            }
            else
            {
                if (subWindow != null)
                {
                    subWindow.SleepForm();
                }
                //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
                if (enableSideHide)
                {
                    JudgeHideStatu();
                }
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Byte数组转Bitmap
        /// </summary>
        /// <param name="ImageByte">Byte数组</param>
        /// <returns>Bitmap图像</returns>
        public Bitmap ByteToBitmap(byte[] ImageByte)
        {
            Bitmap bitmap = null; using (MemoryStream stream = new MemoryStream(ImageByte))
            {
                bitmap = new Bitmap((Image)new Bitmap(stream));
            }
            return bitmap;
        }//From https://www.cnblogs.com/log9527blog/p/17616377.html

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

        private void Activator_Load(object sender, EventArgs e)
        {

        }

        private void Activator_Click(object sender, EventArgs e)
        {
            //MathRepo.CreatePhysicalSmoothMovePointsSet(0, 400, 1, 0.02);
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
        }

        private void Activator_MouseClick(object sender, MouseEventArgs e)
        {
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
        }

        private void Activator_MouseDown(object sender, MouseEventArgs e)
        {
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
        }

        private void Activator_MouseUp(object sender, MouseEventArgs e)
        {
            //MathRepo.MoveWindowSmoothly_MethodA(this, 400, 400, 1, 20);
        }
    }
}
