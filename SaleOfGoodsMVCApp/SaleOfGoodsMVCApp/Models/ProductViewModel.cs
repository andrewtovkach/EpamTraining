using System.Collections.Generic;
using DAL.Models;
using System.Web.Mvc;

namespace BLL
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Countries { get; set; }
    }
}