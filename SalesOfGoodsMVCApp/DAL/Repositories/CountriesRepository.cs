using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using AutoMapper;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CountriesRepository : AbstractRepository, IRepository<Country>, IEnumerable<Country>
    {
        public CountriesRepository()
        {
            Mapper.CreateMap<Country, Model.Country>();
            Mapper.CreateMap<Model.Country, Country>();
        }

        public void Add(Country item)
        {
            Context.Countries.Add(Mapper.Map<Country, Model.Country>(item));
        }

        public void Remove(int id)
        {
            var element = GetCountryById(id);
            if (element != null)
                Context.Countries.Remove(element);
            else throw new ArgumentException("Incorrect argument!");
        }

        private Model.Country GetCountryById(int id)
        {
            return Context.Countries.FirstOrDefault(x => x.Id == id);
        }

        public int GetElementId(Country item)
        {
            var element = Items.FirstOrDefault(it => it.Equals(item));
            return element != null ? element.Id : 0;
        }

        public void Update(int id, Country item)
        {
            var element = GetCountryById(id);
            if (element == null)
                throw new ArgumentException("Incorrect country identification!");
            element.Name = item.Name;
        }

        public IEnumerable<Country> Items
        {
            get { return Context.Countries.Select(Mapper.Map<Model.Country, Country>); }
        }

        public IEnumerator<Country> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}