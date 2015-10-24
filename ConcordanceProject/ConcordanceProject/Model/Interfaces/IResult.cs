namespace ConcordanceProject.Model.Interfaces
{
    public interface IResult : IPrintable
    {
        bool Write(string fileName);
    }
}
