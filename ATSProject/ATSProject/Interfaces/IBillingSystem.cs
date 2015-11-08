using ATSProject.Model;

namespace ATSProject.Interfaces
{
    public interface IBillingSystem
    {
        ITerminal FirstFreeTerminal { get; }
        void AddTerminalMapping(Client client);
        bool RemovePortMapping(string portNumber);
    }
}
