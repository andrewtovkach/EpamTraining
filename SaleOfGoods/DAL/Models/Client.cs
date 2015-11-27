namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public Client(string firstName, string secondName, int id = 0)
        {
            FirstName = firstName;
            SecondName = secondName;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} {2}", Id, FirstName, SecondName);
        }
    }
}
