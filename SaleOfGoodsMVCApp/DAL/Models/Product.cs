using System;

namespace DAL.Models
{
    public class Product : BaseClass, IEquatable<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }

        public Product(string name, string description, Country country, int id = 0)
        {
            Name = name;
            Description = description;
            Country = country;
            Id = id;
        }

        public bool Equals(Product other)
        {
            return other != null && Name == other.Name && Description == other.Description &&
                Country.Equals(other.Country);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} Country: {2}", Name, Description, Country);
        }
    }
}
