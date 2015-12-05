using System.Collections.Generic;
using DAL.Models;
using System.Web.Mvc;

namespace BLL
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