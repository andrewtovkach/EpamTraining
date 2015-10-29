using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IOClasses;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject.Model
{
    public class Concordance : IEnumerable<WordStatistics>, IConcordanceElement
    {
        public ITextElement Text { get; set; }

        private readonly IDictionary<Word, WordStatistics> _dictionary;

        public Concordance(ITextElement text)
        {
            Text = text;
            _dictionary = new SortedDictionary<Word, WordStatistics>();
        }

        public void CountingStatistics()
        {
            foreach (var item in Text.GetSentences())
            {
                foreach (var it in item)
                {
                    Word word = new Word(it);
                    if (!_dictionary.ContainsKey(word))
                    {
                        _dictionary.Add(word, new WordStatistics());
                        _dictionary[word].Word = new Word(it);
                    }
                    _dictionary[word].Add(new Tuple<int, int>(item.Number, item.PageNumber));
                    _dictionary[word].TotalCount++;
                }
            }
        }

        public IEnumerable<WordStatistics> GetResult()
        {
            CountingStatistics();
            return from item in _dictionary
                   select item.Value;
        }

        public string GetResultString(int width = 35)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in GetResult())
                stringBuilder.AppendLine(item.GetResultString(item.GetResultSentencies(), width));
            return stringBuilder.ToString();
        }

        public bool Write(TextWriter textWriter, int width = 35)
        {
            try
            {
                Writer writer = new Writer(textWriter);
                writer.Write(GetResultString(width));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return GetResultString();
        }

        public IEnumerator<WordStatistics> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
