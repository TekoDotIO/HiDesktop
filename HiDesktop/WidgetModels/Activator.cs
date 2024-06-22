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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Widgets.MVP.WidgetModels
{
    public partial class Activator : Form
    {
        Hashtable AppConfig;



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
        public Activator(string Path)
        {

            InitializeComponent();
            //SetWindowRegion(40);

            this.ShowInTaskbar = false;
            Hashtable htStandard = new()
            {
                { "type" ,"Activator" },//
                { "enabled" ,"true" },//
                { "size" ,"100" },//
                { "topMost" ,"true" },//
                { "location" ,"auto" },//
                { "dataSource" ,"./activator.db" },
                { "icon" , "default" },
                { "windowBackColor" , "#000000" },//
                { "windowSize","auto" },
                { "allowMove","true"},//
                { "windowBackground","" },//
                { "iconSet","default"},
                { "rightClick",""},
                { "showTimeBar","true"},
                { "showControl","false"},
                { "allowHiding","false"},
                { "opacity","1"},//
                { "radius","40"}

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

            if ((string)AppConfig["type"] != "Activator")
            {
                Log.SaveLog($"{Path}不是一个Activator的配置文件,已跳过加载.");
                this.Close();
                return;
            }
            Opacity = Convert.ToDouble(AppConfig["opacity"]);
            TopMost = (string)AppConfig["topMost"] == "true";
            Size = new Size(Convert.ToInt32((string)AppConfig["size"]), Convert.ToInt32((string)AppConfig["size"]));
            int w = SystemInformation.PrimaryMonitorSize.Width;
            int h = SystemInformation.PrimaryMonitorSize.Height;
            CheckForIllegalCrossThreadCalls = false;
            if ((string)AppConfig["location"] == "auto")
            {

                Location = new Point(w + 100, h - 400);
            }
            else
            {
                try
                {
                    string Location = (string)AppConfig["location"];
                    if (Location == null)
                    {
                        this.Location = new Point(w + 100, h - 400);
                    }
                    else
                    {
                        this.Location = new Point(Convert.ToInt32(Location.Split(",")[0]), Convert.ToInt32(Location.Split(",")[1]));
                    }

                }
                catch
                {
                    Location = new Point(w + 100, h - 400);
                }
            }
            try
            {
                Image bm = Bitmap.FromFile((string)AppConfig["windowBackground"]);
                BackgroundImage = bm;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Unable to load back img:\n{ex}\n Will use color and icon instead.", "Activator", false);
                BackColor= ColorTranslator.FromHtml((string)AppConfig["windowBackColor"]);

                throw;
            }
            int radius = Convert.ToInt32((string)AppConfig["radius"]);
            SetWindowRegion(radius);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FrmMain_MouseDown(this, e);
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
    }
}
