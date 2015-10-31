using System;
using System.Configuration;
using System.IO;
using System.Linq;
using ConcordanceProject.Model;
using ConcordanceProject.Model.Interfaces;
using ConcordanceProject.Model.IOClasses;

namespace ConcordanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = ConfigurationManager.AppSettings["InputFileName"];
            var separators = ConfigurationManager.AppSettings["Separators"].ToList();
            Reader reader = new Reader(new StreamReader(fileName), separators);
            IText text = reader.Read(5);
            IConcordance concordance = new Concordance(text);
            Console.WriteLine(concordance.GetResultString());
            fileName = ConfigurationManager.AppSettings["ConcordanceFileName"];
            Console.WriteLine(concordance.Write(new StreamWriter(fileName))
                ? "Writing file is successful!" : "Writing file is falid!");
            ISubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.GetResultString());
            fileName = ConfigurationManager.AppSettings["SubjectIndexFileName"];
            Console.WriteLine(subjectIndex.Write(new FileInfo(fileName).CreateText())
                ? "Writing file is successful!" : "Writing file is failed!");
            Console.ReadKey();
        }
    }
}
