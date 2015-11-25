using System;

namespace DAL.Models
{
    public class FileInfo
    {
        public int Id { get; set; }
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }
        public SaleInfo SaleInfo { get; set; }

        public FileInfo(Manager manager, DateTime date, SaleInfo saleInfo)
        {
            Manager = manager;
            Date = date;
            SaleInfo = saleInfo;
        }

        public FileInfo(int id, Manager manager, DateTime date, SaleInfo saleInfo)
            : this(manager, date, saleInfo)
        {
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - Manager: {1} {2} SaleInfo: {3}", Id, Manager, Date.ToShortDateString(), SaleInfo);
        }
    }
}
