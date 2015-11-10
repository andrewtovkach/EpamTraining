using ATSProject.Model.BillingSystem;

namespace ATSProject.Interfaces
{
    public interface IBillingSystem : IMappingElement<Client>
    {
        ITerminal FirstFreeTerminal { get; }
    }
}
