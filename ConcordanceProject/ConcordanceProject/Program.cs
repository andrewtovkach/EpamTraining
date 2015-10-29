using System;
using System.Collections.Generic;
using System.Configuration;
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
            string fileName = ConfigurationManager.AppSettings["InputFileName"];
            char[] separators = ConfigurationManager.AppSettings["Separators"].ToCharArray();
            Reader reader = new Reader(File.OpenText(fileName), new List<char>(separators));
            Text text = reader.Read(5);
            Concordance concordance = new Concordance(text);
            Console.WriteLine(concordance.GetResultString());
            fileName = ConfigurationManager.AppSettings["ConcordanceFileName"];
            Console.WriteLine(concordance.Write(new FileInfo(fileName).CreateText())
                ? "Writing file is successful!" : "Writing file is falid!");
            SubjectIndex subjectIndex = new SubjectIndex(concordance);
            Console.WriteLine(subjectIndex.GetResultString());
            fileName = ConfigurationManager.AppSettings["SubjectIndexFileName"];
            Console.WriteLine(subjectIndex.Write(new FileInfo(fileName).CreateText())
                ? "Writing file is successful!" : "Writing file is failed!");
            Console.ReadKey();
        }
    }
}
