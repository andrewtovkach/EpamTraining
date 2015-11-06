namespace ATSProject
{
    public interface IPort : IStateElement<PortState>
    {
        string Number { get; set; }
        void Disabled();
        void Enabled();
    }
}
