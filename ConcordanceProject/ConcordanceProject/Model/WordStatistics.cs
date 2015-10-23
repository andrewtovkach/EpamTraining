using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConcordanceProject.Model
{
    public class WordStatistics : ICollection<int>, IComparable<WordStatistics>
    {
        public string Value { get; set; }
        public int TotalCount { get; set; }

        private readonly SortedSet<int> _set;

        public WordStatistics()
        {
            _set = new SortedSet<int>();
        }

        public WordStatistics(string value, int totalCount)
            : this()
        {
            Value = value;
            TotalCount = totalCount;
        }

        public void Add(int item)
        {
            _set.Add(item);
        }

        public void Clear()
        {
            _set.Clear();
        }

        public bool Contains(int item)
        {
            return _set.Contains(item);
        }

        public void CopyTo(int[] array, int arrayIndex)
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

        public bool Remove(int item)
        {
            return _set.Remove(item);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} {1}: ", Value.PadRight(25 - TotalCount.ToString().Length, '.'),
                TotalCount);
            foreach (var it in this)
                stringBuilder.AppendFormat("{0} ", it);
            return stringBuilder.ToString();
        }

        public int CompareTo(WordStatistics other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}
