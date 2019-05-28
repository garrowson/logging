using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog
{
    public class ConsoleLogger : LogBase
    {

        public override LoggerType LoggerType => LoggerType.ConsoleLogger;

        private void CommonOut(string name, string message)
        {
            string msg = InternalFormat(System.Threading.Thread.CurrentThread.ManagedThreadId, name, message);
            lock (lockObj)
            {
                Console.WriteLine(msg);
            }
        }
        private void CommonOut(string name, string message, Exception ex)
        {
            string msg = InternalFormat(System.Threading.Thread.CurrentThread.ManagedThreadId, name, $"{message} JSON:{LogManager.GetObjectDetailsJSON(ex)}");
            lock (lockObj)
            {
                Console.WriteLine(msg);
            }
        }

        public override void Debug(string message)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message);
        }
        public override void Debug(string message, Exception ex)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message, ex);
        }

        public override void Info(string message)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message);
        }
        public override void Info(string message, Exception ex)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message, ex);
        }

        public override void Warning(string message)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message);
        }
        public override void Warning(string message, Exception ex)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message, ex);
        }

        public override void Error(string message)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message);
        }
        public override void Error(string message, Exception ex)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message, ex);
        }

        public override void Fatal(string message)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message);
        }
        public override void Fatal(string message, Exception ex)
        {
            CommonOut(System.Reflection.MethodBase.GetCurrentMethod().Name, message, ex);
        }

    }
}
