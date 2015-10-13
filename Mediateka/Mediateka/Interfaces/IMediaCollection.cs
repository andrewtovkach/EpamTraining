using Mediateka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateka.Interfaces
{
    interface IMediaCollection
    {
        IEnumerable<MediaItem> GetNewMediaItems(int countItems);
        IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems);
    }
}
