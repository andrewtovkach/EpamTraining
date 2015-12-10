using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BLL;
using BLL.DTO;
using BLL.Interfaces;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class FileInfoController : Controller
    {
        readonly IElementsService _elementsService = new ElementsService();

        public ActionResult ListPartial(int page = 1)
        {
            return PartialView(GetFileInfoPerPages(_elementsService.FileInfosItems, page));
        }

        public ActionResult List(int? manager, int page = 1)
        {
            var fileInfoViewModel = CreateFileInfoViewModel(manager);
            ViewBag.FileInfoViewModel = fileInfoViewModel;
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]),
                TotalItems = fileInfoViewModel.FileInfos.Count()
            };
            return View(new IndexViewModel<FileInfo> { PageInfo = pageInfo, Elements = GetFileInfoPerPages(fileInfoViewModel.FileInfos, page) });
        }

        private static IEnumerable<FileInfo> GetFileInfoPerPages(IEnumerable<FileInfo> fileInfos, int page)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            return fileInfos.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private FileInfoViewModel CreateFileInfoViewModel(int? manager)
        {
            var managers = _elementsService.ManagersItems.OrderBy(item => item.Name).ToList();
            managers.Insert(0, new Manager { Name = "All", Id = 0 });
            FileInfoViewModel fileInfoViewModel = new FileInfoViewModel
            {
                FileInfos = GetFilteredFileInfo(manager),
                Managers = new SelectList(managers, "Id", "Name")
            };
            return fileInfoViewModel;
        }

        private IEnumerable<FileInfo> GetFilteredFileInfo(int? manager)
        {
            var fileInfos = _elementsService.FileInfosItems.OrderByDescending(item => item.Date).AsEnumerable();
            if (manager != null && manager != 0)
                fileInfos = fileInfos.Where(item => item.Manager.Id == manager);
            return fileInfos;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var fileInfo = _elementsService.FileInfosItems.FirstOrDefault(x => x.Id == id);
            return View(fileInfo);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteFileInfo(int id)
        {
            _elementsService.RemoveFileInfo(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Managers = new SelectList(_elementsService.ManagersItems, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(FileInfo fileInfo)
        {
            fileInfo.Manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
            _elementsService.Add(fileInfo);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var fileInfo = _elementsService.FileInfosItems.FirstOrDefault(x => x.Id == id);
            if (fileInfo != null)
                ViewBag.Managers = new SelectList(_elementsService.ManagersItems, "Id", "Name", fileInfo.Manager.Id);
            return View(fileInfo);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(FileInfo fileInfo)
        {
            fileInfo.Manager = _elementsService.ManagersItems.FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
            _elementsService.Update(fileInfo.Id, fileInfo);
            return RedirectToAction("List");
        }
    }
}
