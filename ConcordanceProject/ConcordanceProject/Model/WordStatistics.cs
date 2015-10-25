using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcordanceProject.Model
{
    public class WordStatistics : ICollection<Tuple<int, int>>, IComparable<WordStatistics>
    {
        public Word Value { get; set; }
        public int TotalCount { get; set; }

        private readonly SortedSet<Tuple<int, int>> _set;

        public WordStatistics()
        {
            _set = new SortedSet<Tuple<int, int>>();
        }

        public WordStatistics(Word value, int totalCount)
            : this()
        {
            Value = value;
            TotalCount = totalCount;
        }

        public void Add(Tuple<int, int> item)
        {
            _set.Add(item);
        }

        public void Clear()
        {
            _set.Clear();
        }

        public bool Contains(Tuple<int, int> item)
        {
            return _set.Contains(item);
        }

        public void CopyTo(Tuple<int, int>[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int Count
        {
            get { return _set.Count; }
        }

        public bool Remove(Tuple<int, int> item)
        {
            return _set.Remove(item);
        }

        public IEnumerator<Tuple<int, int>> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<int> GetSentencies()
        {
            return (from item in _set
                    select item.Item1).Distinct();
        }

        public IEnumerable<int> GetPages()
        {
            return (from item in _set
                    select item.Item2).Distinct();
        }

        public string Print(IEnumerable<int> enumerable, int width)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} {1}: ", Value.ToString().PadRight(width - TotalCount.ToString().Length, '.'),
                TotalCount);
            foreach (var it in enumerable)
                stringBuilder.AppendFormat("{0} ", it);
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return Print(GetSentencies(), 35);
        }

        public int CompareTo(WordStatistics other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
