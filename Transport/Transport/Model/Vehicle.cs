using System;

namespace Transport.Model
{
    abstract class Vehicle : IComparable<Vehicle>
    {
        public int Number { get; set; }
        public DateTime StartUpDate { get; set; }

        protected Vehicle() { }

        protected Vehicle(int number, DateTime startUpDate)
        {
            this.Number = number;
            this.StartUpDate = startUpDate;
        }

        public override string ToString()
        {
            return String.Format("№{0} Дата ввода: {1}", Number, StartUpDate.ToShortDateString());
        }

        public int CompareTo(Vehicle other)
        {
            return this.Number.CompareTo(other.Number);
        }
    }
}
