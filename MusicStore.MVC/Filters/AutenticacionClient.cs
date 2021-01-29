﻿using System;
using MusicStore.MVC.Controllers;

namespace MusicStore.MVC.Filters
{
    public class AutenticacionClient : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext Contexto)
        {
            try
            {
                if (Contexto.HttpContext.Session == null || string.IsNullOrEmpty(Convert.ToString(Contexto.HttpContext.Session["LogonClient"])))
                {
                    UtilidadesController.Salida(Contexto);
                }
                else
                {
                    base.OnActionExecuting(Contexto);
                }
            }
            catch
            {
                UtilidadesController.Salida(Contexto);
            }
        }
    }
}
