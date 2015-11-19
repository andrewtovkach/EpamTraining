namespace DAL.Models
{
    public class Manager
    {
        public string SecondName { get; set; }

        public override string ToString()
        {
            return SecondName;
        }
    }
}
