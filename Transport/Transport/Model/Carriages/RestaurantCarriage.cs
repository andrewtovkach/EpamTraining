using System;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    class RestaurantCarriage : Carriage, IPrintable
    {
        public uint PlacesCount { get; set; }
        public string Description { get; set; }

        public RestaurantCarriage(int number, DateTime startUpDate, uint axisNumber, uint placesCount, 
            string description)
            : base(number, startUpDate, axisNumber)
        {
            this.PlacesCount = placesCount;
            this.Description = description;
        }

        public override string ToString()
        {
            return String.Format("Вагон-ресторан {0} Кол-во мест: {1} Описание: {2}", base.ToString(), 
                PlacesCount, Description);
        }

        public string Print()
        {
            return this.ToString();
        }
    }
}
