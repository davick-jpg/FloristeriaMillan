using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FloristeriaMillan.Querys.Logica_ControlAcceso;
using System.Web.Helpers;
using System.Linq.Expressions;

namespace FloristeriaMillan.Controllers.Control_Acceso
{
    public class HomeLoginController : Controller
    {
        //[UsuarioAutorizado(idOperacion:1)]
        public ActionResult Login()
        {

            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            HttpCookie cookie_default = Request.Cookies["EmailCookieDefault"];
            if (cookie_default != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_default.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Default"] = email;
            }

            if (Session["Usuario"] == null && Session["Default"] == null)
            {
                LO_controlAcceso control = new LO_controlAcceso();

                Usuarios usuario = control.generar_default();

                Session["Default"] = usuario.emailCli;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(String email, String password)
        {

            try
            {

                Usuarios usuario = new LO_controlAcceso().EncontrarUsuario(email, password);

                Boolean estatus;

                if (usuario == null)
                {
                    ViewBag.Error = "Usuario o contraseña invalidos";
                    return View();
                }

                String email_default = Session["Default"].ToString();

                Boolean estatus_default = eliminar_default(email_default);

                if (usuario.idRol == 2 || usuario.idRol == 3)
                {
                    estatus = generar_cockie(email, "EmailCookie");

                    if (estatus)
                    {
                        eliminar_cockie("EmailCookieDefault");
                    }

                    if (estatus)
                    {
                        return RedirectToAction("Index", "HomePrincipal");
                    }
                }
                else if (usuario.idRol == 0 || usuario.idRol == 1)
                {
                    estatus = generar_cockie(email, "EmailCookie");

                    if (estatus)
                    {
                        eliminar_cockie("EmailCookieDefault");
                    }

                    if (estatus)
                    {
                        return RedirectToAction("Index", "HomeStaff");
                    }
                }

                ViewBag.Error = "Usuario o contraseña invalidos";
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public Boolean eliminar_default(String email)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Usuarios usuario = (from usuario_buscar in db.Usuarios
                                        where usuario_buscar.emailCli == email
                                        select usuario_buscar).FirstOrDefault();

                    db.Usuarios.Remove(usuario);

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean generar_cockie(String email, String nombre_cockie)
        {
            try
            {
                Cockie control = new Cockie();

                string encryptedEmail = control.EncryptString(email);
                HttpCookie cookie = new HttpCookie(nombre_cockie, encryptedEmail);
                Response.Cookies.Add(cookie);

                Session["Usuario"] = email;

                Session["Default"] = null;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean eliminar_cockie(String cockie)
        {
            HttpCookie cookie = new HttpCookie(cockie);
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            return true;
        }

        public ActionResult Logout()
        {
            HttpCookie cookie_default = Request.Cookies["EmailCookieDefault"];
            if (cookie_default != null)
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            FormsAuthentication.SignOut();

            LO_controlAcceso control = new LO_controlAcceso();

            Usuarios usuario = control.generar_default();

            Boolean estatus = generar_cockie(usuario.emailCli, "EmailCookieDefault");

            Session["Default"] = usuario.emailCli;

            Session["Usuario"] = null;

            if (estatus)
            {
                eliminar_cockie("EmailCookie");

            }

            return RedirectToAction("Index", "HomePrincipal");
        }

        //[UsuarioAutorizado(idOperacion: 2)]
        public ActionResult Registro()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            HttpCookie cookie_default = Request.Cookies["EmailCookieDefault"];
            if (cookie_default != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_default.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Default"] = email;
            }

            if (Session["Usuario"] == null && Session["Default"] == null)
            {
                LO_controlAcceso control = new LO_controlAcceso();

                Usuarios usuario = control.generar_default();

                Session["Default"] = usuario.emailCli;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Registro(String email, String re_email,
            String password, String re_password, String nombre, String apellidos, String telefono)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    String email_session = (String)Session["Default"];

                    Usuarios usuario_session = (from usuario_ses in db.Usuarios
                                                where usuario_ses.emailCli == email_session
                                                select usuario_ses).FirstOrDefault();

                    if (usuario_session == null)
                    {
                        ViewBag.Error = "ocurrio un error inesperado intentelo mas tarde";
                        return View();
                    }


                    Usuarios usuario = new LO_controlAcceso().RegistrarUsuario(usuario_session.idUsu, email, password, nombre, long.Parse(telefono), 2);

                    Boolean estatus = generar_cockie(usuario.emailCli, "EmailCookie");

                    if (estatus)
                    {
                        eliminar_cockie("EmailCookieDefault");
                    }

                    return RedirectToAction("Index", "HomePrincipal");
                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}