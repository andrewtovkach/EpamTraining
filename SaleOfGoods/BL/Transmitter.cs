using DAL.Models;

namespace BL
{
    public class Transmitter
    {
        public DataRecord DataRecord { get; set; }

        public Transmitter(DataRecord dataRecord)
        {
            DataRecord = dataRecord;
        }

        public SaleInfo CreateSaleInfo()
        {
            Product product = new Product(DataRecord.Product);
            string[] names = DataRecord.Client.Split(' ');
            Client client = null;
            if(names.Length > 1)
            {
                client = new Client(names[0], names[1]);
            }
            string[] values = DataRecord.Cost.Split(' ');
            decimal cost = 0;
            string currencyCode = null;
            if (values.Length > 1)
            {
                cost = decimal.Parse(values[0]);
                currencyCode = values[1];
            }
            return new SaleInfo(DataRecord.Date, client, product, cost, currencyCode);
        }
    }
}
