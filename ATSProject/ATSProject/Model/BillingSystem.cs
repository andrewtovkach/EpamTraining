using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Interfaces;

namespace ATSProject.Model
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
            MappingInit();
        }

        private void MappingInit()
        {
            RegistrationEvents();
            foreach (var item in _station.Terminals)
                _terminalsMapping.Add(item, null);
            foreach (var item in Clients)
                AddTerminalMapping(item);
        }

        public ITerminal FirstFreeTerminal
        {
            get { return _terminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        private Contract ContactByPhoneNumber(PhoneNumber phoneNumber)
        {
            return _contracts.FirstOrDefault(item => item.PhoneNumber == phoneNumber);
        }

        public void AddTerminalMapping(Client client)
        {
            var freeTerminal = FirstFreeTerminal;
            if (freeTerminal == null)
                throw new ArgumentException("All terminals are busy!");
            _terminalsMapping[freeTerminal] = client;
        }

        public bool RemovePortMapping(string portNumber)
        {
            var terminal = _station.TerminalByNumber(portNumber);
            if (!_terminalsMapping.ContainsKey(terminal))
                return false;
            _terminalsMapping[terminal] = null;
            return true;
        }

        private void RegistrationEvents()
        {
            _station.CallProcessed += (sender, info) => { CalculateCostOfСall(info); };
        }

        private void CalculateCostOfСall(CallInfo info)
        {
            Contract sourceContract = ContactByPhoneNumber(info.Source);
            sourceContract.PersonalAccount.Calculate(info);
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
