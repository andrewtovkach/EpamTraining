using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IOClasses;

namespace ConcordanceProject.Model
{
    public class SubjectIndex : IEnumerable<IGrouping<char, WordStatistics>>, IResultElement<IGrouping<char, WordStatistics>>
    {
        public IConcordanceElement Concordance { get; set; }

        public SubjectIndex(IConcordanceElement concordance)
        {
            Concordance = concordance;
        }

        public IEnumerable<IGrouping<char, WordStatistics>> GetResult()
        {
            return from item in Concordance.GetResult()
                   group item by char.ToUpper(item.Word.ToString()[0]);
        }

        public string GetResultString(int width = 35)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var group in GetResult())
            {
                stringBuilder.AppendLine(group.Key.ToString());
                foreach (var item in group)
                    stringBuilder.AppendLine(item.GetResultString(item.GetResultPages(), width));
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        public bool Write(TextWriter textWriter, int width = 35)
        {
            try
            {
                Writer writer = new Writer(textWriter);
                writer.Write(GetResultString(width));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return GetResultString();
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
