using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;

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
            Context.Products.Add(Mapper.Map<Product, Model.Product>(item));
        }

        public void Remove(Product item)
        {
            var element = ProductById(item.Id);
            if (element != null)
                Context.Products.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Product ProductById(int id)
        {
            return Context.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product ProductObjectById(int id)
        {
            return Mapper.Map<Model.Product, Product>(ProductById(id));
        }

        public void Update(int id, Product item)
        {
            var element = ProductById(id);
            if (element != null)
                element.Name = item.Name;
            else throw new ArgumentException("Incorrect product identification!");
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
