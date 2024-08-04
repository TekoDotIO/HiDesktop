using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Widgets.MVP.WidgetModels
{
    class ActivatorUriProcessor
    {
        string className = "ActivatorUriProcessor";
        public string Url;
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
                MessageBox.Show("Please first activate a Widgets.MVP process...", "No Processors Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                pipeClient.Connect();

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
                    var r = MessageBox.Show("Undefined response from main process.", "Widgets.MVP - " + className, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (r == DialogResult.Retry)
                    {
                        TransportToMainProgram();
                    }
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

            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    
        /// <summary>
        /// 处理URL
        /// </summary>
        public void Process()
        {
            MessageBox.Show(Url);
        }
    }
}
