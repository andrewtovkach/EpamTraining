using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(int id);
        void Update(int id, T item);
        IEnumerable<T> Items { get; }
        IEnumerable<T> SortedItems { get; }
        int GetElementId(T item);
        int GetOrCreateElementId(T item);
    }
}