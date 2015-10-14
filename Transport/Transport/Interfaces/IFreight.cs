namespace Transport.Interfaces
{
    interface IFreight
    {
        long TotalVolume { get; }
        long TotalOccupiedVolume { get; }
        long FreeVolume { get; }
        double PercentageFreeVolume { get; }
    }
}
