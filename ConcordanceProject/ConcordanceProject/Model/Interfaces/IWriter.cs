using System;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IWriter
    {
        bool Write(Func<string> function);
    }
}
