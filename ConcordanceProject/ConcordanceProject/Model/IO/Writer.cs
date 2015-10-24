using System;
using System.IO;
using ConcordanceProject.Model.Interfaces;

namespace ConcordanceProject.Model.IO
{
    public class Writer : IWriter
    {
        public string FileName { get; set; }

        public Writer(string fileName)
        {
            FileName = fileName;
        }

        public bool Write(Func<string> function)
        {
            StreamWriter writer = null;
            try
            {
                FileInfo file = new FileInfo(FileName);
                writer = file.CreateText();
                writer.Write(function());
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
