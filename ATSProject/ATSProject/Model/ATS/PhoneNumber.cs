using System;
using System.Text.RegularExpressions;

namespace ATSProject.Model.ATS
{
    public struct PhoneNumber : IEquatable<PhoneNumber>
    {
        private const string Pattern = @"^\+\d{3}\(\d{2,4}\)\d{1,3}-\d{2}-\d{2}$";

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (!IsCorrectNumber(value))
                    throw new ArgumentException("Incorrect telephone number!");
                _value = value;
            }
        }

        private static bool IsCorrectNumber(string number)
        {
            var regex = new Regex(Pattern);
            return regex.IsMatch(number);
        }

        public override string ToString()
        {
            return Value;
        }

        public bool Equals(PhoneNumber other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj is PhoneNumber && Equals((PhoneNumber)obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public static bool operator == (PhoneNumber first, PhoneNumber second)
        {
            return first.Equals(second); 
        }

        public static bool operator != (PhoneNumber first, PhoneNumber second)
        {
            return !(first == second);
        }
    }
}
