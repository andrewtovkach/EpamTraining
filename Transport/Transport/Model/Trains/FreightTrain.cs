using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Comparers;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    class FreightTrain : Train, ISortable, IPrintable, ICollection<FreightCarriage>
    {
        private List<FreightCarriage> listCarriages;

        public FreightTrain()
        {
            listCarriages = new List<FreightCarriage>();
        }

        public FreightTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive)
        {
            listCarriages = new List<FreightCarriage>();
        }

        public Carriage this[int number]
        {
            get { return listCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Грузовой поезд " + base.ToString();
        }

        public void Add(FreightCarriage item)
        {
            listCarriages.Add(item);
        }

        public void Clear()
        {
            listCarriages.Clear();
        }

        public bool Contains(FreightCarriage item)
        {
            return listCarriages.Contains(item);
        }

        public void CopyTo(FreightCarriage[] array, int arrayIndex)
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

        public bool Remove(FreightCarriage item)
        {
            return listCarriages.Remove(item);
        }

        public IEnumerator<FreightCarriage> GetEnumerator()
        {
            return listCarriages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int TotalOccupiedVolume
        {
            get { return listCarriages.Sum(x => x.OccupiedVolume); }
        }

        public long TotalVolume
        {
            get { return listCarriages.Sum(x => x.Volume); }
        }

        public long TotalFreeVolue
        {
            get { return listCarriages.Sum(x => x.Volume - x.OccupiedVolume); }
        }

        public double PercentageTotalFreeVolue
        {
            get { return Math.Round((double) TotalFreeVolue / TotalVolume * 100.0, 2); }
        }

        public void Sort()
        {
            listCarriages.Sort();
        }

        public void SortByUserCondition()
        {
            listCarriages.Sort(new ComparerByOccupiedVolume());
        }

        public string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.ToString());
            result.AppendLine(" * " + Locomotive);
            foreach (var item in listCarriages)
            {
                result.AppendLine(" * " + item.Print());
            }
            return result.ToString();
        }
    }
}
