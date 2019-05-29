using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLLog2
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public class LogManager
    {
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

        public static ILog GetLogger(Type declaringType)
        {
            string datestring = $"{DateTime.Now.Year.ToString("D4")}{DateTime.Now.Month.ToString("D2")}{DateTime.Now.Day.ToString("D2")}-{DateTime.Now.Hour.ToString("D2")}{DateTime.Now.Minute.ToString("D2")}";

            FileLogger fLog = new FileLogger();
            fLog.ClassPath = declaringType.ToString();
            fLog.Format = "%date %level [%thread] %logger - %message%newline";
            fLog.LogFilePath = $"{Environment.CurrentDirectory}\\{datestring}.log";
            
            return fLog;
        }
    }
}
