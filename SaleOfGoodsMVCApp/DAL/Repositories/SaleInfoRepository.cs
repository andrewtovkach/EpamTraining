using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;
using Ninject;

namespace DAL.Repositories
{
    public class SaleInfoRepository : BaseRepository<SaleInfo>, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        private readonly IRepository<Client> _firstRepository;
        private readonly IRepository<FileInfo> _secondRepository;
        private readonly IRepository<Product> _thirdRepository;

        public SaleInfoRepository()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<Client>>().To<ClientsRepository>();
            _firstRepository = ninjectKernel.Get<IRepository<Client>>();
            ninjectKernel.Bind<IRepository<FileInfo>>().To<FileInfoRepository>();
            _secondRepository = ninjectKernel.Get<IRepository<FileInfo>>();
            ninjectKernel.Bind<IRepository<Product>>().To<ProductsRepository>();
            _thirdRepository = ninjectKernel.Get<IRepository<Product>>();
        }

        public void Add(SaleInfo item)
        {
            Context.SaleInfo.Add(new Model.SaleInfo
            {
                ClientId = _firstRepository.GetOrCreateElementId(item.Client),
                FileInfoId = _secondRepository.GetOrCreateElementId(item.FileInfo),
                ProductId = _thirdRepository.GetOrCreateElementId(item.Product),
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
            element.ClientId = _firstRepository.GetOrCreateElementId(item.Client);
            element.ProductId = _secondRepository.GetOrCreateElementId(item.FileInfo);
            element.FileInfoId = _thirdRepository.GetOrCreateElementId(item.Product);
            element.Cost = item.Cost;
            element.Currency = item.Currency;
        }

        public IEnumerable<SaleInfo> Items
        {
            get
            {
                return Context.SaleInfo.AsEnumerable().Select(item => new SaleInfo(item.Date ?? DateTime.Now, Mapper.Map<Model.Client, Client>(item.Client),
                    new Product(item.Product.Name, item.Product.Description, Mapper.Map<Model.Country, Country>(item.Product.Country), item.ProductId),
                    new FileInfo(Mapper.Map<Model.Manager, Manager>(item.FileInfo.Manager), item.FileInfo.Date ?? DateTime.Now, item.FileInfoId), 
                    item.Cost ?? 0,item.Currency, item.Id));
            }
        }

        public IEnumerable<SaleInfo> SortedItems
        {
            get { return Items.OrderByDescending(item => item.Date); }
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