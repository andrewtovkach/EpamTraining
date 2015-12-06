using System;
using System.Collections.Generic;
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

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetCountriesPerPages(page));
        }

        public ActionResult List(int page = 1)
        {
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = _countriesRepository.Count()
            };
            return View(new IndexViewModel<Country> { PageInfo = pageInfo, Elements = GetCountriesPerPages(page) });
        }

        private IEnumerable<Country> GetCountriesPerPages(int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return _countriesRepository.Skip((page - 1) * pageSize).Take(pageSize);
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
            if (ModelState.IsValid)
            {
                _countriesRepository.Update(country.Id, country);
                _countriesRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
