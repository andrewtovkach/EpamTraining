using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class FileInfoRepository : AbstractRepository, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        private static Model.FileInfo ToEntity(FileInfo fileInfo)
        {
            return new Model.FileInfo
            {
                ManagerId = fileInfo.Manager.Id,
                Date = fileInfo.Date,
                SaleInfoId = fileInfo.SaleInfo.Id
            };
        }

        private Model.Manager ManagerByName(string secondName)
        {
            return Context.Managers.FirstOrDefault(x => x.SecondName == secondName);
        }

        private Model.Manager GetManager(string secondName)
        {
            return ManagerByName(secondName) ?? Context.Managers.Add(new Model.Manager { SecondName = secondName });
        }

        public Model.SaleInfo GetSaleInfo(SaleInfo saleInfo)
        {
            var saleInfoRepository = new SaleInfoRepository { saleInfo };
            saleInfoRepository.SaveChanges();
            return Context.SaleInfo.AsEnumerable().Last();
        }

        public void Add(FileInfo item)
        {
            var manager = GetManager(item.Manager.SecondName);
            var saleInfo = GetSaleInfo(item.SaleInfo);
            item.Manager.Id = manager.Id;
            item.SaleInfo.Id = saleInfo.Id;
            Context.FileInfo.Add(ToEntity(item));
        }

        public void Remove(FileInfo item)
        {
            var element = FileInfoById(item.Id);
            if (element != null)
                Context.FileInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.FileInfo FileInfoById(int id)
        {
            return Context.FileInfo.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, FileInfo item)
        {
            var element = FileInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect fileInfo identification!");
            element.Date = item.Date;
            var manager = GetManager(item.Manager.SecondName);
            var saleInfo = GetSaleInfo(item.SaleInfo);
            element.ManagerId = manager.Id;
            element.SaleInfoId = saleInfo.Id;
        }

        public IEnumerable<FileInfo> Items
        {
            get
            {
                return Context.FileInfo.AsEnumerable().Select(item => new FileInfo(item.Id,
                    new ManagersRepository().ManagerObjectById(item.ManagerId), item.Date ?? new DateTime(),
                    new SaleInfoRepository().SaleInfoObjectById(item.SaleInfoId)
                ));
            }
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
