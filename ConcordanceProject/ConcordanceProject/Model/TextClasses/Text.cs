using System;
using System.Collections.Generic;
using System.Linq;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model.TextClasses
{
    public class Text : CollectionElement<Page>, ITextElement
    {
        public Text(ICollection<Page> collection)
            : base(collection)
        {
        }

        public IEnumerable<Sentence> GetSentences()
        {
            return Collection.SelectMany(item => item);
        }

        public IEnumerable<Sentence> GetSentences(Func<Sentence, bool> func)
        {
            return Collection.SelectMany(item => item).Where(func);
        }

        public IOrderedEnumerable<Sentence> GetSortedSentences(bool desc = false)
        {
            return desc ? GetSentences().OrderByDescending(item => item.Number) : GetSentences().OrderByDescending(item => item.Number);
        }
    }
}
