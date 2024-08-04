using Microsoft.Win32;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Widgets.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegisterCustomURLScheme("appname");
            RemoveCustomURLScheme("appname");
            if (args.Length != 0) 
            {
                HandleCustomURL(args[0]);
            }
            //while (true)
            //{

            //}
            // Simulate receiving a custom URL
            //string url = "AppName://action1?arg1=123&arg2=456";
            //HandleCustomURL(url);

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
        public static void RemoveCustomURLScheme(string appName)
        {
            string keyName = $"Software\\Classes\\{appName}";
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
        static void RegisterCustomURLScheme(string appName)
        {
            string keyName = $"Software\\Classes\\{appName}";
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(appName))
            {
                key.SetValue("", $"HiDesktop测试用Schema");
                key.SetValue("URL Protocol", "testerProtocol");
                using (RegistryKey commandKey = key.CreateSubKey("shell\\open\\command"))
                {
                    commandKey.SetValue("", Application.ExecutablePath + " \"%1\"");
                }
            }
        }

        static void HandleCustomURL(string url)
        {
            Uri uri = new Uri(url);
            string action = uri.Host;
            NameValueCollection query = System.Web.HttpUtility.ParseQueryString(uri.Query);

            Console.WriteLine($"Received action: {action}");
            Console.WriteLine("Arguments:");
            foreach (string key in query.AllKeys)
            {
                Console.WriteLine($"{key}: {query[key]}");
            }

            // Implement your logic based on the action and arguments
            // For example:
            if (action == "action1")
            {
                string arg1 = query["arg1"];
                string arg2 = query["arg2"];

                // Process action1 with arg1 and arg2
                MessageBox.Show($"Processing action1 with arg1={arg1} and arg2={arg2}");
            }
        }
    }
}
