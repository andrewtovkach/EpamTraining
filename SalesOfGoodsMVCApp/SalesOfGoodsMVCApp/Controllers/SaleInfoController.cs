using System.Collections.Generic;
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

        public ActionResult List(int page = 1)
        {
            int pageSize = 3;
            IEnumerable<SaleInfo> saleInfosPerPages = _saleInfoRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _saleInfoRepository.Count() };
            IndexViewModel<SaleInfo> ivm = new IndexViewModel<SaleInfo> { PageInfo = pageInfo, Elements = saleInfosPerPages };
            return View(ivm);
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
            var products = new SelectList(new ProductsRepository(), "Id", "Name");
            ViewBag.Products = products;
            var clients = new SelectList(new ClientsRepository(), "Id", "SecondName");
            ViewBag.Clients = clients;
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleInfo saleInfo)
        {
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
            var clients = new SelectList(new ClientsRepository(), "Id", "SecondName", saleInfo.Client.Id);
            ViewBag.Clients = clients;
            var products = new SelectList(new ProductsRepository(), "Id", "Name", saleInfo.Product.Id);
            ViewBag.Products = products;
            return View(saleInfo);
        }

        [HttpPost]
        public ActionResult Edit(SaleInfo saleInfo)
        {
            _saleInfoRepository.Update(saleInfo.Id, saleInfo);
            _saleInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
