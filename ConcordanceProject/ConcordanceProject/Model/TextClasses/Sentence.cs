using System.Collections.Generic;
using System.Text;

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

        public string GetResultString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Collection)
                sb.Append(item.ToUpper() + " ");
            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Format("Page №{0} Sentence №{1}", PageNumber, Number);
        }
    }
}