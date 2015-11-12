using System;
using System.Collections.Generic;
using ATSProject.Model;
using ATSProject.Model.BillingSystem;

namespace ATSProject.Interfaces
{
    public interface IBillingSystem : IMappingElement<Client>
    {
        ITerminal FirstFreeTerminal { get; }
        IEnumerable<Call> GetCalls();
        IEnumerable<Call> GetCalls(Func<Call, bool> predicate);
        IEnumerable<Call> GetSortedCallsByCost(bool descending);
        IEnumerable<Call> GetSortedCallsByCost(Func<Call, bool> predicate, bool descending);
        IEnumerable<Call> GetSortedCallsByPhoneNumber(bool descending);
        IEnumerable<Call> GetSortedCallsByPhoneNumber(Func<Call, bool> predicate, bool descending);
    }
}
