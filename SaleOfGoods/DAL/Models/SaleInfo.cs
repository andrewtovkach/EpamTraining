using System;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public decimal Cost { get; set; }
        public string CurrencyCode { get; set; }

        public SaleInfo(DateTime date, Client client, Product product, decimal cost, string currencyCode)
        {
            Date = date;
            Client = client;
            Product = product;
            Cost = cost;
            CurrencyCode = currencyCode;
        }

        public SaleInfo(int id, DateTime date, Client client, Product product, decimal cost, string currencyCode)
            : this(date, client, product, cost, currencyCode)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id + " " + Date.ToShortDateString() + " " + Client + " " + Product + " " + Cost + " " + CurrencyCode;
        }
    }
}
