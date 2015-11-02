using System;
using System.Collections.Generic;
using System.Linq;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model.TextClasses
{
    public class Text : CollectionElement<Page>, IText
    {
        public Text(ICollection<Page> collection)
            : base(collection)
        {
        }

        public string Title
        {
            get { return GetSentences().First().GetResultString(); }
        }

        public IEnumerable<Sentence> GetSentences()
        {
            return Collection.SelectMany(item => item);
        }

        public IEnumerable<Sentence> GetSentences(Func<Sentence, bool> predicate)
        {
            return Collection.SelectMany(item => item).Where(predicate);
        }

        public int SentencesCount
        {
            get { return Collection.Sum(item => item.Count); }
        }

        public IOrderedEnumerable<Sentence> GetSortedSentences(bool desc = false)
        {
            return desc ? GetSentences().OrderByDescending(item => item.Number) : GetSentences().OrderByDescending(item => item.Number);
        }

        public override string ToString()
        {
            return string.Format("Title - {0}", Title);
        }
    }
}
