using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IO;

namespace ConcordanceProject.Model
{
    public class Concordance : IEnumerable<WordStatistics>, IResult<WordStatistics>
    {
        public Text Text { get; set; }

        private readonly SortedDictionary<Word, WordStatistics> _dictionary;

        public Concordance(Text text)
        {
            Text = text;
            _dictionary = new SortedDictionary<Word, WordStatistics>();
            CountingStatistics();
        }

        private void CountingStatistics()
        {
            foreach (var item in Text.GetSentencies())
            {
                foreach (var it in item)
                {
                    Word word = new Word(it);
                    if (!_dictionary.ContainsKey(word))
                    {
                        _dictionary.Add(word, new WordStatistics());
                        _dictionary[word].Value = new Word(it);
                    }
                    _dictionary[word].Add(new Tuple<int, int>(item.Number, item.PageNumber));
                    _dictionary[word].TotalCount++;
                }
            }
        }

        public IEnumerable<WordStatistics> GetResult()
        {
            return from item in _dictionary
                   select item.Value;
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in GetResult())
                stringBuilder.AppendLine(item.ToString());
            return stringBuilder.ToString();
        }

        public bool Write(string fileName)
        {
            Writer writer = new Writer(fileName);
            return writer.Write(Print());
        }

        public override string ToString()
        {
            return Print();
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
