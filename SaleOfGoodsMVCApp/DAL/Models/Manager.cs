using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Manager : BaseClass, IEquatable<Manager>
    {
        [Required(ErrorMessage = "Field must be set")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        public string SecondName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+\d{3}\(\d{2,4}\)\d{1,3}-\d{2}-\d{2}$", ErrorMessage = "Incorrect telephone number")]
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect e-mail")]
        public string Email { get; set; }

        public Manager(string firstName, string secondName, string telephone, string email)
        {
            FirstName = firstName;
            SecondName = secondName;
            Telephone = telephone;
            Email = email;
        }

        public Manager()
        {

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
