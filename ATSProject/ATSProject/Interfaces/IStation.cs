using System;
using ATSProject.Model.ATS;
using ATSProject.Model.BillingSystem;

namespace ATSProject.Interfaces
{
    public interface IStation : IMappingElement<ITerminal>
    {
        IPort FirstFreePort { get; }
        void IncomingCall(CallInfo info);
        event EventHandler<Tuple<CallInfo, CallStatistic>> CallIsProcessed;
    }
}
