using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcordanceProject.Model.Interfaces
{
    interface ISubjectIndex : IWriteElement
    {
        IEnumerable<IGrouping<char, WordStatistics>> GetResult();
        IEnumerable<IGrouping<char, WordStatistics>> GetResult(Func<WordStatistics, bool> predicate);
    }
}
