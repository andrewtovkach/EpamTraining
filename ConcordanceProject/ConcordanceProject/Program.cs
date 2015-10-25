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
            Console.WriteLine(concordance.Print());
            concordance.Write(@"Files\OutputConcordance.txt");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.Print());
            subjectIndex.Write(@"Files\OutputSubjectIndex.txt");
            Console.ReadKey();
        }
    }
}
