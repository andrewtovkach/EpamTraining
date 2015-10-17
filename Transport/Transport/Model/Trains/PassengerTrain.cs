using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Interfaces;
using Transport.Model.Carriages;

namespace Transport.Model.Trains
{
    class PassengerTrain : Train
    {
        public PassengerTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive) { }

        public Carriage this[int number]
        {
            get { return ListCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Пассажирский поезд " + base.ToString();
        }

        public new void Add(Carriage item)
        {
            if (!(item is IFreightElement))
                ListCarriages.Add(item);
            else throw new ArgumentException("Невозможно добавить данный тип вагона!");
        }

        private IEnumerable<IBaggageElement> BaggageElements
        {
            get { return ListCarriages.OfType<IBaggageElement>(); }
        }

        private IEnumerable<IPassengerElement> PassangerElements
        {
            get { return ListCarriages.OfType<IPassengerElement>(); }
        }

        public IEnumerable<IPassengerElement> GetPassangerCarriages(Func<IPassengerElement, bool> predicate)
        {
            return PassangerElements.Where(predicate);
        }

        public IEnumerable<IBaggageElement> GetBaggageCarriages(Func<IBaggageElement, bool> predicate)
        {
            return BaggageElements.Where(predicate);
        }

        public void FreePlace(int numberCarriage, int numberPlace)
        {
            try
            {
                var passengerCarriage = this[numberCarriage] as PassengerCarriage;
                if (passengerCarriage != null)
                    passengerCarriage.Remove(new Place { Number = numberPlace });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
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
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public long TotalPlacesCount
        {
            get { return PassangerElements.Sum(x => x.PlacesCount); }
        }

        public long TotalBusyPlacesCount
        {
            get { return PassangerElements.Sum(x => x.BusyPlacesCount); }
        }

        public long TotalCellsCount
        {
            get { return BaggageElements.Sum(x => x.CellsCount); }
        }

        public long TotalFreePlacesCount
        {
            get { return PassangerElements.Sum(x => x.FreePlacesCount); }
        }

        public int TotalBusyCellsCount
        {
            get { return BaggageElements.Sum(x => x.BusyCellsCount); }
        }

        public long TotalFreeCellsCount
        {
            get { return BaggageElements.Sum(x => x.FreeCellsCount); }
        }

        public Baggage GetBaggage(int numberCarriage, int baggageNumber)
        {
            try
            {
                BaggageCarriage baggageCarriage = this[numberCarriage] as BaggageCarriage;
                if (baggageCarriage != null)
                {
                    Baggage baggage = baggageCarriage[baggageNumber];
                    baggageCarriage.Remove(baggage);
                    return baggage;
                }
                return new Baggage();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Baggage();
            }
        }

        public void GiveBaggage(int numberCarriage, Baggage baggage)
        {
            try
            {
                BaggageCarriage baggageCarriage = this[numberCarriage] as BaggageCarriage;
                if (baggageCarriage != null)
                    baggageCarriage.Add(baggage);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public int GetCellNumber(int numberCarriage, int baggageNumber)
        {
            try
            {
                BaggageCarriage baggageCarriage = this[numberCarriage] as BaggageCarriage;
                if (baggageCarriage != null) 
                    return baggageCarriage.GetCellNumber(baggageNumber);
                return -1;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return -1;
            }
        }

        public double TotalWeight
        {
            get { return BaggageElements.Sum(x => x.Weight); }
        }

        public long TotalCapacity
        {
            get { return BaggageElements.Sum(x => x.Capacity); }
        }
    }
}
