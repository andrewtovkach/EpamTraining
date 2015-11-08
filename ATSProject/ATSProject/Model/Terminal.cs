using System;
using ATSProject.Enums;
using ATSProject.Interfaces;

namespace ATSProject.Model
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

        protected void OnStateChanged(TerminalState state)
        {
            if (StateChanged != null)
                StateChanged(this, state);
        }

        public event EventHandler InsertedIntoPort;

        protected void OnInsertedIntoPort()
        {
            if (InsertedIntoPort != null)
                InsertedIntoPort(this, EventArgs.Empty);
        }

        public void InsertIntoPort()
        {
            State = TerminalState.Online;
            OnInsertedIntoPort();
        }

        public event EventHandler RemovedFromPort;

        protected void OnRemovedFromPort()
        {
            if (RemovedFromPort != null)
                RemovedFromPort(this, EventArgs.Empty);
        }

        public void RemoveFromPort()
        {
            State = TerminalState.Offline;
            OnRemovedFromPort();
        }

        public event EventHandler<CallInfo> OutgoingRequest;

        protected void OnOutgoingRequest(CallInfo info)
        {
            if (OutgoingRequest != null)
                OutgoingRequest(this, info);
        }

        public event EventHandler<CallInfo> IncomingRequest;

        public void OnIncomingRequest(CallInfo info)
        {
            if (IncomingRequest != null)
                IncomingRequest(this, info);
        }

        public void OutgoingCall(string phoneNumber)
        {
            Result result = IsOnline ? Result.Success : Result.Fail;
            int minutes = new Random().Next(60), seconds = new Random().Next(60);
            OnOutgoingRequest(new CallInfo(PhoneNumber, new PhoneNumber { Value = phoneNumber }, DateTime.Now,
                new TimeSpan(0, minutes, seconds), CallType.OutgoingCall, result));
        }

        public virtual void RegistrationEvents()
        {
            OutgoingRequest += (sender, info) => { Console.WriteLine(info.ToString()); };
            IncomingRequest += (sender, info) => { Console.WriteLine(info.ToString()); };
            StateChanged += (sender, state) => { Console.WriteLine(ToString()); };
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
