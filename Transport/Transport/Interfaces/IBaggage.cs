﻿using System.Collections.Generic;

namespace Transport.Interfaces
{
    interface IBaggage
    {
        long TotalCellsCount { get; }
        double TotalWeight { get; }
        long TotalCapacity { get; }
        IEnumerable<int> GetBusyCells();
        int BusyCellsCount { get; }
        IEnumerable<int> GetFreeCells();
        long FreeCellsCount { get; }
    }
}
