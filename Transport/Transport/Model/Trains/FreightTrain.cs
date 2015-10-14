using System;
using System.Linq;
using Transport.Comparers;
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

        public long TotalOccupiedVolume
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var freightCarriage = x as FreightCarriage;
                        return freightCarriage != null ? freightCarriage.TotalOccupiedVolume : 0;
                    });
            }
        }

        public long TotalVolume
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var freightCarriage = x as FreightCarriage;
                        return freightCarriage != null ? freightCarriage.TotalVolume : 0;
                    });
            }
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


        public void SortByUserCondition()
        {
            listCarriages.Sort(new ComparerByOccupiedVolume());
        }
    }
}
