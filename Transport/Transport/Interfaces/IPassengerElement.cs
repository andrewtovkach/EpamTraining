using System.Collections.Generic;

namespace Transport.Interfaces
{
    interface IPassengerElement
    {
        long TotalPlacesCount { get; }
        IEnumerable<int> GetFreePlaces();
        long FreePlacesCount { get; }
        IEnumerable<int> GetBusyPlaces();
        long BusyPlacesCount { get; }
    }
}
