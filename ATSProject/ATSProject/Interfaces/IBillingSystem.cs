using System;
using System.Collections.Generic;
using System.Linq;
using ATSProject.Model;
using ATSProject.Model.ATS;
using ATSProject.Model.BillingSystem;

namespace ATSProject.Interfaces
{
    public interface IBillingSystem : IMappingElement<Client>
    {
        ITerminal FirstFreeTerminal { get; }
        Call this[int index] { get; }
        IEnumerable<Call> GetCalls();
        IEnumerable<Call> GetCalls(Func<Call, bool> predicate);
        IEnumerable<IGrouping<PhoneNumber, Call>> GetCallsGroupedByNumber();
        IEnumerable<IGrouping<PhoneNumber, Call>> GetCallsGroupedByNumber(Func<Call, bool> predicate);
        IEnumerable<Call> GetCallsByClient(Client cl);
        IEnumerable<Call> GetCallsByClient(Client cl, Tuple<DateTime, DateTime> tuple);
        IEnumerable<Call> GetSortedCallsByCost(bool descending);
        IEnumerable<Call> GetSortedCallsByPhoneNumber(bool descending);
    }
}
