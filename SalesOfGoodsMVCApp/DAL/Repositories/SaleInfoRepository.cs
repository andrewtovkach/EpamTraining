using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class SaleInfoRepository : AbstractRepository, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        public SaleInfoRepository()
        {
            Mapper.CreateMap<SaleInfo, Model.SaleInfo>();
            Mapper.CreateMap<Model.SaleInfo, SaleInfo>();
        }

        public void Add(SaleInfo item)
        {
            item.Client.Id = GetOrCreateClient(item.Client);
            item.Product.Id = GetOrCreateProduct(item.Product);
            item.FileInfo.Id = GetOrCreateFileInfo(item.FileInfo);
            Context.SaleInfo.Add(Mapper.Map<SaleInfo, Model.SaleInfo>(item));
        }

        public void Remove(int id)
        {
            var element = GetSaleInfoById(id);
            if (element != null)
                Context.SaleInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.SaleInfo GetSaleInfoById(int id)
        {
            return Context.SaleInfo.FirstOrDefault(x => x.Id == id);
        }

        public int GetElementId(SaleInfo item)
        {
            var element = Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        private static int GetOrCreateClient(Client client)
        {
            var clientsRepository = new ClientsRepository();
            int id = clientsRepository.GetElementId(client);
            if (id != 0) return id;
            clientsRepository.Add(client);
            clientsRepository.SaveChanges();
            return clientsRepository.Last().Id;
        }

        private static int GetOrCreateProduct(Product product)
        {
            var productsRepository = new ProductsRepository();
            int id = productsRepository.GetElementId(product);
            if (id != 0) return id;
            productsRepository.Add(product);
            productsRepository.SaveChanges();
            return productsRepository.Last().Id;
        }

        private static int GetOrCreateFileInfo(FileInfo fileInfo)
        {
            var fileInfoRepository = new FileInfoRepository();
            int id = fileInfoRepository.GetElementId(fileInfo);
            if (id != 0) return id;
            fileInfoRepository.Add(fileInfo);
            fileInfoRepository.SaveChanges();
            return fileInfoRepository.Last().Id;
        }

        public void Update(int id, SaleInfo item)
        {
            var element = GetSaleInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect saleInfo identification!");
            element.Date = item.Date;
            element.ClientId = GetOrCreateClient(item.Client);
            element.ProductId = GetOrCreateProduct(item.Product);
            element.FileInfoId = GetOrCreateFileInfo(item.FileInfo);
            element.Cost = item.Cost;
            element.Currency = item.Currency;
        }

        public IEnumerable<SaleInfo> Items
        {
            get { return Context.SaleInfo.Select(Mapper.Map<Model.SaleInfo, SaleInfo>); }
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