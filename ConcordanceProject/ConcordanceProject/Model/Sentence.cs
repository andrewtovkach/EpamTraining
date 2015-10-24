using System.Collections.Generic;

namespace ConcordanceProject.Model
{
    public class Sentence : Collection<string>
    {
        public int Number { get; set; }

        public Sentence(ICollection<string> collection, int number) 
            : base(collection)
        {
            Number = number;
        }

        public override string ToString()
        {
            return "Sentence №" + Number;
        }
    }
}