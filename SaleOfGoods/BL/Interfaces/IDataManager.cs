using System.Collections.Generic;
using DAL.Models;

namespace BL.Interfaces
{
    public interface IDataManager
    {
        void OnStart();
        List<FileInfo> AddInformationToTheDb(string fileName);
    }
}
