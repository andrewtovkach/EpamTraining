using System;
using ATSProject.Enums;
using ATSProject.Model;
using ATSProject.Model.ATS;

namespace ATSProject.Interfaces
{
    public interface ITerminal : IStateElement<TerminalState>
    {
        string Number { get; set; }
        PhoneNumber PhoneNumber { get; set; }
        event EventHandler<CallInfo> OutgoingRequest;
        event EventHandler<CallInfo> IncomingRequest;
        event EventHandler InsertedIntoPort;
        event EventHandler RemovedFromPort;
        event EventHandler AnsweredTheCall;
        event EventHandler DroppedTheCall;
        void OutgoingCall(string phoneNumber);
        bool IsOnline { get; }
        void InsertIntoPort();
        void RemoveFromPort();
        void AnswerTheCall(CallInfo info);
        void DropTheCall(CallInfo info);
    }
}
