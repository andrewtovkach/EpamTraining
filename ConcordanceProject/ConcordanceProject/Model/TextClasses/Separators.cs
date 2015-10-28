using System.Collections.Generic;

namespace ConcordanceProject.Model.TextClasses
{
    public class Separators: CollectionElement<char>
    {
        public Separators(ICollection<char> collection)
            : base(collection)
        {
        }

        public Separators(params char [] collection) 
        {
            foreach (var item in collection)
                Collection.Add(item);
        }
    }
}
