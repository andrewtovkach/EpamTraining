using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        readonly ProductsRepository _productsRepository = new ProductsRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var productsPerPages = _productsRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _productsRepository.Count()
            };
            return View(new IndexViewModel<Product> { PageInfo = pageInfo, Elements = productsPerPages });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var product = _productsRepository.FirstOrDefault(x => x.Id == id);
            if (product == null) 
                return RedirectToAction("List");
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
