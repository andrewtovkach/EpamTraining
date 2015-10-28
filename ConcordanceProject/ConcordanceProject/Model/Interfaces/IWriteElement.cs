using System.IO;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IWriteElement
    {
        string GetResultString(int width = 35);
        bool Write(TextWriter textWriter, int width = 35);
    }
}
