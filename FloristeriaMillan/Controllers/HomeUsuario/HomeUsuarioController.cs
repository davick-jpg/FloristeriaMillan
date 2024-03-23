using FloristeriaMillan.Models;
using FloristeriaMillan.Querys.Logica_CanalUsuarios;
using FloristeriaMillan.Querys.Logica_ControlAcceso;
using Stripe.BillingPortal;
using Stripe;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Controllers.HomeUsuario
{
    public class HomeUsuarioController : Controller
    {
        // GET: HomeUsuario
        public ActionResult Perfil()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_principal.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Usuario"] = email;
            }
            else
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            Session["UsuarioActual"] = query.buscar_usuario(emailCli);

            return View();
        }

        [HttpPost]
        public ActionResult Perfil(String nombre, String calle, String colonia, String municipio, decimal codigo_postal, decimal numero)
        {
            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            Session["UsuarioActual"] = query.editar_usuario(emailCli, nombre, calle, colonia, municipio, codigo_postal, numero);

            return View();
        }

        public ActionResult Direcciones()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_principal.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Usuario"] = email;
            }
            else
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            Session["UsuarioActual"] = query.buscar_usuario(emailCli);

            Session["Direcciones"] = query.buscar_direcciones(emailCli);

            return View();
        }

        [HttpPost]
        public ActionResult Direcciones(String identificador, String calle, String colonia, String municipio, decimal cpostal)
        {
            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            DireccionesCliente direccion = query.buscar_direccion(identificador, emailCli);

            Boolean estatus = false;

            if (direccion != null)
            {
                estatus = query.editar_direccion(emailCli, identificador, calle, colonia, municipio, cpostal);
            }
            else
            {
                estatus = query.agregar_direccion(emailCli, identificador, calle, colonia, municipio, cpostal);
            }

            Session["Direcciones"] = query.buscar_direcciones(emailCli);

            return RedirectToAction("Direcciones", "HomeUsuario");
        }

        public ActionResult buscar_direccion(String identificador)
        {
            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            DireccionesCliente direccion = query.buscar_direccion(identificador, emailCli);

            return Json(new { identificador = direccion.nombreDir, calle = direccion.calleDir, colonia = direccion.coloniaDir, municipio = direccion.municipioDir, codigo_postal = direccion.codigoPostalCli });
        }

        public ActionResult eliminar_direccion(String identificador)
        {
            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            String emailCli = "";

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            Boolean estatus = query.eliminar_direccion(identificador, emailCli);

            Session["Direcciones"] = query.buscar_direcciones(emailCli);

            return Json(new { estatus = true });
        }

        public ActionResult Favoritos()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_principal.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Usuario"] = email;
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

            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            Session["UsuarioActual"] = query.buscar_usuario(emailCli);

            Session["Productos_favoritos"] = query.buscar_favoritos(emailCli);

            Session["Favoritos"] = query.buscar_favoritos(emailCli);

            return View();
        }

        public ActionResult Carrito()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_principal.Value;
                string email = control.DecryptString(encryptedEmail);

                Session["Usuario"] = email;
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

            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            Session["UsuarioActual"] = query.buscar_usuario(emailCli);

            Session["Carrito"] = query.buscar_carrito(emailCli);

            Session["Total"] = query.calcular_precio(emailCli);

            Session["Envio"] = 0.00;

            return View();
        }

        public ActionResult generar_descripcion()
        {
            try
            {
                CarritoProducto[] carrito = (CarritoProducto[])Session["Carrito"];

                String[] descripciones = new string[carrito.Length];

                for (int i = 0; i < descripciones.Length; i++)
                {
                    descripciones[i] = carrito[i].producto.descripcionPro;
                }

                return Json(new { descripciones = descripciones });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult editar_informacion(String nombre, decimal cantidad)
        {
            try
            {
                String emailCli = "";

                if (Session["Default"] != null)
                {
                    emailCli = (String)Session["Default"];
                }

                if (Session["Usuario"] != null)
                {
                    emailCli = (String)Session["Usuario"];
                }

                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Carrito carrito = (from carrito_buscar in db.Carrito
                                       join usuario in db.Usuarios on carrito_buscar.idUsu equals usuario.idUsu
                                       join producto in db.Producto on carrito_buscar.idPro equals producto.idPro
                                       where usuario.emailCli == emailCli && producto.nombrePro == nombre
                                       select carrito_buscar).FirstOrDefault();

                    carrito.cantidadCar = cantidad;

                    db.SaveChanges();
                }

                return Json(new { estatus = "exito" });
            }
            catch (Exception)
            {

                return Json(new { estatus = "fallo" });
            }
        }

        public ActionResult Pasarela(String direccion)
        {
            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            LO_CanalUsuariosController query = new LO_CanalUsuariosController();

            Session["UsuarioActual"] = query.buscar_usuario(emailCli);

            Session["Carrito"] = query.buscar_carrito(emailCli);

            Session["Total"] = query.calcular_precio(emailCli);

            Session["Direccion_pedido"] = query.buscar_direcciones(emailCli);

            return View();
        }

        public ActionResult generar_checkout(String direccion, String fecha, String hora)
        {
            Session["Direccion_envio"] = direccion;

            String hora_seleccionada = "";

            if (hora == "1")
            {
                hora_seleccionada = "08:00";
            }
            else if (hora == "2")
            {
                hora_seleccionada = "14:00";
            }
            else if (hora == "3")
            {
                hora_seleccionada = "18:00";
            }
            else
            {
                return Json(new { estatus = "false" });
            }

            Session["Fecha_envio"] = fecha + " " + hora_seleccionada;

            return Json(new { estatus = "true" });
        }

        public ActionResult Sucess(string sessionId)
        {
            try
            {
                LO_CanalUsuariosController query = new LO_CanalUsuariosController();

                String emailCli = "";

                if (Session["Default"] != null)
                {
                    emailCli = (String)Session["Default"];
                }

                if (Session["Usuario"] != null)
                {
                    emailCli = (String)Session["Usuario"];
                }

                CarritoProducto[] carrito = (CarritoProducto[])Session["Carrito"];

                String direccion = (String)Session["Direccion_envio"];

                String fecha = (String)Session["Fecha_envio"];

                Decimal pago = 0;

                decimal total = query.calcular_precio(emailCli);

                Factura estatus = query.registrar_carrito(carrito, direccion, pago, emailCli, total, fecha);

                if (estatus != null)
                {
                    String desglose = query.desglosar_factura(estatus, carrito);

                    Boolean eliminar = query.eliminar_carrito(emailCli);
                }

                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Carrito", "HomeUsuario");
            }
        }

        public ActionResult Cancel()
        {
            return View();
        }
    }
}