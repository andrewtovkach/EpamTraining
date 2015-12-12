using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;
using Ninject;

namespace DAL.Repositories
{
    public class FileInfoRepository : BaseRepository<FileInfo>, IRepository<FileInfo>, IEnumerable<FileInfo>
    {
        private readonly IRepository<Manager> _repository;

        public FileInfoRepository()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<Manager>>().To<ManagersRepository>();
            _repository = ninjectKernel.Get<IRepository<Manager>>();
        }

        public void Add(FileInfo item)
        {
            Context.FileInfo.Add(new Model.FileInfo
            {
                Date = item.Date, 
                ManagerId = _repository.GetOrCreateElementId(item.Manager)
            });
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
            element.ManagerId = _repository.GetOrCreateElementId(item.Manager);
        }

        public IEnumerable<FileInfo> Items
        {
            get
            {
                return Context.FileInfo.AsEnumerable().Select(item => new FileInfo(Mapper.Map<Model.Manager, Manager>(item.Manager), 
                    item.Date ?? DateTime.Now, item.Id));
            }
        }

        public IEnumerable<FileInfo> SortedItems
        {
            get { return Items.OrderByDescending(item => item.Date); }
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