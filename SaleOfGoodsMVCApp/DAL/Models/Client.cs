using System;

namespace DAL.Models
{
    public class Client : BaseClass, IEquatable<Client>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }

        public Client()
        {
            
        }

        public Client(string firstName, string secondName, DateTime birthDay)
        {
            FirstName = firstName;
            SecondName = secondName;
            BirthDay = birthDay;
        }

        public bool Equals(Client other)
        {
            return other != null && FirstName == other.FirstName && SecondName == other.SecondName && BirthDay == other.BirthDay;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Client);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FirstName, SecondName, BirthDay.ToShortDateString());
        }
    }
}
