using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog2
{
    /// <summary>
    /// Warn-levels of logging
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    /// <summary>
    /// Providing helpers for logging class library
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// Gets selected data from an object recursively and prepares it in JSON format
        /// </summary>
        /// <param name="obj">The object to prepare</param>
        /// <returns></returns>
        public static string GetObjectDetailsJSON(object obj)
        {
            System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();

            List<Tuple<string, Type, object>> fields = new List<Tuple<string, Type, object>>();
            foreach (System.Reflection.PropertyInfo prop in properties)
            {
                object val = null;
                try
                {
                    val = prop.GetValue(obj, null);
                }
                catch { }
                finally { val = val != null ? val : String.Empty; }



                fields.Add(new Tuple<string, Type, object>(
                    prop.Name,
                    prop.PropertyType,
                    val
                ));
            }

            string json = "{ ";
            foreach (Tuple<string, Type, object> item in fields)
            {
                if (item.Item2.ToString() == "System.String")
                {
                    json += $"{item.Item1} : \"{item.Item3.ToString()}\", ";
                }
                else if (item.Item2.ToString() == "System.Char")
                {
                    json += $"{item.Item1} : '{item.Item3.ToString()}', ";
                }
                else if (item.Item2.IsValueType)
                {
                    json += $"{item.Item1} : {item.Item3.ToString()}, ";
                }
                else if (item.Item2.ToString() == "System.Exception")
                {
                    json += $"{item.Item1} : {GetObjectDetailsJSON(item.Item3)}, ";
                }
                else
                {
                    json += $"{item.Item1} : ({item.Item2.ToString()}), "; // {GetObjectDetailsJSON(item.Item3)}, ";
                }

            }
            json = json.Remove(json.Length - 2, 2);
            json += " }";

            return json;
        }


        //public static FileLogger GetFileLogger()
        //{
            //System.Diagnostics.Process.GetCurrentProcess().Id
              // (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod()).DeclaringType.
            //return GetFileLogger("");
        //}
        /// <summary>
        /// Creates a new FileLogger object and initialises it
        /// </summary>
        /// <param name="loggername">The name to identify the logger</param>
        /// <returns></returns>
        public static FileLogger GetFileLogger(string loggername = null)
        {
            DateTime starttime = System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime();

            string datestring = $"{starttime.Year.ToString("D4")}{starttime.Month.ToString("D2")}{starttime.Day.ToString("D2")}-{starttime.Hour.ToString("D2")}{starttime.Minute.ToString("D2")}";

            FileLogger fLog = new FileLogger();
            fLog.Format = "%date %level [%thread] %class.%method - %message";
            fLog.LogFilePath = $"{Environment.CurrentDirectory}\\logs\\{datestring}{((String.IsNullOrEmpty(loggername)) ? "" : $"-{loggername}")}.log";

            if(!System.IO.Directory.Exists($"{Environment.CurrentDirectory}\\logs\\"))
                System.IO.Directory.CreateDirectory($"{Environment.CurrentDirectory}\\logs\\");

            return fLog;
        } 
    }
}
