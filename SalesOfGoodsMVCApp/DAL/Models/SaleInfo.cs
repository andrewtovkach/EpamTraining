using System;

namespace DAL.Models
{ 
    public class SaleInfo : BaseClass
    {
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInfo FileInfo { get; set; }
        public int Cost { get; set; }
        public string Currency { get; set; }

        public SaleInfo(DateTime date, Client client, Product product, FileInfo fileInfo, int cost, string currency, int id = 0)
        {
            Date = date;
            Client = client;
            Product = product;
            FileInfo = fileInfo;
            Cost = cost;
            Currency = currency;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} \nClient: {2} \nProduct: {3} \nFileInfo: {4} {5} {6}", Id, Date.ToShortDateString(),
                Client, Product, FileInfo, Cost, Currency);
        }
    }
}
