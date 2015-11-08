using System;
using ATSProject.Model;

namespace ATSProject.Interfaces
{
    public interface IStation
    {
        IPort FirstFreePort { get; }
        void AddPortMapping(ITerminal terminal);
        bool RemovePortMapping(string portNumber);
        void IncomingCall(CallInfo info);
        event EventHandler<CallInfo> CallProcessed;
        void ClearEvents();
    }
}
