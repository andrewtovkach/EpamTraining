﻿namespace ConcordanceProject.Model.TextClasses
{
    public class Page : CollectionElement<Sentence>
    {
        public int Number { get; set; }

        public Page(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return string.Format("Page №{0}", Number);
        }
    }
}
