using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPLog2
{
    /// <summary>
    /// Interface for logging class
    /// </summary>
    public abstract class ILog
    {
#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
        protected readonly object lockObj = new object();
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
#pragma warning restore CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element

        /// <summary>
        /// Formats the message using the format provided by the 'Format' property
        /// </summary>
        /// <param name="level">Debuglevel</param>
        /// <param name="methodBase">Methodbase of the calling method</param>
        /// <param name="message">The message to format</param>
        /// <returns></returns>
        public string InternalFormat(string level, System.Reflection.MethodBase methodBase, string message)
        {
            if (!String.IsNullOrEmpty(Format))
            {
                var dt = DateTime.Now.ToLocalTime();
                return Format
                    .Replace("%date", $"{dt.Year.ToString("D4")}-{dt.Month.ToString("D2")}-{dt.Day.ToString("D2")} {dt.Hour.ToString("D2")}:{dt.Minute.ToString("D2")}:{dt.Second.ToString("D2")}.{dt.Millisecond.ToString("D3")}")
                    .Replace("%thread", System.Threading.Thread.CurrentThread.ManagedThreadId.ToString())
                    .Replace("%level", level)
                    .Replace("%class", methodBase.DeclaringType.ToString())
                    .Replace("%method", methodBase.Name)
                    .Replace("%message", message)
                    .Replace("%newline", Environment.NewLine);
            }
            else
                return $"(noFormat) {message}{Environment.NewLine}";

        }
    }
}
