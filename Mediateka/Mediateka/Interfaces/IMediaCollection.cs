using Mediateka.Model;
using System.Collections.Generic;

namespace Mediateka.Interfaces
{
    interface IMediaCollection
    {
        IEnumerable<MediaItem> GetNewMediaItems(int countItems);
        IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems);
    }
}
