﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

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

        public void Remove(int id)
        {
            var element = GetClientById(id);
            if (element != null)
                Context.Clients.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Client GetClientById(int id)
        {
            return Context.Clients.FirstOrDefault(x => x.Id == id);
        }

        public int GetElementId(Client item)
        {
            var element = Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        public void Update(int id, Client item)
        {
            var element = GetClientById(id);
            if (element == null)
                throw new ArgumentException("Incorrect client identification!");
            element.FirstName = item.FirstName;
            element.SecondName = item.SecondName;
            element.BirthDay = item.BirthDay;
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