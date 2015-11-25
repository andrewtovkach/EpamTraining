using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class SaleInfoRepository : AbstractRepository, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        private static Model.SaleInfo ToEntity(SaleInfo saleInfo)
        {
            return new Model.SaleInfo
            {
                Date = saleInfo.Date,
                Cost = saleInfo.Cost,
                СurrencyСode = saleInfo.CurrencyCode,
                ClientId = saleInfo.Client.Id,
                ProductId = saleInfo.Product.Id
            };
        }

        private Model.Clients ClientByName(string firstName, string secondName)
        {
            return Context.Clients.FirstOrDefault(x => x.FirstName == firstName && x.SecondName == secondName);
        }

        private Model.Clients GetClient(string firstName, string secondName)
        {
            return ClientByName(firstName, secondName) ?? Context.Clients.Add(new Model.Clients
            {
                FirstName = firstName,
                SecondName = secondName
            });
        }

        private Model.Products ProductByName(string name)
        {
            return Context.Products.FirstOrDefault(x => x.Name == name);
        }

        private Model.Products GetProduct(string name)
        {
            return ProductByName(name) ?? Context.Products.Add(new Model.Products { Name = name });
        }

        public void Add(SaleInfo item)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var client = GetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = GetProduct(item.Product.Name);
                    item.Client.Id = client.Id;
                    item.Product.Id = product.Id;
                    Context.SaleInfo.Add(ToEntity(item));
                   transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Remove(SaleInfo item)
        {
            var element = SaleInfoById(item.Id);
            if (element != null)
                Context.SaleInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.SaleInfo SaleInfoById(int id)
        {
            return Context.SaleInfo.FirstOrDefault(x => x.Id == id);
        }

        public SaleInfo SaleInfoObjectById(int id)
        {
            var saleInfo = Context.SaleInfo.FirstOrDefault(x => x.Id == id);
            return saleInfo != null ? new SaleInfo(saleInfo.Id, saleInfo.Date ?? new DateTime(),
                new ClientsRepository().ClientObjectById(saleInfo.ClientId), new ProductsRepository().ProductObjectById(saleInfo.ProductId),
                saleInfo.Cost ?? 0, saleInfo.СurrencyСode) : null;
        }

        public void Update(int id, SaleInfo item)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = SaleInfoById(id);
                    if (element == null)
                        throw new ArgumentException("Incorrect saleInfo identification!");
                    element.Date = item.Date;
                    var client = GetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = GetProduct(item.Product.Name);
                    element.ClientId = client.Id;
                    element.ProductId = product.Id;
                    element.Cost = item.Cost;
                    element.СurrencyСode = item.CurrencyCode;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public IEnumerable<SaleInfo> Items
        {
            get { return Context.SaleInfo.AsEnumerable().Select(item => SaleInfoObjectById(item.Id)); }
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
