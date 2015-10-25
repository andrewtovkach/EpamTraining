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

        public bool Write(string result)
        {
            StreamWriter writer = null;
            try
            {
                FileInfo file = new FileInfo(FileName);
                writer = file.CreateText();
                writer.Write(result);
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
