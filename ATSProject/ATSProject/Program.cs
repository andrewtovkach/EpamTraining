﻿using System;
using System.Collections.Generic;
using ATSProject.Interfaces;
using ATSProject.Model.ATS;
using ATSProject.Model.BillingSystem;
using ATSProject.TestClasses;

namespace ATSProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 50);
            var station = CreateStation();
            var billingSystem = CreateBillingSystem(station);
            WorkWithStation(station, billingSystem);
            WorkWithBillingSystem(billingSystem);
            Console.ReadKey();
        }

        private static void WorkWithBillingSystem(BillingSystem billingSystem)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Calls with cost > 5000BYR: ");
            foreach (var item in billingSystem.GetCalls(item => item.Statistic.Cost > 5000))
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Calls grouped by number: ");
            foreach (var group in billingSystem.GetCallsGroupedByNumber())
            {
                Console.WriteLine("-> " + group.Key + " <-");
                foreach (var item in group)
                    Console.WriteLine(item);
            }
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Sorted calls by cost: ");
            foreach (var item in billingSystem.GetSortedCallsByCost())
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Calls by first client");
            foreach (var item in billingSystem.GetCallsByClient(billingSystem.Clients[0]))
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));
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
            BillingSystem billingSystem = new TestBillingSystem(new List<IContract>
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
            Station station = new TestStation(new List<IPort>
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
