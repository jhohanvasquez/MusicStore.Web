using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            HttpContext.Response.Cache.SetExpires(System.DateTime.Now);
            HttpContext.Response.Cache.SetValidUntilExpires(true);

            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetExpires(System.DateTime.Now);
            Response.Cache.SetValidUntilExpires(true);
            return View();
        }

       
        public ActionResult SalidaSegura()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            singleton.Dispose();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PaginaError(byte CodigoError)
        {
            return View();
        }
    }
}