using System;

namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
