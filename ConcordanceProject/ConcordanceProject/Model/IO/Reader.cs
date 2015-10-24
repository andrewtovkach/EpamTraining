using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model.IO
{
    public class Reader : IReader
    {
        public string FileName { get; set; }
        public Separators Separators { get; set; }

        public Reader(string fileName, Separators separators)
        {
            FileName = fileName;
            Separators = separators;
        }

        public Text Read(int senteceisCount)
        {
            StreamReader reader = null;
            try
            {
                var listPages = new List<Page>();
                reader = File.OpenText(FileName);
                int count = 1, number = 0;
                Page page = new Page(count);
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    input = input.ToLower();
                    var strings = input.Split(Separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    page.Add(new Sentence(strings, ++number));
                    if (number % senteceisCount != 0)
                        continue;
                    listPages.Add(page);
                    page = new Page(++count);
                }
                return new Text(listPages);
            }
            catch
            {
                return new Text();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
