using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject.Model.IOClasses
{
    public class Reader
    {
        public string FileName { get; set; }
        public Separators Separators { get; set; }

        public Reader(string fileName, Separators separators)
        {
            FileName = fileName;
            Separators = separators;
        }

        private List<string> GetParsedWords(string input)
        {
            input = input.ToLower();
            var strings = input.Split(Separators.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            return strings;
        }

        public Text Read(int senteceisCount)
        {
            if (senteceisCount <= 0)
                throw new ArgumentException("Incorrect data!");
            using (StreamReader reader = File.OpenText(FileName))
            {
                var listPages = new List<Page>();
                int pageNumber = 1, number = 0;
                Page page = new Page(pageNumber);
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    var strings = GetParsedWords(input);
                    page.Add(new Sentence(strings, ++number, pageNumber));
                    if (number % senteceisCount != 0)
                        continue;
                    listPages.Add(page);
                    page = new Page(++pageNumber);
                }
                reader.Close();
                return new Text(listPages, FileName);
            }
        }
    }
}
