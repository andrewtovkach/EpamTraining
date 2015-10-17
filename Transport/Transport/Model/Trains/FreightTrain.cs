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
            get { return ListCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Грузовой поезд " + base.ToString();
        }

        public new void Add(Carriage item)
        {
            if (item is IFreightElement)
                ListCarriages.Add(item);
            else throw new ArgumentException("Невозможно добавить данный тип вагона!");
        }

        private IEnumerable<IFreightElement> AllElements
        {
            get { return ListCarriages.OfType<IFreightElement>(); }
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
            ListCarriages.Sort();
        }

        public void Sort(IComparer<Carriage> comparer)
        {
            ListCarriages.Sort(comparer);
        }
    }
}
