using System;
using System.Text.RegularExpressions;

namespace ATSProject
{
    public struct PhoneNumber
    {
        private const string Pattern = @"^\+\d{3}\(\d{2,4}\)\d{1,3}-\d{2}-\d{2}$";

        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                if (!IsCorrectNumber(value))
                    throw new ArgumentException("Incorrect telephone number!");
                _number = value;
            }
        }

        public static bool IsCorrectNumber(string number)
        {
            var regex = new Regex(Pattern);
            return regex.IsMatch(number);
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
