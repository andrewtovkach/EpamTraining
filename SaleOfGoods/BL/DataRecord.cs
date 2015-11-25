using System;

namespace BL
{
    public struct DataRecord
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public string Cost { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Date.ToShortDateString(), Client, Product, Cost);
        }
    }
}
