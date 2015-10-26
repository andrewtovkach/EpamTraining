using System;
using ConcordanceProject.Model;
using ConcordanceProject.Model.IO;

namespace ConcordanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader(@"Files\Input.txt", new Separators(' ', ',', ';', '.', '!', '?', ':', '-', '—', '"', '\'', '/'));
            Text text = reader.Read(3);
            Concordance concordance = new Concordance(text);
            Console.WriteLine(concordance.Print(35));
            Console.WriteLine(concordance.Write(@"Files\OutputConcordance.txt", 35)
                ? "Writing file is successful!" : "Writing file is falid!");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.Print(35));
            Console.WriteLine(subjectIndex.Write(@"Files\OutputSubjectIndex.txt", 35)
                ? "Writing file is successful!" : "Writing file is failed!");
            Console.ReadKey();
        }
    }
}
