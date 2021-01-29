using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicStore.MVC.App_Start;
using MusicStore.MVC.Models;

namespace MusicStore.MVC.Controllers
{
    public class AlbumSetController : Controller
    {
        private BdMusicStoreEntities db = new BdMusicStoreEntities();

        // GET: AlbumSet
        [Filters.AutenticacionAdmin]
        public ActionResult Index()
        {
            return View(db.AlbumSet.ToList());
        }

        // GET: AlbumSet/Details/5
        [Filters.AutenticacionAdmin]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumSet AlbumSet = db.AlbumSet.Find(id);
            if (AlbumSet == null)
            {
                return HttpNotFound();
            }
            return View(AlbumSet);
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        [Filters.AutenticacionAdmin]
        public JsonResult RegistrarAlbumSets(string desc)
        {
            string result;
            AlbumSet AlbumSetSearchs = db.AlbumSet.Where(d => d.Name == desc).FirstOrDefault();

            if (AlbumSetSearchs == null)
            {
                AlbumSet AlbumSet = new AlbumSet();
                AlbumSet.Name = desc;
                db.AlbumSet.Add(AlbumSet);
                db.SaveChanges();
                result = "save";
            }
            else
            {
                result = "exist";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: AlbumSet/Create
        [Filters.AutenticacionAdmin]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlbumSet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AlbumSet AlbumSet)
        {
            if (ModelState.IsValid)
            {
                db.AlbumSet.Add(AlbumSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AlbumSet);
        }

        // GET: AlbumSet/Edit/5
        [Filters.AutenticacionAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumSet AlbumSet = db.AlbumSet.Find(id);
            if (AlbumSet == null)
            {
                return HttpNotFound();
            }
            return View(AlbumSet);
        }

        // POST: AlbumSet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] AlbumSet AlbumSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AlbumSet).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AlbumSet);
        }

        // POST: AlbumSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumSet AlbumSet = db.AlbumSet.Find(id);
            db.AlbumSet.Remove(AlbumSet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        [Filters.AutenticacionAdmin]
        public JsonResult DeleteState(int IdAlbumSet)
        {
            int result = 0;
            AlbumSet AlbumSet = db.AlbumSet.Find(IdAlbumSet);

            db.AlbumSet.Remove(AlbumSet);
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
