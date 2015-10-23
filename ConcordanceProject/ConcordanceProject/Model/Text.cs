using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConcordanceProject.Model
{
    public class Text : Collection<Page>
    {      
        public Separators  Separators { get; set; }

        public Text(Separators separators)
        {
            Separators = separators;
        }

        public bool Read(string fileName, int senteceisCount)
        {
            StreamReader reader = null;
            try
            {
                reader = File.OpenText(fileName);
                int count = 1, number = 0;
                Page page = new Page(count);
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    input = input.ToLower();
                    var strings = input.Split(Separators.Collection, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    page.Add(new Sentence(strings, ++number));
                    if (number % senteceisCount != 0) 
                        continue;
                    List.Add(page);
                    page = new Page(++count);
                }
                return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                if (reader != null) 
                    reader.Close();
            }
        }

        public IEnumerable<Sentence> GetSentencies()
        {
            return List.SelectMany(item => item);
        }

        public override string ToString()
        {
            return Separators.ToString();
        }
    }
}
