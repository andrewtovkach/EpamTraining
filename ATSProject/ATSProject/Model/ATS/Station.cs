﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model.ATS
{
    public abstract class Station : IStation, IEnumerable<CallInfo>
    {
        public IList<ITerminal> Terminals { get; private set; }
        public IList<IPort> Ports { get; private set; }
        private readonly IList<CallInfo> _calls;
        private readonly IDictionary<IPort, ITerminal> _portsMapping;

        protected Station(IList<IPort> ports, IList<ITerminal> terminals)
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
                SubscriptionToEvents(item);
            }
        }

        private void SubscriptionToEvents(ITerminal item)
        {
            item.InsertedIntoPort += (sender, args) =>
            {
                IPort port = PortByTerminal(sender as Terminal);
                port.State = PortState.Free;
            };
            item.RemovedFromPort += (sender, args) =>
            {
                IPort port = PortByTerminal(sender as Terminal);
                port.State = PortState.NotConnected;
            };
            item.OutgoingRequest += (sender, info) => { IncomingCall(info); };
        }

        public IPort FirstFreePort
        {
            get { return _portsMapping.FirstOrDefault(item => item.Value == null).Key; }
        }

        protected IPort PortByNumber(string portNumber)
        {
            return Ports.FirstOrDefault(item => item.Number == portNumber);
        }

        protected IPort PortByTerminal(ITerminal terminal)
        {
            return _portsMapping.FirstOrDefault(item => item.Value == terminal).Key;
        }

        protected ITerminal TerminalByPhoneNumber(PhoneNumber phoneNumber)
        {
            return Terminals.FirstOrDefault(item => item.PhoneNumber == phoneNumber);
        }

        public ITerminal TerminalByNumber(string terminalNumber)
        {
            return Terminals.FirstOrDefault(item => item.Number == terminalNumber);
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
            port.ClearEvents();
            _portsMapping[port] = null;
            return true;
        }

        public void IncomingCall(CallInfo info)
        {
            try
            {
                ITerminal sourceTerminal = TerminalByPhoneNumber(info.Source),
                    targetTerminal = TerminalByPhoneNumber(info.Target);
                if (CheckConnection(info.Source) && CheckConnection(info.Target) && info.Result != Result.Fail)
                {
                    _calls.Add(info);
                    Call call = Calling(info, sourceTerminal, targetTerminal);
                    _calls.Add(call.Info);
                    OnCallIsProcessed(call);
                    Call outgoingCall = new Call { Info = call.Info, Statistic = call.Statistic };
                    outgoingCall.Info.Type = CallType.OutgoingCall;
                    OnCallIsProcessed(outgoingCall);
                }
                else
                {
                    _calls.Add(info);
                    OnCallIsProcessed(new Call { Info = info });
                }
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
            ITerminal terminal = TerminalByPhoneNumber(number);
            IPort port = PortByTerminal(terminal);
            return terminal.IsOnline && port.State == PortState.Free;
        }

        public Call Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal)
        {
            ChangePortState(sourceTerminal, targetTerminal, PortState.Busy);
            info.Type = CallType.IncomingCall;
            ((Terminal)targetTerminal).OnIncomingRequest(info);
            Call call = CallingImulation(info);
            ChangePortState(sourceTerminal, targetTerminal, PortState.Free);
            return call;
        }

        private void ChangePortState(ITerminal sourceTerminal, ITerminal targetTerminal, PortState state)
        {
            PortByTerminal(sourceTerminal).State = state;
            PortByTerminal(targetTerminal).State = state;
        }

        public abstract Call CallingImulation(CallInfo info);

        public event EventHandler<Call> CallIsProcessed;

        protected virtual void OnCallIsProcessed(Call call)
        {
            if (CallIsProcessed != null)
                CallIsProcessed(this, call);
        }

        public abstract void SubscriptionToTerminalEvents(ITerminal terminal);

        public abstract void SubscriptionToPortEvents(IPort port);

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
