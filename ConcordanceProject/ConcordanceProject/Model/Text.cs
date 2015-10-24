using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model
{
    public class Text : Collection<Page>, IPrintable
    {
        public Text()
        {
        }

        public Text(ICollection<Page> collection)
            :base(collection)
        {
        }

        public IEnumerable<Sentence> GetSentencies()
        {
            return List.SelectMany(item => item);
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in this)
            {
                stringBuilder.AppendLine(item.ToString());
                foreach (var it in item)
                    stringBuilder.AppendLine(it.ToString());
            }
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return Print();
        }
    }
}
