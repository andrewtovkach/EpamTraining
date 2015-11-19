using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class ManagersRepository : IRepository<Manager>, IEnumerable<Manager>
    {
        public Model.SalesDBEntities Context { get; private set; }

        public ManagersRepository()
        {
            Context = new Model.SalesDBEntities();   
        }

        private static Model.Manager ToEntity(Manager manager)
        {
            return new Model.Manager { SecondName = manager.SecondName };
        }

        public void Add(Manager item)
        {
            Context.Managers.Add(ToEntity(item));
        }

        public void Remove(Manager item)
        {
            var element = Context.Managers.FirstOrDefault(x => x.SecondName == item.SecondName);
            if (element != null)
                Context.Managers.Remove(element);
        }

        public void Update(Manager item, object info)
        {
            string secondName = info as string;
            if(secondName == null)
                throw new ArgumentException("Incorrect argument!");
            var element = Context.Managers.FirstOrDefault(x => x.SecondName == item.SecondName);
            if (element != null) 
                element.SecondName = secondName;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public IEnumerable<Manager> Items
        {
            get { return Context.Managers.Select(item => new Manager { SecondName = item.SecondName.Trim() }); }
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
