using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    abstract class Train : Vehicle, ICollection<Carriage>
    {
        public Locomotive Locomotive { get; set; }

        protected readonly List<Carriage> listCarriages;

        protected Train(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate)
        {
            this.Locomotive = locomotive;
            listCarriages = new List<Carriage>();
        }

        public string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.ToString());
            foreach (var item in listCarriages)
                result.AppendLine(" * " + item.Print());
            return result.ToString();
        }

        public void Add(Carriage item)
        {
            throw new InvalidOperationException();
        }

        public void Clear()
        {
            listCarriages.Clear();
        }

        public bool Contains(Carriage item)
        {
            return listCarriages.Contains(item);
        }

        public void CopyTo(Carriage[] array, int arrayIndex)
        {
            listCarriages.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return listCarriages.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Carriage item)
        {
            return listCarriages.Remove(item);
        }

        public IEnumerator<Carriage> GetEnumerator()
        {
            return listCarriages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
