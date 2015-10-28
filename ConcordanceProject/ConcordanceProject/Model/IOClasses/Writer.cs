using System.IO;

namespace ConcordanceProject.Model.IOClasses
{
    public class Writer
    {
        public TextWriter TextWriter { get; set; }

        public Writer(TextWriter textWriter)
        {
            TextWriter = textWriter;
        }

        public void Write(string value)
        {
            using (TextWriter)
            {
                TextWriter.Write(value);
                TextWriter.Close();
            }
        }
    }
}
