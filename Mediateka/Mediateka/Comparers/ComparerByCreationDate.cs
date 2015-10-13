using Mediateka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else return 0;
        }
    }
}
