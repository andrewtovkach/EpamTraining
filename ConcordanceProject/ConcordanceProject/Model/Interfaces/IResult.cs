using System.Collections.Generic;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IResult<out T> : IPrintable, IWriteble
    {
        IEnumerable<T> GetResult();
    }
}
