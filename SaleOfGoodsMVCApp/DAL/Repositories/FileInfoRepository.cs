using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FileInfoRepository : BaseRepository<FileInfo>, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        public void Add(FileInfo item)
        {
            item.Manager.Id = new ManagersRepository().GetOrCreateElementId(item.Manager);
            Context.FileInfo.Add(Mapper.Map<FileInfo, Model.FileInfo>(item));
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

        public void Update(int id, FileInfo item)
        {
            var element = GetFileInfoById(id);
            if (element == null)
                throw new ArgumentException("Incorrect fileInfo identification!");
            element.Date = item.Date;
            element.ManagerId = new ManagersRepository().GetOrCreateElementId(item.Manager);
        }

        public IEnumerable<FileInfo> Items
        {
            get
            {
                return Context.FileInfo.AsEnumerable().Select(item => new FileInfo(Mapper.Map<Model.Manager, Manager>(item.Manager), item.Date ?? DateTime.Now, item.Id));
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