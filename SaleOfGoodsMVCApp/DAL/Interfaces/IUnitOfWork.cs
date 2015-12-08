using System;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Client> Clients { get; }
        IRepository<Country> Countries { get; }
        IRepository<FileInfo> FileInfos { get; }
        IRepository<Manager> Managers { get; }
        IRepository<Product> Products { get; }
        IRepository<SaleInfo> SaleInfos { get; }
        void SaveChanges();
    }
}
