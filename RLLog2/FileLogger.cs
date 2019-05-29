using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog2
{
    public class FileLogger : ILog
    {
        public string LogFilePath { get; set; }
        public override string ClassPath { get; set; }
        public override string Format { get; set; }

        private void CommonOut(LogLevel logLevel, string message)
        {
            string level = "NULL";
            switch (logLevel)
            {
                case LogLevel.Debug:
                    level = "DEBUG";
                    break;
                case LogLevel.Info:
                    level = "INFO";
                    break;
                case LogLevel.Warning:
                    level = "WARN";
                    break;
                case LogLevel.Error:
                    level = "ERROR";
                    break;
                case LogLevel.Fatal:
                    level = "FATAL";
                    break;
                default:
                    break;
            }

            message = InternalFormat(level, Format, message);

            lock (lockObj)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogFilePath, true))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
        }
        private void CommonOut(LogLevel logLevel, string message, Exception ex)
        {
            CommonOut(logLevel, $"{message} JSON:{LogManager.GetObjectDetailsJSON(ex)}");
        }


        public override void Debug(string message) => CommonOut(LogLevel.Debug, message);
        public override void Debug(string message, Exception ex) => CommonOut(LogLevel.Debug, message, ex);

        public override void Error(string message) => CommonOut(LogLevel.Error, message);
        public override void Error(string message, Exception ex) => CommonOut(LogLevel.Error, message, ex);

        public override void Fatal(string message) => CommonOut(LogLevel.Fatal, message);
        public override void Fatal(string message, Exception ex) => CommonOut(LogLevel.Fatal, message, ex);

        public override void Info(string message) => CommonOut(LogLevel.Info, message);
        public override void Info(string message, Exception ex) => CommonOut(LogLevel.Info, message, ex);

        public override void Warning(string message) => CommonOut(LogLevel.Warning, message);
        public override void Warning(string message, Exception ex) => CommonOut(LogLevel.Warning, message, ex);
    }
}
