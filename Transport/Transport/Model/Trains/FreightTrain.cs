using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    public class FreightTrain : Train
    {
        public FreightTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive) { }

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

        private IEnumerable<IFreightElement> FreightElements
        {
            get { return ListCarriages.OfType<IFreightElement>(); }
        }

        public IEnumerable<IFreightElement> GetFreightCarriages(Func<IFreightElement, bool> predicate)
        {
            return FreightElements.Where(predicate);
        }

        public long TotalOccupiedVolume
        {
            get { return FreightElements.Sum(item => item.OccupiedVolume); }
        }

        public long TotalVolume
        {
            get { return FreightElements.Sum(item => item.Volume); }
        }

        public long TotalFreeVolue
        {
            get { return TotalVolume - TotalOccupiedVolume; }
        }

        public double TotalPercentageFreeVolue
        {
            get { return Math.Round((double)TotalFreeVolue / TotalVolume * 100.0, 2); }
        }
    }
}
