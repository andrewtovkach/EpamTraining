using System;
using System.Collections.Generic;

namespace ATSProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Station station = new Station(new List<Port>
            {
                new Port("7898"),
                new Port("7466"),
                new Port("4561"),
                new Port("2456"),
                new Port("8974")
            },
            new List<Terminal>
            {
                new Terminal("8794", "+375(17)275-75-25"),
                new Terminal("4568", "+375(152)24-15-10"),
                new Terminal("1564", "+375(1512)2-70-15"),
                new Terminal("8700", "+375(17)25-25-25"),
                new Terminal("1236", "+375(16)24-10-10")
            },
            new List<Client>
            {       
                new Client("Ivan", "Ivanov", "1993-5-4", "Minsk"),
                new Client("Petr", "Semenov", "1963-10-24", "Grodno"),
                new Client("Anna", "Petrova", "1987-1-14", "Lida")
            });
            Console.WriteLine(station.RemovePortMapping("7898"));
            Console.WriteLine(station.RemoveTerminalMapping("8794"));
            Console.ReadKey();
        }
    }
}
