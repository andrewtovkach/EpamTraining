using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SaleOfGoodsMVCApp.Models;

namespace SaleOfGoodsMVCApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult List(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var users = _db.Users.Where(item => !item.UserName.Contains("admin"))
                            .OrderBy(item => item.UserName).ToList();
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var models = _db.Users.Where(item => item.UserName.Contains(term))
                            .Select(item => new { value = item.UserName }).Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPartial(string name, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var users = name != null ? _db.Users.Where(item => !item.UserName.Contains("admin") && item.UserName.Contains(name)).ToList() :
                _db.Users.Where(item => !item.UserName.Contains("admin")).ToList();
            return PartialView(users.OrderBy(item => item.UserName).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser applicationUser = _db.Users.Find(id);
            if (applicationUser == null)
                return HttpNotFound();
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,UserName,PasswordHash,SecurityStamp")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(applicationUser).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(applicationUser);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser applicationUser = _db.Users.Find(id);
            if (applicationUser == null)
                return HttpNotFound();
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = _db.Users.Find(id);
            _db.Users.Remove(applicationUser);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
