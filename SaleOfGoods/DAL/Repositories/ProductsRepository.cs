using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class ProductsRepository : AbstractRepository, IRepository<Product>, IEnumerable<Product>
    {
        private static Model.Products ToEntity(Product product)
        {
            return new Model.Products { Name = product.Name };
        }

        public void Add(Product item)
        {
            Context.Products.Add(ToEntity(item));
        }

        public void Remove(Product item)
        {
            var element = ProductById(item.Id);
            if (element != null)
                Context.Products.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Products ProductById(int id)
        {
            return Context.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product ProductObjectById(int id)
        {
            var product = Context.Products.FirstOrDefault(x => x.Id == id);
            return product != null ? new Product(product.Id, product.Name.Trim()) : null;
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
            get { return Context.Products.AsEnumerable().Select(item => ProductObjectById(item.Id)); }
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
