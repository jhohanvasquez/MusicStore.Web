using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using MusicStore.Web.Models;

namespace MusicStore.Web.Controllers
{
    public class PurchaseDetailController : Controller
    {
        private BdMusicStoreEntitiesModel db = new BdMusicStoreEntitiesModel();

        // GET: ClientClient
        [Filters.AutenticacionClient]
        public ActionResult Index()
        {
            List<AlbumSetViewModel> AlbumSet = db.AlbumSet.Select(Album => new AlbumSetViewModel
            { Id = Album.Id, Name = Album.Name }).ToList();
            ViewBag.AlbumSet = AlbumSet;
            return View();

        }


        [Filters.AutenticacionClient]
        public JsonResult RegistrarPurchaseDetail(int Album_Id, float Total)
        {
            string result;
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "PurchaseDetail");

            PurchaseDetail oPurchaseDetail = new PurchaseDetail();
            oPurchaseDetail.Client_Id = (string)Session["LogonClient"];
            oPurchaseDetail.Album_Id = Convert.ToInt32(Album_Id);
            oPurchaseDetail.Total = Total;
            db.PurchaseDetail.Add(oPurchaseDetail);
            db.SaveChanges();
            result = "okPay";

            return Json(new { respond = result, Url = redirectUrl });
        }

    }
}