using System;
using System.Collections.Generic;
using System.Linq;

namespace ATSProject
{
    public class Station
    {
        public readonly IDictionary<Port, Terminal> PortsMapping;
        public readonly IDictionary<Terminal, Client> TerminalsMapping;

        public Station(ICollection<Port> ports, ICollection<Terminal> terminals, ICollection<Client> clients)
        {
            PortsMapping = new Dictionary<Port, Terminal>();
            TerminalsMapping = new Dictionary<Terminal, Client>();
            Init(ports, terminals);
            Mapping(terminals, clients);
        }

        private void Init(ICollection<Port> ports, ICollection<Terminal> terminals)
        {
            foreach (var item in ports)
            {
                PortsMapping.Add(item, null);
                item.StateChanged += (sender, state) => { Console.WriteLine("State Changed"); };
            }
            foreach (var item in terminals)
                TerminalsMapping.Add(item, null);
        }

        private void Mapping(ICollection<Terminal> terminals, ICollection<Client> clients)
        {
            foreach (var item in terminals)
                AddPortMapping(item);
            foreach (var item in clients)
                AddTerminalMapping(item);
        }

        public void AddTerminalMapping(Client client)
        {
            Terminal freeTerminal = FirstFreeTerminal;
            if (freeTerminal == null)
                throw new ArgumentException("All terminals are busy!");
            TerminalsMapping[freeTerminal] = client;
        }

        public bool RemoveTerminalMapping(string terminalNumber)
        {
            Terminal terminal = TerminalsMapping.Keys.First(item => item.Numer == terminalNumber);
            if (!TerminalsMapping.ContainsKey(terminal))
                return false;
            TerminalsMapping[terminal] = null;
            return true;
        }

        private void AddPortMapping(Terminal terminal)
        {
            Port freePort = FirstFreePort;
            if (freePort == null)
                throw new ArgumentException("All ports are busy!");
            PortsMapping[freePort] = terminal;
        }

        public bool RemovePortMapping(string portNumber)
        {
            Port port = PortsMapping.Keys.First(item => item.Number == portNumber);
            if (!PortsMapping.ContainsKey(port)) 
                return false;
            PortsMapping[port] = null;
            return true;
        }

        public Port FirstFreePort
        {
            get { return PortsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public Terminal FirstFreeTerminal
        {
            get { return TerminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }
    }
}
