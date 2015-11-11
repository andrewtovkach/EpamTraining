using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model.ATS
{
    public class Station : IStation, IEnumerable<CallInfo>
    {
        public IList<ITerminal> Terminals { get; private set; }
        public IList<IPort> Ports { get; private set; }
        private readonly IList<CallInfo> _calls;
        private readonly IDictionary<IPort, ITerminal> _portsMapping;

        public Station(IList<IPort> ports, IList<ITerminal> terminals)
        {
            _portsMapping = new Dictionary<IPort, ITerminal>();
            _calls = new List<CallInfo>();
            Terminals = terminals;
            Ports = ports;
            MappingInit();
        }

        public CallInfo this[int index]
        {
            get { return _calls[index]; }
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
            OnCallIsProcessed(new Call { Info = info });
            if (info.Result == Result.Fail)
                return;
            try
            {
                ITerminal sourceTerminal = TerminalByPhoneNumber(info.Source.ToString()),
                targetTerminal = TerminalByPhoneNumber(info.Target.ToString());
                if (CheckConnection(info.Source) && CheckConnection(info.Target))
                {
                    Call call = Calling(info, sourceTerminal, targetTerminal);
                    OnCallIsProcessed(call);
                }
                else info.Result = Result.Fail;
            }
            catch
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

        private Call Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal)
        {
            PortByTerminal(sourceTerminal).State = PortState.Busy;
            PortByTerminal(targetTerminal).State = PortState.Busy;
            info.Type = CallType.IncomingCall;
            ((Terminal)targetTerminal).OnIncomingRequest(info);
            TimeSpan duration = CallingImulation(info);
            Call call = new Call
            {
                Info = info,
                Statistic = new CallStatistic { Duration = duration }
            };
            Thread.Sleep(duration.Minutes * 100);
            info.Result = Result.Success;
            PortByTerminal(sourceTerminal).State = PortState.Free;
            PortByTerminal(targetTerminal).State = PortState.Free;
            return call;
        }

        public TimeSpan CallingImulation(CallInfo info)
        {
            int minutes = new Random().Next(60), seconds = new Random().Next(60);
            return new TimeSpan(0, minutes, seconds);
        }

        public event EventHandler<Call> CallIsProcessed;

        public void OnCallIsProcessed(Call call)
        {
            if (CallIsProcessed != null)
                CallIsProcessed(this, call);
        }

        private void SubscriptionToTerminalEvents(ITerminal terminal)
        {
            terminal.OutgoingRequest += (sender, info) =>
            {
                _calls.Add(info);
                Console.WriteLine(info.ToString());
                IncomingCall(info);
            };
            terminal.IncomingRequest += (sender, info) =>
            {
                _calls.Add(info);
                Console.WriteLine(info.ToString());
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
            terminal.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
        }

        private void SubscriptionToPortEvents(IPort port)
        {
            port.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
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
