using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BLL;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class CountriesController : Controller
    {
        readonly CountriesRepository _countriesRepository = new CountriesRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var countriesPerPages = _countriesRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _countriesRepository.Count()
            };
            return View(new IndexViewModel<Country> { PageInfo = pageInfo, Elements = countriesPerPages });
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _countriesRepository.FirstOrDefault(x => x.Id == id);
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
            if (ModelState.IsValid)
            {
                _countriesRepository.Add(country);
                _countriesRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
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
            if (!ModelState.IsValid)
                return View(country);
            _countriesRepository.Update(country.Id, country);
            _countriesRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
