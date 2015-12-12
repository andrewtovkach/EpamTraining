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
        private readonly IElementsService _elementsService;

        public ClientsController()
        {
            _elementsService = new ElementsService();
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetClientsPerPages(page));
        }

        [Authorize(Roles = "admin, user")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteClient(int id)
        {
            _elementsService.RemoveClient(id);
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
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Add(client);
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
            var client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _elementsService.Update(client.Id, client);
            }
            return RedirectToAction("List");
        }
    }
}
