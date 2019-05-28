using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog
{
    public abstract class LogBase
    {
        protected readonly object lockObj = new object();

        public readonly string classPath;
        public string Format;

        public abstract LoggerType LoggerType { get; }

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

        public string InternalFormat(int threadid, string level, string message)
        {
            if (!String.IsNullOrEmpty(Format))
                return Format
                    .Replace("%date", DateTime.Now.ToString())
                    .Replace("%thread", threadid.ToString())
                    .Replace("%level", level)
                    .Replace("%logger", classPath)
                    .Replace("%message", message)
                    .Replace("%newline", Environment.NewLine);
            else
                return $"(noFormat) {message}{Environment.NewLine}";
        }
    }
}