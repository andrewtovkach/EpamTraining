namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(string name)
        {
            Name = name;
        }

        public Product(int id, string name) 
            : this(name)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
