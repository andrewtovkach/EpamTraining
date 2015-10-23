using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConcordanceProject.Model
{
    public class Concordance
    {
        public Text Text { get; set; }

        private readonly SortedDictionary<string, WordStatistics> _dictionary;

        public Concordance(Text text)
        {
            Text = text;
            _dictionary = new SortedDictionary<string, WordStatistics>();
        }

        public void CountingStatistics()
        {
            foreach (var item in Text.GetSentencies())
            {
                foreach (var it in item)
                {
                    if (_dictionary.ContainsKey(it))
                    {
                        _dictionary[it].Add(item.Number);
                        _dictionary[it].TotalCount++;
                    }
                    else
                    {
                        _dictionary.Add(it, new WordStatistics());
                        _dictionary[it].Value = it;
                        _dictionary[it].Add(item.Number);
                        _dictionary[it].TotalCount++;
                    }
                }
            }
        }

        public IEnumerable<WordStatistics> Result()
        {
            return from x in _dictionary
                   select x.Value;
        }

        public string Print()
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in Result())
                stringBuilder.AppendLine(item.ToString());
            return stringBuilder.ToString();
        }

        public bool Write(string fileName)
        {
            StreamWriter writer = null;
            try
            {
                var file = new FileInfo(fileName);
                writer = file.CreateText();
                writer.Write(Print());
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (writer != null) 
                    writer.Close();
            }
        }

        public override string ToString()
        {
            return Print();
        }
    }
}
