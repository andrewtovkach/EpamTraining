using Mediateka.Model;
using System.Collections.Generic;

namespace Mediateka.Comparers
{
    class ComparerByCreationDate : IComparer<MediaItem>
    {
        public int Compare(MediaItem x, MediaItem y)
        {
            if (x.CreationDate < y.CreationDate)
                return 1;
            if (x.CreationDate > y.CreationDate)
                return -1;
            return 0;
        }
    }
}
