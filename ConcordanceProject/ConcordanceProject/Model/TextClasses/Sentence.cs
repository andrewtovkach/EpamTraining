using System.Collections.Generic;

namespace ConcordanceProject.Model.TextClasses
{
    public class Sentence : CollectionElement<string>
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
            return string.Format("PageNumber №{0}  Sentence №{1}", PageNumber, Number);
        }
    }
}