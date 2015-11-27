using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;

namespace DAL.Repositories
{
    public class ClientsRepository : AbstractRepository, IRepository<Client>, IEnumerable<Client>
    {
        public ClientsRepository()
        {
            Mapper.CreateMap<Client, Model.Client>();
            Mapper.CreateMap<Model.Client, Client>();
        }

        public void Add(Client item)
        {
            Context.Clients.Add(Mapper.Map<Client, Model.Client>(item));
        }

        public void Remove(Client item)
        {
            var element = ClientById(item.Id);
            if (element != null)
                Context.Clients.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Client ClientById(int id)
        {
            return Context.Clients.FirstOrDefault(x => x.Id == id);
        }

        public Client ClientObjectById(int id)
        {
            return Mapper.Map<Model.Client, Client>(ClientById(id));
        }

        public void Update(int id, Client item)
        {
            var element = ClientById(id);
            if (element == null)
                throw new ArgumentException("Incorrect client identification!");
            element.FirstName = item.FirstName;
            element.SecondName = item.SecondName;
        }

        public IEnumerable<Client> Items
        {
            get { return Context.Clients.Select(Mapper.Map<Model.Client, Client>); }
        }

        public IEnumerator<Client> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
