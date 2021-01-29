using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Filters.AutenticacionAdmin]
        public ActionResult Index()
        {
            ViewBag.MigaPan = @"<div id='breadcrumb'>
                                    <ul class='breadcrumb'>
                                        <li><i class='fa fa-home'></i><a href='" + Url.Action("Index", "Admin") + @"'> Inicio </a></li>
                                        <li class='active'>Administrador</li>
                                    </ul>
                                </div>";
            return View();
        }
    }
}