using System;
using System.Text.RegularExpressions;

namespace ATSProject.Model
{
    public struct PhoneNumber
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

        public static bool IsCorrectNumber(string number)
        {
            var regex = new Regex(Pattern);
            return regex.IsMatch(number);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
