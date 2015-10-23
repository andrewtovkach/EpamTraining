namespace Transport.Interfaces
{
    public interface IFreightElement
    {
        uint Volume { get; }
        uint OccupiedVolume { get; }
        long FreeVolume { get; }
        double PercentageFreeVolume { get; }
    }
}
