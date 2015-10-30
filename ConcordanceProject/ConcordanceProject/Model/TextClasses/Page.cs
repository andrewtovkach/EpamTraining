using System;
using System.Linq;

namespace ConcordanceProject.Model.TextClasses
{
    public class Page : CollectionElement<Sentence>
    {
        public int Number { get; set; }

        public Page(int number)
        {
            Number = number;
        }

        public string GetTitle()
        {
            if (Number != 1)
                throw new ArgumentException("Incorrect data!");
            return this.First().GetResultString();
        }

        public override string ToString()
        {
            return string.Format("Page №{0}", Number);
        }
    }
}
