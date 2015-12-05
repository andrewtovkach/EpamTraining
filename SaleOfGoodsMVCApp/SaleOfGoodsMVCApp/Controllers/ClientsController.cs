﻿using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BLL;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class ClientsController : Controller
    {
        readonly ClientsRepository _clientsRepository = new ClientsRepository();
        
        public ActionResult List(int page = 1)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var clientsPerPages = _clientsRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _clientsRepository.Count()
            };
            return View(new IndexViewModel<Client> { PageInfo = pageInfo, Elements = clientsPerPages });
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var client = _clientsRepository.FirstOrDefault(x => x.Id == id);
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
            if (ModelState.IsValid)
            {
                _clientsRepository.Add(client);
                _clientsRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
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
            if (!ModelState.IsValid)
                return View(client);
            _clientsRepository.Update(client.Id, client);
            _clientsRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
