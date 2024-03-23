using FloristeriaMillan.Models;
using FloristeriaMillan.Querys.Logica_Buscador;
using FloristeriaMillan.Querys.Logica_ControlAcceso;
using FloristeriaMillan.Querys.Logica_Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace FloristeriaMillan.Controllers.HomePrincipal
{
    public class HomePrincipalController : Controller
    {
        // GET: HomePrincipal
        public ActionResult Index()
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

            LO_HomeController query = new LO_HomeController();

            Session["Producto_estrella"] = query.buscar_producto("Millan Floristeria");

            Session["Productos_recomendados"] = query.buscar_recomendados();

            Session["Favoritos"] = query.buscar_favoritos(emailCli);

            string rutaFisica = Path.Combine(HostingEnvironment.MapPath("~/Imagenes/HomePrincipal/Index/Promociones"));

            string[] archivos = Directory.GetFiles(rutaFisica);

            for (int i = 0; i < archivos.Length; i++)
            {
                archivos[i] = "https://localhost:44394/Imagenes/HomePrincipal/Index/Promociones/" + Path.GetFileName(archivos[i]);
            }

            Session["Banners"] = archivos;

            Session["Productos_Novedad"] = query.buscar_novedad();

            return View();
        }

        [HttpPost]
        public ActionResult buscar_sugerencias(String cadena)
        {
            LO_BuscadorController query = new LO_BuscadorController();

            String[] sugerencias = query.buscador_productos(cadena);

            return Json(new { sugerencias = sugerencias });
        }

        public ActionResult agregar_favorito(String nombrePro)
        {
            LO_HomeController query = new LO_HomeController();

            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            Boolean estatus = query.agregar_favorito(nombrePro, emailCli);

            if (estatus)
            {
                return Json(new { estatus = "Se ha agreado a favoritos" });
            }
            else
            {
                return Json(new { estatus = "Secedio algun error intentelo mas tarde" });

            }
        }

        public ActionResult quitar_favorito(String nombrePro)
        {
            LO_HomeController query = new LO_HomeController();

            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            Boolean estatus = query.quitar_favorito(nombrePro, emailCli);

            if (estatus)
            {
                return Json(new { estatus = "Se ha eliminado de favoritos" });
            }
            else
            {
                return Json(new { estatus = "Secedio algun error intentelo mas tarde" });

            }
        }

        [HttpPost]

        public ActionResult Index(String categoria)
        {

            if (categoria != null)
            {
                Session["NombreCat"] = categoria;
            }

            return View();
        }

        public ActionResult Categoria()
        {
            LO_HomeController query = new LO_HomeController();

            if (Session["NombreCat"] == null)
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            String categoria = (String)Session["NombreCat"];

            Session["Categoria"] = query.buscar_categoria(categoria);

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

            Session["Catalogo"] = query.buscar_catalogo((Categorias)Session["Categoria"]);

            Session["Favoritos"] = query.buscar_favoritos(emailCli);

            Session["Clases"] = query.buscar_clases(categoria);

            Session["ClaseActiva"] = null;

            //ViewBag.Filtros = clase_filtros;

            return View();
        }

        [HttpPost]
        public ActionResult filtrar_catalogo(String nombreCla)
        {
            LO_HomeController query = new LO_HomeController();

            Categorias categoria = (Categorias)Session["Categoria"];

            Session["ClaseActiva"] = nombreCla;

            Session["Catalogo"] = query.filtrar_catalogo(nombreCla, categoria.nombreCat);

            return View("Categoria");
        }

        [HttpPost]
        public ActionResult Categoria(String articulo)
        {
            if (articulo != null)
            {
                Session["nombreArt"] = articulo;
            }

            return View();
        }


        public ActionResult Articulo()
        {
            LO_HomeController query = new LO_HomeController();

            String nombreArt = (String)Session["nombreArt"];

            if (Session["nombreArt"] == null)
            {
                return RedirectToAction("Index", "HomePrincipal");
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

            Session["Favoritos"] = query.buscar_favoritos(emailCli);

            Session["Recomendados"] = query.buscar_articulos_recomendados(nombreArt);

            String[] direcciones = new String[4];

            for (int i = 0; i < direcciones.Length; i++)
            {
                direcciones[i] = "https://localhost:44394/Galeria/Productos/" + nombreArt.ToLower().Replace(" ", "") + i + ".png";
            }

            Session["Direcciones"] = direcciones;

            Session["Articulo"] = query.buscar_producto((string)Session["nombreArt"]);

            return View();
        }

        public ActionResult anadir_carrito(String nombrePro, String dedicatoria)
        {
            LO_HomeController query = new LO_HomeController();

            String emailCli = "";

            if (Session["Default"] != null)
            {
                emailCli = (String)Session["Default"];
            }

            if (Session["Usuario"] != null)
            {
                emailCli = (String)Session["Usuario"];
            }

            String estatus = query.anadir_carrito(nombrePro, emailCli, dedicatoria);

            return Json(new { estatus = "exito" });
        }
    }
}