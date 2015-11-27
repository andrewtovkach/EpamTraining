using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;

namespace DAL.Repositories
{
    public class FileInfoRepository : AbstractRepository, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        public FileInfoRepository()
        {
            Mapper.CreateMap<FileInfo, Model.FileInfo>()
                .ForMember("ManagerId", opt => opt.MapFrom(c => c.Manager.Id))
                .ForMember("SaleInfoId", opt => opt.MapFrom(src => src.SaleInfo.Id));
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
            new SaleInfoRepository().Add(saleInfo);
            return Context.SaleInfo.AsEnumerable().Last();
        }

        public void Add(FileInfo item)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var manager = GetManager(item.Manager.SecondName);
                    var saleInfo = GetSaleInfo(item.SaleInfo);
                    item.Manager.Id = manager.Id;
                    item.SaleInfo.Id = saleInfo.Id;
                    Context.FileInfo.Add(Mapper.Map<FileInfo, Model.FileInfo>(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
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
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = FileInfoById(id);
                    if (element == null)
                        throw new ArgumentException("Incorrect fileInfo identification!");
                    element.Date = item.Date;
                    var manager = GetManager(item.Manager.SecondName);
                    var saleInfo = GetSaleInfo(item.SaleInfo);
                    element.ManagerId = manager.Id;
                    element.SaleInfoId = saleInfo.Id;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        public FileInfo FileInfoObjectById(int id)
        {
            var fileInfo = FileInfoById(id);
            return new FileInfo(new ManagersRepository().ManagerObjectById(fileInfo.ManagerId), fileInfo.Date,
                    new SaleInfoRepository().SaleInfoObjectById(fileInfo.SaleInfoId), fileInfo.Id);
        }

        public IEnumerable<FileInfo> Items
        {
            get { return Context.FileInfo.AsEnumerable().Select(item => FileInfoObjectById(item.Id)); }
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
