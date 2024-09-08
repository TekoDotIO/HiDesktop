using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.WindowApps;
using System.Threading;

namespace Widgets.MVP.WidgetModels
{
    class ActivatorUriProcessor
    {
        RandomPicker randomPicker;
        string className = "ActivatorUriProcessor";
        public string Url;
        static string savedExceptNum = "";
        static string savedExceptItem = "";
        static string savedExceptTags = "";
        public ActivatorUriProcessor()
        {

        }
        public ActivatorUriProcessor(string url)
        {
            Url = url;
        }
        /// <summary>
        /// 将URL传输给主程序
        /// </summary>
        public void TransportToMainProgram()//Code by GPT-4o
        {
            if (!CommandRepo.IsMultiProcess("Widgets.MVP"))
            {
                var r = MessageBox.Show("Please first activate a Widgets.MVP process...\nDo you want to boot Widgets.MVP? \n(This command won't be executed.)", "No Processors Found", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (r == DialogResult.Yes)
                {
                    string[] arg = new string[1];
                    arg[0] = "--MainProcess";
                    Program.Main(arg);
                }
                return;
            }
            Uri uri = new Uri(Url);
            string action = uri.Host;
            NameValueCollection args = System.Web.HttpUtility.ParseQueryString(uri.Query);
            Log.SaveLog($"Sub-process fetched action:{action}", className, false);
            // 定义命名管道名称（与应用程序A中相同）
            string pipeName = "ActivatorUriTransPipe";

            try
            {
                // 连接到命名管道
                NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);

                // 等待管道连接
                Log.SaveLog("Connecting ActivatorUriTransPipe...", className);
                pipeClient.Connect(5000);

                // 发送数据
                string message = Url;
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                pipeClient.Write(buffer, 0, buffer.Length);

                // 接收来自应用程序A的响应
                byte[] responseBuffer = new byte[4096];
                int bytesRead = pipeClient.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead).Trim();
                if (response != "DONE") 
                {
                    var r = MessageBox.Show("Undefined response from main process. Chek if your main process has stuck or your args are illegal.", "Widgets.MVP - " + className, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (r == DialogResult.Retry)
                    {
                        TransportToMainProgram();
                    }
                }
            }
            catch (TimeoutException ex)
            {
                Log.SaveLog("Error(ConnectTimeOut): " + ex.Message, className, false);
                var r = MessageBox.Show($"Connect time out! It could occur if Widgets.MVP main process is stuck or it hasn't been booted. \nDetails:\n{ex}", "Widgets.MVP - " + className, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (r == DialogResult.Retry)
                {
                    TransportToMainProgram();
                }
            }
            catch (Exception ex)
            {
                Log.SaveLog("Error: " + ex.Message, className, false);
                var r = MessageBox.Show($"Undefined  error:\n{ex}", "Widgets.MVP - " + className, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (r == DialogResult.Retry)
                {
                    TransportToMainProgram();
                }
            }
            

        }
    
        /// <summary>
        /// 处理URL
        /// </summary>
        public void Process()
        {
            //MessageBox.Show(Url);
            Uri uri = new Uri(Url);
            string action = uri.Host;
            NameValueCollection args = System.Web.HttpUtility.ParseQueryString(uri.Query);
            Log.SaveLog($"Main-process fetched action:{action}", className, false);
            if (uri.Scheme != "hidesktop") 
            {
                Log.SaveLog($"Not a hidesktop:// protocol input:{uri}. Using browser to open...", className);
                System.Diagnostics.Process.Start("explorer.exe", Url);
                return;
            }
            switch (action)
            {
                case "command":
                    CommandProcess(args);
                    break;
                case "activator":
                    ActivatorProcess(args);
                    break;
                case "start":
                    StartProcess(args);
                    break;
                default:
                    Log.SaveLog($"Not a legal hidesktop:// protocol input:{uri}.", className);
                    throw new System.Exception($"Not a legal hidesktop:// protocol input:{uri}.");
            }
        }

        void CommandProcess(NameValueCollection args)
        {
            switch (args["action"])
            {
                case "showDesktop":
                    //Code by https://blog.csdn.net/qq_46603687/article/details/116534545
                    Type shellType = Type.GetTypeFromProgID("Shell.Application");
                    object shellObject = System.Activator.CreateInstance(shellType);
                    shellType.InvokeMember("ToggleDesktop", System.Reflection.BindingFlags.InvokeMethod, null, shellObject, null);
                    break;
                case "randomPicker":
                    if (randomPicker == null) 
                    {
                        randomPicker = new();
                    }
                    if (randomPicker.IsDisposed)
                    {
                        randomPicker = new();
                    }
                    //RandomPicker rp = new();
                    Thread t = new(new ThreadStart(() =>
                    {
                        randomPicker.savedExceptItem = savedExceptItem;
                        randomPicker.savedExceptNum = savedExceptNum;
                        randomPicker.savedExceptTags = savedExceptTags;
                        randomPicker.ShowDialog();
                        savedExceptItem = randomPicker.savedExceptItem;
                        savedExceptNum = randomPicker.savedExceptNum;
                        savedExceptTags = randomPicker.savedExceptTags;
                    }));
                    t.Start();
                    break;
                default:
                    Log.SaveLog($"Not a legal hidesktop://command protocol input:{args["action"]}.", className);
                    throw new System.Exception($"Not a legal hidesktop://command protocol input:{args["action"]}.");
            }
        }

        void StartProcess(NameValueCollection args)
        {
            try
            {
                Process p = new();
                p.StartInfo.FileName = args["target"];
                Log.SaveLog($"Build process: {args["targrt"]}");
                if (args.AllKeys.Contains("args"))
                {
                    p.StartInfo.Arguments = args["args"];
                    Log.SaveLog($"Fetch args:{args["args"]}", className);
                }
                p.Start();
            }
            catch (Exception ex)
            {
                Log.SaveLog($"Not a legal hidesktop://start protocol input:{args["action"]}.\nEx:{ex}", className);
                throw new System.Exception($"Not a legal hidesktop://start protocol input:{args["action"]}.\nEx:{ex}");
            }
        }

        void ActivatorProcess(NameValueCollection args)
        {
            if (Program.activatedActivator == null) 
            {
                Log.SaveLog($"Not a legal hidesktop://activator command protocol input because Activator not exists:{args["action"]}.", className);
                throw new System.Exception($"Activator not exists! If a operation on Activator is needed, build one first.");
            }
            switch (args["action"])
            {
                case "saveLocation":
                    Program.activatedActivator.SaveLocation();
                    break;
                case "show":
                    if (Program.activatedActivator.subWindow == null)
                    {
                        Program.activatedActivator.subWindow = new(Program.activatedActivator.AppConfig, Program.activatedActivator);


                    }
                    Program.activatedActivator.subWindow.stopNextAwake = false;
                    if (!Program.activatedActivator.subWindow.hasBooted)
                    {
                        Log.SaveLog("If you recently booted Activator, you need to at least activate it once to enable calling.", className);
                        throw new Exception("System.If you recently booted Activator, you need to at least activate it once to enable calling.");
                    }
                    try
                    {
                        Program.activatedActivator.subWindow.Invoke((MethodInvoker)delegate ()
                        {
                            Program.activatedActivator.subWindow.CallUpForm();
                        });
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog($"Error when creating form:{ex}. \nIf you recently booted Activator, you need to at least activate it once to enable calling.", className);
                        throw new System.Exception($"Error when creating form:{ex}. \nIf you recently booted Activator, you need to at least activate it once to enable calling.");
                    }
                    
                    break;
                case "hide":
                    Program.activatedActivator.SildeAndHide();
                    break;
                case "NewActivatorShortcut":
                    string id = "";
                    string description = "";
                    string icon = "";
                    string action = "";
                    if (args.AllKeys.Contains("id"))
                    {
                        id = args["id"];
                    }
                    if (args.AllKeys.Contains("description"))
                    {
                        description = args["description"];
                    }
                    if (args.AllKeys.Contains("icon"))
                    {
                        icon = args["icon"];
                    }
                    if (args.AllKeys.Contains("action"))
                    {
                        action = args["action"];
                    }
                    ActivatorObjectEditor a = new()
                    {
                        dataSrc = Program.activatedActivator.subWindow.dataScr,
                        ID = id,
                        description = description,
                        imgPath = icon,
                        action = action
                    };
                    a.ShowDialog();
                    break;
                default:
                    Log.SaveLog($"Not a legal hidesktop://activator command protocol input:{args["action"]}.", className);
                    throw new System.Exception($"Not a legal hidesktop://activator command protocol input:{args["action"]}.");
            }
        }
    }
}
