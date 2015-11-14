using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Interfaces;
using ATSProject.Model.ATS;

namespace ATSProject.Model.BillingSystem
{
    public abstract class BillingSystem : IBillingSystem, IEnumerable<Call>
    {
        private readonly IStation _station;

        public IList<IContract> Contracts { get; private set; }
        public IList<Client> Clients { get; private set; }
        private readonly IList<Call> _calls;
        private readonly IDictionary<ITerminal, Client> _terminalsMapping;

        protected BillingSystem(IList<IContract> contracts, IList<Client> clients, IStation station)
        {
            _terminalsMapping = new Dictionary<ITerminal, Client>();
            _calls = new List<Call>();
            Contracts = contracts;
            Clients = clients;
            _station = station;
            MappingInit();
        }

        public Call this[int index]
        {
            get { return _calls[index]; }
        }

        private void MappingInit()
        {
            SubscriptionToEvents(_station);
            SubscriptionToStationEvents(_station);
            foreach (var item in ((Station)_station).Terminals)
                _terminalsMapping.Add(item, null);
            foreach (var item in Clients)
                AddMapping(item);
        }

        public ITerminal FirstFreeTerminal
        {
            get { return _terminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public IContract ContactByPhoneNumber(PhoneNumber phoneNumber)
        {
            return Contracts.FirstOrDefault(item => item.PhoneNumber == phoneNumber);
        }

        public PhoneNumber PhoneNumberByClient(Client client)
        {
            return _terminalsMapping.FirstOrDefault(item => item.Value == client).Key.PhoneNumber;
        }

        private void SubscriptionToEvents(IStation station)
        {
            station.CallIsProcessed += (sender, call) => { CalculateCostOfCall(call); };
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
            var terminal = ((Station)_station).TerminalByNumber(elementNumber);
            if (!_terminalsMapping.ContainsKey(terminal))
                return false;
             terminal.ClearEvents();
            _terminalsMapping[terminal] = null;
            return true;
        }

        public abstract void SubscriptionToStationEvents(IStation station);

        public IEnumerable<Call> GetCallsByClient(Client cl)
        {
            DateTime fromDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
               toDateTime = fromDateTime.AddMonths(1);
            return GetCalls(item => item.Info.Date >= fromDateTime && item.Info.Date < toDateTime
                && item.Info.Source == PhoneNumberByClient(cl));
        }

        public IEnumerable<Call> GetCallsByClient(Client cl, Tuple<DateTime, DateTime> tuple)
        {
            return GetCalls(item => item.Info.Date >= tuple.Item1 && item.Info.Date < tuple.Item2
                && item.Info.Source == PhoneNumberByClient(cl));
        }

        private void CalculateCostOfCall(Call call)
        {
            IContract sourceContract = ContactByPhoneNumber(call.Info.Source);
            double cost = sourceContract.CalculateCost(call.Statistic.Duration);
            call.Statistic.Cost = cost;
            _calls.Add(call);
            sourceContract.IncreaseDebt(cost);
        }

        public IEnumerable<Call> GetCalls()
        {
            return _calls;
        }

        public IEnumerable<Call> GetCalls(Func<Call, bool> predicate)
        {
            return _calls.Where(predicate);
        }

        public IEnumerable<Call> GetSortedCallsByCost(bool descending = false)
        {
            return descending ? _calls.OrderBy(item => item.Statistic.Cost)
                : _calls.OrderByDescending(item => item.Statistic.Cost);
        }

        public IEnumerable<Call> GetSortedCallsByCost(Func<Call, bool> predicate, bool descending = false)
        {
            return GetSortedCallsByCost(descending).Where(predicate);
        }

        public IEnumerable<Call> GetSortedCallsByPhoneNumber(bool descending = false)
        {
            return descending ? _calls.OrderBy(item => item.Info.Source).ThenBy(item => item.Info.Target)
                : _calls.OrderByDescending(item => item.Info.Source).ThenByDescending(item => item.Info.Target);
        }

        public IEnumerable<Call> GetSortedCallsByPhoneNumber(Func<Call, bool> predicate, bool descending = false)
        {
            return GetSortedCallsByPhoneNumber(descending).Where(predicate);
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
