using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Web.Security.FormsAuthentication;

namespace MusicStore.MVC.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ValidateAdmin(string usu, string pass)
        {
            bool result;

            string password = Util.GetCurrentPassAdmin();
            string user = Util.GetCurrentUserAdmin();
            string Pwd = Util.GetSHA1(pass);
            if (user == usu && Pwd == password)
            {
                Session["LogonAdmin"] = user;
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}