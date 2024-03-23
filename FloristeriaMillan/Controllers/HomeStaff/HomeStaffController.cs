using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FloristeriaMillan.Models;
using FloristeriaMillan.Querys.Logica_Articulos;
using FloristeriaMillan.Querys.Logica_Buscador;
using FloristeriaMillan.Querys.Logica_CanalUsuarios;
using FloristeriaMillan.Querys.Logica_ControlAcceso;
using FloristeriaMillan.Querys.Logica_Pedidos;
using FloristeriaMillan.Querys.Logica_Productos;
using FloristeriaMillan.Querys.Logica_Proveedores;
using FloristeriaMillan.Querys.Logica_Usuarios;
using FloristeriaMillan.Querys.Logica_Ventas;

namespace FloristeriaMillan.Controllers.HomeStaff
{
    public class HomeStaffController : Controller
    {
        // GET: HomeStaff
        public ActionResult Index()
        {
            HttpCookie cookie_principal = Request.Cookies["EmailCookie"];
            if (cookie_principal != null)
            {
                Cockie control = new Cockie();

                string encryptedEmail = cookie_principal.Value;
                string email = control.DecryptString(encryptedEmail);

                LO_UsuariosController query = new LO_UsuariosController();

                UsuarioRol usuario = query.buscar_usuario(email);

                Session["UsuarioActual"] = usuario.Usuario.nombreCli;
            }

            HttpCookie cookie_default = Request.Cookies["EmailCookieDefault"];
            if (cookie_default != null)
            {
                return RedirectToAction("Index", "HomePrincipal");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Usuarios()
        {
            LO_UsuariosController query = new LO_UsuariosController();

            Session["Usuarios"] = query.buscar_usuarios();

            Session["Roles"] = query.buscar_roles();

            return PartialView("Usuarios");
        }

        [HttpPost]
        public ActionResult buscar_usuario(String correo)
        {
            LO_UsuariosController query = new LO_UsuariosController();

            var usuario = query.buscar_usuario(correo);

            if (usuario != null)
            {
                UsuarioRol datos_usuario = (UsuarioRol)usuario;

                String nombreCli = (datos_usuario.Usuario).nombreCli;
                String emailCli = (datos_usuario.Usuario).emailCli;
                String passwordCli = (datos_usuario.Usuario).passwordCli;
                decimal telefonoCli = (decimal)(datos_usuario.Usuario).telefonoCli;
                String rol = (datos_usuario.Rol).nombreRol;

                Session["Busqueda_Usuario"] = datos_usuario.Usuario.emailCli;

                return Json(new { nombreCli = nombreCli, emailCli = emailCli, passwordCli = passwordCli, telefonoCli = telefonoCli, rol = rol });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult eliminar_usuario(String nombre)
        {
            LO_UsuariosController query = new LO_UsuariosController();

            ViewBag.estatus = query.eliminar_usuario(nombre);

            Session["Usuarios"] = query.buscar_usuarios();

            Session["Busqueda_Usuario"] = null;

            return PartialView("Usuarios");
        }

        [HttpGet]
        public ActionResult Usuarios_formulario(String nombre,
                                     String email,
                                     String password,
                                     decimal telefono,
                                     String rol)
        {
            LO_UsuariosController query = new LO_UsuariosController();

            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                bool vacia = !db.Usuarios.Any();

                if (!vacia && rol != "undefined")
                {

                    if (Session["Busqueda_Usuario"] != null)
                    {
                        String email_buscar = (String)Session["Busqueda_Usuario"];

                        ViewBag.estatus = query.editar_usuario(email_buscar,
                                                               nombre,
                                                               email,
                                                               password,
                                                               telefono,
                                                               rol);

                        Session["Busqueda_Usuario"] = null;

                        Session["Usuarios"] = query.buscar_usuarios();

                        return PartialView("Usuarios");
                    }
                }

                if (rol != "undefined")
                {
                    ViewBag.estatus = query.agregar_usuario(nombre,
                                        email,
                                        password,
                                        telefono,
                                        rol);

                    Session["Usuarios"] = query.buscar_usuarios();

                    return PartialView("Usuarios");
                }
            }

            return PartialView("Usuarios");

        }

        [HttpGet]
        public ActionResult Proveedores()
        {
            LO_ProveedoresController query = new LO_ProveedoresController();

            Session["Proveedores"] = query.buscar_proveedores();

            Session["Busqueda_Proveedor"] = null;

            return PartialView("Proveedores");
        }

        [HttpPost]
        public ActionResult buscar_proveedor(String nombre)
        {
            LO_ProveedoresController query = new LO_ProveedoresController();

            var proveedor = query.buscar_proveedor(nombre);

            if (proveedor != null)
            {
                ProveedorContacto datos = (ProveedorContacto)proveedor;

                Proveedores datos_proveedor = datos.Proveedor;
                Contacto_proveedor datos_contacto = datos.Contacto;


                String nomEmpresa = datos_proveedor.nombreEmpresaProv;
                String dirEmpresa = datos_proveedor.direccionEmpresaProv;
                decimal telEmpresa = (decimal)datos_proveedor.telefonoEmpresaProv;

                String nomContacto = datos_contacto.nombreCon;
                String emailContacto = datos_contacto.emailCon;
                decimal telContacto = (decimal)datos_contacto.telefonoCon;

                Session["Busqueda_Proveedor"] = datos_proveedor.nombreEmpresaProv;

                return Json(new { nomEmpresa = nomEmpresa, dirEmpresa = dirEmpresa, telEmpresa = telEmpresa, nomContacto = nomContacto, emailContacto = emailContacto, telContacto = telContacto });
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult eliminar_proveedor(String nombre)
        {
            LO_ProveedoresController query = new LO_ProveedoresController();

            ViewBag.estatus = query.eliminar_proveedor(nombre);

            Session["Proveedores"] = query.buscar_proveedores();

            return PartialView("Proveedores");
        }

        [HttpGet]
        public ActionResult Proveedores_formulario(String nombre_empresa,
                                        String direccion_empresa,
                                        decimal telefono_empresa,
                                        String nombre_contacto,
                                        decimal telefono_contacto,
                                        String email_contacto)
        {
            LO_ProveedoresController query = new LO_ProveedoresController();

            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                bool vacia = !db.Proveedores.Any();

                if (!vacia)
                {

                    if (Session["Busqueda_Proveedor"] != null)
                    {

                        ViewBag.estatus = query.editar_proveedor(Session["Busqueda_Proveedor"].ToString(), nombre_empresa, direccion_empresa, telefono_empresa, nombre_contacto, telefono_contacto, email_contacto);

                        Session["Busqueda_Proveedor"] = null;

                        Session["Proveedores"] = query.buscar_proveedores();

                        return PartialView("Proveedores");
                    }
                }

                ViewBag.estatus = query.agregar_proveedor(nombre_empresa, direccion_empresa, telefono_empresa, nombre_contacto, telefono_contacto, email_contacto);

                Session["Proveedores"] = query.buscar_proveedores();

                return PartialView("Proveedores");
            }

        }

        [HttpGet]
        public ActionResult Articulos()
        {
            LO_ArticulosController query = new LO_ArticulosController();
            LO_ProveedoresController queryDis = new LO_ProveedoresController();

            Session["Articulos"] = query.buscar_articulos();

            Session["Distribuidores"] = queryDis.buscar_proveedores();

            Session["Busqueda_Articulo"] = null;

            return PartialView("Articulos");
        }

        [HttpPost]
        public ActionResult Articulos_formulario(String nombre,
                                      decimal precio,
                                      decimal precioDis,
                                      decimal precioCon,
                                      decimal stockMin,
                                      decimal stock,
                                      String nombreDis,
                                      HttpPostedFileBase[] fotoArt)
        {

            LO_ArticulosController query = new LO_ArticulosController();

            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                bool vacia = !db.Articulos.Any();

                String rutaFoto = (nombre.ToLower()).Replace(" ", "");

                Boolean estatus;

                if (!vacia)
                {

                    if (Session["Busqueda_Articulo"] != null)
                    {
                        if (fotoArt[0] != null && fotoArt[0].ContentLength > 0)
                        {
                            String rutaFoto_nueva = (nombre.ToLower()).Replace(" ", "");
                            String rutaFoto_vieja = (Session["Busqueda_Articulo"].ToString().ToLower()).Replace(" ", "");

                            estatus = query.editar_imagen(rutaFoto_vieja, rutaFoto_nueva, fotoArt[0]);

                        }
                        else
                        {
                            String rutaFoto_nueva = (nombre.ToLower()).Replace(" ", "");
                            String rutaFoto_vieja = (Session["Busqueda_Articulo"].ToString().ToLower()).Replace(" ", "");

                            estatus = query.editar_imagen(rutaFoto_vieja, rutaFoto_nueva);
                        }

                        if (!estatus)
                        {
                            return PartialView("Articulos");
                        }

                        ViewBag.estatus = query.editar_articulo(Session["Busqueda_Articulo"].ToString(), precio, nombre, precioDis, precioCon, stockMin, stock, rutaFoto, nombreDis);

                        Session["Busqueda_Articulo"] = null;

                        Session["Articulos"] = query.buscar_articulos();

                        return PartialView("Articulos");

                    }
                }

                estatus = query.subir_foto(fotoArt[0], rutaFoto);

                if (estatus)
                {
                    ViewBag.estatus = query.agregar_articulo(precio, nombre, precioDis, precioCon, stockMin, stock, rutaFoto, nombreDis);
                }

                Session["Articulos"] = query.buscar_articulos();

                return PartialView("Articulos");
            }
        }

        [HttpGet]
        public ActionResult eliminar_articulo(String nombre)
        {
            LO_ArticulosController query = new LO_ArticulosController();

            ViewBag.estatus = query.eliminar_articulo(nombre);

            Session["Articulos"] = query.buscar_articulos();

            return PartialView("Articulos");
        }

        public ActionResult buscar_articulo(String nombre)
        {
            LO_ArticulosController query = new LO_ArticulosController();
            LO_ProveedoresController queryDis = new LO_ProveedoresController();

            var articulo = query.buscar_articulo(nombre);

            if (articulo != null)
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    ArticuloProveedor datos = (ArticuloProveedor)articulo;


                    decimal precioArt = (decimal)datos.Articulo.precioArt;
                    String nombreArt = datos.Articulo.nombreArt;
                    decimal precioDis = (decimal)datos.Articulo.precioDistribuidorArt;
                    decimal precioCom = (decimal)datos.Articulo.precioComercialArt;
                    decimal stockMin = (decimal)datos.Articulo.stockMinimoArt;
                    decimal stockArt = (decimal)datos.Articulo.stockArt;
                    String fotoArt = datos.Articulo.fotoArt;

                    Proveedores Dis = (from proveedor in db.Proveedores
                                       where proveedor.idProv == datos.Distribuidor.idProv
                                       select proveedor).FirstOrDefault();

                    String nombreDis = Dis.nombreEmpresaProv;

                    Session["Busqueda_Articulo"] = nombreArt;

                    return Json(new { nombreArt = nombreArt, precioArt = precioArt, precioDis = precioDis, precioCom = precioCom, stockMin = stockMin, stockArt = stockArt, fotoArt = fotoArt, nombreDis = nombreDis });
                }
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult Productos()
        {
            LO_ProductosController query = new LO_ProductosController();

            Session["Productos"] = query.buscar_productos();

            Session["Categorias"] = query.buscar_categoria();

            Session["Componentes"] = null;

            return PartialView("Productos");
        }

        [HttpPost]
        public ActionResult Productos(HttpPostedFileBase[] fotoArt,
                                      String nombrePro,
                                      String nombreCla,
                                      String nombreCat)
        {
            try
            {

                if (Session["Componentes"] == null || nombreCla == "0" || nombreCat == "0" || fotoArt.Length != 4)
                {
                    return PartialView("Productos");
                }

                LO_ProductosController query = new LO_ProductosController();

                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    String rutaFoto = (nombrePro.ToLower()).Replace(" ", "");

                    Boolean estatus;

                    estatus = query.subir_fotos(fotoArt, rutaFoto);

                    if (estatus)
                    {
                        List<Componentes_producto> componentes = (List<Componentes_producto>)Session["Componentes"];

                        decimal? idPro_promesa = db.Producto.Max(t => (decimal?)t.idPro);

                        decimal idPro = 0;

                        if (idPro_promesa.HasValue)
                        {
                            idPro = idPro_promesa.Value + 1;
                        }

                        for (int i = 0; i < componentes.Count; i++)
                        {
                            componentes[i].idPro = idPro;
                        }

                        String ruta_imagen = "https://localhost:44394/Galeria/Productos/" + rutaFoto + "1.png";

                        String descripcionPro = query.generar_descripcion(componentes);

                        ViewBag.estatus = query.agregar_producto(ruta_imagen, nombrePro, descripcionPro, nombreCat, nombreCla);

                        ViewBag.estatus = query.agregar_componentes(componentes);

                    }

                    Session["Productos"] = query.buscar_productos();

                    return PartialView("Productos");
                }
            }
            catch (Exception)
            {
                return PartialView("Productos");
            }
        }

        [HttpGet]
        public ActionResult eliminar_producto(String nombrePro)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto producto = (from prod in db.Producto
                                         where prod.nombrePro == nombrePro
                                         select prod).FirstOrDefault();

                    Componentes_producto[] componentes = (Componentes_producto[])(from comp in db.Componentes_producto
                                                                                  where comp.idPro == producto.idPro
                                                                                  select comp).ToArray();

                    for (int i = 0; i < componentes.Length; i++)
                    {
                        db.Componentes_producto.Remove(componentes[i]);
                    }

                    LO_ProductosController query = new LO_ProductosController();

                    ViewBag.estatus = query.eliminar_imagenes(nombrePro);

                    db.Producto.Remove(producto);

                    db.SaveChanges();

                    Session["Productos"] = query.buscar_productos();

                    Session["Componentes"] = null;

                }

                return PartialView("Productos");
            }
            catch (Exception)
            {
                return PartialView("Productos");
            }
        }

        public ActionResult agregar_componente(String nombre,
                                               decimal cantidad)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulo_buscar = (from articulo in db.Articulos
                                                 where articulo.nombreArt == nombre
                                                 select articulo).FirstOrDefault();

                    Componentes_producto componente = new Componentes_producto();
                    componente.idArt = articulo_buscar.idArt;
                    componente.cantidadComPro = cantidad;

                    List<Componentes_producto> componentes = new List<Componentes_producto>();

                    if (Session["Componentes"] != null)
                    {
                        componentes = (List<Componentes_producto>)Session["Componentes"];

                        int indice = componentes.FindIndex(c => c.idArt == articulo_buscar.idArt);

                        if (indice != -1)
                        {
                            return Json(new { });
                        }
                        else
                        {
                            componentes.Add(componente);

                            Session["Componentes"] = componentes;

                            return Json(new { fotoArt = articulo_buscar.fotoArt, nombreArt = articulo_buscar.nombreArt, precioArt = (((decimal)articulo_buscar.precioArt) * cantidad), cantidad = cantidad });
                        }
                    }

                    componentes.Add(componente);

                    Session["Componentes"] = componentes;

                    return Json(new { fotoArt = articulo_buscar.fotoArt, nombreArt = articulo_buscar.nombreArt, precioArt = (((decimal)articulo_buscar.precioArt) * cantidad), cantidad = cantidad });

                }
            }
            catch (Exception ex)
            {

                return Json(new { });
            }
        }

        public ActionResult eliminar_componente(String nombre)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulo_buscar = (from articulo in db.Articulos
                                                 where articulo.nombreArt == nombre
                                                 select articulo).FirstOrDefault();

                    List<Componentes_producto> componentes;

                    if (Session["Componentes"] != null)
                    {
                        componentes = (List<Componentes_producto>)Session["Componentes"];

                        int indice = componentes.FindIndex(c => c.idArt == articulo_buscar.idArt);

                        if (indice != -1)
                        {
                            componentes.RemoveAt(indice);

                            Session["Componentes"] = componentes;

                            return Json(new { estatus = "Se elimino con exito" });
                        }
                        else
                        {
                            return Json(new { estatus = "Ocurrio algun error" });
                        }
                    }

                    return Json(new { estatus = "Ocurrio algun error" });

                }
            }
            catch (Exception ex)
            {

                return Json(new { });
            }
        }

        public ActionResult buscar_articulos(String cadena)
        {
            try
            {
                LO_BuscadorController query = new LO_BuscadorController();

                String[] resultados = query.buscador_articulos(cadena);

                return Json(new { resultados = resultados });

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult generar_descripcion()
        {
            try
            {

                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    ProductoClaseCategoria[] componentes = (ProductoClaseCategoria[])Session["Productos"];

                    String[] descripciones = new string[componentes.Length];

                    for (int i = 0; i < componentes.Length; i++)
                    {

                        descripciones[i] = componentes[i].Producto.descripcionPro;

                    }

                    return Json(new { descripciones = descripciones });
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult mostrar_informacion(String nombre,
                                                decimal cantidad)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulos = (from articulo in db.Articulos
                                           where articulo.nombreArt == nombre
                                           select articulo).FirstOrDefault();

                    if (Session["Componentes"] != null)
                    {
                        List<Componentes_producto> componentes = (List<Componentes_producto>)Session["Componentes"];

                        decimal idart = (from articulo in db.Articulos
                                         where articulo.nombreArt == nombre
                                         select articulo.idArt).SingleOrDefault();

                        int componente = componentes.FindIndex(c => c.idArt == idart);

                        if (componente >= 0)
                        {
                            componentes[componente].cantidadComPro = cantidad;
                        }
                    }

                    if (articulos != null)
                    {
                        return Json(new { fotoArt = articulos.fotoArt, nombreArt = articulos.nombreArt, precioArt = (((decimal)articulos.precioArt) * cantidad) });
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult buscar_clases(String categoria)
        {
            try
            {
                LO_ProductosController query = new LO_ProductosController();

                String[] clases = query.buscar_clases(categoria);

                return Json(new { clases = clases });
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpGet]
        public ActionResult Ventas()
        {

            Session["Venta"] = null;

            return PartialView("Ventas");
        }

        public ActionResult buscar_articulos_venta(String cadena)
        {
            try
            {
                LO_BuscadorController query = new LO_BuscadorController();

                String[] resultados = query.buscador_articulos(cadena);

                return Json(new { resultados = resultados });

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult mostrar_informacion_venta(String nombre,
                                                      decimal cantidad)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulos = (from articulo in db.Articulos
                                           where articulo.nombreArt == nombre
                                           select articulo).FirstOrDefault();

                    if (Session["Venta"] != null)
                    {
                        List<Componentes_producto> componentes = (List<Componentes_producto>)Session["Venta"];

                        decimal idart = (from articulo in db.Articulos
                                         where articulo.nombreArt == nombre
                                         select articulo.idArt).SingleOrDefault();

                        int componente = componentes.FindIndex(c => c.idArt == idart);

                        if (componente >= 0)
                        {
                            componentes[componente].cantidadComPro = cantidad;
                        }
                    }

                    if (articulos != null)
                    {
                        return Json(new { fotoArt = articulos.fotoArt, nombreArt = articulos.nombreArt, precioArt = (((decimal)articulos.precioArt) * cantidad) });
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult agregar_componente_venta(String nombre,
                                                     decimal cantidad)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulo_buscar = (from articulo in db.Articulos
                                                 where articulo.nombreArt == nombre
                                                 select articulo).FirstOrDefault();

                    Componentes_producto componente = new Componentes_producto();
                    componente.idArt = articulo_buscar.idArt;
                    componente.cantidadComPro = cantidad;

                    List<Componentes_producto> componentes = new List<Componentes_producto>();

                    if (Session["Venta"] != null)
                    {
                        componentes = (List<Componentes_producto>)Session["Venta"];

                        int indice = componentes.FindIndex(c => c.idArt == articulo_buscar.idArt);

                        if (indice != -1)
                        {
                            return Json(new { });
                        }
                        else
                        {
                            componentes.Add(componente);

                            Session["Venta"] = componentes;

                            return Json(new { fotoArt = articulo_buscar.fotoArt, nombreArt = articulo_buscar.nombreArt, precioArt = (((decimal)articulo_buscar.precioArt) * cantidad), cantidad = cantidad });
                        }
                    }

                    componentes.Add(componente);

                    Session["Venta"] = componentes;

                    return Json(new { fotoArt = articulo_buscar.fotoArt, nombreArt = articulo_buscar.nombreArt, precioArt = (((decimal)articulo_buscar.precioArt) * cantidad), cantidad = cantidad });

                }
            }
            catch (Exception ex)
            {

                return Json(new { });
            }
        }

        public ActionResult eliminar_componente_venta(String nombre)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Articulos articulo_buscar = (from articulo in db.Articulos
                                                 where articulo.nombreArt == articulo.nombreArt
                                                 select articulo).FirstOrDefault();

                    List<Componentes_producto> componentes;

                    if (Session["Venta"] != null)
                    {
                        componentes = (List<Componentes_producto>)Session["Venta"];

                        int indice = componentes.FindIndex(c => c.idArt == articulo_buscar.idArt);

                        if (indice != -1)
                        {
                            componentes.RemoveAt(indice);

                            Session["Venta"] = componentes;

                            return Json(new { estatus = "Se elimino con exito" });
                        }
                        else
                        {
                            return Json(new { estatus = "Ocurrio algun error" });
                        }
                    }

                    return Json(new { estatus = "Ocurrio algun error" });

                }
            }
            catch (Exception ex)
            {

                return Json(new { });
            }
        }

        public ActionResult calcular_precio_venta()
        {
            try
            {
                LO_VentasController query = new LO_VentasController();

                double total = Convert.ToDouble(query.calcular_total_venta((List<Componentes_producto>)Session["Venta"]));

                double subtotal = total - (total * 0.16);

                double envio = 0.0;

                double impuestos = total * 0.16;

                total = Math.Round(total, 2);
                subtotal = Math.Round(subtotal, 2);
                envio = Math.Round(envio, 2);
                impuestos = Math.Round(impuestos, 2);

                return Json(new { estatus = 1, total = total, subtotal = subtotal, envio = envio, impuestos = impuestos });
            }
            catch (Exception)
            {

                return Json(new { estatus = 0 });
            }
        }

        [HttpGet]
        public ActionResult realizar_venta()
        {
            try
            {
                String email = "";

                if (Session["UsuarioActual"] != null)
                {
                    email = (string)Session["UsuarioActual"];
                }

                LO_VentasController query = new LO_VentasController();

                decimal total = query.calcular_total_venta((List<Componentes_producto>)Session["Venta"]);

                String estatus = query.registrar_venta(email, total);

                return PartialView("Ventas");
            }
            catch (Exception)
            {
                return PartialView("Ventas");
            }
        }

        public ActionResult Pedidos()
        {
            LO_PedidosController query = new LO_PedidosController();

            Session["Pedidos"] = query.buscar_pedidos();

            return PartialView("Pedidos");
        }

        public ActionResult finalizar_factura(decimal idFac)
        {
            LO_PedidosController query = new LO_PedidosController();

            String estatus = query.finalizar_factura(idFac);

            return Json(new { estatus = estatus });
        }

        public ActionResult cancelar_factura(decimal idFac)
        {
            LO_PedidosController query = new LO_PedidosController();

            String estatus = query.cancelar_factura(idFac);

            return Json(new { estatus = estatus });
        }

        public ActionResult filtrar_pedidos(String filtro)
        {
            decimal idFiltro = -1;

            switch (filtro)
            {
                case "Todos": idFiltro = -1;
                    break;
                case "Cancelado": idFiltro = 2; 
                    break;
                case "Finalizado": idFiltro = 1; 
                    break;
                case "En proceso": idFiltro = 0;
                    break;
            }

            LO_PedidosController query = new LO_PedidosController();

            if (idFiltro == -1)
            {
                Session["Pedidos"] = query.buscar_pedidos();
            }
            else
            {
                Session["Pedidos"] = query.filtrar_factura(idFiltro);
            }

            Session["Filtro"] = filtro;

            return PartialView("Pedidos");
        }

    }
}