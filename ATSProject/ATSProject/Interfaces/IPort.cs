using ATSProject.Enums;

namespace ATSProject.Interfaces
{
    public interface IPort : IStateElement<PortState>
    {
        string Number { get; set; }
    }
}
