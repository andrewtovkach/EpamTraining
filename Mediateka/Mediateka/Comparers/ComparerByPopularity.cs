using Mediateka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else return 0;
        }
    }
}
