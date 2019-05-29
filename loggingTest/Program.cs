using System;

namespace logging
{
    class Program
    {
        //ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static readonly RLLog2.ILog log = RLLog2.LogManager.GetLogger();

        static void Main(string[] args)
        {
            log.Debug("Startup");

            Console.WriteLine("Hello World!");

            getItDone();


            Console.ReadKey();
            log.Info(String.Format("Closing at {0}", DateTime.Now.ToString()));
        }

        static void getItDone()
        {
            string classST = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().DeclaringType.ToString();
            string methodST = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.ToString();

            string classMB = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
            string methodMB = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();

            Console.WriteLine("ST. {0}.{1}", classST, methodST);
            Console.WriteLine("MB. {0}.{1}", classMB, methodMB);
        }
    }
}
