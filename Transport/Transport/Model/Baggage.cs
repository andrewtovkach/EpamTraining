using System;

namespace Transport.Model
{
    struct Baggage
    {
        public int Number { get; set; }
        public string Name { get; set; }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Некорректное значение!");
                _weight = value;
            }
        }

        public override string ToString()
        {
            return String.Format("Багаж №{0} Название продукта: {1}, Вес: {2}", Number, Name, Weight);
        }
    }
}
