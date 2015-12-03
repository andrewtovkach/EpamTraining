using System.Collections.Generic;
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
            int pageSize = 3;
            IEnumerable<Product> productsPerPages = _productsRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _productsRepository.Count() };
            IndexViewModel<Product> ivm = new IndexViewModel<Product> { PageInfo = pageInfo, Elements = productsPerPages };
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = _productsRepository.FirstOrDefault(x => x.Id == id);
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
            SelectList countries = new SelectList(new CountriesRepository(), "Id", "Name");
            ViewBag.Countries = countries;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
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
            var countries = new SelectList(new CountriesRepository(), "Id", "Name", product.Country.Id);
            ViewBag.Countries = countries;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            _productsRepository.Update(product.Id, product);
            _productsRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
