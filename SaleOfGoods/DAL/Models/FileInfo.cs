using System;

namespace DAL.Models
{
    public class FileInfo
    {
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }
        public SaleInfo SaleInfo { get; set; }

        public override string ToString()
        {
            return Manager + " " + Date.ToShortDateString() + " " + SaleInfo;
        }
    }
}
