using System;

namespace logging
{
    class Program
    {
        //ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static readonly RLLog2.ILog log = RLLog2.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Debug("Startup");

            Console.WriteLine("Hello World!");

            Console.ReadKey();
            log.Info(String.Format("Closing at {0}", DateTime.Now.ToString()));
        }
    }
}
