using System;
using System.Diagnostics;
using System.IO;

namespace ScreenProtect.MVP
{
    //日志模块更新信息：20220726.a
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 存储日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void SaveLog(string message)
        {
            try
            {
                string Path = $"{System.IO.Path.GetDirectoryName(Environment.ProcessPath)}/";
                message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "] " + message;
                //空格是为了增强日志可读性,DateTime的作用是获取目前时间
                Directory.CreateDirectory($"{Path}/Log/");
                //如果不存在Log文件夹,则创建(会略微拖慢运行速度,但是用if判断一次代码量和工作量会大很多)
                File.AppendAllText($"{Path}/Log/Console" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "\r\n" + message);
                //AppendAllText是追加到文件末尾.因为文件名不能出现"/",所以这里在ToString里面指定格式为yyyy-MM-dd.
                //为了使文件便于查找,因此一天一个文件
                Console.WriteLine(message);
                //同时将信息反馈到控制台
                //return;
            }
            catch
            {
                SaveLog(message);
            }
        }

        /// <summary>
        /// 存储特定模块日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="module">模块名称</param>
        public static void SaveLog(string message, string module)
        {
            try
            {
                string Path = $"{System.IO.Path.GetDirectoryName(Environment.ProcessPath)}/";
                message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + $"] [{module}] " + message;
                //空格是为了增强日志可读性,DateTime的作用是获取目前时间
                Directory.CreateDirectory($"{Path}/Log/");
                //如果不存在Log文件夹,则创建(会略微拖慢运行速度,但是用if判断一次代码量和工作量会大很多)
                File.AppendAllText($"{Path}/Log/Console" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", $"\r\n" + message);
                //AppendAllText是追加到文件末尾.因为文件名不能出现"/",所以这里在ToString里面指定格式为yyyy-MM-dd.
                //为了使文件便于查找,因此一天一个文件
                Console.WriteLine($"{message}");
                //同时将信息反馈到控制台
                //return;
            }
            catch
            {
                SaveLog(message, module);
            }
        }

        /// <summary>
        /// 存储日志，如果output为false则不输出在控制台
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="output">是否在控制台输出</param>
        public static void SaveLog(string message, bool output)
        {
            try
            {
                string Path = $"{System.IO.Path.GetDirectoryName(Environment.ProcessPath)}/";
                message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "] " + message;
                //空格是为了增强日志可读性,DateTime的作用是获取目前时间
                Directory.CreateDirectory($"{Path}/Log/");
                //如果不存在Log文件夹,则创建(会略微拖慢运行速度,但是用if判断一次代码量和工作量会大很多)
                File.AppendAllText($"{Path}/Log/Console" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "\r\n" + message);
                //AppendAllText是追加到文件末尾.因为文件名不能出现"/",所以这里在ToString里面指定格式为yyyy-MM-dd.
                //为了使文件便于查找,因此一天一个文件
                if (output)
                    Console.WriteLine(message);

                //同时将信息反馈到控制台
                //return;
            }
            catch
            {
                SaveLog(message, output);
            }
        }

        /// <summary>
        /// 存储特定模块日志，如果output为false则不输出在控制台
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="module">模块名称</param>
        /// <param name="output">是否在控制台输出</param>
        public static void SaveLog(string message, string module, bool output)
        {
            try
            {
                string Path = $"{System.IO.Path.GetDirectoryName(Environment.ProcessPath)}/";
                message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + $"] [{module}] " + message;
                //空格是为了增强日志可读性,DateTime的作用是获取目前时间
                Directory.CreateDirectory($"{Path}/Log/");
                //如果不存在Log文件夹,则创建(会略微拖慢运行速度,但是用if判断一次代码量和工作量会大很多)
                File.AppendAllText($"{Path}/Log/Console" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", $"\r\n " + message);
                //AppendAllText是追加到文件末尾.因为文件名不能出现"/",所以这里在ToString里面指定格式为yyyy-MM-dd.
                //为了使文件便于查找,因此一天一个文件
                if (output)
                    Console.WriteLine($"{message}");
                //同时将信息反馈到控制台
                //return;
            }
            catch
            {
                SaveLog(message, module, output);
            }
        }
    }
}
