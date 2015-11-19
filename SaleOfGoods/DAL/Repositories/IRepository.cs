using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item, object info);
        void SaveChanges();
        IEnumerable<T> Items { get; }
    }
}
