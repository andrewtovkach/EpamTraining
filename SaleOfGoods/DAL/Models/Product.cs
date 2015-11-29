namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(string name, int id = 0)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Name);
        }
    }
}
