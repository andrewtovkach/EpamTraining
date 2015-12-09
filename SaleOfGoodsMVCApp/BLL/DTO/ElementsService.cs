using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;

namespace BLL.DTO
{
    public class ElementsService : IElementsService
    {
        IUnitOfWork Database { get; set; }

        public ElementsService()
        {
            Database = new EFUnitOfWork();
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
            Database.Clients.Add(Mapper.Map<Client, DAL.Models.Client>(client));
            Database.Clients.SaveChanges();
        }

        public void Add(Country country)
        {
            Database.Countries.Add(Mapper.Map<Country, DAL.Models.Country>(country));
            Database.Countries.SaveChanges();
        }

        public void Add(Manager manager)
        {
            Database.Managers.Add(Mapper.Map<Manager, DAL.Models.Manager>(manager));
            Database.Managers.SaveChanges();
        }

        public void Add(Product product)
        {
            Database.Products.Add(Mapper.Map<Product, DAL.Models.Product>(product));
            Database.Products.SaveChanges();
        }

        public void Add(FileInfo fileInfo)
        {
            Database.FileInfos.Add(Mapper.Map<FileInfo, DAL.Models.FileInfo>(fileInfo));
            Database.FileInfos.SaveChanges();
        }

        public void Add(SaleInfo saleInfo)
        {
            Database.SaleInfos.Add(Mapper.Map<SaleInfo, DAL.Models.SaleInfo>(saleInfo));
            Database.SaleInfos.SaveChanges();
        }

        public void RemoveClient(int id)
        {
            Database.Clients.Remove(id);
            Database.Clients.SaveChanges();
        }

        public void RemoveCountry(int id)
        {
            Database.Countries.Remove(id);
            Database.Countries.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
            Database.Products.Remove(id);
            Database.Products.SaveChanges();
        }

        public void RemoveManager(int id)
        {
            Database.Managers.Remove(id);
            Database.Managers.SaveChanges();
        }

        public void RemoveFileInfo(int id)
        {
            Database.FileInfos.Remove(id);
            Database.FileInfos.SaveChanges();
        }

        public void RemoveSaleInfo(int id)
        {
            Database.SaleInfos.Remove(id);
            Database.SaleInfos.SaveChanges();
        }

        public void Update(int id, Client item)
        {
            Database.Clients.Update(id, Mapper.Map<Client, DAL.Models.Client>(item));
            Database.Clients.SaveChanges();
        }

        public void Update(int id, Country item)
        {
            Database.Countries.Update(id, Mapper.Map<Country, DAL.Models.Country>(item));
            Database.Countries.SaveChanges();
        }

        public void Update(int id, Manager item)
        {
            Database.Managers.Update(id, Mapper.Map<Manager, DAL.Models.Manager>(item));
            Database.Managers.SaveChanges();
        }

        public void Update(int id, Product item)
        {
            Database.Products.Update(id, Mapper.Map<Product, DAL.Models.Product>(item));
            Database.Products.SaveChanges();
        }

        public void Update(int id, FileInfo item)
        {
            Database.FileInfos.Update(id, Mapper.Map<FileInfo, DAL.Models.FileInfo>(item));
            Database.FileInfos.SaveChanges();
        }

        public void Update(int id, SaleInfo item)
        {
            Database.SaleInfos.Update(id, Mapper.Map<SaleInfo, DAL.Models.SaleInfo>(item));
            Database.SaleInfos.SaveChanges();
        }

        public IEnumerable<Client> ClientsItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Client>, IEnumerable<Client>>(Database.Clients.Items); }
        }

        public IEnumerable<Country> CountriesItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Country>, IEnumerable<Country>>(Database.Countries.Items); }
        }

        public IEnumerable<Manager> ManagersItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Manager>, IEnumerable<Manager>>(Database.Managers.Items); }
        }

        public IEnumerable<Product> ProductsItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.Product>, IEnumerable<Product>>(Database.Products.Items); }
        }

        public IEnumerable<FileInfo> FileInfosItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.FileInfo>, IEnumerable<FileInfo>>(Database.FileInfos.Items); }
        }

        public IEnumerable<SaleInfo> SaleInfosItems
        {
            get { return Mapper.Map<IEnumerable<DAL.Models.SaleInfo>, IEnumerable<SaleInfo>>(Database.SaleInfos.Items); }
        }
    }
}
