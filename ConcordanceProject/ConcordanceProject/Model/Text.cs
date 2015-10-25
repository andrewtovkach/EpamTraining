using System.Collections.Generic;
using System.Linq;

namespace ConcordanceProject.Model
{
    public class Text : Collection<Page>
    {
        public string FileName { get; set; }

        public Text(ICollection<Page> collection, string fileName)
            :base(collection)
        {
            FileName = fileName;
        }

        public IEnumerable<Sentence> GetSentencies()
        {
            return List.SelectMany(item => item);
        }

        public override string ToString()
        {
            return "Text " + FileName;
        }
    }
}
