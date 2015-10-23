using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.Enums;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    public class PassengerCarriage : Carriage, ICollection<Place>, IPassengerElement
    {
        public TypePassengerCarriage TypePassengerCarriage { get; set; }
        public uint PlacesCount { get; set; }

        private readonly List<Place> _listPlaces;

        public PassengerCarriage(int number, DateTime startUpDate, uint axisNumber, uint placesCount, 
            TypePassengerCarriage typePassengerCarriage)
            : base(number, startUpDate, axisNumber)
        {
            this.PlacesCount = placesCount;
            this.TypePassengerCarriage = typePassengerCarriage;
            _listPlaces = new List<Place>();
            for (int i = 1; i <= PlacesCount; i++)
                _listPlaces.Add(new Place { Number =  i });
        }

        public override string ToString()
        {
            return string.Format("Пассажирский вагон {0}, Кол-во мест: {1}, Тип вагона: {2}", base.ToString(), 
                PlacesCount, TypePassengerCarriage);
        }

        public Place this[int number]
        {
            get { return _listPlaces.FirstOrDefault(item => item.Number == number); }
        }

        public void Add(Place item)
        {
            if (item.Number <= PlacesCount && item.Number > 0)
                _listPlaces[item.Number - 1] = new Place { IsBusy = true, Number = item.Number};
            else throw new InvalidOperationException("Номер места имеет недопустимое значение!");
        }

        public void Clear()
        {
            _listPlaces.ForEach(item => item.IsBusy = false);
        }

        public bool Contains(Place item)
        {
            return _listPlaces.Contains(item);
        }

        public void CopyTo(Place[] array, int arrayIndex)
        {
            _listPlaces.CopyTo(array, arrayIndex);
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
            if (item.Number > PlacesCount || item.Number <= 0) 
                return false;
            _listPlaces[item.Number - 1] = new Place { Number = item.Number };
            return true;
        }

        public IEnumerator<Place> GetEnumerator()
        {
            return _listPlaces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<int> GetFreePlaces()
        {
            return _listPlaces.Where(place => !place.IsBusy).Select(item => item.Number).AsEnumerable();
        }

        public long FreePlacesCount
        {
            get { return PlacesCount - BusyPlacesCount; }
        }

        public IEnumerable<int> GetBusyPlaces()
        {
            return _listPlaces.Where(place => place.IsBusy).Select(item => item.Number).AsEnumerable();
        }

        public long BusyPlacesCount
        {
            get { return _listPlaces.Count(item => item.IsBusy); }
        }

        public override string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(ToString());
            foreach (var item in _listPlaces)
                result.AppendLine("   - " + item);
            return result.ToString();
        }
    }
}
