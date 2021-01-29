using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.MVC.Models;
using static System.Web.Security.FormsAuthentication;

namespace MusicStore.MVC.Controllers
{
    public class LoginClientController : Controller
    {
        private BdMusicStoreEntities db = new BdMusicStoreEntities();

        // GET: LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ValidateClient(string usu)
        {
            string result;
            var activeUser = db.Client.Where(a => a.Id == usu).FirstOrDefault();
            if (activeUser != null)
            {
                Session["LogonClient"] = usu;
                Session["NameClient"] = activeUser.Name;
                result = "ok";
            }
            else
            {
                result = "invalid";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}