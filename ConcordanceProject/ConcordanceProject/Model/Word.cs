using System;

namespace ConcordanceProject.Model
{
    public struct Word : IComparable<Word>
    {
        public string Value { get; set; }

        public Word(string value)
            : this()
        {
            Value = value;
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
