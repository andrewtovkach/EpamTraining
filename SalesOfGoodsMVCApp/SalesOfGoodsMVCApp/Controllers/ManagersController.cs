using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class ManagersController : Controller
    {
        readonly ManagersRepository _managersRepository = new ManagersRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = 3;
            IEnumerable<Manager> managersPerPages = _managersRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _managersRepository.Count() };
            IndexViewModel<Manager> ivm = new IndexViewModel<Manager> { PageInfo = pageInfo, Elements = managersPerPages };
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Manager manager = _managersRepository.FirstOrDefault(x => x.Id == id);
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
            _managersRepository.Add(manager);
            _managersRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
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
            _managersRepository.Update(manager.Id, manager);
            _managersRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
