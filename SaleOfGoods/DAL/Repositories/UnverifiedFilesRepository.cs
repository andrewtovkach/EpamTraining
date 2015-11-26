using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;

namespace DAL.Repositories
{
    public class UnverifiedFilesRepository : AbstractRepository, IRepository<UnverifiedFile>, IEnumerable<UnverifiedFile>
    {
        public UnverifiedFilesRepository()
        {
            Mapper.CreateMap<UnverifiedFile, Model.UnverifiedFile>()
                .ForMember("Id", opt => opt.MapFrom(c => c.Id))
                .ForMember("FileName", opt => opt.MapFrom(src => src.FileName));    
        }

        public void Add(UnverifiedFile item)
        {
            Context.UnverifiedFiles.Add(Mapper.Map<UnverifiedFile, Model.UnverifiedFile>(item));
        }

        public void Remove(UnverifiedFile item)
        {
            var element = UnverifiedFileById(item.Id);
            if (element != null)
                Context.UnverifiedFiles.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.UnverifiedFile UnverifiedFileById(int id)
        {
            return Context.UnverifiedFiles.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, UnverifiedFile item)
        {
            var element = UnverifiedFileById(id);
            if (element == null)
                throw new ArgumentException("Incorrect file identification!");
            element.FileName = item.FileName;
        }

        public UnverifiedFile UnverifiedFileObjectById(int id)
        {
            var unverifiedFile = UnverifiedFileById(id);
            return unverifiedFile != null ? new UnverifiedFile(unverifiedFile.Id, unverifiedFile.FileName) : null;
        }

        public IEnumerable<UnverifiedFile> Items
        {
            get { return Context.UnverifiedFiles.AsEnumerable().Select(item => UnverifiedFileObjectById(item.Id)); }
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
