using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IElementsService
    {
        void Add(Client client);
        void Add(Country country);
        void Add(Manager manager);
        void Add(Product product);
        void Add(FileInfo fileInfo);
        void Add(SaleInfo saleInfo);
        void RemoveClient(int id);
        void RemoveCountry(int id);
        void RemoveProduct(int id);
        void RemoveManager(int id);
        void RemoveFileInfo(int id);
        void RemoveSaleInfo(int id);
        void Update(int id, Client item);
        void Update(int id, Country item);
        void Update(int id, Manager item);
        void Update(int id, Product item);
        void Update(int id, FileInfo item);
        void Update(int id, SaleInfo item);
        IEnumerable<Client> ClientsItems { get; }
        IEnumerable<Country> CountriesItems { get; }
        IEnumerable<Manager> ManagersItems { get; }
        IEnumerable<Product> ProductsItems { get; }
        IEnumerable<FileInfo> FileInfosItems { get; }
        IEnumerable<SaleInfo> SaleInfosItems { get; }
        void SaveChanges();
    }
}
