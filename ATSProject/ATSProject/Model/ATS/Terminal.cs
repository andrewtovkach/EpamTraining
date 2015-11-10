using System;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model.ATS
{
    public class Terminal : ITerminal
    {
        public string Number { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        private TerminalState _state;
        public TerminalState State
        {
            get { return _state; }
            set
            {
                if (_state == value) return;
                _state = value;
                OnStateChanged(value);
            }
        }

        public Terminal(string number, string phoneNumber)
        {
            Number = number;
            PhoneNumber = new PhoneNumber { Value = phoneNumber };
            State = TerminalState.Offline;
        }

        public bool IsOnline
        {
            get { return State == TerminalState.Online; }
        }

        public event EventHandler<TerminalState> StateChanged;

        private void OnStateChanged(TerminalState state)
        {
            if (StateChanged != null) 
                StateChanged(this, state);
        }

        public event EventHandler InsertedIntoPort;

        public void InsertIntoPort()
        {
            State = TerminalState.Online;
            if (InsertedIntoPort != null)
                InsertedIntoPort(this, EventArgs.Empty);
        }

        public event EventHandler RemovedFromPort;

        public void RemoveFromPort()
        {
            State = TerminalState.Offline;
            if (RemovedFromPort != null)
                RemovedFromPort(this, EventArgs.Empty);
        }

        public event EventHandler<CallInfo> OutgoingRequest;

        private void OnOutgoingRequest(CallInfo info)
        {
            if (OutgoingRequest == null) 
                return;
            Print(info);
            OutgoingRequest(this, info);
        }

        public event EventHandler<CallInfo> IncomingRequest;

        public void OnIncomingRequest(CallInfo info)
        {
            if (IncomingRequest == null) 
                return;
            Print(info);
            IncomingRequest(this, info);
        }

        public void OutgoingCall(string phoneNumber)
        {
            Result result = IsOnline ? Result.Success : Result.Fail;
            CallInfo callInfo = new CallInfo
            {
                Source = PhoneNumber,
                Target = new PhoneNumber { Value = phoneNumber},
                Type = CallType.OutgoingCall,
                Date = DateTime.Now,
                Result = result
            };
            OnOutgoingRequest(callInfo);
        }

        private static void Print(CallInfo info)
        {
            Console.WriteLine(info.ToString());
        }

        public void ClearEvents()
        {
            StateChanged = null;
            OutgoingRequest = null;
            IncomingRequest = null;
            InsertedIntoPort = null;
            RemovedFromPort = null;
        }

        public override string ToString()
        {
            return string.Format("Terminal №{0} ({1}) - {2}", Number, PhoneNumber, State);
        }
    }
}
