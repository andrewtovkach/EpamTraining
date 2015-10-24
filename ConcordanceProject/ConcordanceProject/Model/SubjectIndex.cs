using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IO;

namespace ConcordanceProject.Model
{
    public class SubjectIndex : IEnumerable<IGrouping<char, WordStatistics>>, IResult
    {
        public Concordance Concordance { get; set; }

        public SubjectIndex(Concordance concordance)
        {
            Concordance = concordance;
        }

        private IEnumerable<IGrouping<char, WordStatistics>> GetResult()
        {
            return from item in Concordance
                   group item by item.Value.ToString()[0];
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var group in this)
            {
                stringBuilder.AppendLine(char.ToUpper(group.Key).ToString());
                foreach (var item in group)
                    stringBuilder.AppendLine(item.ToString());
                stringBuilder.AppendLine();
            }
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

        public IEnumerator<IGrouping<char, WordStatistics>> GetEnumerator()
        {
            return GetResult().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
