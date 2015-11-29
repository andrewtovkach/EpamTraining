namespace BL.Interfaces
{
    public interface IDataManager
    {
        void OnStart();
        bool AddInformationToTheDb(string fileName);
    }
}
