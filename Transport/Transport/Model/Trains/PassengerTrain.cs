using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    class PassengerTrain : Train, ISortable
    {
        public PassengerTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive)
        {
        }

        public Carriage this[int number]
        {
            get { return listCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Пассажирский поезд " + base.ToString();
        }

        public void FreePlace(int numberCarriage, int numberPlace)
        {
            try
            {
                var passengerCarriage = this[numberCarriage] as PassengerCarriage;
                if (passengerCarriage != null)
                    passengerCarriage.Remove(new Place { Number = numberPlace });
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        public void TakePlace(int numberCarriage, int numberPlace)
        {
            try
            {
                var passengerCarriage = this[numberCarriage] as PassengerCarriage;
                if (passengerCarriage != null)
                    passengerCarriage.Add(new Place { Number = numberPlace });
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        private IEnumerable<IBaggageElement> GetAllBaggageElements()
        {
            return listCarriages.OfType<IBaggageElement>();
        }

        private IEnumerable<IPassengerElement> GetAllPassangerElements()
        {
            return listCarriages.OfType<IPassengerElement>();
        }

        public long TotalPlacesCount
        {
            get { return GetAllPassangerElements().Sum(x => x.PlacesCount); }
        }

        public long TotalBusyPlacesCount
        {
            get { return GetAllPassangerElements().Sum(x => x.BusyPlacesCount); }
        }

        public long TotalCellsCount
        {
            get { return GetAllBaggageElements().Sum(x => x.CellsCount); }
        }

        public long TotalFreePlacesCount
        {
            get { return GetAllPassangerElements().Sum(x => x.FreePlacesCount); }
        }

        public int TotalBusyCellsCount
        {
            get { return GetAllBaggageElements().Sum(x => x.BusyCellsCount); }
        }

        public long TotalFreeCellsCount
        {
            get { return GetAllBaggageElements().Sum(x => x.FreeCellsCount); }
        }

        public Baggage GetBaggage(int numberCarriage, int baggageNumber)
        {
            try
            {
                Baggage baggage = ((BaggageCarriage)this[numberCarriage])[baggageNumber];
                ((BaggageCarriage)this[numberCarriage]).Remove(baggage);
                return baggage;
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        public void GiveBaggage(int numberCarriage, Baggage baggage)
        {
            try
            {
                var baggageCarriage = this[numberCarriage] as BaggageCarriage;
                if (baggageCarriage != null)
                    baggageCarriage.Add(baggage);
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        public int GetCellNumber(int numberCarriage, int baggageNumber)
        {
            try
            {
                return ((BaggageCarriage)this[numberCarriage]).GetCellNumber(baggageNumber);
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        public double TotalWeight
        {
            get { return GetAllBaggageElements().Sum(x => x.Weight); }
        }

        public long TotalCapacity
        {
            get { return GetAllBaggageElements().Sum(x => x.Capacity); }
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
