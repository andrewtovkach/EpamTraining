using DAL.Interfaces;
using DAL.Repositories;
using Model;
using Client = DAL.Models.Client;
using Country = DAL.Models.Country;
using FileInfo = DAL.Models.FileInfo;
using Manager = DAL.Models.Manager;
using Product = DAL.Models.Product;
using SaleInfo = DAL.Models.SaleInfo;

namespace DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly Model.SalesDBEntities _context;

        private ClientsRepository _clientsRepository;
        private CountriesRepository _countriesRepository;
        private ManagersRepository _managersRepository;
        private FileInfoRepository _fielFileInfoRepository;
        private ProductsRepository _productsRepository;
        private SaleInfoRepository _saleInfoRepository;

        public EFUnitOfWork()
        {
            _context = new SalesDBEntities();
        }

        public IRepository<Client> Clients
        {
            get { return _clientsRepository ?? (_clientsRepository = new ClientsRepository()); }
        }

        public IRepository<Country> Countries
        {
            get { return _countriesRepository ?? (_countriesRepository = new CountriesRepository()); }
        }

        public IRepository<FileInfo> FileInfos
        {
            get { return _fielFileInfoRepository ?? (_fielFileInfoRepository = new FileInfoRepository()); }
        }

        public IRepository<Manager> Managers
        {
            get { return _managersRepository ?? (_managersRepository = new ManagersRepository()); }
        }

        public IRepository<Product> Products
        {
            get { return _productsRepository ?? (_productsRepository = new ProductsRepository()); }
        }

        public IRepository<SaleInfo> SaleInfos
        {
            get { return _saleInfoRepository ?? (_saleInfoRepository = new SaleInfoRepository()); }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
