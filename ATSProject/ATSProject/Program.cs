using System;
using System.Collections.Generic;
using ATSProject.Interfaces;
using ATSProject.Model.ATS;
using ATSProject.Model.BillingSystem;

namespace ATSProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var station = CreateStation();
            var billingSystem = CreateBillingSystem(station);
            WorkWithStation(station, billingSystem);
            WorkWithBillingSystem(billingSystem);
            Console.ReadKey();
        }

        private static void WorkWithBillingSystem(BillingSystem billingSystem)
        {
            Console.WriteLine(billingSystem.Contracts[0].IsPaid ? "No debt" : "Debt");
            Console.WriteLine(billingSystem.Contracts[0].ToString());
            billingSystem.Contracts[1].PayOnDelivery(20000);
            Console.WriteLine(billingSystem.Contracts[1].IsPaid ? "No debt" : "Debt");
            Console.WriteLine(billingSystem.Contracts[1].ToString());
            Console.WriteLine(new string('-', 50));
            foreach (var item in billingSystem.GetCalls())
                Console.WriteLine(item.ToString());
            Console.WriteLine(new string('-', 50));
            foreach (var item in billingSystem.GetSortedCallsByPhoneNumber(item => item.Statistic.Cost > 0))
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
            foreach (var item in billingSystem.GetSortedCallsByCost())
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
            var calls = billingSystem.GetCallsByClient(billingSystem.Clients[0]);
            foreach (var item in calls)
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
            calls = billingSystem.GetCallsByClient(billingSystem.Clients[0], new Tuple<DateTime, DateTime>(new DateTime(2015, 5, 10),
                new DateTime(2015, 11, 10)));
            foreach (var item in calls)
                Console.WriteLine(item);
        }

        private static void WorkWithStation(Station station, BillingSystem billingSystem)
        {
            foreach (var item in station.Terminals)
                item.InsertIntoPort();
            station.Terminals[0].OutgoingCall("+375(152)24-15-10");
            station.Terminals[0].OutgoingCall("+375(152)24-15-10");
            station.TerminalByNumber("4568").OutgoingCall("+375(1512)2-70-15");
            station.TerminalByNumber("4568").OutgoingCall("+375(1512)2-70-16");
            Console.WriteLine(billingSystem.Contracts[0].ChangeTariffPlan(new TariffPlan("Middle", 30000, 20, 250))
                ? "Tariff plan is changed!" : "Tariff plan can be changed only 1 time per month!");
            Console.WriteLine(billingSystem.Contracts[1].ChangeTariffPlan(new TariffPlan("Middle", 30000, 20, 250))
                ? "Tariff plan is changed!" : "Tariff plan can be changed only 1 time per month!");
            station.Terminals[0].OutgoingCall("+375(1512)2-70-15");
            station.Terminals[3].RemoveFromPort();
            station.Terminals[2].OutgoingCall("+375(17)25-25-25");
            station.Terminals[2].RemoveFromPort();
            station.Terminals[2].OutgoingCall("+375(17)25-25-25");
        }

        private static BillingSystem CreateBillingSystem(Station station)
        {
            BillingSystem billingSystem = new BillingSystem(new List<IContract>
            {
                new Contract("1001", "+375(17)275-75-25", new TariffPlan("Active", 30000, 50, 150), "15", "2015-10-1"),
                new Contract("1002", "+375(152)24-15-10", new TariffPlan("Middle", 20000, 20, 250), "16", "2015-11-20"),
                new Contract("1003", "+375(1512)2-70-15", new TariffPlan("Light", 15000, 10, 500), "17", "2015-10-14")
            },
            new List<Client>
            {
                new Client("Ivan", "Ivanov", "1993-5-4", "Minsk"),
                new Client("Petr", "Semenov", "1963-10-24", "Grodno"),
                new Client("Anna", "Petrova", "1987-1-14", "Lida")
            }, station);
            return billingSystem;
        }

        private static Station CreateStation()
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
            return station;
        }
    }
}
