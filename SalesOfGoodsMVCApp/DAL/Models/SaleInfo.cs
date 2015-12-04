using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{ 
    public class SaleInfo : BaseClass
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInfo FileInfo { get; set; }
        public int Cost { get; set; }
        [StringLength(3, ErrorMessage = "The field does not match the currency code")]
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

        public SaleInfo()
        {
            Client = new Client();
            Product = new Product();
            FileInfo = new FileInfo();
        }

        public override string ToString()
        {
            return string.Format("{0} \nClient: {1} \nProduct: {2} \nFileInfo: {3} {4} {5}", Date.ToShortDateString(),
                Client, Product, FileInfo, Cost, Currency);
        }
    }
}
