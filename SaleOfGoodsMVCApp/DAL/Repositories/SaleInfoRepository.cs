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
        private readonly IRepository<Client> _clientsRepository;
        private readonly IRepository<FileInfo> _fileInfoRepository;
        private readonly IRepository<Product> _productsRepository;

        public SaleInfoRepository()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<Client>>().To<ClientsRepository>();
            _clientsRepository = ninjectKernel.Get<IRepository<Client>>();
            ninjectKernel.Bind<IRepository<FileInfo>>().To<FileInfoRepository>();
            _fileInfoRepository = ninjectKernel.Get<IRepository<FileInfo>>();
            ninjectKernel.Bind<IRepository<Product>>().To<ProductsRepository>();
            _productsRepository = ninjectKernel.Get<IRepository<Product>>();
        }

        public void Add(SaleInfo item)
        {
            Context.SaleInfo.Add(new Model.SaleInfo
            {
                ClientId = _clientsRepository.GetOrCreateElementId(item.Client),
                FileInfoId = _fileInfoRepository.GetOrCreateElementId(item.FileInfo),
                ProductId = _productsRepository.GetOrCreateElementId(item.Product),
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
            element.ClientId = _clientsRepository.GetOrCreateElementId(item.Client);
            element.ProductId = _productsRepository.GetOrCreateElementId(item.Product);
            element.FileInfoId = _fileInfoRepository.GetOrCreateElementId(item.FileInfo);
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