using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IOClasses;
using ConcordanceProject.Model.TextClasses;

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
                        _dictionary[word].Word = new Word(it);
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

        public string Print(int width = 35)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in GetResult())
                stringBuilder.AppendLine(item.Print(item.GetSentencies(), width));
            return stringBuilder.ToString();
        }

        public bool Write(string fileName, int width = 35)
        {
            try
            {
                Writer writer = new Writer(fileName);
                writer.Write(Print(width));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
