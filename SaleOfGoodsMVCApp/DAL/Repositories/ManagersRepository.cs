using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Models;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ManagersRepository : BaseRepository<Manager>, IRepository<Manager>, IEnumerable<Manager>
    {
        public void Add(Manager item)
        {
            Context.Managers.Add(Mapper.Map<Manager, Model.Manager>(item));
        }

        public void Remove(int id)
        {
            var element = GetManagerById(id);
            if (element != null)
                Context.Managers.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Manager GetManagerById(int id)
        {
            return Context.Managers.FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, Manager item)
        {
            var element = GetManagerById(id);
            if (element == null)
                throw new ArgumentException("Incorrect manager identification!");
            element.FirstName = item.FirstName;
            element.SecondName = item.SecondName;
            element.Telephone = item.Telephone;
            element.Email = item.Email;
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