using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BL.Interfaces;
using DAL.Models;

namespace BL.Model
{
    public class Parser : IParser
    {
        public IReadWriter ReadWriter { get; private set; }

        public Parser(IReadWriter readWriter)
        {
            ReadWriter = readWriter;
        }

        private static SaleInfo ParseDataRecord(DataRecord dataRecord)
        {
            var names = dataRecord.Client.Split(' ');
            Client client = null;
            if (names.Length > 1)
                client = new Client(names[0], names[1]);
            return new SaleInfo(dataRecord.Date, client, new Product(dataRecord.Product), dataRecord.Cost);    
        }

        private static IList<SaleInfo> ParseDataRecodsList(IEnumerable<DataRecord> dataRecords)
        {
            return dataRecords.Select(ParseDataRecord).ToList();
        }

        private Tuple<string, DateTime> ParseFileName()
        {
            var fileName = System.IO.Path.GetFileName(ReadWriter.FilePath);
            if (fileName == null) 
                return null;
            var values = fileName.Split('_');
            var date = DateTime.ParseExact(values[1].Substring(0, 8), "ddMMyyyy", Thread.CurrentThread.CurrentCulture);
            return new Tuple<string, DateTime>(values[0], date);
        }

        public FileInfo ParseFile()
        {
            var tuple = ParseFileName();
            var dataRecords = ReadWriter.Read();
            return new FileInfo(new Manager(tuple.Item1), tuple.Item2, ParseDataRecodsList(dataRecords));
        }
    }
}
