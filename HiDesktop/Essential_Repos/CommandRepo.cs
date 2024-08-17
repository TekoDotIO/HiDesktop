using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace Widgets.MVP.Essential_Repos
{
    public class CommandRepo
    {
        public string ProductName;
        public static void RegisterCustomURLScheme(string appName)
        {
            //string keyName = $"Software\\Classes\\{appName}";
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(appName))
            {
                key.SetValue("", $"{appName}");
                key.SetValue("URL Protocol", appName);
                using RegistryKey commandKey = key.CreateSubKey("shell\\open\\command");
                commandKey.SetValue("", Process.GetCurrentProcess().MainModule.FileName + " \"%1\"");
            }
        }

        public static void RemoveCustomURLScheme(string appName)
        {
            //string keyName = $"Software\\Classes\\{appName}";
            Registry.ClassesRoot.DeleteSubKeyTree(appName);
            //using (RegistryKey key = )
            //{
            //    key.SetValue("", $"{appName}");
            //    key.SetValue("URL Protocol", appName);
            //    using (RegistryKey commandKey = key.CreateSubKey("shell\\open\\command"))
            //    {
            //        commandKey.SetValue("", Process.GetCurrentProcess().MainModule.FileName + " \"%1\"");
            //    }
            //}
        }
        public static void CreateStartUpScript()
        {
            string CdPath = Directory.GetCurrentDirectory();
            Log.SaveLog("The program will use path-method to install.");
            string ThisFile = Process.GetCurrentProcess().MainModule.FileName;
            Log.SaveLog("Got path :" + ThisFile);
            //下方指令:必须使用"cd /d",否则当Windows目录与程序目录不在同一磁盘时会启动失败(启动到System32/{ProductName}/文件夹)

            if (!File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd"))
            {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd", "cd /d \"" + CdPath + "\"\n" + ThisFile);
            }
            else
            {
                if (!File.ReadAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd").Contains("cd /d \"" + CdPath + "\"\n" + ThisFile))
                {
                    File.AppendAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd", "\ncd /d \"" + CdPath + "\"\n" + ThisFile);
                }
                else
                {
                    Log.SaveLog("Program is already in the startup script.");
                }
            }
            Log.SaveLog("Created start-up script.");
            //创建自启动批处理文件
        }
        public static void ExitAll(string product)
        {
            var Running = Process.GetProcessesByName(product);
            //获取所有名为product的进程
            var ThisID = Process.GetCurrentProcess().Id;
            //获取当前进程ID,防止自己结束自己导致结束程序不彻底
            foreach (Process process in Running)//为每个识别到的进程重复
            {
                if (ThisID != process.Id)//防止自行结束
                {
                    process.Kill();//结束此进程
                    Log.SaveLog("Killed process:" + process.Id);
                }
            }
        }
        public static void Uninstall(string product)
        {
            string CdPath = Directory.GetCurrentDirectory();
            string ThisFile = Process.GetCurrentProcess().MainModule.FileName;
            Log.SaveLog("Got path :" + ThisFile);
            ExitAll(product);
            //退出所有除自己外的进程
            if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd"))
            {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd", File.ReadAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd").Replace("cd /d \"" + CdPath + "\"\n" + ThisFile, ""));
            }
            //删除自启动文件夹内的批处理文件
            Log.SaveLog("Uninstalled successfully.");
        }
        public void Uninstall()
        {
            string CdPath = Directory.GetCurrentDirectory();
            string ThisFile = Process.GetCurrentProcess().MainModule.FileName;
            Log.SaveLog("Got path :" + ThisFile);
            ExitAll(ProductName);
            //退出所有除自己外的进程
            if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd"))
            {
                File.WriteAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd", File.ReadAllText(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\EachTech_StartupScripts.cmd").Replace("cd /d \"" + CdPath + "\"\n" + ThisFile, ""));
            }
            //删除自启动文件夹内的批处理文件
            Log.SaveLog("Uninstalled successfully.");
        }
        public static bool IsMultiProcess(string product)
        {
            var Running = Process.GetProcessesByName(product);
            int i = 0;
            foreach (Process process in Running)
            {
                i++;
            }
            if (i > 1)
            {
                Log.SaveLog($"One or more {product} process is already running.");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsMultiProcess()
        {
            var Running = Process.GetProcessesByName(ProductName);
            int i = 0;
            foreach (Process process in Running)
            {
                i++;
            }
            if (i != 1)
            {
                Log.SaveLog($"One or more {ProductName} process is already running.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
