using System.Collections.Generic;

namespace ConcordanceProject.Model.IO
{
    public class Separators: Collection<char>
    {
        public Separators(ICollection<char> collection)
            : base(collection)
        {
        }

        public Separators(params char [] collection) 
        {
            foreach (var item in collection)
                List.Add(item);
        }
    }
}
