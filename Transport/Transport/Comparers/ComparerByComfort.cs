using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Comparers
{
    class ComparerByComfort : IComparer<PassengerCarriage>
    {
        public int Compare(PassengerCarriage x, PassengerCarriage y)
        {
            return ((int)x.TypePassengerCarriage).CompareTo((int)y.TypePassengerCarriage);
        }
    }
}
