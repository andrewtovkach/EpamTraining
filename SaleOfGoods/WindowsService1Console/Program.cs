using System;
using System.ServiceProcess;
using WindowsService1;

namespace WindowsService1Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service1();
            ServiceBase [] servicesToRun = { service };
            if (Environment.UserInteractive)
            {
                Console.CancelKeyPress += (x, y) => service.Stop();
                service.Start();
                Console.WriteLine("Service is running");
                Console.ReadKey();
                service.Stop();
                Console.WriteLine("Service is stopped");
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
