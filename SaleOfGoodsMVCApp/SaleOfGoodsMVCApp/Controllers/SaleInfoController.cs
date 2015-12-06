﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BLL;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class SaleInfoController : Controller
    {
        readonly SaleInfoRepository _saleInfoRepository = new SaleInfoRepository();

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetSaleInfoPerPages(_saleInfoRepository.Items, page));
        }

        public ActionResult List(int? client, DateTime? date, int? product, int? manager, int page = 1)
        {
            var saleInfoViewModel = CreateSaleInfoViewModel(client, date, product, manager);
            ViewBag.SaleInfoViewModel = saleInfoViewModel;
            ViewBag.TotalCost = saleInfoViewModel.SaleInfos.TotalCost();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = saleInfoViewModel.SaleInfos.Count()
            };
            return View(new IndexViewModel<SaleInfo> { PageInfo = pageInfo, Elements = GetSaleInfoPerPages(saleInfoViewModel.SaleInfos, page) });
        }

        private IEnumerable<SaleInfo> GetSaleInfoPerPages(IEnumerable<SaleInfo> saleInfos, int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return saleInfos.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private SaleInfoViewModel CreateSaleInfoViewModel(int? client, DateTime? date, int? product, int? manager)
        {
            var managers = new ManagersRepository().OrderBy(item => item.SecondName).ToList();
            managers.Insert(0, new Manager { SecondName = "All", Id = 0 });
            var clients = new ClientsRepository().OrderBy(item => item.SecondName).ToList();
            clients.Insert(0, new Client { SecondName = "All", Id = 0 });
            var products = new ProductsRepository().OrderBy(item => item.Name).ToList();
            products.Insert(0, new Product { Name = "All", Id = 0 });
            var dates = (from element in new SaleInfoRepository()
                         select element.Date.ToString(CultureInfo.InvariantCulture))
                         .Distinct().OrderBy(item => item).ToList();
            dates.Insert(0, "All");
            SaleInfoViewModel saleInfoViewModel = new SaleInfoViewModel
            {
                SaleInfos = GetFilteredSaleInfos(client, date, product, manager),
                Managers = new SelectList(managers, "Id", "SecondName"),
                Clients = new SelectList(clients, "Id", "SecondName"),
                Products = new SelectList(products, "Id", "Name"),
                Dates = new SelectList(dates)
            };
            return saleInfoViewModel;
        }

        private IEnumerable<SaleInfo> GetFilteredSaleInfos(int? client, DateTime? date, int? product, int? manager)
        {
            var saleInfos = _saleInfoRepository.Items;
            if (client != null && client != 0)
                saleInfos = saleInfos.Where(item => item.Client.Id == client);
            if (date != null)
                saleInfos = saleInfos.Where(item => item.Date == date);
            if (manager != null && manager != 0)
                saleInfos = saleInfos.Where(item => item.FileInfo.Manager.Id == manager);
            if (product != null && product != 0)
                saleInfos = saleInfos.Where(item => item.Product.Id == product);
            return saleInfos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
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
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var saleInfo = _saleInfoRepository.FirstOrDefault(x => x.Id == id);
            if (saleInfo != null)
            {
                ViewBag.Clients = new SelectList(new ClientsRepository(), "Id", "SecondName", saleInfo.Client.Id);
                ViewBag.Products = new SelectList(new ProductsRepository(), "Id", "Name", saleInfo.Product.Id);
                ViewBag.Managers = new SelectList(new ManagersRepository(), "Id", "SecondName", saleInfo.FileInfo.Manager.Id);
            }
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
