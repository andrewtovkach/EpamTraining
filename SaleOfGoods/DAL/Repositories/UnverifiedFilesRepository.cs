using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnverifiedFilesRepository : AbstractRepository, IRepository<UnverifiedFile>, IEnumerable<UnverifiedFile>
    {
        public UnverifiedFilesRepository()
        {
            Mapper.CreateMap<UnverifiedFile, Model.UnverifiedFile>();
            Mapper.CreateMap<Model.UnverifiedFile, UnverifiedFile>();
        }

        public void Add(UnverifiedFile item)
        {
            Context.UnverifiedFiles.Add(Mapper.Map<UnverifiedFile, Model.UnverifiedFile>(item));
        }

        public void Remove(UnverifiedFile item)
        {
            var element = GetUnverifiedFileById(item.Id);
            if (element != null)
                Context.UnverifiedFiles.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.UnverifiedFile GetUnverifiedFileById(int id)
        {
            return Context.UnverifiedFiles.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, UnverifiedFile item)
        {
            var element = GetUnverifiedFileById(id);
            if (element == null)
                throw new ArgumentException("Incorrect file identification!");
            element.FileName = item.FileName;
        }

        public UnverifiedFile GetUnverifiedFileObjectById(int id)
        {
            return Mapper.Map<Model.UnverifiedFile, UnverifiedFile>(GetUnverifiedFileById(id));
        }

        public IEnumerable<UnverifiedFile> Items
        {
            get { return Context.UnverifiedFiles.Select(Mapper.Map<Model.UnverifiedFile, UnverifiedFile>); }
        }

        public IEnumerator<UnverifiedFile> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
