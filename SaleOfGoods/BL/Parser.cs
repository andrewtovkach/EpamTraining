using System;
using System.Threading;
using DAL.Models;

namespace BL
{
    public static class Parser
    {
        private static SaleInfo ParseSaleInfo(DataRecord dataRecord)
        {
            var names = dataRecord.Client.Split(' ');
            Client client = null;
            if (names.Length > 1)
                client = new Client(names[0], names[1]);
            return new SaleInfo(dataRecord.Date, client, new Product(dataRecord.Product), dataRecord.Cost);
        }

        private static Tuple<string, DateTime> ParseFileName(string filePath)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            if (fileName == null) 
                return null;
            var values = fileName.Split('_');
            if (values.Length <= 1) 
                return null;
            var date = DateTime.ParseExact(values[1].Substring(0, 8), "ddMMyyyy", Thread.CurrentThread.CurrentCulture);
            return new Tuple<string, DateTime>(values[0], date);
        }

        public static FileInfo ParseFile(string filePath, DataRecord dataRecord)
        {
            var tuple = ParseFileName(filePath);
            return new FileInfo(new Manager(tuple.Item1), tuple.Item2, ParseSaleInfo(dataRecord));
        }
    }
}
