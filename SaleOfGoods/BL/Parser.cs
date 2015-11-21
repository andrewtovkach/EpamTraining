using System;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Linq;

namespace BL
{
    public class Parser
    {
        public string FilePath { get; set; }

        public Parser(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(IEnumerable<DataRecord> list)
        {
            using (TextWriter sw = new StreamWriter(FilePath))
            {
                var writer = new CsvWriter(sw);
                writer.WriteHeader<DataRecord>();
                foreach (var item in list)
                {
                    writer.WriteRecord(item);
                }
            }
        }

        public IEnumerable<DataRecord> Read()
        {
            IEnumerable<DataRecord> records = null;
            using (TextReader sr = new StreamReader(FilePath))
            {
                var reader = new CsvReader(sr);
                records = reader.GetRecords<DataRecord>().ToList();
            }
            return records;
        }

        public Tuple<string, DateTime> ParseFileName()
        {
            string fileName = Path.GetFileName(FilePath);
            if (fileName != null)
            {
                string[] values = fileName.Split('_');
                int date = int.Parse(values[1].Substring(0, 2)), month = int.Parse(values[1].Substring(2, 2)),
                    year = int.Parse(values[1].Substring(4, 4));
                return new Tuple<string, DateTime>(values[0], new DateTime(year, month, date));
            }
            return null;
        }
    }
}
