using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConcordanceProject.Model
{
    public class SubjectIndex
    {
        public Concordance Concordance { get; set; }

        public SubjectIndex(Concordance concordance)
        {
            Concordance = concordance;
        }

        public IEnumerable<IGrouping<char, WordStatistics>> Result()
        {
            return from item in Concordance.Result()
                   group item by item.Value[0];
        }

        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var group in Result())
            {
                stringBuilder.AppendLine(Char.ToUpper(group.Key).ToString());
                foreach (var item in group)
                    stringBuilder.AppendLine(item.ToString());
                stringBuilder.AppendLine();
            }
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
