using System.Collections.Generic;
using Transport.Model.Carriages;

namespace Transport.Interfaces
{
    interface ISortable
    {
        void Sort();
        void Sort(IComparer<Carriage> comparer);
    }
}
