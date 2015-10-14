using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Comparers
{
    class ComparerByComfort : IComparer<Carriage>
    {
        public int Compare(Carriage x, Carriage y)
        {
            var carriageFirst = x as PassengerCarriage;
            if (carriageFirst != null)
            {
                var carriageSecond = y as PassengerCarriage;
                if (carriageSecond != null)
                    return ((int)carriageFirst.TypePassengerCarriage).CompareTo((int)carriageSecond.TypePassengerCarriage);
            }
            return 1;
        }
    }
}
