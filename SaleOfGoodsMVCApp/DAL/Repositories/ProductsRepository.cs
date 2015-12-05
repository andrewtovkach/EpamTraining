using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductsRepository : BaseRepository<Product>, IRepository<Product>, IEnumerable<Product>
    {
        public void Add(Product item)
        {
            item.Country.Id = new CountriesRepository().GetOrCreateElementId(item.Country);
            Context.Products.Add(Mapper.Map<Product, Model.Product>(item));
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
            element.CountryId = new CountriesRepository().GetOrCreateElementId(item.Country);
        }

        public IEnumerable<Product> Items
        {
            get
            {
                return Context.Products.AsEnumerable().Select(item => new Product(item.Name, item.Description, Mapper.Map<Model.Country, Country>(item.Country), item.Id));
            }
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