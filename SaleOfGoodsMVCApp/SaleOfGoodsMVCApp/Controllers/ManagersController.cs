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
    public class ManagersController : Controller
    {
        readonly IElementsService _elementsService = new ElementsService();

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetManagersPerPages(page));
        }

        public ActionResult List(int page = 1)
        {
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = _elementsService.ManagersItems.Count()
            };
            return View(new IndexViewModel<Manager> { PageInfo = pageInfo, Elements = GetManagersPerPages(page) });
        }

        private IEnumerable<Manager> GetManagersPerPages(int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return _elementsService.ManagersItems.OrderBy(item => item.Name).Skip((page - 1) * pageSize).Take(pageSize);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == id);
            return View(manager);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteManager(int id)
        {
            _elementsService.RemoveManager(id);
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
        public ActionResult Create(Manager manager)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Add(manager);
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
            var manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == id);
            return View(manager);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Manager manager)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Update(manager.Id, manager);
            }
            return RedirectToAction("List");
        }
    }
}
