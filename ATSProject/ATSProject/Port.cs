using System;

namespace ATSProject
{
    public class Port
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
                OnStateChanged(this, value);
            }
        }

        public event EventHandler<PortState> StateChanged;

        public void OnStateChanged(object sender, PortState state)
        {
            if (StateChanged != null)
            {
                StateChanged(sender, state);
            }
        }

        public Port(string number)
        {
            Number = number;
            State = PortState.NotConnected;
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
            get { return State == PortState.Busy || State == PortState.Free; }
        }

        public bool IsOffline
        {
            get { return !IsOnline; }
        }

        public void ClearEvents()
        {
            StateChanged = null;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
