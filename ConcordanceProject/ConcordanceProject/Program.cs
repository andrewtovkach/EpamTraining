using System;
using ConcordanceProject.Model;

namespace ConcordanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Text text = new Text(new Separators());
            text.Read(@"Files\Input.txt", 3);
            Concordance concordance = new Concordance(text);
            concordance.CountingStatistics();
            Console.WriteLine(concordance.Print());
            concordance.Write(@"Files\OutputConcordance.txt");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.Print());
            subjectIndex.Write(@"Files\OutputSubjectIndex.txt");
            Console.ReadKey();
        }
    }
}
