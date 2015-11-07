using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model
{
    public class Station : IStation
    {
        public readonly ICollection<CallInfo> Calls;
        public readonly ICollection<ITerminal> Terminals;
        private readonly IDictionary<IPort, ITerminal> _portsMapping;

        public Station(ICollection<IPort> ports, ICollection<ITerminal> terminals)
        {
            _portsMapping = new Dictionary<IPort, ITerminal>();
            Terminals = terminals;
            Calls = new List<CallInfo>();
            MappingInit(ports, terminals);
        }

        private void MappingInit(ICollection<IPort> ports, ICollection<ITerminal> terminals)
        {
            foreach (var item in ports)
                _portsMapping.Add(item, null);
            foreach (var item in terminals)
            {
                AddPortMapping(item);
                item.RegistrationEvents();
                RegistrationEvents(item);
            }
        }

        public IPort FirstFreePort
        {
            get { return _portsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        public IPort PortByNumber(string portNumber)
        {
            return _portsMapping.Keys.FirstOrDefault(item => item.Number == portNumber);
        }

        public IPort PortByTerminal(ITerminal terminal)
        {
            return _portsMapping.FirstOrDefault(item => item.Value == terminal).Key;
        }

        public ITerminal TerminalByNumber(string terminalNumber)
        {
            return Terminals.FirstOrDefault(item => item.Number == terminalNumber);
        }

        public ITerminal TerminalByPhoneNumber(string phoneNumber)
        {
            return Terminals.FirstOrDefault(item => item.PhoneNumber.ToString() == phoneNumber);
        }

        public void AddPortMapping(ITerminal terminal)
        {
            var freePort = FirstFreePort;
            if (freePort == null)
                throw new ArgumentException("All ports are busy!");
            _portsMapping[freePort] = terminal;
            freePort.RegistrationEvents();
        }

        public bool RemovePortMapping(string portNumber)
        {
            var port = PortByNumber(portNumber);
            if (!_portsMapping.ContainsKey(port))
                return false;
            _portsMapping[port] = null;
            port.ClearEvents();
            return true;
        }

        public void IncomingCall(CallInfo info)
        {
            if (info.Result == Result.Fail)
                return;
            ITerminal sourceTerminal = TerminalByPhoneNumber(info.Source.ToString()),
                targetTerminal = TerminalByPhoneNumber(info.Target.ToString());
            if ((CheckConnection(info.Source) && CheckConnection(info.Target)))
                Calling(info, sourceTerminal, targetTerminal);
        }

        private bool CheckConnection(PhoneNumber number)
        {
            ITerminal terminal = TerminalByPhoneNumber(number.ToString());
            IPort port = PortByTerminal(terminal);
            return terminal.IsOnline && port.State == PortState.Free;
        }

        private void Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal)
        {
            PortByTerminal(sourceTerminal).State = PortState.Busy;
            CallInfo callInfo = new CallInfo(info.Source, info.Target, info.Date, info.Duration, 
                CallType.IncomingCall, Result.Success);
            ((Terminal)targetTerminal).OnIncomingRequest(callInfo);
            OnCallProcessed(callInfo);
            PortByTerminal(targetTerminal).State = PortState.Busy;
            Thread.Sleep(callInfo.Duration.Seconds * 1000);
            PortByTerminal(sourceTerminal).State = PortState.Free;
            PortByTerminal(targetTerminal).State = PortState.Free;
        }

        public event EventHandler<CallInfo> CallProcessed;

        public void OnCallProcessed(CallInfo info)
        {
            if (CallProcessed != null)
                CallProcessed(this, info);
        }

        private void RegistrationEvents(ITerminal terminal)
        {
            terminal.OutgoingRequest += (sender, info) =>
            {
                Calls.Add(info);
                IncomingCall(info);
            };
            terminal.IncomingRequest += (sender, info) => { Calls.Add(info); };
            terminal.InsertedIntoPort += (sender, args) =>
            {
                IPort port = PortByTerminal(sender as Terminal);
                port.State = PortState.Free;
            };
            terminal.RemovedFromPort += (sender, args) =>
            {
                IPort port = PortByTerminal(sender as Terminal);
                port.State = PortState.NotConnected;
            };
        }
    }
}
