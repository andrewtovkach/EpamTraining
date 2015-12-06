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
    public class ManagersController : Controller
    {
        readonly ManagersRepository _managersRepository = new ManagersRepository();

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
                TotalItems = _managersRepository.Count()
            };
            return View(new IndexViewModel<Manager> { PageInfo = pageInfo, Elements = GetManagersPerPages(page) });
        }

        private IEnumerable<Manager> GetManagersPerPages(int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return _managersRepository.Skip((page - 1) * pageSize).Take(pageSize);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var manager = _managersRepository.FirstOrDefault(x => x.Id == id);
            return View(manager);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteManager(int id)
        {
            _managersRepository.Remove(id);
            _managersRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            if (ModelState.IsValid)
            {
                _managersRepository.Add(manager);
                _managersRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var manager = _managersRepository.FirstOrDefault(x => x.Id == id);
            return View(manager);
        }

        [HttpPost]
        public ActionResult Edit(Manager manager)
        {
            if (ModelState.IsValid)
            {
                _managersRepository.Update(manager.Id, manager);
                _managersRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
