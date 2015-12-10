using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BLL;
using BLL.DTO;
using BLL.Interfaces;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        readonly IElementsService _elementsService = new ElementsService();

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetProductsPerPages(_elementsService.ProductsItems, page));
        }

        public ActionResult List(int? country, int page = 1)
        {
            var productViewModel = CreateProductViewModel(country);
            ViewBag.ProductViewModel = productViewModel;
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = productViewModel.Products.Count()
            };
            return View(new IndexViewModel<Product> { PageInfo = pageInfo, Elements = GetProductsPerPages(productViewModel.Products, page) });
        }

        private static IEnumerable<Product> GetProductsPerPages(IEnumerable<Product> products, int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private ProductViewModel CreateProductViewModel(int? country)
        {
            var countries = _elementsService.CountriesItems.OrderBy(item => item.Name).ToList();
            countries.Insert(0, new Country { Name = "All", Id = 0 });
            ProductViewModel productViewModel = new ProductViewModel
            {
                Products = GetFilteredProducts(country),
                Countries = new SelectList(countries, "Id", "Name")
            };
            return productViewModel;
        }

        private IEnumerable<Product> GetFilteredProducts(int? country)
        {
            var products = _elementsService.ProductsItems.OrderBy(item => item.Name).AsEnumerable();
            if (country != null && country != 0)
                products = products.Where(item => item.Country.Id == country);
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var product = _elementsService.ProductsItems.FirstOrDefault(x => x.Id == id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteProduct(int id)
        {
            _elementsService.RemoveProduct(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(_elementsService.CountriesItems, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Product product)
        {
            product.Country = _elementsService.CountriesItems.FirstOrDefault(item => item.Id == product.Country.Id);
            _elementsService.Add(product);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var product = _elementsService.ProductsItems.FirstOrDefault(x => x.Id == id);
            if (product != null)
                ViewBag.Countries = new SelectList(_elementsService.CountriesItems, "Id", "Name", product.Country.Id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Product product)
        {
            product.Country = _elementsService.CountriesItems.FirstOrDefault(item => item.Id == product.Country.Id);
            _elementsService.Update(product.Id, product);
            return RedirectToAction("List");
        }
    }
}
