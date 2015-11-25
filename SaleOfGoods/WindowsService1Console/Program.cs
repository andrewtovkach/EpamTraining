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
                Console.WriteLine("Служба запущена");
                Console.ReadKey();
                service.Stop();
                Console.WriteLine("Служба остановлена");
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
