using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Interfaces;
using ATSProject.Model.ATS;

namespace ATSProject.Model.BillingSystem
{
    public class BillingSystem : IBillingSystem, IEnumerable<Call>
    {
        private readonly Station _station;

        public IList<Contract> Contracts { get; private set; }
        public ICollection<Client> Clients { get; private set; }
        private readonly IList<Call> _calls; 
        private readonly IDictionary<ITerminal, Client> _terminalsMapping;

        public BillingSystem(IList<Contract> contracts, ICollection<Client> clients, Station station)
        {
            _terminalsMapping = new Dictionary<ITerminal, Client>();
            _calls = new List<Call>();
            Contracts = contracts;
            Clients = clients;
            _station = station;
            SubscriptionToStationEvents(_station);
            MappingInit();
        }

        public Call this[int index]
        {
            get { return _calls[index]; }
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
            return Contracts.FirstOrDefault(item => item.PhoneNumber == phoneNumber);
        }

        public Contract ContactByNumber(string terminalNumber)
        {
            return Contracts.FirstOrDefault(item => item.Number == terminalNumber);
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
            station.CallIsProcessed += (sender, call) =>
            {
                CalculateCostOfCall(call);
                Console.WriteLine("Statistics: {0} {1} BYR", call.Info.Source, call.Statistic);
            };
        }

        private void CalculateCostOfCall(Call call)
        {
            Contract sourceContract = ContactByPhoneNumber(call.Info.Source);
            double cost = sourceContract.TariffPlan.CalculateCost(call.Statistic.Duration);
            call.Statistic.Cost = cost;
            _calls.Add(call);
            sourceContract.PersonalAccount.Debt += cost;
        }

        public IEnumerator<Call> GetEnumerator()
        {
            return _calls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
