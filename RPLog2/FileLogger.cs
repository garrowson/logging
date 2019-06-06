using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPLog2
{
    /// <summary>
    /// Logs informations to a file
    /// </summary>
    public class FileLogger : ILog
    {
        /// <summary>
        /// Path for the logging file
        /// </summary>
        public string LogFilePath { get; set; }
        /// <summary>
        /// The format for logging messages
        /// </summary>
        public override string Format { get; set; }

        private void CommonOut(LogLevel logLevel, string message, System.Reflection.MethodBase methodBase)
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

            message = InternalFormat(level, methodBase, message);

            lock (lockObj)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogFilePath, true))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
        }
        private void CommonOut(LogLevel logLevel, string message, System.Reflection.MethodBase methodBase, Exception ex)
        {
            CommonOut(logLevel, $"{message} JSON:{LogManager.GetObjectDetailsJSON(ex)}", methodBase);
        }


        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Debug(string message) => CommonOut(LogLevel.Debug, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to add as an JSON entry at the end of the logging message</param>
        public override void Debug(string message, Exception ex) => CommonOut(LogLevel.Debug, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod(), ex);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Error(string message) => CommonOut(LogLevel.Error, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to add as an JSON entry at the end of the logging message</param>
        public override void Error(string message, Exception ex) => CommonOut(LogLevel.Error, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod(), ex);

        /// <summary>
        /// Logs a fatal message
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Fatal(string message) => CommonOut(LogLevel.Fatal, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        /// <summary>
        /// Logs a fatal message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to add as an JSON entry at the end of the logging message</param>
        public override void Fatal(string message, Exception ex) => CommonOut(LogLevel.Fatal, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod(), ex);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Info(string message) => CommonOut(LogLevel.Info, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to add as an JSON entry at the end of the logging message</param>
        public override void Info(string message, Exception ex) => CommonOut(LogLevel.Info, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod(), ex);

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Warning(string message) => CommonOut(LogLevel.Warning, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to add as an JSON entry at the end of the logging message</param>
        public override void Warning(string message, Exception ex) => CommonOut(LogLevel.Warning, message, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod(), ex);
    }
}
