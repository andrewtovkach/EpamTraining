using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class SaleInfoRepository : AbstractRepository, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        public SaleInfoRepository()
        {
            Mapper.CreateMap<SaleInfo, Model.SaleInfo>()
                .ForMember("ClientId", opt => opt.MapFrom(c => c.Client.Id))
                .ForMember("ProductId", opt => opt.MapFrom(src => src.Product.Id));
        }

        private Model.Client ClientByName(string firstName, string secondName)
        {
            return Context.Clients.FirstOrDefault(x => x.FirstName == firstName && x.SecondName == secondName);
        }

        public Model.Client GetClient(string firstName, string secondName)
        {
            return ClientByName(firstName, secondName) ?? Context.Clients.Add(new Model.Client
            {
                FirstName = firstName,
                SecondName = secondName
            });
        }

        private Model.Product ProductByName(string name)
        {
            return Context.Products.FirstOrDefault(x => x.Name == name);
        }

        public Model.Product GetProduct(string name)
        {
            return ProductByName(name) ?? Context.Products.Add(new Model.Product { Name = name });
        }

        public void Add(SaleInfo item)
        {
            var client = GetClient(item.Client.FirstName, item.Client.SecondName);
            var product = GetProduct(item.Product.Name);
            item.Client.Id = client.Id;
            item.Product.Id = product.Id;
            Context.SaleInfo.Add(Mapper.Map<SaleInfo, Model.SaleInfo>(item));
        }

        public void Remove(SaleInfo item)
        {
            var element = SaleInfoById(item.Id);
            if (element != null)
                Context.SaleInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.SaleInfo SaleInfoById(int id)
        {
            return Context.SaleInfo.FirstOrDefault(x => x.Id == id);
        }

        public SaleInfo SaleInfoObjectById(int id)
        {
            var saleInfo = SaleInfoById(id);
            return new SaleInfo(saleInfo.Date, new ClientsRepository().ClientObjectById(saleInfo.ClientId),
                new ProductsRepository().ProductObjectById(saleInfo.ProductId), saleInfo.Cost, saleInfo.Id);
        }

        public void Update(int id, SaleInfo item)
        {
            var element = SaleInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect saleInfo identification!");
            element.Date = item.Date;
            var client = GetClient(item.Client.FirstName, item.Client.SecondName);
            var product = GetProduct(item.Product.Name);
            element.ClientId = client.Id;
            element.ProductId = product.Id;
            element.Cost = item.Cost;
        }

        public IEnumerable<SaleInfo> Items
        {
            get { return Context.SaleInfo.AsEnumerable().Select(item => SaleInfoObjectById(item.Id)); }
        }

        public IEnumerator<SaleInfo> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
