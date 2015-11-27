using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Linq;
using System.Text;

namespace BL
{
    public class ReadWriter
    {
        public string FilePath { get; set; }

        public ReadWriter(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(IEnumerable<DataRecord> list)
        {
            using (var streamWriter = new StreamWriter(FilePath, false, Encoding.Default))
            {
                var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteHeader<DataRecord>();
                foreach (var item in list)
                {
                    csvWriter.WriteRecord(item);
                }
            }
        }

        public IEnumerable<DataRecord> Read()
        {
            using (var streamReader = new StreamReader(FilePath, Encoding.Default))
            {
                return new CsvReader(streamReader).GetRecords<DataRecord>().ToList();
            }
        }
    }
}
