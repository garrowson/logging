using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog2
{
    public abstract class ILog
    {
        protected readonly object lockObj = new object();

        public abstract string ClassPath { get; set; }
        public abstract string Format { get; set; }

        public abstract void Debug(string message);
        public abstract void Debug(string message, Exception ex);

        public abstract void Info(string message);
        public abstract void Info(string message, Exception ex);

        public abstract void Warning(string message);
        public abstract void Warning(string message, Exception ex);

        public abstract void Error(string message);
        public abstract void Error(string message, Exception ex);

        public abstract void Fatal(string message);
        public abstract void Fatal(string message, Exception ex);

        public string InternalFormat(string level, string format, string message)
        {
            if (!String.IsNullOrEmpty(Format))
                return Format
                    .Replace("%date", DateTime.Now.ToString())
                    .Replace("%thread", System.Threading.Thread.CurrentThread.ManagedThreadId.ToString())
                    .Replace("%level", level)
                    .Replace("%logger", ClassPath)
                    .Replace("%message", message)
                    .Replace("%newline", Environment.NewLine);
            else
                return $"(noFormat) {message}{Environment.NewLine}";
        }
    }
}
