using System;

namespace Transport.Model
{
    public class Place
    {
        public int Number { get; set; }
        public int CarriageNumber { get; set; }

        public bool IsBusy { get; set; }

        public override string ToString()
        {
            return String.Format("Место №{0} {1}", Number, IsBusy ? "занято" : "свободно");
        }
    }
}
