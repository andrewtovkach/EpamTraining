using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BLL;
using BLL.DTO;
using BLL.Interfaces;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class CountriesController : Controller
    {
        readonly IElementsService _elementsService = new ElementsService();

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
                TotalItems = _elementsService.CountriesItems.Count()
            };
            return View(new IndexViewModel<Country> { PageInfo = pageInfo, Elements = GetCountriesPerPages(page) });
        }

        private IEnumerable<Country> GetCountriesPerPages(int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return _elementsService.CountriesItems.OrderBy(item => item.Name).Skip((page - 1) * pageSize).Take(pageSize);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _elementsService.CountriesItems.FirstOrDefault(x => x.Id == id);
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCountry(int id)
        {
            _elementsService.RemoveCountry(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Add(country);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _elementsService.CountriesItems.FirstOrDefault(x => x.Id == id);
            return View(country);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Update(country.Id, country);
            }
            return RedirectToAction("List");
        }
    }
}
