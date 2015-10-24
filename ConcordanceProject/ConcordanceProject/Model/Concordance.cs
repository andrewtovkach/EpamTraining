using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IO;

namespace ConcordanceProject.Model
{
    public class Concordance : IEnumerable<WordStatistics>, IResult
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
                    if (_dictionary.ContainsKey(word))
                    {
                        _dictionary[word].Add(item.Number);
                        _dictionary[word].TotalCount++;
                    }
                    else
                    {
                        _dictionary.Add(word, new WordStatistics());
                        _dictionary[word].Value = new Word(it);
                        _dictionary[word].Add(item.Number);
                        _dictionary[word].TotalCount++;
                    }
                }
            }
        }

        private IEnumerable<WordStatistics> GetResult()
        {
            return from x in _dictionary
                   select x.Value;
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in this)
                stringBuilder.AppendLine(item.ToString());
            return stringBuilder.ToString();
        }

        public bool Write(string fileName)
        {
            Writer writer = new Writer(fileName);
            return writer.Write(Print);
        }

        public override string ToString()
        {
            return Print();
        }

        public IEnumerator<WordStatistics> GetEnumerator()
        {
            return GetResult().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
