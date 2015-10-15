namespace Transport.Interfaces
{
    interface IFreightElement
    {
        uint Volume { get; }
        uint OccupiedVolume { get; }
        long FreeVolume { get; }
        double PercentageFreeVolume { get; }
    }
}
