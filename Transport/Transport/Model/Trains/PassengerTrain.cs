﻿using System;
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

        private IEnumerable<IBaggageElement> AllBaggageElements
        {
            get { return ListCarriages.OfType<IBaggageElement>(); }
        }

        private IEnumerable<IPassengerElement> AllPassangerElements
        {
            get { return ListCarriages.OfType<IPassengerElement>(); }
        }

        public long TotalPlacesCount
        {
            get { return AllPassangerElements.Sum(x => x.PlacesCount); }
        }

        public long TotalBusyPlacesCount
        {
            get { return AllPassangerElements.Sum(x => x.BusyPlacesCount); }
        }

        public long TotalCellsCount
        {
            get { return AllBaggageElements.Sum(x => x.CellsCount); }
        }

        public long TotalFreePlacesCount
        {
            get { return AllPassangerElements.Sum(x => x.FreePlacesCount); }
        }

        public int TotalBusyCellsCount
        {
            get { return AllBaggageElements.Sum(x => x.BusyCellsCount); }
        }

        public long TotalFreeCellsCount
        {
            get { return AllBaggageElements.Sum(x => x.FreeCellsCount); }
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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return new Baggage();
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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return -1;
        }

        public double TotalWeight
        {
            get { return AllBaggageElements.Sum(x => x.Weight); }
        }

        public long TotalCapacity
        {
            get { return AllBaggageElements.Sum(x => x.Capacity); }
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
