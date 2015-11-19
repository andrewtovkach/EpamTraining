namespace DAL.Models
{
    public class Client
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + SecondName;
        }
    }
}
