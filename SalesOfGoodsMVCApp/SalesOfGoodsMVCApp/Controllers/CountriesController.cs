using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class CountriesController : Controller
    {
        readonly CountriesRepository _countriesRepository = new CountriesRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = 3;
            IEnumerable<Country> countriesPerPages = _countriesRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _countriesRepository.Count() };
            IndexViewModel<Country> ivm = new IndexViewModel<Country> { PageInfo = pageInfo, Elements = countriesPerPages };
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Country country = _countriesRepository.FirstOrDefault(x => x.Id == id);
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCountry(int id)
        {
            _countriesRepository.Remove(id);
            _countriesRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            _countriesRepository.Add(country);
            _countriesRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _countriesRepository.FirstOrDefault(x => x.Id == id);
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(Country country)
        {
            _countriesRepository.Update(country.Id, country);
            _countriesRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
