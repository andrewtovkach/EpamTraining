﻿using System.Collections.Generic;
using System.Web.Mvc;
using BLL.DTO;

namespace SaleOfGoodsMVCApp.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Countries { get; set; }
    }
}