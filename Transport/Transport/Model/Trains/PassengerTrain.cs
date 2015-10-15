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

        public long TotalPlacesCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var passenger = x as IPassengerElement;
                        return passenger != null ? passenger.TotalPlacesCount : 0;
                    });
            }
        }

        public long TotalBusyPlacesCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var passenger = x as IPassengerElement;
                        return passenger != null ? passenger.BusyPlacesCount : 0;
                    });
            }
        }

        public long TotalCellsCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var baggage = x as IBaggageElement;
                        return baggage != null ? baggage.TotalCellsCount : 0;
                    });
            }
        }

        public long TotalFreePlacesCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var passenger = x as IPassengerElement;
                        return passenger != null ? passenger.FreePlacesCount : 0;
                    });
            }
        }

        public int TotalBusyCellsCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var baggage = x as IBaggageElement;
                        return baggage != null ? baggage.BusyCellsCount : 0;
                    });
            }
        }

        public long FreeCellsCount
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var baggage = x as IBaggageElement;
                        return baggage != null ? baggage.FreeCellsCount : 0;
                    });
            }
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
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var baggage = x as IBaggageElement;
                        return baggage != null ? baggage.TotalWeight : 0;
                    });
            }
        }

        public long TotalCapacity
        {
            get
            {
                return listCarriages.Sum(x =>
                    {
                        var baggage = x as IBaggageElement;
                        return baggage != null ? baggage.TotalCapacity : 0;
                    });
            }
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
