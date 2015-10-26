using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcordanceProject.Model.TextClasses
{
    public class Text : Collection<Page>
    {
        public string FileName { get; set; }

        public Text(ICollection<Page> collection, string fileName)
            : base(collection)
        {
            FileName = fileName;
        }

        public IEnumerable<Sentence> GetSentencies()
        {
            return List.SelectMany(item => item);
        }

        public IEnumerable<Sentence> GetSentencies(Func<Sentence, bool> func)
        {
            return List.SelectMany(item => item).Where(func);
        }

        public IOrderedEnumerable<Sentence> GetSortedSentencies(bool desc = false)
        {
            return desc ? GetSentencies().OrderByDescending(item => item.Number) : GetSentencies().OrderByDescending(item => item.Number);
        }

        public override string ToString()
        {
            return string.Format("Text {0}", FileName);
        }
    }
}
