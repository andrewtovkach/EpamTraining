using System;

namespace DAL.Models
{
    public class SaleInfo
    {
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public decimal Cost { get; set; }
        public string CurrencyCode { get; set; }

        public override string ToString()
        {
            return Date.ToShortDateString() + " " + Client + " " + Product + " " + Cost + " " + CurrencyCode;
        }
    }
}
