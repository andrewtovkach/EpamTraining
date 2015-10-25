using System.Collections.Generic;

namespace ConcordanceProject.Model
{
    public class Sentence : Collection<string>
    {
        public int Number { get; set; }
        public int PageNumber { get; set; }

        public Sentence(ICollection<string> collection, int number, int pageNumber) 
            : base(collection)
        {
            Number = number;
            PageNumber = pageNumber;
        }

        public override string ToString()
        {
            return "Sentence №" + Number;
        }
    }
}