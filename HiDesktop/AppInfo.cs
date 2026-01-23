using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widgets.MVP.Essential_Repos;

namespace Widgets.MVP
{
    public static class AppInfo
    {
        public const string Version = "v.2.0.5.0  Nightly Alpha";
        public const string StartupInfo = "幻愿Recovery, 歪锅包肉, imCXT 作品\n\n相互科技（teko.IO SisTemS!）保留所有权利\nPowered by .NET,\nOpen-sourced under Apache-2.0 LICENSE. About details, view https://github.com/TekoDotIO/HiDesktop/ .\n--------\n\"May we be together, stepping through this tough time, leaving not a speck of dust on our hearts.\"";
        public static Hashtable ApplicationConfig = new()
        {
            { "type", "AppSettings" },
            { "notifyIcon.autoExitApp", "true" },
            { "notifyIcon.sendNotifyAfterAllDisposed", "true" },
            { "debugMode", "true" },
            { "clearLastNLogs", "5" }
        };
        public static void InitializeSettings()
        {
            ApplicationConfig = PropertiesHelper.AutoCheck(ApplicationConfig, "./AppDefaultSettings/App.properties");


        }

    }
    
}
