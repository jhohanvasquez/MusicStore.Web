using System;
using MusicStore.Web.Controllers;

namespace MusicStore.Web.Filters
{
    public class AutenticacionAdmin : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext Contexto)
        {
            try
            {
                if (Contexto.HttpContext.Session == null || string.IsNullOrEmpty(Convert.ToString(Contexto.HttpContext.Session["LogonAdmin"])))
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
