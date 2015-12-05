using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class SaleInfo : BaseClass
    {
        [Required(ErrorMessage = "Field must be set")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInfo FileInfo { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [CostValidation(ErrorMessage = "Incorrect cost")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(3, ErrorMessage = "Incorrect currency code")]
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
