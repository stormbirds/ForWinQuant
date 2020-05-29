using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant.Helper
{

    class Log
    {
        public static RichTextBox actionLog;

        public static void Info(string stringFormat, params object[] info)
        {
            Info(string.Format(stringFormat, info));
        }
        /// <summary>
        /// 用户日志信息
        /// </summary>
        /// <param name="info">日志内容</param>
        public static void Info(string info)
        {
            string result = string.Format("[{0}] {1}", DateTimeOffset.Now.ToString("MM-dd HH:mm:ss"), info);
            Console.WriteLine(result);
            if (actionLog != null) {
                actionLog.AppendText(result + "\r\n");
                actionLog.SelectionStart = actionLog.Text.Length;
                actionLog.ScrollToCaret();
            }


            WriteLog(result);
        }

        public static void Debug(string stringFormat, params object[] info)
        {
            Debug(string.Format(stringFormat, info));
        }
        public static void Debug(string info)
        {
            string result = string.Format("[{0}] {1}", DateTimeOffset.Now.ToString("MM-dd HH:mm:ss"), info);
            Console.WriteLine(result);
            WriteLog(result);
        }

        public static void Warning(string stringFormat, params object[] info)
        {
            Warning(string.Format(stringFormat, info));
        }
        public static void Warning(string info)
        {
            string result = string.Format("[{0}] {1}", DateTimeOffset.Now.ToString("MM-dd HH:mm:ss"), info);
            Console.WriteLine(result);
            WriteLog(result);
        }

        public static void Error(string stringFormat, params object[] info)
        {
            Error(string.Format(stringFormat, info));
        }
        public static void Error(string info)
        {
            string result = string.Format("[{0}] {1}", DateTimeOffset.Now.ToString("MM-dd HH:mm:ss"), info);
            Console.WriteLine(result);
            WriteLog(result);
        }

        private static void WriteLog(string content)
        {
            string logFilePath = Application.StartupPath + "\\log";
            if (false == System.IO.Directory.Exists(logFilePath))
                System.IO.Directory.CreateDirectory(logFilePath);

            using (System.IO.StreamWriter logFile = new System.IO.StreamWriter(logFilePath + "\\"+
                Properties.Settings.Default.username + "_" + DateTimeOffset.Now.ToString("yyyy-MM-dd")+".txt", true,new UTF8Encoding())) {
                logFile.WriteLine(content);
            }
        }
    }
}
