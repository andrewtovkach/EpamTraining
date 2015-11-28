using BL.Model;
using DAL.Models;

namespace BL.Interfaces
{
    public interface IParse
    {
        FileInfo ParseFile(string filePath, DataRecord dataRecord);
    }
}
