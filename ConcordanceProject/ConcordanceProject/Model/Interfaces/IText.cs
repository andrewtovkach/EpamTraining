using System;
using System.Collections.Generic;
using System.Linq;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject.Model.Interfaces
{
    public interface IText
    {
        IEnumerable<Sentence> GetSentences();
        IEnumerable<Sentence> GetSentences(Func<Sentence, bool> func);
        int SentencesCount { get; }
        IOrderedEnumerable<Sentence> GetSortedSentences(bool desc = false);
    }
}
