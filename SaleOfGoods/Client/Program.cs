using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            service.Start();
            Console.CancelKeyPress += (x, y) => service.Stop();
        }
    }
}
