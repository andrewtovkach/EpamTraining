using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BLL;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class SaleInfoController : Controller
    {
        private readonly IElementsService _elementsService;

        public SaleInfoController()
        {
            _elementsService = new ElementsService();
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetSaleInfoPerPages(_elementsService.SaleInfosItems, page));
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult List(int? client, int? product, int? manager, int page = 1)
        {
            var saleInfoViewModel = CreateSaleInfoViewModel(client, product, manager);
            ViewBag.SaleInfoViewModel = saleInfoViewModel;
            ViewBag.TotalCost = saleInfoViewModel.SaleInfos.Sum(item => ConverterCurrency.Convert(item.Currency, item.Cost));
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = saleInfoViewModel.SaleInfos.Count()
            };
            return View(new IndexViewModel<SaleInfo> { PageInfo = pageInfo, Elements = GetSaleInfoPerPages(saleInfoViewModel.SaleInfos, page) });
        }

        private static IEnumerable<SaleInfo> GetSaleInfoPerPages(IEnumerable<SaleInfo> saleInfos, int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return saleInfos.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private SaleInfoViewModel CreateSaleInfoViewModel(int? client, int? product, int? manager)
        {
            var managers = _elementsService.ManagersItems.OrderBy(item => item.Name).ToList();
            managers.Insert(0, new Manager { Name = "All", Id = 0 });
            var clients = _elementsService.ClientsItems.OrderBy(item => item.Name).ToList();
            clients.Insert(0, new Client { Name = "All", Id = 0 });
            var products = _elementsService.ProductsItems.OrderBy(item => item.Name).ToList();
            products.Insert(0, new Product { Name = "All", Id = 0 });
            SaleInfoViewModel saleInfoViewModel = new SaleInfoViewModel
            {
                SaleInfos = GetFilteredSaleInfos(client, product, manager),
                Managers = new SelectList(managers, "Id", "Name"),
                Clients = new SelectList(clients, "Id", "Name"),
                Products = new SelectList(products, "Id", "Name")
            };
            return saleInfoViewModel;
        }

        private IEnumerable<SaleInfo> GetFilteredSaleInfos(int? client, int? product, int? manager)
        {
            var saleInfos = _elementsService.SaleInfosItems.OrderByDescending(item => item.Date).AsEnumerable();
            if (client != null && client != 0)
                saleInfos = saleInfos.Where(item => item.Client.Id == client);
            if (manager != null && manager != 0)
                saleInfos = saleInfos.Where(item => item.FileInfo.Manager.Id == manager);
            if (product != null && product != 0)
                saleInfos = saleInfos.Where(item => item.Product.Id == product);
            return saleInfos;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var saleInfo = _elementsService.SaleInfosItems.FirstOrDefault(x => x.Id == id);
            return View(saleInfo);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSaleInfo(int id)
        {
            _elementsService.RemoveSaleInfo(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(_elementsService.ProductsItems, "Id", "Name");
            ViewBag.Clients = new SelectList(_elementsService.ClientsItems, "Id", "Name");
            ViewBag.Managers = new SelectList(_elementsService.ManagersItems, "Id", "Name");
            var listCurrencies = ConverterCurrency.ListCurrencyInfos.Select(item => item.CharCode).ToList();
            listCurrencies.Add("BYR");
            ViewBag.Currencies = new SelectList(listCurrencies.OrderBy(item => item).ToList());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(SaleInfo saleInfo)
        {
            saleInfo.Client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == saleInfo.Client.Id);
            saleInfo.Product = _elementsService.ProductsItems.FirstOrDefault(x => x.Id == saleInfo.Product.Id);
            var manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == saleInfo.FileInfo.Manager.Id);
            saleInfo.FileInfo = new FileInfo
            {
                Manager = manager, 
                Date = saleInfo.Date
            };
            _elementsService.Add(saleInfo);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var saleInfo = _elementsService.SaleInfosItems.FirstOrDefault(x => x.Id == id);
            if (saleInfo != null)
            {
                ViewBag.Clients = new SelectList(_elementsService.ClientsItems, "Id", "Name", saleInfo.Client.Id);
                ViewBag.Products = new SelectList(_elementsService.ProductsItems, "Id", "Name", saleInfo.Product.Id);
                ViewBag.Managers = new SelectList(_elementsService.ManagersItems, "Id", "Name", saleInfo.FileInfo.Manager.Id);
                var listCurrencies = ConverterCurrency.ListCurrencyInfos.Select(item => item.CharCode).ToList();
                listCurrencies.Add("BYR");
                ViewBag.Currencies = new SelectList(listCurrencies.OrderBy(item => item).ToList(), saleInfo.Currency);
            }
            return View(saleInfo);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(SaleInfo saleInfo)
        {
            saleInfo.Client = _elementsService.ClientsItems.FirstOrDefault(x => x.Id == saleInfo.Client.Id);
            saleInfo.Product = _elementsService.ProductsItems.FirstOrDefault(x => x.Id == saleInfo.Product.Id);
            var manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == saleInfo.FileInfo.Manager.Id);
            saleInfo.FileInfo = new FileInfo
            {
                Manager = manager, 
                Date = saleInfo.Date
            };
            _elementsService.Update(saleInfo.Id, saleInfo);
            return RedirectToAction("List");
        }
    }
}
