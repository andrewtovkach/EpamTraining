using System;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInfo FileInfo { get; set; }
        public int Cost { get; set; }
        public string Currency { get; set; }

        public SaleInfo(DateTime date, Client client, Product product, FileInfo fileInfo, int cost, string currency)
        {
            Date = date;
            Client = client;
            Product = product;
            FileInfo = fileInfo;
            Cost = cost;
            Currency = currency;
        }

        public SaleInfo()
        {
            
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} Client: {2} Product: {3} \n FileInfo: {4} {5} {6}", Id, Date, Client, 
                Product, FileInfo, Cost, Currency);
        }
    }
}
