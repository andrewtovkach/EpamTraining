using System.Collections.Generic;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IResult<T> : IPrintable, IWriter
    {
        IEnumerable<T> GetResult();
    }
}
