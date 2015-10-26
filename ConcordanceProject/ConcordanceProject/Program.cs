using System;
using ConcordanceProject.Model;
using ConcordanceProject.Model.IOClasses;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader(@"Files\Input.txt", new Separators(' ', ',', ';', '.', '!', '?', ':', '-', '—', '"', '\'', '/'));
            Text text = reader.Read(5);
            Concordance concordance = new Concordance(text);
            Console.WriteLine(concordance.Print());
            Console.WriteLine(concordance.Write(@"Files\OutputConcordance.txt")
                ? "Writing file is successful!" : "Writing file is falid!");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.Print());
            Console.WriteLine(subjectIndex.Write(@"Files\OutputSubjectIndex.txt")
                ? "Writing file is successful!" : "Writing file is failed!");
            Console.ReadKey();
        }
    }
}
