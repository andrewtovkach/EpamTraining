using System;

namespace BL
{
    public class DataRecord
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public string Cost { get; set; }

        public override string ToString()
        {
            return Date.ToShortDateString() + " " + Client + " " + Product + " " + Cost;
        }
    }
}
