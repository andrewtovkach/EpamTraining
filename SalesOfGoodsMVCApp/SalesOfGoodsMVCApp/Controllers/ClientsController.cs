using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class ClientsController : Controller
    {
        readonly ClientsRepository _clientsRepository = new ClientsRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = 3;
            IEnumerable<Client> clientsPerPages = _clientsRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _clientsRepository.Count() };
            IndexViewModel<Client> ivm = new IndexViewModel<Client> { PageInfo = pageInfo, Elements = clientsPerPages };
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Client client = _clientsRepository.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteClient(int id)
        {
            _clientsRepository.Remove(id);
            _clientsRepository.SaveChanges();
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
            _clientsRepository.Add(client);
            _clientsRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var client = _clientsRepository.FirstOrDefault(x => x.Id == id);
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            _clientsRepository.Update(client.Id, client);
            _clientsRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
