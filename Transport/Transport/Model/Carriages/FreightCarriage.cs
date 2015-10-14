using System;
using Transport.Enums;
using Transport.Interfaces;

namespace Transport.Model.Carriages
{
    class FreightCarriage : Carriage, IFreight
    {
        public string ProductName { get; set; }
        public TypeFreightCarriage TypeFreightCarriage { get; set; }

        private uint _occupiedVolume;
        public uint OccupiedVolume
        {
            get { return _occupiedVolume; }
            set
            {
                if (value > 0 && value <= Volume)
                    _occupiedVolume = value;
                else throw new ArgumentException("Некорректное значение!");
            }
        }

        public uint Volume { get; set; }

        public FreightCarriage(int number, DateTime startUpDate, uint axisNumber, string productName, 
            TypeFreightCarriage typeFreightCarriage, uint volume, uint occupiedVolume)
            : base(number, startUpDate, axisNumber)
        {
            this.ProductName = productName;
            this.TypeFreightCarriage = typeFreightCarriage;
            this.Volume = volume;
            this.OccupiedVolume = occupiedVolume;
        }

        public long TotalVolume
        {
            get { return Volume; }
        }
        
        public long TotalOccupiedVolume
        {
            get { return OccupiedVolume; }
        }

        public long FreeVolume
        {
            get { return Volume - OccupiedVolume; }
        }

        public double PercentageFreeVolume
        {
            get { return Math.Round((double)(Volume - OccupiedVolume) / Volume * 100.0, 2); }
        }

        public override string ToString()
        {
            return String.Format("Грузовой вагон {0}, Название продукта: {1}, Вид вагона: {2}, Общ. вместимость: {3}," + 
                " Занято: {4}", base.ToString(), ProductName, TypeFreightCarriage, Volume, OccupiedVolume);
        }

        public override string Print()
        {
            return this.ToString();
        }
    }
}
