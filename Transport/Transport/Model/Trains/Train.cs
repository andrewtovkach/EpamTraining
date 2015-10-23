using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{

    public abstract class Train : Vehicle, ICollection<Carriage>, ISortable
    {
        public Locomotive Locomotive { get; set; }

        protected readonly List<Carriage> ListCarriages;

        protected Train(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate)
        {
            this.Locomotive = locomotive;
            ListCarriages = new List<Carriage>();
        }

        public Carriage this[int number]
        {
            get { return ListCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(ToString());
            foreach (var item in ListCarriages)
                result.AppendLine(" * " + item.Print());
            return result.ToString();
        }

        public void Add(Carriage item)
        {
            throw new InvalidOperationException();
        }

        public void Clear()
        {
            ListCarriages.Clear();
        }

        public bool Contains(Carriage item)
        {
            return ListCarriages.Contains(item);
        }

        public void CopyTo(Carriage[] array, int arrayIndex)
        {
            ListCarriages.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ListCarriages.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Carriage item)
        {
            return ListCarriages.Remove(item);
        }

        public IEnumerator<Carriage> GetEnumerator()
        {
            return ListCarriages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Carriage> GetCarriages(Func<Carriage, bool> predicate)
        {
            return ListCarriages.Where(predicate);
        }

        public void Sort()
        {
            ListCarriages.Sort();
        }

        public void Sort(IComparer<Carriage> comparer)
        {
            ListCarriages.Sort(comparer);
        }
    }
}
