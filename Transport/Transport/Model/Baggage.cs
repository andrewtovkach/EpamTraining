namespace Transport.Model
{
    public struct Baggage
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public override string ToString()
        {
            return string.Format("Багаж №{0} Название продукта: {1}, Вес: {2}", Number, Name, Weight);
        }
    }
}
