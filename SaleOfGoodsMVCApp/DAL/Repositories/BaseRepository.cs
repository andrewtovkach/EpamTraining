using System;
using System.Linq;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class BaseRepository<T> : IDisposable
        where T : BaseClass
    {
        private bool _disposed;
        protected Model.SalesDBEntities Context { get; private set; }

        protected BaseRepository()
        {
            Context = new Model.SalesDBEntities();
            _disposed = false;
            Mapper.CreateMap<Model.SaleInfo, SaleInfo>().ReverseMap();
            Mapper.CreateMap<Model.Client, Client>().ReverseMap();
            Mapper.CreateMap<Model.Product, Product>().ReverseMap();
            Mapper.CreateMap<Model.FileInfo, FileInfo>().ReverseMap();
            Mapper.CreateMap<Model.Country, Country>().ReverseMap();
            Mapper.CreateMap<Model.Manager, Manager>().ReverseMap();
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            CleanUp(false);
        }

        private void CleanUp(bool clean)
        {
            if (!_disposed)
            {
                if (clean)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public int GetElementId(T item)
        {
            var repository = this as IRepository<T>;
            if (repository == null)
                return 0;
            var element = repository.Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        public int GetOrCreateElementId(T item)
        {
            int id = GetElementId(item);
            if (id != 0) return id;
            var repository = this as IRepository<T>;
            if (repository == null)
                return 0;
            repository.Add(item);
            SaveChanges();
            return repository.Items.Last().Id;
        }
    }
}
