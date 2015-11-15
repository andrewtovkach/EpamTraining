using System;
using System.Collections.Generic;
using ATSProject.Interfaces;
using ATSProject.Model.BillingSystem;

namespace ATSProject.TestClasses
{
    public class TestBillingSystem : BillingSystem
    {
        public TestBillingSystem(IList<IContract> contracts, IList<Client> clients, IStation station) 
            : base(contracts, clients, station)
        {
        }

        public override void SubscriptionToStationEvents(IStation station)
        {
            station.CallIsProcessed += (sender, call) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Statistics: {0}", call);
                Console.ForegroundColor = ConsoleColor.Gray;
            };
        }
    }
}
