using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        
        public ActionResult Index(string message)
        {
            ViewBag.Mensaje = message;
            return View();
        }
    }
}