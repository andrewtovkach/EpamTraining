using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BLL;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        readonly ProductsRepository _productsRepository = new ProductsRepository();

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetProductsPerPages(_productsRepository.Items, page));
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

        private IEnumerable<Product> GetProductsPerPages(IEnumerable<Product> products, int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private ProductViewModel CreateProductViewModel(int? country)
        {
            var countries = new CountriesRepository().OrderBy(item => item.Name).ToList();
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
            var products = _productsRepository.Items;
            if (country != null && country != 0)
                products = products.Where(item => item.Country.Id == country);
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var product = _productsRepository.FirstOrDefault(x => x.Id == id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProduct(int id)
        {
            _productsRepository.Remove(id);
            _productsRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(new CountriesRepository(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            product.Country = new CountriesRepository().FirstOrDefault(item => item.Id == product.Country.Id);
            _productsRepository.Add(product);
            _productsRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var product = _productsRepository.FirstOrDefault(x => x.Id == id);
            if (product != null)
                ViewBag.Countries = new SelectList(new CountriesRepository(), "Id", "Name", product.Country.Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            product.Country = new CountriesRepository().FirstOrDefault(item => item.Id == product.Country.Id);
            _productsRepository.Update(product.Id, product);
            _productsRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
