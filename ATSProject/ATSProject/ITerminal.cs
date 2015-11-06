using System;

namespace ATSProject
{
    public interface ITerminal : IStateElement<TerminalState>
    {
        string Number { get; set; }
        PhoneNumber PhoneNumber { get; set; }
        event EventHandler<CallInfo> OutgoingCall;
        event EventHandler<CallInfo> IncomingCall;
        void OutgoingCallAction(string phoneNumber);
        void Online();
        void Offline();
    }
}
