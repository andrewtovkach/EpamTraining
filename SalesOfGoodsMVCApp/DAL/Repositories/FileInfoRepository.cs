using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FileInfoRepository : AbstractRepository, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        public FileInfoRepository()
        {
            Mapper.CreateMap<FileInfo, Model.FileInfo>();
            Mapper.CreateMap<Model.FileInfo, FileInfo>();
        }

        public void Add(FileInfo item)
        {
            item.Manager.Id = GetOrCreateManager(item.Manager);
            Context.FileInfo.Add(Mapper.Map<FileInfo, Model.FileInfo>(item));
        }

        private static int GetOrCreateManager(Manager manager)
        {
            var managersRepository = new ManagersRepository();
            int id = managersRepository.GetElementId(manager);
            if (id != 0) return id;
            managersRepository.Add(manager);
            managersRepository.SaveChanges();
            return managersRepository.Last().Id;
        }

        public void Remove(int id)
        {
            var element = GetFileInfoById(id);
            if (element != null)
                Context.FileInfo.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.FileInfo GetFileInfoById(int id)
        {
            return Context.FileInfo.FirstOrDefault(x => x.Id == id);
        }

        public int GetElementId(FileInfo item)
        {
            var element = Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        public void Update(int id, FileInfo item)
        {
            var element = GetFileInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect fileInfo identification!");
            element.Date = item.Date;
            element.ManagerId = GetOrCreateManager(item.Manager);
        }

        public IEnumerable<FileInfo> Items
        {
            get { return Context.FileInfo.Select(Mapper.Map<Model.FileInfo, FileInfo>); }
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