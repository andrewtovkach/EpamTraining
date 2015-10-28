using System;
using System.IO;
using ConcordanceProject.Model;
using ConcordanceProject.Model.IOClasses;
using ConcordanceProject.Model.TextClasses;

namespace ConcordanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader(File.OpenText(@"Files\Input.txt"), new Separators(' ', ',', ';', '.', '!', '?', ':', '-', '—', '"', '\'', '/'));
            Text text = reader.Read(5);
            Concordance concordance = new Concordance(text);
            Console.WriteLine(concordance.GetResultString());
            Console.WriteLine(concordance.Write(new FileInfo(@"Files\OutputConcordance.txt").CreateText())
                ? "Writing file is successful!" : "Writing file is falid!");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.GetResultString());
            Console.WriteLine(subjectIndex.Write(new FileInfo(@"Files\OutputSubjectIndex.txt").CreateText())
                ? "Writing file is successful!" : "Writing file is failed!");
            Console.ReadKey();
        }
    }
}
