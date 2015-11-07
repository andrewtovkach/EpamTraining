using System;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Interfaces;

namespace ATSProject.Model
{
    public class BillingSystem
    {
        private readonly Station _station;

        public readonly ICollection<Contract> Contracts;
        public readonly ICollection<Client> Clients; 
        private readonly IDictionary<ITerminal, Client> _terminalsMapping; 

        public BillingSystem(ICollection<Contract> contracts, ICollection<Client> clients, Station station)
        {
            Contracts = contracts;
            Clients = clients;
            _terminalsMapping = new Dictionary<ITerminal, Client>();
            _station = station;
            MappingInit(clients);
        }

        private void MappingInit(ICollection<Client> clients)
        {
            foreach (var item in _station.Terminals)
                _terminalsMapping.Add(item, null);
            foreach (var item in clients)
                AddTerminalMapping(item);
        }

        public ITerminal FirstFreeTerminal
        {
            get { return _terminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public ITerminal TerminalByClient(Client client)
        {
            return _terminalsMapping.FirstOrDefault(item => item.Value == client).Key;
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

    }
}
