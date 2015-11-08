using System;
using System.Collections.Generic;
using ATSProject.Interfaces;
using ATSProject.Model;

namespace ATSProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Station station = new Station(new List<IPort>
            {
                new Port("7898"),
                new Port("7466"),
                new Port("4561"),
                new Port("2456"),
                new Port("8974"),
                new Port("8989"),
                new Port("1300")
            },
            new List<ITerminal>
            {
                new Terminal("8794", "+375(17)275-75-25"),
                new Terminal("4568", "+375(152)24-15-10"),
                new Terminal("1564", "+375(1512)2-70-15"),
                new Terminal("8700", "+375(17)25-25-25"),
                new Terminal("1236", "+375(16)24-10-10")
            });
            BillingSystem billingSystem = new BillingSystem(new List<Contract>
            {
                new Contract("1001", "+375(17)275-75-25", new TariffPlan("Active", 30000, 50, 150), 
                    "15/2015", "2015-10-1"),
                new Contract("1002", "+375(152)24-15-10", new TariffPlan("Middle", 20000, 20, 250), 
                    "16/2015", "2015-11-4"),
                new Contract("1003", "+375(1512)2-70-15", new TariffPlan("Light", 15000, 10, 500), 
                    "17/2015", "2015-10-14")
            },
            new List<Client>
            {       
                new Client("Ivan", "Ivanov", "1993-5-4", "Minsk"),
                new Client("Petr", "Semenov", "1963-10-24", "Grodno"),
                new Client("Anna", "Petrova", "1987-1-14", "Lida")
            }, station);
            station.TerminalByNumber("8794").InsertIntoPort();
            station.TerminalByNumber("4568").InsertIntoPort();
            station.TerminalByNumber("4568").InsertIntoPort();
            station.TerminalByNumber("1564").InsertIntoPort();
            station.TerminalByNumber("8794").OutgoingCall("+375(152)24-15-10");
            station.TerminalByNumber("8794").OutgoingCall("+375(152)24-15-10");
            station.TerminalByNumber("8794").OutgoingCall("+375(152)24-15-11");
            station.TerminalByNumber("8794").OutgoingCall("+375(1512)2-70-15");
            station.TerminalByNumber("8794").OutgoingCall("+375(1512)2-70-15");
            station.TerminalByNumber("1564").RemoveFromPort();
            station.TerminalByNumber("1564").RemoveFromPort();
            station.TerminalByNumber("1564").OutgoingCall("+375(17)25-25-25");
            Console.ReadKey();
        }
    }
}
