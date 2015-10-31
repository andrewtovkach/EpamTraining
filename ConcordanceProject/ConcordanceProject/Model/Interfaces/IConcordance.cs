using System.Collections.Generic;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IConcordance : IWriteElement
    {
        IEnumerable<WordStatistics> GetResult();
    }
}
