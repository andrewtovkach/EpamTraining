using System.Collections.Generic;

namespace Transport.Interfaces
{
    interface IBaggageElement
    {
        uint CellsCount { get; }
        double Weight { get; }
        long Capacity { get; }
        IEnumerable<int> GetBusyCells();
        int BusyCellsCount { get; }
        IEnumerable<int> GetFreeCells();
        long FreeCellsCount { get; }
    }
}
