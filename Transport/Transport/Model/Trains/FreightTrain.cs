using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    class FreightTrain : Train, ISortable
    {
        public FreightTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive)
        {
        }

        public Carriage this[int number]
        {
            get { return listCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Грузовой поезд " + base.ToString();
        }

        public new void Add(Carriage item)
        {
            if (item is FreightCarriage)
                listCarriages.Add(item);
            else throw new ArgumentException("Невозможно добавить данный тип вагона!");
        }

        private IEnumerable<IFreightElement> AllElements
        {
            get { return listCarriages.OfType<IFreightElement>(); }
        }

        public long TotalOccupiedVolume
        {
            get { return AllElements.Sum(x => x.OccupiedVolume); }
        }

        public long TotalVolume
        {
            get { return AllElements.Sum(x => x.Volume); }
        }

        public long TotalFreeVolue
        {
            get { return TotalVolume - TotalOccupiedVolume; }
        }

        public double PercentageTotalFreeVolue
        {
            get { return Math.Round((double)TotalFreeVolue / TotalVolume * 100.0, 2); }
        }

        public void Sort()
        {
            listCarriages.Sort();
        }

        public void Sort(IComparer<Carriage> comparer)
        {
            listCarriages.Sort(comparer);
        }
    }
}
