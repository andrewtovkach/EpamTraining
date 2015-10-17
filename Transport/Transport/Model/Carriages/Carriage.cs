using System;

namespace Transport.Model.Carriages
{
    abstract class Carriage : Vehicle
    {
        public uint AxisNumber { get; set; }

        protected Carriage(int number, DateTime startUpDate, uint axisNumber)
            : base(number, startUpDate)
        {
            this.AxisNumber = axisNumber;
        }

        public abstract string Print();

        public override string ToString()
        {
            return string.Format("{0} Кол-во осей: {1}", base.ToString(), AxisNumber);
        }
    }
}
