using System;

namespace DAL.Models
{
    public class Manager : BaseClass, IEquatable<Manager>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Manager()
        {
            
        }

        public Manager(string firstName, string secondName, string telephone, string email)
        {
            FirstName = firstName;
            SecondName = secondName;
            Telephone = telephone;
            Email = email;
        }

        public bool Equals(Manager other)
        {
            return other != null && FirstName == other.FirstName && SecondName == other.SecondName &&
                   Telephone == other.Telephone && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Manager);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", FirstName, SecondName, Telephone, Email);
        }
    }
}
