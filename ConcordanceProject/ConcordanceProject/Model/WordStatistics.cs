﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject.Model
{
    public class WordStatistics : ICollection<Tuple<int, int>>, IComparable<WordStatistics>
    {
        public Word Word { get; set; }
        public uint TotalCount { get; set; }

        private readonly ISet<Tuple<int, int>> _set;

        public WordStatistics()
        {
            _set = new SortedSet<Tuple<int, int>>();
        }

        public WordStatistics(Word word, uint totalCount)
            : this()
        {
            Word = word;
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

        private string GetFormattedString(int width = 35)
        {
            return Word.ToString().PadRight(width - TotalCount.ToString().Length, '.');
        }

        private string GetResultString(IEnumerable<int> enumerable, int width = 35)
        {
            if (width <= 0)
                throw new ArgumentException("Incorrect data!");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} {1}: ", GetFormattedString(width), TotalCount);
            foreach (var it in enumerable)
                stringBuilder.AppendFormat("{0} ", it);
            return stringBuilder.ToString();
        }

        public string GetResultStringSentences(int width = 35)
        {
            var query = from item in _set
                    select item.Item1;
            return GetResultString(query, width);
        }

        public string GetResultStringPages(int width = 35)
        {
            var query = (from item in _set
                    select item.Item2).Distinct();
            return GetResultString(query, width);
        }

        public override string ToString()
        {
            return string.Format("{0}, Total Count - {1}", Word, TotalCount);
        }

        public int CompareTo(WordStatistics other)
        {
            return Word.CompareTo(other.Word);
        }
    }
}
