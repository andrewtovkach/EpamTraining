using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            Console.CancelKeyPress += (x, y) =>
            {
                Console.WriteLine("Service is stopped");
                service.Stop();
            };
            Console.WriteLine("Service is running");
            service.Start();
        }
    }
}
