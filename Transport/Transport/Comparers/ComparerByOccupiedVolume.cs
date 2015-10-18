using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Comparers
{
    class ComparerByOccupiedVolume : IComparer<Carriage>
    {
        public int Compare(Carriage x, Carriage y)
        {
            var carriageFirst = x as FreightCarriage;
            var carriageSecond = y as FreightCarriage;
            if (carriageFirst != null && carriageSecond != null)
                return carriageFirst.OccupiedVolume.CompareTo(carriageSecond.OccupiedVolume);
            return x.CompareTo(y);
        }
    }
}
