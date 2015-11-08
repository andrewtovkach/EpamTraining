using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model
{
    public class Station : IStation, IEnumerable<CallInfo>
    {
        public ICollection<ITerminal> Terminals { get; private set; }
        public ICollection<IPort> Ports { get; private set; }
        private readonly ICollection<CallInfo> _calls;
        private readonly IDictionary<IPort, ITerminal> _portsMapping;

        public Station(ICollection<IPort> ports, ICollection<ITerminal> terminals)
        {
            _portsMapping = new Dictionary<IPort, ITerminal>();
            _calls = new List<CallInfo>();
            Terminals = terminals;
            Ports = ports;
            MappingInit();
        }

        private void MappingInit()
        {
            foreach (var item in Ports)
                _portsMapping.Add(item, null);
            foreach (var item in Terminals)
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
            return Ports.FirstOrDefault(item => item.Number == portNumber);
        }

        private IPort PortByTerminal(ITerminal terminal)
        {
            return _portsMapping.FirstOrDefault(item => item.Value == terminal).Key;
        }

        public ITerminal TerminalByNumber(string terminalNumber)
        {
            return Terminals.FirstOrDefault(item => item.Number == terminalNumber);
        }

        private ITerminal TerminalByPhoneNumber(string phoneNumber)
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
            try
            {
                ITerminal sourceTerminal = TerminalByPhoneNumber(info.Source.ToString()),
                targetTerminal = TerminalByPhoneNumber(info.Target.ToString());
                if (CheckConnection(info.Source) && CheckConnection(info.Target))
                    Calling(info, sourceTerminal, targetTerminal);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Allowed only call within the ATS!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private bool CheckConnection(PhoneNumber number)
        {
            ITerminal terminal = TerminalByPhoneNumber(number.ToString());
            IPort port = PortByTerminal(terminal);
            return terminal.IsOnline && port.State == PortState.Free;
        }

        private void Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal)
        {
            CallInfo callInfo = new CallInfo(info.Source, info.Target, info.Date, info.Duration,
                CallType.IncomingCall, Result.Success);
            ((Terminal)targetTerminal).OnIncomingRequest(callInfo);
            PortByTerminal(sourceTerminal).State = PortState.Busy;
            PortByTerminal(targetTerminal).State = PortState.Busy;
            CallImulation(callInfo);
            PortByTerminal(sourceTerminal).State = PortState.Free;
            PortByTerminal(targetTerminal).State = PortState.Free;
        }

        public virtual void CallImulation(CallInfo callInfo)
        {
            Thread.Sleep(callInfo.Duration.Minutes * 100);
        }

        public event EventHandler<CallInfo> CallProcessed;

        public void OnCallProcessed(CallInfo info)
        {
            if (CallProcessed != null && info.Result == Result.Success)
                CallProcessed(this, info);
        }

        private void RegistrationEvents(ITerminal terminal)
        {
            terminal.OutgoingRequest += (sender, info) =>
            {
                _calls.Add(info);
                IncomingCall(info);
            };
            terminal.IncomingRequest += (sender, info) =>
            {
                _calls.Add(info);
                OnCallProcessed(info);
            };
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

        public void ClearEvents()
        {
            CallProcessed = null;
        }

        public IEnumerator<CallInfo> GetEnumerator()
        {
            return _calls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
