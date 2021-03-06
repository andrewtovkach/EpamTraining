﻿using System;

namespace ConcordanceProject.Model.TextClasses
{
    public struct Word : IComparable<Word>
    {
        public string Value { get; set; }

        public Word(string value)
            : this()
        {
            Value = value;
        }

        public char[] ToCharArray()
        {
            return Value.ToCharArray();
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(Word other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }
}
