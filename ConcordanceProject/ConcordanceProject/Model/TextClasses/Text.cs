using System;
using System.Collections.Generic;
using System.Linq;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model.TextClasses
{
    public class Text : CollectionElement<Page>, IText
    {
        public string Title { get; set; }

        public Text(ICollection<Page> collection, string title)
            : base(collection)
        {
            Title = title;
        }

        public IEnumerable<Sentence> GetSentences()
        {
            return Collection.SelectMany(item => item);
        }

        public IEnumerable<Sentence> GetSentences(Func<Sentence, bool> func)
        {
            return Collection.SelectMany(item => item).Where(func);
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
            return Title;
        }
    }
}
