namespace DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string SecondName { get; set; }

        public Manager(string secondName, int id = 0) 
        {
            SecondName = secondName;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, SecondName);
        }
    }
}
