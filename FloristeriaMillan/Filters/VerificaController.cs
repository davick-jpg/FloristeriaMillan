using FloristeriaMillan.Controllers.Control_Acceso;
using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Filters
{
    public class VerficaSession : ActionFilterAttribute
    {

        private Usuarios usuario;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {

                base.OnActionExecuted(filterContext);

                usuario = (Usuarios)HttpContext.Current.Session["usuario"];
                if (usuario == null)
                {

                    if (filterContext.Controller is HomeLoginController == false)
                    {

                        filterContext.HttpContext.Response.Redirect("/HomeLogin/login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/HomeLogin/Login");
            }
        }
    }
}