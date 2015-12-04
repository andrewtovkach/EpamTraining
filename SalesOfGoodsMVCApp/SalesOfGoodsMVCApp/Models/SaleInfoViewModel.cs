using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;

namespace SalesOfGoodsMVCApp.Models
{
    public class SaleInfoViewModel
    {
        public IEnumerable<SaleInfo> SaleInfos { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Clients { get; set; }
        public SelectList Products { get; set; }
        public SelectList Dates { get; set; }
    }
}