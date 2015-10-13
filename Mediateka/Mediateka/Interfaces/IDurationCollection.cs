using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateka.Interfaces
{
    interface IDurationCollection : IMediaCollection
    {
        TimeSpan TotalDuration { get; }
    }
}
