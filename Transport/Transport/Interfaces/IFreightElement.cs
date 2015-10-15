namespace Transport.Interfaces
{
    interface IFreightElement
    {
        long TotalVolume { get; }
        long TotalOccupiedVolume { get; }
        long FreeVolume { get; }
        double PercentageFreeVolume { get; }
    }
}
