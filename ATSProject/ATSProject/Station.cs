using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ATSProject
{
    public class Station
    {
        public readonly ICollection<CallInfo> Calls;
        public readonly IDictionary<IPort, ITerminal> PortsMapping;
        public readonly IDictionary<ITerminal, Client> TerminalsMapping;

        public Station(ICollection<IPort> ports, ICollection<ITerminal> terminals, ICollection<Client> clients)
        {
            PortsMapping = new Dictionary<IPort, ITerminal>();
            TerminalsMapping = new Dictionary<ITerminal, Client>();
            Calls = new List<CallInfo>();
            Init(ports, terminals);
            Mapping(terminals, clients);
        }

        private void Init(ICollection<IPort> ports, ICollection<ITerminal> terminals)
        {
            foreach (var item in ports)
                PortsMapping.Add(item, null);
            foreach (var item in terminals)
                TerminalsMapping.Add(item, null);
        }

        private void Mapping(ICollection<ITerminal> terminals, ICollection<Client> clients)
        {
            foreach (var item in terminals)
                AddPortMapping(item);
            foreach (var item in clients)
                AddTerminalMapping(item);
        }

        public void AddTerminalMapping(Client client)
        {
            var freeTerminal = FirstFreeTerminal;
            if (freeTerminal == null)
                throw new ArgumentException("All terminals are busy!");
            TerminalsMapping[freeTerminal] = client;
            freeTerminal.OutgoingCall += (sender, info) =>
            {
                Console.WriteLine("Outgoing Call " + info.ToString());
                IncomingCallAction(info);
            };
            freeTerminal.IncomingCall += (sender, info) =>
            {
                Console.WriteLine("Incoming Call " + info.ToString());
            };
            freeTerminal.StateChanged += (sender, state) =>
            {
                var terminal = sender as ITerminal;
                if (terminal != null)
                    Console.WriteLine(terminal.ToString());
            };
        }

        private void IncomingCallAction(CallInfo info)
        {
            if (info.Result == Result.Fail)
                return;
            ITerminal sourceTerminal = TerminalByPhoneNumer(info.Source.ToString()),
                targetTerminal = (Terminal) TerminalByPhoneNumer(info.Target.ToString());
            if (!(sourceTerminal.IsOnline && targetTerminal.IsOnline))
            {
                info.Result = Result.Fail;
                ((Terminal)targetTerminal).OnIncomingCall(info);
                return;
            }
            Calling(info, sourceTerminal, targetTerminal);
        }

        private void Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal)
        {
            PortByTerminal(sourceTerminal).State = PortState.Busy;
            PortByTerminal(targetTerminal).State = PortState.Busy;
            info.Result = Result.Success;
            ((Terminal)targetTerminal).OnIncomingCall(info);
            Thread.Sleep(info.Duration.Seconds * 1000);
            Calls.Add(info);
            PortByTerminal(sourceTerminal).State = PortState.Free;
            PortByTerminal(targetTerminal).State = PortState.Free;
        }

        public bool RemoveTerminalMapping(string terminalNumber)
        {
            var terminal = TerminalsMapping.Keys.First(item => item.Number == terminalNumber);
            if (!TerminalsMapping.ContainsKey(terminal))
                return false;
            TerminalsMapping[terminal] = null;
            terminal.ClearEvents();
            return true;
        }

        private void AddPortMapping(ITerminal terminal)
        {
            var freePort = FirstFreePort;
            if (freePort == null)
                throw new ArgumentException("All ports are busy!");
            PortsMapping[freePort] = terminal;
            freePort.StateChanged += (sender, state) =>
            {
                var port = sender as Port;
                if (port != null)
                    Console.WriteLine(port.ToString());
                if (state != PortState.NotConnected)
                    terminal.Online();
                else terminal.Offline();
            };
        }

        public bool RemovePortMapping(string portNumber)
        {
            var port = PortsMapping.Keys.First(item => item.Number == portNumber);
            if (!PortsMapping.ContainsKey(port))
                return false;
            PortsMapping[port] = null;
            port.ClearEvents();
            return true;
        }

        public IPort PortByNumer(string portNumber)
        {
            IPort port = PortsMapping.Keys.FirstOrDefault(item => item.Number == portNumber);
            if (port == null)
                throw new ArgumentException("Incorrect port number!");
            return port;
        }

        public IPort PortByTerminal(ITerminal terminal)
        {
            IPort port = PortsMapping.FirstOrDefault(item => item.Value == terminal).Key;
            if (port == null)
                throw new ArgumentException("Incorrect terminal!");
            return port;
        }

        public ITerminal TerminalByNumer(string terminalNumber)
        {
            ITerminal terminal = TerminalsMapping.Keys.FirstOrDefault(item => item.Number == terminalNumber);
            if (terminal == null)
                throw new ArgumentException("Incorrect terminal number!");
            return terminal;
        }

        public ITerminal TerminalByPhoneNumer(string phoneNumber)
        {
            ITerminal terminal = TerminalsMapping.Keys.First(item => item.PhoneNumber.ToString() == phoneNumber);
            if (terminal == null)
                throw new ArgumentException("Incorrect phone number!");
            return terminal;
        }

        public IPort FirstFreePort
        {
            get { return PortsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public ITerminal FirstFreeTerminal
        {
            get { return TerminalsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public bool TerminalIsOnline(string terminalNumber)
        {
            ITerminal terminal = PortsMapping.Values.First(item => item.Number == terminalNumber);
            if (terminal == null)
                throw new ArgumentException("Incorrect terminal number!");
            return PortsMapping.Where(item => item.Value == terminal).Select(item => item.Key.IsOnline).FirstOrDefault();
        }

        public bool TerminalIsOffline(string terminalNumber)
        {
            return !TerminalIsOnline(terminalNumber);
        }
    }
}
