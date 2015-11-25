using System;
using System.IO;
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
            Client client = new Client();
            if (names.Length > 1)
                client = new Client(names[0], names[1]);
            string[] values = dataRecord.Cost.Split(' ');
            decimal cost = 0;
            string currencyCode = string.Empty;
            if (values.Length > 1)
            {
                cost = decimal.Parse(values[0]);
                currencyCode = values[1];
            }
            return new SaleInfo(dataRecord.Date, client, product, cost, currencyCode);
        }

        private static Tuple<string, DateTime> ParseFileName(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            var values = fileName.Split('_');
            if (values.Length > 1)
            {
                string managerSecondName = values[0];
                string dateTime = values[1].Substring(0, 8);
                var date = DateTime.ParseExact(dateTime, "ddMMyyyy", Thread.CurrentThread.CurrentCulture);
                return new Tuple<string, DateTime>(managerSecondName, date);
            }
            return null;
        }

        public static DAL.Models.FileInfo ParseFile(string filePath, DataRecord dataRecord)
        {
            var tuple = ParseFileName(filePath);
            return new DAL.Models.FileInfo(new Manager(tuple.Item1), tuple.Item2, ParseSaleInfo(dataRecord));
        }
    }
}
