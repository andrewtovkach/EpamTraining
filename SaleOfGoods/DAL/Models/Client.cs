namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public Client(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public Client(int id, string firstName, string secondName) 
            : this(firstName, secondName)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id + " " + FirstName + " " + SecondName;
        }
    }
}
