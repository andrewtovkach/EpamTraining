using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Interfaces;
using ATSProject.Model.ATS;

namespace ATSProject.Model.BillingSystem
{
    public class BillingSystem : IBillingSystem, IEnumerable<Contract>
    {
        private readonly Station _station;

        private readonly ICollection<Contract> _contracts;
        public ICollection<Client> Clients { get; private set; }
        private readonly IDictionary<ITerminal, Client> _terminalsMapping;

        public BillingSystem(ICollection<Contract> contracts, ICollection<Client> clients, Station station)
        {
            _terminalsMapping = new Dictionary<ITerminal, Client>();
            _contracts = contracts;
            Clients = clients;
            _station = station;
            SubscriptionToStationEvents(_station);
            MappingInit();
        }

        private void MappingInit()
        {
            foreach (var item in _station.Terminals)
                _terminalsMapping.Add(item, null);
            foreach (var item in Clients)
                AddMapping(item);
        }

        public ITerminal FirstFreeTerminal
        {
            get { return _terminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        private Contract ContactByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _contracts.FirstOrDefault(item => item.PhoneNumber == phoneNumber);
        }

        public void AddMapping(Client element)
        {
            var freeTerminal = FirstFreeTerminal;
            if (freeTerminal == null)
                throw new ArgumentException("All terminals are busy!");
            _terminalsMapping[freeTerminal] = element;
        }

        public bool RemoveMapping(string elementNumber)
        {
            var terminal = _station.TerminalByNumber(elementNumber);
            if (!_terminalsMapping.ContainsKey(terminal))
                return false;
            _terminalsMapping[terminal] = null;
            return true;
        }

        private void SubscriptionToStationEvents(IStation station)
        {
            station.CallIsProcessed += (sender, info) => { Print(CalculateCostOfСall(info)); };
        }

        private Tuple<CallInfo, CallStatistic> CalculateCostOfСall(Tuple<CallInfo, CallStatistic> info)
        {
            Contract sourceContract = ContactByPhoneNumber(info.Item1.Source);
            return sourceContract.PersonalAccount.Calculate(info);
        }

        private static void Print(Tuple<CallInfo, CallStatistic> info)
        {
            Console.WriteLine("Statistics: {0} {1} BYR", info.Item1.Source, info.Item2);
        }

        public IEnumerator<Contract> GetEnumerator()
        {
            return _contracts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
