using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
    public class ClientsRepository : AbstractRepository, IRepository<Client>, IEnumerable<Client>
    {
        private static Model.Client ToEntity(Client client)
        {
            return new Model.Client
            {
                FirstName = client.FirstName,
                SecondName = client.SecondName
            };
        }

        public void Add(Client item)
        {
            Context.Clients.Add(ToEntity(item));
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
            var client = ClientById(id);
            return client != null ? new Client(client.Id, client.FirstName.Trim(), client.SecondName.Trim()) : null;
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
            get { return Context.Clients.AsEnumerable().Select(item => ClientObjectById(item.Id)); }
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
