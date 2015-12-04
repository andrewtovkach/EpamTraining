using System;

namespace DAL.Models
{
    public class Country : BaseClass, IEquatable<Country>
    {
        public string Name { get; set; }

        public Country(string name)
        {
            Name = name;
        }

        public Country()
        {
            
        }

        public bool Equals(Country other)
        {
            return other != null && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Country);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
