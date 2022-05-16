using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace ScreenProtect.Ultra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Path;
        Hashtable AppConfig;
        Thread refreshThread;
        int w = SystemInformation.PrimaryMonitorSize.Width;
        int h = SystemInformation.PrimaryMonitorSize.Height;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Tips.Margin = new Thickness(0, h - 60, 0, 0);
            //Opacity = 0;
            Topmost = false;
            this.Path = $"{System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}/config.properties";
            Hashtable htStandard = new Hashtable()
            {
                { "type", "screenProtector" },
                { "font", "auto" },
                { "time_fontSize", "200" },
                { "tips_fontSize", "20" },
                { "opacity", "1" },
                { "topMost", "true" },
                { "enableSmoothStart", "true" },
                { "enabled","true" },
                { "refreshTime","50" },
                { "timeType","HH:mm:ss" },
                { "tips","Click any place to exit..." },
                { "time_Color","#FFFFFF" },
                { "tips_Color","#FFFFFF" }
            };
            if (!File.Exists(Path))
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
                Log.SaveLog($"{System.IO.Path.GetFullPath(Path)} created.");
            }
            if (File.ReadAllText(Path) == "")
            {
                Hashtable Config = htStandard;
                PropertiesHelper.Save(Path, Config);
                Log.SaveLog($"{System.IO.Path.GetFullPath(Path)} fixed.");
            }
            PropertiesHelper.FixProperties(htStandard, Path);
            AppConfig = PropertiesHelper.Load(Path);
            Log.SaveLog($"{System.IO.Path.GetFullPath(Path)} loaded.");
            if (!((string)AppConfig["enabled"] == "true"))
            {
                Log.SaveLog("Screen protector disabled...");
                return;
            }
        }

        private void InitializateWindow(object sender, RoutedEventArgs e)
        {
            
        }



        private void timeBox_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Tips_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
