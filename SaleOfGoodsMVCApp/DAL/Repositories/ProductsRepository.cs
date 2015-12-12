using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;
using Ninject;

namespace DAL.Repositories
{
    public class ProductsRepository : BaseRepository<Product>, IRepository<Product>, IEnumerable<Product>
    {
        private readonly IRepository<Country> _repository;

        public ProductsRepository()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<Country>>().To<CountriesRepository>();
            _repository = ninjectKernel.Get<IRepository<Country>>();
        }

        public void Add(Product item)
        {
            Context.Products.Add(new Model.Product
            {
                CountryId = _repository.GetOrCreateElementId(item.Country),
                Name = item.Name,
                Description = item.Description
            });
        }

        public void Remove(int id)
        {
            var element = GetProductById(id);
            if (element != null)
                Context.Products.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Product GetProductById(int id)
        {
            return Context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, Product item)
        {
            var element = GetProductById(id);
            if (element == null)
                throw new ArgumentException("Incorrect product identification!");
            element.Name = item.Name;
            element.Description = item.Description;
            element.CountryId = _repository.GetOrCreateElementId(item.Country);
        }

        public IEnumerable<Product> Items
        {
            get
            {
                return Context.Products.AsEnumerable().Select(item => new Product(item.Name, item.Description,
                    Mapper.Map<Model.Country, Country>(item.Country), item.Id));
            }
        }

        public IEnumerable<Product> SortedItems
        {
            get { return Items.OrderBy(item => item.Name); }
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}