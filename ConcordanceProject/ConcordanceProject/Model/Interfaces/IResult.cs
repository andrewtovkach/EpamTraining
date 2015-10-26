using System.Collections.Generic;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IResult<out T> : IPrintable, IWriter
    {
        IEnumerable<T> GetResult();
    }
}
