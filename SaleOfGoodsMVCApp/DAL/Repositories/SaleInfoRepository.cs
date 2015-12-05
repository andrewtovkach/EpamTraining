using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class SaleInfoRepository : BaseRepository<SaleInfo>, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        public void Add(SaleInfo item)
        {
            Context.SaleInfo.Add(new Model.SaleInfo
            {
                ClientId = new ClientsRepository().GetOrCreateElementId(item.Client),
                FileInfoId = new FileInfoRepository().GetOrCreateElementId(item.FileInfo),
                ProductId = new ProductsRepository().GetOrCreateElementId(item.Product),
                Cost = item.Cost,
                Currency = item.Currency,
                Date = item.Date
            });
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

        public void Update(int id, SaleInfo item)
        {
            var element = GetSaleInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect saleInfo identification!");
            element.Date = item.Date;
            element.ClientId = new ClientsRepository().GetOrCreateElementId(item.Client);
            element.ProductId = new ProductsRepository().GetOrCreateElementId(item.Product);
            element.FileInfoId = new FileInfoRepository().GetOrCreateElementId(item.FileInfo);
            element.Cost = item.Cost;
            element.Currency = item.Currency;
        }

        public IEnumerable<SaleInfo> Items
        {
            get
            {
                return Context.SaleInfo.AsEnumerable().Select(item => new SaleInfo(item.Date, Mapper.Map<Model.Client, Client>(item.Clients),
                    new Product(item.Products.Name, item.Products.Description, Mapper.Map<Model.Country, Country>(item.Products.Countries), item.ProductId),
                    new FileInfo(Mapper.Map<Model.Manager, Manager>(item.FileInfo.Managers), item.FileInfo.Date, item.FileInfoId), item.Cost, item.Currency, item.Id));
            }
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