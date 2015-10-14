using System;

namespace Transport.Model
{
    class Place
    {
        public int Number { get; set; }
        public bool IsBusy { get; set; }

        public override string ToString()
        {
            return String.Format("Место №{0} {1}", Number, IsBusy ? "занято" : "свободно");
        }
    }
}
