using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductsRepository : AbstractRepository, IRepository<Product>, IEnumerable<Product>
    {
        public ProductsRepository()
        {
            Mapper.CreateMap<Product, Model.Product>();
            Mapper.CreateMap<Model.Product, Product>();
        }

        public void Add(Product item)
        {
            item.Country.Id = GetOrCreateCountry(item.Country);
            Context.Products.Add(Mapper.Map<Product, Model.Product>(item));
        }

        private static int GetOrCreateCountry(Country country)
        {
            var countriesRepository = new CountriesRepository();
            int id = countriesRepository.GetElementId(country);
            if (id != 0) return id;
            countriesRepository.Add(country);
            countriesRepository.SaveChanges();
            return countriesRepository.Last().Id;
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

        public int GetElementId(Product item)
        {
            var element = Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        public void Update(int id, Product item)
        {
            var element = GetProductById(id);
            if (element == null)
                throw new ArgumentException("Incorrect product identification!");
            element.Name = item.Name;
            element.Description = item.Description;
            element.CountryId = GetOrCreateCountry(item.Country);
        }

        public IEnumerable<Product> Items
        {
            get { return Context.Products.Select(Mapper.Map<Model.Product, Product>); }
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