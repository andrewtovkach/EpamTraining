using System.Collections.Generic;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IResultElement<out T> : IWriteElement
    {
        IEnumerable<T> GetResult();
    }
}
