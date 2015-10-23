using System.Collections;
using System.Collections.Generic;

namespace ConcordanceProject.Model
{
    public abstract class Collection<T> : ICollection<T>
    {
        protected readonly ICollection<T> List;

        protected Collection()
        {
            List = new List<T>();
        }

        protected Collection(ICollection<T> list)
        {
            List = list;
        }

        public void Add(T item)
        {
            List.Add(item);   
        }

        public void Clear()
        {
            List.Clear();
        }

        public bool Contains(T item)
        {
            return List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return List.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
