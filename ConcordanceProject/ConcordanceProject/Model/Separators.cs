using System.Linq;

namespace ConcordanceProject.Model
{
    public class Separators
    {
        public char[] Collection { get; set; }

        public Separators(char[] collection)
        {
            Collection = collection;
        }

        public Separators()
        {
            Collection = new []
            {
                ' ', ',', ';', '.', '!', '?', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '='
            };
        }

        public override string ToString()
        {
            return Collection.Select(item => item.ToString()).ToString();
        }
    }
}
