using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IOClasses;

namespace ConcordanceProject.Model
{
    public class SubjectIndex : IEnumerable<IGrouping<char, WordStatistics>>, IResult<IGrouping<char, WordStatistics>>
    {
        public Concordance Concordance { get; set; }

        public SubjectIndex(Concordance concordance)
        {
            Concordance = concordance;
        }

        public IEnumerable<IGrouping<char, WordStatistics>> GetResult()
        {
            return from item in Concordance
                   group item by char.ToUpper(item.Word.ToString()[0]);
        }

        public string Print(int width = 35)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var group in GetResult())
            {
                stringBuilder.AppendLine(group.Key.ToString());
                foreach (var item in group)
                    stringBuilder.AppendLine(item.Print(item.GetPages(), width));
                stringBuilder.AppendLine();
            }
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
