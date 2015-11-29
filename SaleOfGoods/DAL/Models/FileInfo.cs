using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Models
{
    public class FileInfo : IEnumerable<SaleInfo>
    {
        public int Id { get; set; }
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }
        public ICollection<SaleInfo> SaleInfo { get; set; }

        public FileInfo(Manager manager, DateTime date, ICollection<SaleInfo> saleInfo, int id = 0)
        {
            Manager = manager;
            Date = date;
            SaleInfo = saleInfo;
            Id = id;
        }

        public override string ToString()
        {
            var str = string.Format("{0} - Manager: {1} {2} SaleInfo: \n", Id, Manager, Date.ToShortDateString());
            return SaleInfo.Aggregate(str, (current, item) => current + (item + "\n"));
        }

        public IEnumerator<SaleInfo> GetEnumerator()
        {
            return SaleInfo.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
