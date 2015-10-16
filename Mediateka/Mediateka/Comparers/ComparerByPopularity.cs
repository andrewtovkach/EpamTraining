using Mediateka.Model;
using System.Collections.Generic;

namespace Mediateka.Comparers
{
    class ComparerByPopularity : IComparer<MediaItem>
    {
        public int Compare(MediaItem x, MediaItem y)
        {
            if (x.Popularity < y.Popularity)
                return 1;
            if (x.Popularity > y.Popularity)
                return -1;
            return 0;
        }
    }
}
