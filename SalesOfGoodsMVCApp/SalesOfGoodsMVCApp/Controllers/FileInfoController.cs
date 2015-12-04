using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using SalesOfGoodsMVCApp.Models;

namespace SalesOfGoodsMVCApp.Controllers
{
    public class FileInfoController : Controller
    {
        readonly FileInfoRepository _fileInfoRepository = new FileInfoRepository();

        public ActionResult List(int page = 1)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            var fileInfosPerPages = _fileInfoRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _fileInfoRepository.Count()
            };
            return View(new IndexViewModel<FileInfo> { PageInfo = pageInfo, Elements = fileInfosPerPages });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
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
            fileInfo.Manager = new ManagersRepository().FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
            _fileInfoRepository.Add(fileInfo);
            _fileInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
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
            fileInfo.Manager = new ManagersRepository().FirstOrDefault(x => x.Id == fileInfo.Manager.Id);
            _fileInfoRepository.Update(fileInfo.Id, fileInfo);
            _fileInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
