using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Comparers
{
    class ComparerByOccupiedVolume : IComparer<FreightCarriage>
    {
        public int Compare(FreightCarriage x, FreightCarriage y)
        {
            return x.OccupiedVolume.CompareTo(y.OccupiedVolume);
        }
    }
}
