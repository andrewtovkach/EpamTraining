using System;

namespace ATSProject
{
    public class Port : IPort
    {
        public string Number { get; set; }

        private PortState _state;
        public PortState State
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

        public Port(string number)
        {
            Number = number;
            State = PortState.NotConnected;
        }

        public event EventHandler<PortState> StateChanged;

        protected void OnStateChanged(PortState state)
        {
            if (StateChanged != null)
                StateChanged(this, state);
        }

        public void ClearEvents()
        {
            StateChanged = null;
        }

        public void Disabled()
        {
            State = PortState.NotConnected;
        }

        public void Enabled()
        {
            State = PortState.Free;
        }

        public bool IsOnline
        {
            get { return State != PortState.NotConnected; }
        }

        public bool IsOffline
        {
            get { return !IsOnline; }
        }

        public override string ToString()
        {
            return string.Format("Port №{0}  - {1}", Number, State);
        }
    }
}
