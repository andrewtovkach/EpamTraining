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
    }
}
