using System;

namespace ATSProject
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
                if (_state == value)
                    return;
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

        public bool IsOffline
        {
            get { return !IsOnline; }
        }

        public void Online()
        {
            State = TerminalState.Online;
        }

        public void Offline()
        {
            State = TerminalState.Offline;
        }

        public event EventHandler<TerminalState> StateChanged;

        protected void OnStateChanged(TerminalState state)
        {
            if (StateChanged != null)
                StateChanged(this, state);
        }

        public event EventHandler<CallInfo> OutgoingCall;

        protected void OnOutgoingCall(CallInfo info)
        {
            if (OutgoingCall != null)
                OutgoingCall(this,info);
        }

        public event EventHandler<CallInfo> IncomingCall;

        public void OnIncomingCall(CallInfo info)
        {
            if (IncomingCall != null)
                IncomingCall(this, info);
        }

        public void OutgoingCallAction(string phoneNumber)
        {
            PhoneNumber number = new PhoneNumber { Value = phoneNumber };
            if (IsOffline)
            {
                OnOutgoingCall(new CallInfo(PhoneNumber, number, DateTime.Now, new TimeSpan(0, 0, 0), Result.Fail));
                return;
            }
            int seconds = new Random().Next(10);
            OnOutgoingCall(new CallInfo(PhoneNumber, number, DateTime.Now, new TimeSpan(0, 0, seconds), Result.Success));
        }

        public void ClearEvents()
        {
            StateChanged = null;
            OutgoingCall = null;
        }

        public override string ToString()
        {
            return string.Format("Terminal №{0} {1} - {2}", Number, PhoneNumber, State);
        }
    }
}
