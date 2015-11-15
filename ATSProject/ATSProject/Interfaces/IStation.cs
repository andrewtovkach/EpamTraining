using System;
using ATSProject.Model;

namespace ATSProject.Interfaces
{
    public interface IStation : IMappingElement<ITerminal>
    {
        IPort FirstFreePort { get; }
        ITerminal TerminalByNumber(string terminalNumber);
        CallInfo this[int index] { get; }
        void IncomingCall(CallInfo info);
        event EventHandler<Call> CallIsProcessed;
        Call Calling(CallInfo info, ITerminal sourceTerminal, ITerminal targetTerminal);
    }
}
