using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject.Model.IOClasses
{
    public class Reader
    {
        public TextReader TextReader { get; set; } 
        public ICollection<char> Separators { get; set; }

        public Reader(TextReader textReader, ICollection<char> separators)
        {
            TextReader = textReader;
            Separators = separators;
        }

        private IList<string> GetParsedWords(string input)
        {
            input = input.ToLower();
            var strings = input.Split(Separators.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            return strings;
        }

        public Text Read(int sentecesCount)
        {
            if (sentecesCount <= 0)
                throw new ArgumentException("Incorrect data!");
            using (TextReader)
            {
                var listPages = new List<Page>();
                int pageNumber = 1, number = 0;
                Page page = new Page(pageNumber);
                string input;
                while ((input = TextReader.ReadLine()) != null)
                {
                    var strings = GetParsedWords(input);
                    page.Add(new Sentence(strings, ++number, pageNumber));
                    if (number % sentecesCount != 0)
                        continue;
                    listPages.Add(page);
                    page = new Page(++pageNumber);
                }
                TextReader.Close();
                return new Text(listPages);
            }
        }
    }
}
