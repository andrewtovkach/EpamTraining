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

        private IEnumerable<IFreightElement> GetAllElements()
        {
            return listCarriages.OfType<IFreightElement>();
        }

        public long TotalOccupiedVolume
        {
            get { return GetAllElements().Sum(x => x.OccupiedVolume); }
        }

        public long TotalVolume
        {
            get { return GetAllElements().Sum(x => x.Volume); }
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
