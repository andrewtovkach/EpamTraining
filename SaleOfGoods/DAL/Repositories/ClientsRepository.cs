using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class ClientsRepository : IRepository<Client>, IEnumerable<Client>
    {
        public Model.SalesDBEntities Context { get; private set; }

        public ClientsRepository()
        {
            Context = new Model.SalesDBEntities();
        }

        private static Model.Client ToEntity(Client manager)
        {
            return new Model.Client
            {
                FirstName = manager.FirstName,
                SecondName = manager.SecondName
            };
        }

        public void Add(Client item)
        {
            Context.Clients.Add(ToEntity(item));
        }

        public void Remove(Client item)
        {
            var element = Context.Clients.FirstOrDefault(x => x.FirstName == item.FirstName && x.SecondName == item.SecondName);
            if (element != null)
                Context.Clients.Remove(element);
        }

        public void Update(Client item, object info)
        {
            var clientInto = info as Tuple<string, string>;
            if (clientInto == null)
                throw new ArgumentException("Incorrect argumnet!");
            var element = Context.Clients.FirstOrDefault(x => x.FirstName == item.FirstName && x.SecondName == item.SecondName);
            if (element != null)
            {
                element.FirstName = clientInto.Item1;
                element.SecondName = clientInto.Item2;
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public IEnumerable<Client> Items
        {
            get
            {
                return Context.Clients.Select(item => new Client
                {
                    FirstName = item.FirstName.Trim(), 
                    SecondName = item.SecondName.Trim()
                });
            }
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
