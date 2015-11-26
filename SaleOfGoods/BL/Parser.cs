using System;
using System.Threading;
using DAL.Models;

namespace BL
{
    public static class Parser
    {
        private static SaleInfo ParseSaleInfo(DataRecord dataRecord)
        {
            var product = new Product(dataRecord.Product);
            var names = dataRecord.Client.Split(' ');
            Client client = null;
            if (names.Length > 1)
                client = new Client(names[0], names[1]);
            decimal cost = decimal.Parse(dataRecord.Cost);
            return new SaleInfo(dataRecord.Date, client, product, cost);
        }

        private static Tuple<string, DateTime> ParseFileName(string filePath)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            if (fileName == null) 
                return null;
            var values = fileName.Split('_');
            if (values.Length <= 1) 
                return null;
            string managerSecondName = values[0];
            string dateTime = values[1].Substring(0, 8);
            var date = DateTime.ParseExact(dateTime, "ddMMyyyy", Thread.CurrentThread.CurrentCulture);
            return new Tuple<string, DateTime>(managerSecondName, date);
        }

        public static FileInfo ParseFile(string filePath, DataRecord dataRecord)
        {
            var tuple = ParseFileName(filePath);
            return new FileInfo(new Manager(tuple.Item1), tuple.Item2, ParseSaleInfo(dataRecord));
        }
    }
}
