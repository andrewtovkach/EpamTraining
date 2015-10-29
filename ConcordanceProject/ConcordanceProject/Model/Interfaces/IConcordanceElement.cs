namespace ConcordanceProject.Model.Interfaces
{
    public interface IConcordanceElement : IResultElement<WordStatistics>
    {
        void CountingStatistics();
    }
}
