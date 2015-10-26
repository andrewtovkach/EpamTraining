using System.IO;

namespace ConcordanceProject.Model.IOClasses
{
    public class Writer
    {
        public string FileName { get; set; }

        public Writer(string fileName)
        {
            FileName = fileName;
        }

        public void Write(string value)
        {
            using (StreamWriter writer = new FileInfo(FileName).CreateText())
            {
                writer.Write(value);
                writer.Close();
            }
        }
    }
}
