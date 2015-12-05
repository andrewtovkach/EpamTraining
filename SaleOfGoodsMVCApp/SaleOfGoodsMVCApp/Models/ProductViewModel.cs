using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;

namespace SalesOfGoodsMVCApp.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Countries { get; set; }
    }
}