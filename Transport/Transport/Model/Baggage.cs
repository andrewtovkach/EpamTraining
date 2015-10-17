using System;

namespace Transport.Model
{
    struct Baggage
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public override string ToString()
        {
            return String.Format("Багаж №{0} Название продукта: {1}, Вес: {2}", Number, Name, Weight);
        }
    }
}
