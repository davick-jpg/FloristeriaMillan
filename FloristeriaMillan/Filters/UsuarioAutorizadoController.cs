using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UsuarioAutorizado : AuthorizeAttribute
    {
        private Usuarios usuario;
        private FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2();
        private int idOperacion;

        public UsuarioAutorizado(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {

                usuario = (Usuarios)HttpContext.Current.Session["usuario"];

                var listaUsuarioOperaciones = from m in db.Rol_operacion
                                              where m.idRol == usuario.idRol &&
                                              m.idOpe == idOperacion
                                              select m;

                if (listaUsuarioOperaciones.ToList().Count() < 1)
                {

                    filterContext.Result = new RedirectResult("~/Error/Index");
                }


            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Error/Index");
            }
        }

    }
}