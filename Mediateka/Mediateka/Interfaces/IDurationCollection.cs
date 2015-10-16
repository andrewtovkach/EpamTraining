using System;

namespace Mediateka.Interfaces
{
    interface IDurationCollection : IMediaCollection
    {
        TimeSpan TotalDuration { get; }
    }
}
