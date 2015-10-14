using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Enums;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    class PassengerCarriage : Carriage, ICollection<Place>, IPassenger
    {
        public TypePassengerCarriage TypePassengerCarriage { get; set; }
        public uint PlacesCount { get; set; }

        private List<Place> listPlaces;

        public PassengerCarriage(int number, DateTime startUpDate, uint axisNumber, uint placesCount, 
            TypePassengerCarriage typePassengerCarriage)
            : base(number, startUpDate, axisNumber)
        {
            this.PlacesCount = placesCount;
            this.TypePassengerCarriage = typePassengerCarriage;
            listPlaces = new List<Place>();
            for (int i = 1; i <= PlacesCount; i++)
            {
                listPlaces.Add(new Place { Number = i });
            }
        }

        public override string ToString()
        {
            return String.Format("Пассажирский вагон {0}, Кол-во мест: {1}, Тип вагона: {2}", base.ToString(), 
                PlacesCount, TypePassengerCarriage);
        }

        public Place this[int number]
        {
            get { return listPlaces.FirstOrDefault(x => x.Number == number); }
        }

        public void Add(Place item)
        {
            if (item.Number <= PlacesCount && item.Number > 0)
                listPlaces[item.Number - 1].IsBusy = true;
            else throw new InvalidOperationException("Номер места имеет недопустимое значение!");
        }

        public void Clear()
        {
            listPlaces.ForEach(x => x.IsBusy = false);
        }

        public bool Contains(Place item)
        {
            return listPlaces.Contains(item);
        }

        public void CopyTo(Place[] array, int arrayIndex)
        {
            listPlaces.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return (int)PlacesCount; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Place item)
        {
            if (item.Number <= PlacesCount && item.Number > 0)
            {
                listPlaces[item.Number - 1].IsBusy = false;
                return true;
            }
            return false;
        }

        public IEnumerator<Place> GetEnumerator()
        {
            return listPlaces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public long TotalPlacesCount
        {
            get { return PlacesCount; }
        }

        public IEnumerable<int> GetFreePlaces()
        {
            return listPlaces.Where(place => !place.IsBusy).Select(x => x.Number).AsEnumerable();
        }

        public long FreePlacesCount
        {
            get { return listPlaces.Count(x => !x.IsBusy); }
        }

        public IEnumerable<int> GetBusyPlaces()
        {
            return listPlaces.Where(place => place.IsBusy).Select(x => x.Number).AsEnumerable();
        }

        public long BusyPlacesCount
        {
            get { return listPlaces.Count(x => x.IsBusy); }
        }

        public override string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.ToString());
            foreach (var item in listPlaces)
            {
                result.AppendLine("   - " + item.ToString());
            }
            return result.ToString();
        }
    }
}
