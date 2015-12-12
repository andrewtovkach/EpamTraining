using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL
{
    public class ElementsService : IElementsService
    {
        private readonly IRepository<DAL.Models.Client> _clientsRepository;
        private readonly IRepository<DAL.Models.Product> _productsRepository;
        private readonly IRepository<DAL.Models.Manager> _managersRepository;
        private readonly IRepository<DAL.Models.Country> _countriesRepository;
        private readonly IRepository<DAL.Models.FileInfo> _fileInfoRepository;
        private readonly IRepository<DAL.Models.SaleInfo> _saleInfoRepository;

        public ElementsService()
        {
            _clientsRepository = new ClientsRepository();
            _productsRepository = new ProductsRepository();
            _managersRepository = new ManagersRepository();
            _countriesRepository = new CountriesRepository();
            _fileInfoRepository = new FileInfoRepository();
            _saleInfoRepository = new SaleInfoRepository();
            CreateMapping();
        }

        private static void CreateMapping()
        {
            Mapper.CreateMap<DAL.Models.SaleInfo, SaleInfo>().ReverseMap();
            Mapper.CreateMap<DAL.Models.Client, Client>()
                .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.SecondName));
            Mapper.CreateMap<Client, DAL.Models.Client>()
                .ForMember("FirstName", opt => opt.MapFrom(c => c.Name.Split(' ')[0]));
            Mapper.CreateMap<Client, DAL.Models.Client>()
                .ForMember("SecondName", opt => opt.MapFrom(c => c.Name.Split(' ')[1]));
            Mapper.CreateMap<DAL.Models.Product, Product>().ReverseMap();
            Mapper.CreateMap<DAL.Models.FileInfo, FileInfo>().ReverseMap();
            Mapper.CreateMap<DAL.Models.Country, Country>().ReverseMap();
            Mapper.CreateMap<DAL.Models.Manager, Manager>()
                .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.SecondName));
            Mapper.CreateMap<Manager, DAL.Models.Manager>()
                .ForMember("FirstName", opt => opt.MapFrom(c => c.Name.Split(' ')[0]));
            Mapper.CreateMap<Manager, DAL.Models.Manager>()
                .ForMember("SecondName", opt => opt.MapFrom(c => c.Name.Split(' ')[1]));
        }

        public void Add(Client client)
        {
            _clientsRepository.Add(Mapper.Map<Client, DAL.Models.Client>(client));
            _clientsRepository.SaveChanges();
        }

        public void Add(Country country)
        {
            _countriesRepository.Add(Mapper.Map<Country, DAL.Models.Country>(country));
            _countriesRepository.SaveChanges();
        }

        public void Add(Manager manager)
        {
            _managersRepository.Add(Mapper.Map<Manager, DAL.Models.Manager>(manager));
            _managersRepository.SaveChanges();
        }

        public void Add(Product product)
        {
            _productsRepository.Add(Mapper.Map<Product, DAL.Models.Product>(product));
            _productsRepository.SaveChanges();
        }

        public void Add(FileInfo fileInfo)
        {
            _fileInfoRepository.Add(Mapper.Map<FileInfo, DAL.Models.FileInfo>(fileInfo));
            _fileInfoRepository.SaveChanges();
        }

        public void Add(SaleInfo saleInfo)
        {
            _saleInfoRepository.Add(Mapper.Map<SaleInfo, DAL.Models.SaleInfo>(saleInfo));
            _saleInfoRepository.SaveChanges();
        }

        public void RemoveClient(int id)
        {
            _clientsRepository.Remove(id);
            _clientsRepository.SaveChanges();
        }

        public void RemoveCountry(int id)
        {
            _countriesRepository.Remove(id);
            _countriesRepository.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
            _productsRepository.Remove(id);
            _productsRepository.SaveChanges();
        }

        public void RemoveManager(int id)
        {
            _managersRepository.Remove(id);
            _managersRepository.SaveChanges();
        }

        public void RemoveFileInfo(int id)
        {
            _fileInfoRepository.Remove(id);
            _fileInfoRepository.SaveChanges();
        }

        public void RemoveSaleInfo(int id)
        {
            _saleInfoRepository.Remove(id);
            _saleInfoRepository.SaveChanges();
        }

        public void Update(int id, Client item)
        {
            _clientsRepository.Update(id, Mapper.Map<Client, DAL.Models.Client>(item));
            _clientsRepository.SaveChanges();
        }

        public void Update(int id, Country item)
        {
            _countriesRepository.Update(id, Mapper.Map<Country, DAL.Models.Country>(item));
            _countriesRepository.SaveChanges();
        }

        public void Update(int id, Manager item)
        {
            _managersRepository.Update(id, Mapper.Map<Manager, DAL.Models.Manager>(item));
            _managersRepository.SaveChanges();
        }

        public void Update(int id, Product item)
        {
           _productsRepository.Update(id, Mapper.Map<Product, DAL.Models.Product>(item));
            _productsRepository.SaveChanges();
        }

        public void Update(int id, FileInfo item)
        {
            _fileInfoRepository.Update(id, Mapper.Map<FileInfo, DAL.Models.FileInfo>(item));
            _fileInfoRepository.SaveChanges();
        }

        public void Update(int id, SaleInfo item)
        {
            _saleInfoRepository.Update(id, Mapper.Map<SaleInfo, DAL.Models.SaleInfo>(item));
            _saleInfoRepository.SaveChanges();
        }

        public IEnumerable<Client> ClientsItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Client>, IEnumerable<Client>>(_clientsRepository.Items); }
        }

        public IEnumerable<Country> CountriesItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Country>, IEnumerable<Country>>(_countriesRepository.Items); }
        }

        public IEnumerable<Manager> ManagersItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Manager>, IEnumerable<Manager>>(_managersRepository.Items); }
        }

        public IEnumerable<Product> ProductsItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Product>, IEnumerable<Product>>(_productsRepository.Items); }
        }

        public IEnumerable<FileInfo> FileInfosItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.FileInfo>, IEnumerable<FileInfo>>(_fileInfoRepository.Items); }
        }

        public IEnumerable<SaleInfo> SaleInfosItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.SaleInfo>, IEnumerable<SaleInfo>>(_saleInfoRepository.Items); }
        }
    }
}
