using System;
using ATSProject.Model;

namespace ATSProject.Interfaces
{
    public interface IStation : IMappingElement<ITerminal>
    {
        IPort FirstFreePort { get; }
        void IncomingCall(CallInfo info);
        event EventHandler<Call> CallIsProcessed;
    }
}
