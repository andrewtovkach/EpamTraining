namespace ConcordanceProject.Model.Interfaces
{
    public interface IConcordance : IResultElement<WordStatistics>
    {
        void CountingStatistics();
    }
}
