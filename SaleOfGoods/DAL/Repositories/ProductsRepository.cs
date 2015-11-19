using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class ProductsRepository : IRepository<Product>, IEnumerable<Product>
    {
        public Model.SalesDBEntities Context { get; private set; }

        public ProductsRepository()
        {
            Context = new Model.SalesDBEntities();
        }

        private static Model.Product ToEntity(Product manager)
        {
            return new Model.Product { Name = manager.Name };
        }

        public void Add(Product item)
        {
            Context.Products.Add(ToEntity(item));
        }

        public void Remove(Product item)
        {
            var element = Context.Products.FirstOrDefault(x => x.Name == item.Name);
            if (element != null)
                Context.Products.Remove(element);
        }

        public void Update(Product item, object info)
        {
            string name = info as string;
            if(name == null)
                throw new ArgumentException("Incorrect argument!");
            var element = Context.Products.FirstOrDefault(x => x.Name == item.Name);
            if (element != null)
                element.Name = name;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public IEnumerable<Product> Items
        {
            get { return Context.Products.Select(item => new Product { Name = item.Name.Trim() }); }
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
