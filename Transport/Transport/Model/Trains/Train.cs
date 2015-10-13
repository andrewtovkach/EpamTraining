using System;

namespace Transport.Model.Trains
{
    abstract class Train : Vehicle
    {
        public Locomotive Locomotive { get; set; }

        protected Train() { }

        protected Train(int number, DateTime startUpDate, Locomotive locomotive)
            : base(number, startUpDate)
        {
            this.Locomotive = locomotive;
        }
    }
}
