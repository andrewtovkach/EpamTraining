using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ManagersRepository : AbstractRepository, IRepository<Manager>, IEnumerable<Manager>
    {
        public ManagersRepository()
        {
            Mapper.CreateMap<Manager, Model.Manager>();
            Mapper.CreateMap<Model.Manager, Manager>();
        }

        public void Add(Manager item)
        {
            Context.Managers.Add(Mapper.Map<Manager, Model.Manager>(item));
        }

        public void Remove(Manager item)
        {
            var element = GetManagerById(item.Id);
            if (element != null)
                Context.Managers.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Manager GetManagerById(int id)
        {
            return Context.Managers.FirstOrDefault(x => x.Id == id);
        }

        public Manager GetManagerObjectById(int id)
        {
            return Mapper.Map<Model.Manager, Manager>(GetManagerById(id)); 
        }

        public void Update(int id, Manager item)
        {
            var element = GetManagerById(id);
            if (element != null)
                element.SecondName = item.SecondName;
            else throw new ArgumentException("Incorrect manager identification!");
        }

        public IEnumerable<Manager> Items
        {
            get { return Context.Managers.Select(Mapper.Map<Model.Manager, Manager>); }
        }

        public IEnumerator<Manager> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
