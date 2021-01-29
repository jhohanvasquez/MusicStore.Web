using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Web.Models;

namespace MusicStore.Web.Controllers
{
    public class UtilidadesController : Controller
    {
        private static BdMusicStoreEntitiesModel db = new BdMusicStoreEntitiesModel();
        // GET: Utilidades

        public static void Salida(System.Web.Mvc.ActionExecutingContext Contexto)
        {
            if (Contexto.HttpContext.Session != null)
            {
                Contexto.HttpContext.Session.Clear();
                Contexto.HttpContext.Session.Abandon();
            }
            Contexto.Result = new System.Web.Mvc.EmptyResult();
            System.Web.Mvc.UrlHelper UrlTmp = new System.Web.Mvc.UrlHelper(Contexto.RequestContext);
            String Url = UrlTmp.Action("SalidaSegura", "Home");
            if (Url != null) Contexto.HttpContext.Response.Redirect(Url, false);
        }

    }
}