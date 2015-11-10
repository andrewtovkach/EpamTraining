using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATSProject.Enums;
using ATSProject.Interfaces;
using ATSProject.Model.BillingSystem;

namespace ATSProject.Model.ATS
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
            {
                _portsMapping.Add(item, null);
                SubscriptionToPortEvents(item);
            }
            foreach (var item in Terminals)
            {
                AddMapping(item);
                SubscriptionToTerminalEvents(item);
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

        public void AddMapping(ITerminal element)
        {
            var freePort = FirstFreePort;
            if (freePort == null)
                throw new ArgumentException("All ports are busy!");
            _portsMapping[freePort] = element;
        }

        public bool RemoveMapping(string elementNumber)
        {
            var port = PortByNumber(elementNumber);
            if (!_portsMapping.ContainsKey(port))
                return false;
            _portsMapping[port] = null;
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
            PortByTerminal(sourceTerminal).State = PortState.Busy;
            PortByTerminal(targetTerminal).State = PortState.Busy;
            info.Type = CallType.IncomingCall;
            ((Terminal)targetTerminal).OnIncomingRequest(info);
            Tuple<CallInfo, CallStatistic> callInfo = new Tuple<CallInfo, CallStatistic>(info, 
                new CallStatistic { Duration = CallingImulation(info) });
            Thread.Sleep(callInfo.Item2.Duration.Minutes * 100);
            PortByTerminal(sourceTerminal).State = PortState.Free;
            PortByTerminal(targetTerminal).State = PortState.Free;
            OnCallIsProcessed(callInfo);
        }

        public TimeSpan CallingImulation(CallInfo info)
        {
            int minutes = new Random().Next(60), seconds = new Random().Next(60);
            return new TimeSpan(0, minutes, seconds);
        }

        public event EventHandler<Tuple<CallInfo, CallStatistic>> CallIsProcessed;

        public void OnCallIsProcessed(Tuple<CallInfo, CallStatistic> info)
        {
            if (CallIsProcessed == null || info.Item1.Result != Result.Success) 
                return;
            Print(info);
            CallIsProcessed(this, info);
        }

        private void SubscriptionToTerminalEvents(ITerminal terminal)
        {
            terminal.OutgoingRequest += (sender, info) =>
            {
                _calls.Add(info);
                IncomingCall(info);
            };
            terminal.IncomingRequest += (sender, info) => { _calls.Add(info); };
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
            terminal.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
        }

        private void SubscriptionToPortEvents(IPort port)
        {
            port.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
        }

        private static void Print(Tuple<CallInfo, CallStatistic> info)
        {
            Console.WriteLine("Transmits a request: {0}", info.Item1.Source);
        }

        public void ClearEvents()
        {
            CallIsProcessed = null;
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
