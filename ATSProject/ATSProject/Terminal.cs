namespace ATSProject
{
    public class Terminal
    {
        public string Numer { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public Terminal(string number, string phoneNumber)
        {
            Numer = number;
            PhoneNumber = new PhoneNumber{ Number = phoneNumber };
        }

        public override string ToString()
        {
            return Numer + " " + PhoneNumber;
        }
    }
}
