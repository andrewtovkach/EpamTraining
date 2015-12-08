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
    public class ClientsController : Controller
    {
        readonly IElementsService _elementsService;

        public ClientsController()
        {
            _elementsService = new ElementsService();
        }

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetClientsPerPages(page));
        }

        public ActionResult List(int page = 1)
        {
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = _elementsService.ClientsItems.Count()
            };
            return View(new IndexViewModel<Client> { PageInfo = pageInfo, Elements = GetClientsPerPages(page) });
        }

        private IEnumerable<Client> GetClientsPerPages(int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return _elementsService.ClientsItems.OrderBy(item => item.Name).Skip((page - 1) * pageSize).Take(pageSize);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteClient(int id)
        {
            _elementsService.RemoveClient(id);
            _elementsService.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Add(client);
                _elementsService.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Update(client.Id, client);
                _elementsService.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
