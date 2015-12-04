using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class SaleInfoController : Controller
    {
        readonly SaleInfoRepository _saleInfoRepository = new SaleInfoRepository();

        /*public ActionResult List(int page = 1)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var saleInfosPerPages = _saleInfoRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _saleInfoRepository.Count()
            };
            return View(new IndexViewModel<SaleInfo> { PageInfo = pageInfo, Elements = saleInfosPerPages });
            return View();
        }*/

        public ActionResult List(int? client, int? product, int? manager)
        {
            var saleInfos = _saleInfoRepository.Items;
            if (client != null && client != 0)
            {
                saleInfos = saleInfos.Where(item => item.Client.Id == client);
            }
            if (manager != null && manager != 0)
            {
                saleInfos = saleInfos.Where(item => item.FileInfo.Manager.Id == manager);
            }
            if (product != null && product != 0)
            {
                saleInfos = saleInfos.Where(item => item.Product.Id == product);
            }

            var managers = new ManagersRepository().ToList();
            managers.Insert(0, new Manager { SecondName = "All", Id = 0 });
            var clients = new ClientsRepository().ToList();
            clients.Insert(0, new Client { SecondName = "All", Id = 0 });
            var products = new ProductsRepository().ToList();
            products.Insert(0, new Product { Name = "All", Id = 0 });
            SaleInfoViewModel plvm = new SaleInfoViewModel
            {
                SaleInfos = saleInfos.ToList(),
                Managers = new SelectList(managers, "Id", "SecondName"),
                Clients = new SelectList(clients, "Id", "SecondName"),
                Products = new SelectList(products, "Id", "Name")
            };
            return View(plvm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var saleInfo = _saleInfoRepository.FirstOrDefault(x => x.Id == id);
            return View(saleInfo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteSaleInfo(int id)
        {
            _saleInfoRepository.Remove(id);
            _saleInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(new ProductsRepository(), "Id", "Name");
            ViewBag.Clients = new SelectList(new ClientsRepository(), "Id", "SecondName");
            ViewBag.Managers = new SelectList(new ManagersRepository(), "Id", "SecondName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleInfo saleInfo)
        {
            saleInfo.Client = new ClientsRepository().FirstOrDefault(x => x.Id == saleInfo.Client.Id);
            saleInfo.Product = new ProductsRepository().FirstOrDefault(x => x.Id == saleInfo.Product.Id);
            var manager = new ManagersRepository().FirstOrDefault(x => x.Id == saleInfo.FileInfo.Manager.Id);
            saleInfo.FileInfo = new FileInfo(manager, saleInfo.Date);
            _saleInfoRepository.Add(saleInfo);
            _saleInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var saleInfo = _saleInfoRepository.FirstOrDefault(x => x.Id == id);
            if (saleInfo == null)
                return RedirectToAction("List");
            ViewBag.Clients = new SelectList(new ClientsRepository(), "Id", "SecondName", saleInfo.Client.Id);
            ViewBag.Products = new SelectList(new ProductsRepository(), "Id", "Name", saleInfo.Product.Id);
            ViewBag.Managers = new SelectList(new ManagersRepository(), "Id", "SecondName", saleInfo.FileInfo.Manager.Id);
            return View(saleInfo);
        }

        [HttpPost]
        public ActionResult Edit(SaleInfo saleInfo)
        {
            saleInfo.Client = new ClientsRepository().FirstOrDefault(x => x.Id == saleInfo.Client.Id);
            saleInfo.Product = new ProductsRepository().FirstOrDefault(x => x.Id == saleInfo.Product.Id);
            var manager = new ManagersRepository().FirstOrDefault(x => x.Id == saleInfo.FileInfo.Manager.Id);
            saleInfo.FileInfo = new FileInfo(manager, saleInfo.Date);
            _saleInfoRepository.Update(saleInfo.Id, saleInfo);
            _saleInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
