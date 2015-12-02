using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FileInfoRepository : AbstractRepository, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        private Model.Manager GetManagerByName(string secondName)
        {
            return Context.Managers.FirstOrDefault(x => x.SecondName == secondName);
        }

        private Model.Manager GetOrCreateManager(string secondName)
        {
            return GetManagerByName(secondName) ?? Context.Managers.Add(new Model.Manager { SecondName = secondName });
        }

        private Model.SaleInfo GetSaleInfoByName(SaleInfo info)
        {
            var client = new SaleInfoRepository().GetClient(info.Client.FirstName, info.Client.SecondName);
            var product = new SaleInfoRepository().GetProduct(info.Product.Name);
            return Context.SaleInfo.FirstOrDefault(x => x.Date == info.Date && x.ClientId == client.Id && 
                x.ProductId == product.Id && x.Cost == info.Cost);
        }

        public Model.SaleInfo GetOrCreateSaleInfo(SaleInfo info)
        {
            var saleInfo = GetSaleInfoByName(info);
            if (saleInfo != null)
                return saleInfo;
            var saleInfoRepository = new SaleInfoRepository { info };
            saleInfoRepository.SaveChanges();
            return Context.SaleInfo.AsEnumerable().Last();  
        }


        static readonly object Locker = new object();

        public void Add(FileInfo item)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var manager = GetOrCreateManager(item.Manager.SecondName);
                    foreach (var saleInfo in item.SaleInfo.Select(GetOrCreateSaleInfo))
                    {
                        lock (Locker)
                        {
                            Context.FileInfo.Add(new Model.FileInfo
                            {
                                Date = item.Date,
                                ManagerId = manager.Id,
                                SaleInfoId = saleInfo.Id
                            });
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new Exception(exception.Message);
                }
            }
        }

        public void Remove(FileInfo item)
        {
            var element = GetFileInfoById(item.Id);
            if (element != null)
                Context.FileInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.FileInfo GetFileInfoById(int id)
        {
            return Context.FileInfo.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, FileInfo item)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = GetFileInfoById(id);
                    if (element == null)
                        throw new ArgumentException("Incorrect fileInfo identification!");
                    var manager = GetOrCreateManager(item.Manager.SecondName);
                    foreach (var saleInfo in item.SaleInfo.Select(GetOrCreateSaleInfo))
                    {
                        element.Date = item.Date;
                        element.ManagerId = manager.Id;
                        element.SaleInfoId = saleInfo.Id;
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new Exception(exception.Message);
                }
            }
        }

        public FileInfo GetFileInfoObjectById(int id)
        {
            var fileInfo = GetFileInfoById(id);
            var query = (from element in Context.FileInfo
                         where element.ManagerId == fileInfo.ManagerId && element.Date == fileInfo.Date
                         select element.SaleInfo).ToList();
            var list = query.Select(item => new SaleInfoRepository().GetSaleInfoObjectById(item.Id)).ToList();
            return new FileInfo(new ManagersRepository().GetManagerObjectById(fileInfo.ManagerId), fileInfo.Date,
                    list, fileInfo.Id);
        }

        public IEnumerable<FileInfo> Items
        {
            get { return Context.FileInfo.AsEnumerable().Select(item => GetFileInfoObjectById(item.Id)); }
        }

        public IEnumerator<FileInfo> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
