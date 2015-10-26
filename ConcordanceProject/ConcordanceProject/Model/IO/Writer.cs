using System.IO;

namespace ConcordanceProject.Model.IO
{
    public class Writer
    {
        public string FileName { get; set; }

        public Writer(string fileName)
        {
            FileName = fileName;
        }

        public void Write(string result)
        {
            using (StreamWriter writer = new FileInfo(FileName).CreateText())
            {
                writer.Write(result);
                writer.Close();
            }
        }
    }
}
