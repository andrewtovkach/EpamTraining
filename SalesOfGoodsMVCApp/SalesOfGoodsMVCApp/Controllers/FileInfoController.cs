using System.Collections.Generic;
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
            int pageSize = 3;
            IEnumerable<FileInfo> fileInfosPerPages = _fileInfoRepository.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _fileInfoRepository.Count() };
            IndexViewModel<FileInfo> ivm = new IndexViewModel<FileInfo> { PageInfo = pageInfo, Elements = fileInfosPerPages };
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            FileInfo fileInfo = _fileInfoRepository.FirstOrDefault(x => x.Id == id);
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
            var managers = new SelectList(new ManagersRepository(), "Id", "SecondName");
            ViewBag.Managers = managers;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FileInfo fileInfo)
        {
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
            var managers = new SelectList(new ManagersRepository(), "Id", "SecondName", fileInfo.Manager.Id);
            ViewBag.Managers = managers;
            return View(fileInfo);
        }

        [HttpPost]
        public ActionResult Edit(FileInfo fileInfo)
        {
            _fileInfoRepository.Update(fileInfo.Id, fileInfo);
            _fileInfoRepository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
