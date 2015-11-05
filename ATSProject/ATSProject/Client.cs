﻿using System;

namespace ATSProject
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }

        public Client(string firstName, string lastName, string birthDay, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDay = DateTime.Parse(birthDay);
            Address = address; 
        }

        public override string ToString()
        {
            return string.Format("{0} {1} Birth Day: {2} Address: {3}", FirstName, LastName, BirthDay.ToShortDateString(), Address);
        }
    }
}
