using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BLL;

namespace SaleOfGoodsMVCApp.Controllers
{
    public class FileInfoController : Controller
    {
        readonly FileInfoRepository _fileInfoRepository = new FileInfoRepository();

        public ActionResult List(int? manager, int page = 1)
        {
            var fileInfoViewModel = CreateFileInfoViewModel(manager);
            ViewBag.FileInfoViewModel = fileInfoViewModel;
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var fileInfoPerPages = fileInfoViewModel.FileInfos.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = fileInfoViewModel.FileInfos.Count()
            };
            return View(new IndexViewModel<FileInfo> { PageInfo = pageInfo, Elements = fileInfoPerPages });
        }

        private FileInfoViewModel CreateFileInfoViewModel(int? manager)
        {
            var managers = new ManagersRepository().OrderBy(item => item.SecondName).ToList();
            managers.Insert(0, new Manager { SecondName = "All", Id = 0 });
            FileInfoViewModel fileInfoViewModel = new FileInfoViewModel
            {
                FileInfos = GetFilteredFileInfo(manager),
                Managers = new SelectList(managers, "Id", "SecondName")
            };
            return fileInfoViewModel;
        }

        private IEnumerable<FileInfo> GetFilteredFileInfo(int? manager)
        {
            var fileInfos = _fileInfoRepository.Items;
            if (manager != null && manager != 0)
                fileInfos = fileInfos.Where(item => item.Manager.Id == manager);
            return fileInfos;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var fileInfo = _fileInfoRepository.FirstOrDefault(x => x.Id == id);
            return View(fileInfo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteFileInfo(int id)
        {
            _fileInfoRepository.Remove(id);
            _fileInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Managers = new SelectList(new ManagersRepository(), "Id", "SecondName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FileInfo fileInfo)
        {
            if (ModelState.IsValid)
            {
                fileInfo.Manager = new ManagersRepository().FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
                _fileInfoRepository.Add(fileInfo);
                _fileInfoRepository.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var fileInfo = _fileInfoRepository.FirstOrDefault(x => x.Id == id);
            if (fileInfo == null)
                return RedirectToAction("List");
            ViewBag.Managers = new SelectList(new ManagersRepository(), "Id", "SecondName", fileInfo.Manager.Id);
            return View(fileInfo);
        }

        [HttpPost]
        public ActionResult Edit(FileInfo fileInfo)
        {
            if (!ModelState.IsValid)
                return View(fileInfo);
            fileInfo.Manager = new ManagersRepository().FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
            _fileInfoRepository.Update(fileInfo.Id, fileInfo);
            _fileInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
