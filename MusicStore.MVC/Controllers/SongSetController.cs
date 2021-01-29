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
    public class SongSetController : Controller
    {
        private BdMusicStoreEntities db = new BdMusicStoreEntities();

        // GET: SongSet
        [Filters.AutenticacionAdmin]
        public ActionResult Index(int id, string name)
        {
            if (id != 0)
            {
                singleton.GetCurrentSingleton().IdAlbumSetSelect = id;
                singleton.GetCurrentSingleton().NameAlbumSetSelect = name;
            }
            else
            {
                id = singleton.GetCurrentSingleton().IdAlbumSetSelect;
                name = singleton.GetCurrentSingleton().NameAlbumSetSelect;
            }
            ViewBag.AlbumSet = name;
            return View(db.SongSet.Where(s => s.Album_Id == id).ToList());
        }


        // GET: SongSet/Details/5
        [Filters.AutenticacionAdmin]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongSet SongSet = db.SongSet.Find(id);
            if (SongSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumSet = singleton.GetCurrentSingleton().NameAlbumSetSelect;
            return View(SongSet);
        }


        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult RegistrarSongSet(string Names)
        {
            string result;
            var idAlbum = singleton.GetCurrentSingleton().IdAlbumSetSelect;
            var activeAlbumSet = db.SongSet.Where(a => a.Name == Names && a.Album_Id == idAlbum).FirstOrDefault();
            if (activeAlbumSet == null)
            {

                //Guardar registro en tabla SongSet
                var oSongSet = new SongSet
                {
                    Album_Id = idAlbum,
                    Name = Names
                };

                db.SongSet.Add(oSongSet);
                db.SaveChanges();

                result = "save";
            }
            else
            {
                //Cedula registrada en elección
                result = "exist";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: SongSet/Create
        [Filters.AutenticacionAdmin]
        public ActionResult Create()
        {
            ViewBag.AlbumSet = singleton.GetCurrentSingleton().NameAlbumSetSelect;
            return View();
        }


        // GET: SongSet/Edit/5
        [Filters.AutenticacionAdmin]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongSet SongSet = db.SongSet.Where(s => s.Id == id).FirstOrDefault();
            if (SongSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumSet = singleton.GetCurrentSingleton().NameAlbumSetSelect;
            return View(SongSet);
        }

        [HttpPost]
        public JsonResult EditarSongSet(int Id, string Name)
        {
            string result;

            var activeSongSet = db.SongSet.FirstOrDefault(e => e.Id == Id);
            if (activeSongSet != null)
            {
                activeSongSet.Id = Id;
                activeSongSet.Album_Id = singleton.GetCurrentSingleton().IdAlbumSetSelect;
                activeSongSet.Name = Name;

                db.SongSet.Attach(activeSongSet);
                var entry = db.Entry(activeSongSet);
                entry.State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result = "save";
            }
            else
            {
                //Cedula registrada en elección
                result = "noexist";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: SongSet/Delete/5
        [Filters.AutenticacionAdmin]
        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongSet SongSet = db.SongSet.Where(s => s.Id == id).FirstOrDefault();
            if (SongSet == null)
            {
                return HttpNotFound();
            }
            return View(SongSet);
        }


        [HttpPost, ValidateHeaderAntiForgeryToken]
        [Filters.AutenticacionAdmin]
        public JsonResult DeleteConfirmed(int id)
        {
            string result;

            SongSet SongSet = db.SongSet.Where(s => s.Id == id).FirstOrDefault();
            db.SongSet.Remove(SongSet);
            db.SaveChanges();
            //Cedula registrada en elección
            result = "delete";

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
