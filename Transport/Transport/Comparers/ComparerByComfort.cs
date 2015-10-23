using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Comparers
{
    public class ComparerByComfort : IComparer<Carriage>
    {
        public int Compare(Carriage x, Carriage y)
        {
            var carriageFirst = x as PassengerCarriage;
            var carriageSecond = y as PassengerCarriage;
            if (carriageFirst != null && carriageSecond != null)
                return ((int)carriageFirst.TypePassengerCarriage).CompareTo((int)carriageSecond.TypePassengerCarriage);
            return x.CompareTo(y);
        }
    }
}
