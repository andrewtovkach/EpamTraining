using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;

namespace DAL.Repositories
{
    public class ManagersRepository : AbstractRepository, IRepository<Manager>, IEnumerable<Manager>
    {
        private static Model.Manager ToEntity(Manager manager)
        {
            return Mapper.Map<Manager, Model.Manager>(manager);
        }

        public void Add(Manager item)
        {
            Context.Managers.Add(ToEntity(item));
        }

        public void Remove(Manager item)
        {
            var element = ManagerById(item.Id);
            if (element != null)
                Context.Managers.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Manager ManagerById(int id)
        {
            return Context.Managers.FirstOrDefault(x => x.Id == id);
        }

        public Manager ManagerObjectById(int id)
        {
            var manager = Context.Managers.FirstOrDefault(x => x.Id == id);
            return manager != null ? new Manager(manager.Id, manager.SecondName) : null;
        }

        public void Update(int id, Manager item)
        {
            var element = ManagerById(id);
            if (element != null)
                element.SecondName = item.SecondName;
            else throw new ArgumentException("Incorrect manager identification!");
        }

        public IEnumerable<Manager> Items
        {
            get { return Context.Managers.AsEnumerable().Select(item => ManagerObjectById(item.Id)); }
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
