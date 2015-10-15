using System.Collections.Generic;

namespace Transport.Interfaces
{
    interface IPassengerElement
    {
        uint PlacesCount { get; }
        IEnumerable<int> GetFreePlaces();
        long FreePlacesCount { get; }
        IEnumerable<int> GetBusyPlaces();
        long BusyPlacesCount { get; }
    }
}
