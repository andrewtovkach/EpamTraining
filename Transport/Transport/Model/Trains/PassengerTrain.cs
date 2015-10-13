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
    class PassengerTrain : Train, ISortable, IPrintable, ICollection<PassengerCarriage>
    {
        public BaggageCarriage BaggageCarriage { get; set; }
        public RestaurantCarriage RestaurantCarriage { get; set; }

        private List<PassengerCarriage> listCarriages;

        public PassengerTrain()
        {
            listCarriages = new List<PassengerCarriage>();
        }

        public PassengerTrain(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate, locomotive)
        {
            listCarriages = new List<PassengerCarriage>();
        }

        public Carriage this[int number]
        {
            get { return listCarriages.FirstOrDefault(x => x.Number == number); }
        }

        public override string ToString()
        {
            return "Пассажирский поезд " + base.ToString();
        }

        public void Add(PassengerCarriage item)
        {
            listCarriages.Add(item);
        }

        public void Clear()
        {
            listCarriages.Clear();
        }

        public bool Contains(PassengerCarriage item)
        {
            return listCarriages.Contains(item);
        }

        public void CopyTo(PassengerCarriage[] array, int arrayIndex)
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

        public bool Remove(PassengerCarriage item)
        {
            return listCarriages.Remove(item);
        }

        public IEnumerator<PassengerCarriage> GetEnumerator()
        {
            return listCarriages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void FreePlace(int numberCarriage, int numberPlace)
        {
            try
            {
                ((PassengerCarriage)this[numberCarriage]).Remove(new Place { Number = numberPlace, 
                    CarriageNumber = numberCarriage });
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
                ((PassengerCarriage)this[numberCarriage]).Add(new Place { Number = numberPlace, 
                    CarriageNumber = numberCarriage });
            }
            catch (Exception)
            {
                throw new ArgumentException("Номер вагона имеет недопустимое значение!");
            }
        }

        public IEnumerable<Place> GetFreePlaces()
        {
            return from item in listCarriages 
                   from Place it in item 
                   where !it.IsBusy 
                   select it;
        }

        public IEnumerable<Place> GetBusyPlaces()
        {
            return from item in listCarriages
                   from Place it in item
                   where it.IsBusy
                   select it;
        }

        public long TotalPlacesCount
        {
            get { return listCarriages.Sum(x => x.PlacesCount); }
        }

        public int TotalBusyPlacesCount
        {
            get { return listCarriages.Sum(x => x.BusyPlacesCount); }
        }

        public int TotalFreePlacesCount
        {
            get { return listCarriages.Sum(x => x.FreePlacesCount); }
        }

        public void SortByUserCondition()
        {
            listCarriages.Sort(new ComparerByComfort());
        }

        public void Sort()
        {
            listCarriages.Sort();
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
            result.AppendLine(" * " + (BaggageCarriage == null ? "Багажное отделение отсутствует!" : 
                BaggageCarriage.Print()));
            result.AppendLine(" * " + (RestaurantCarriage == null ? "Вагон-ресторан отсутствует!" : 
                RestaurantCarriage.Print()));
            return result.ToString();
        }
    }
}
