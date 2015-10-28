using System.Collections;
using System.Collections.Generic;

namespace ConcordanceProject.Model.TextClasses
{
    public abstract class CollectionElement<T> : ICollection<T>
    {
        protected ICollection<T> Collection;

        protected CollectionElement()
        {
            Collection = new List<T>();
        }

        protected CollectionElement(ICollection<T> collection)
        {
            Collection = collection;
        }

        public void Add(T item)
        {
            Collection.Add(item);   
        }

        public void Clear()
        {
            Collection.Clear();
        }

        public bool Contains(T item)
        {
            return Collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return Collection.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
