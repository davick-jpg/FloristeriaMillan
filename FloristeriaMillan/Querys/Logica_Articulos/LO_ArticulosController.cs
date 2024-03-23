using FloristeriaMillan.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Articulos
{
    public class LO_ArticulosController : Controller
    {
        // GET: LO_Articulos
        public ArticulosProveedores[] buscar_articulos()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_articulos = from articulos in db.Articulos
                                      join cartera in db.Cartera_proveedores on articulos.idArt equals cartera.idArt
                                      join distribuidor in db.Proveedores on cartera.idProv equals distribuidor.idProv
                                      select new ArticulosProveedores
                                      {
                                          Articulo = articulos,
                                          Proveedor = distribuidor
                                      };

                return lista_articulos.ToArray();
            }
        }

        public ArticuloProveedor buscar_articulo(String nombreArt)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var datos_articulo = (from articulo in db.Articulos
                                      join distribuidor in db.Cartera_proveedores on articulo.idArt equals distribuidor.idArt
                                      where articulo.nombreArt == nombreArt
                                      select new ArticuloProveedor
                                      {
                                          Articulo = articulo,
                                          Distribuidor = distribuidor
                                      }).FirstOrDefault();

                return datos_articulo;
            }
        }

        public String eliminar_articulo(String nombreArt)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    Boolean estatus = eliminar_imagen(nombreArt);

                    if (!estatus)
                    {
                        return "Se ha producido un error";
                    }

                    var articulo_eliminar = (from articulo in db.Articulos
                                             where articulo.nombreArt == nombreArt
                                             select articulo).FirstOrDefault();

                    String estatus_cartera = eliminar_cartera(articulo_eliminar.idArt);

                    db.Articulos.Remove(articulo_eliminar);

                    db.SaveChanges();

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error" + ex;
                }

            }
        }

        public String eliminar_cartera(decimal idArt)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var cartera_eliminar = (from cartera in db.Cartera_proveedores
                                            where cartera.idArt == idArt
                                            select cartera).FirstOrDefault();

                    db.Cartera_proveedores.Remove(cartera_eliminar);

                    db.SaveChanges();

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error";
                }

            }
        }

        public Boolean eliminar_imagen(String nombreArt)
        {
            string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Articulos/"), (nombreArt.Trim().ToLower()).Replace(" ","") + ".png"); ;

            try
            {
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public String agregar_articulo(decimal precioArt,
                                      String nombreArt,
                                      decimal precioDistribuidorArt,
                                      decimal precioComercialArt,
                                      decimal stockMinimoArt,
                                      decimal stackArt,
                                      String fotoArt,
                                      String nombreDistribuidor)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    decimal? idArt_promesa = db.Articulos.Max(t => (decimal?)t.idArt);

                    decimal idArt = 0;

                    if (idArt_promesa.HasValue)
                    {
                        idArt = idArt_promesa.Value + 1;
                    }

                    Articulos articulo = new Articulos();
                    articulo.idArt = idArt;
                    articulo.nombreArt = nombreArt;
                    articulo.precioDistribuidorArt = precioDistribuidorArt;
                    articulo.precioComercialArt = precioComercialArt;
                    articulo.stockMinimoArt = stockMinimoArt;
                    articulo.stockArt = stackArt;
                    articulo.precioArt = precioArt;

                    String ruta_imagen = "https://localhost:44394/Galeria/Articulos/" + fotoArt + ".png";

                    articulo.fotoArt = ruta_imagen;

                    db.Articulos.Add(articulo);

                    db.SaveChanges();

                    Boolean estatus = definir_distribuidor(nombreArt, nombreDistribuidor);

                    return "Se ha registrado con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error " + ex;
                }

            }
        }

        public Boolean subir_foto(HttpPostedFileBase fotoArt, String nombreArt)
        {

            try
            {
                if (fotoArt != null && fotoArt.ContentLength > 0)
                {
                    string nombreFoto = Path.GetFileName(nombreArt + ".png");
                    string rutaFoto = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Articulos/"), nombreFoto);
                    fotoArt.SaveAs(rutaFoto);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean definir_distribuidor(String nombreArt, String nombreDistribuidor)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal? idCar_promesa = db.Cartera_proveedores.Max(t => (decimal?)t.idCar);

                    decimal idCar = 0;

                    if (idCar_promesa.HasValue)
                    {
                        idCar = idCar_promesa.Value + 1;
                    }

                    var idArt = (from articulo in db.Articulos
                                 where articulo.nombreArt == nombreArt
                                 select articulo.idArt).FirstOrDefault();

                    var idDis = (from proveedor in db.Proveedores
                                 where proveedor.nombreEmpresaProv == nombreDistribuidor
                                 select proveedor.idProv).FirstOrDefault();

                    Cartera_proveedores cartera = new Cartera_proveedores();
                    cartera.idCar = idCar;
                    cartera.idArt = idArt;
                    cartera.idProv = idDis;

                    db.Cartera_proveedores.Add(cartera);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }

        public String editar_articulo(String nombre_editar,
                                     decimal precioArt,
                                     String nombreArt,
                                     decimal precioDistribuidorArt,
                                     decimal precioComercialArt,
                                     decimal stockMinimoArt,
                                     decimal stackArt,
                                     String fotoArt,
                                     String nombreDistribuidor)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var articulo = db.Articulos.SingleOrDefault(t => t.nombreArt == nombre_editar);

                    if (articulo == null)
                    {
                        return "Se ha producido un error";
                    }

                    articulo.nombreArt = nombreArt;
                    articulo.precioDistribuidorArt = precioDistribuidorArt;
                    articulo.precioComercialArt = precioComercialArt;
                    articulo.stockMinimoArt = stockMinimoArt;
                    articulo.stockArt = stackArt;

                    String ruta_imagen = "https://localhost:44394/Galeria/Articulos/" + fotoArt + ".png";

                    articulo.fotoArt = ruta_imagen;

                    db.SaveChanges();

                    return "Se ha registrado con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error " + ex;
                }

            }
        }

        public Boolean editar_imagen(String nombre_antiguo, String nombre_nuevo, HttpPostedFileBase fotoArt)
        {

            try
            {
                if (fotoArt != null && fotoArt.ContentLength > 0)
                {
                    string nombreFoto = Path.GetFileName(nombre_nuevo);
                    string rutaFoto = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Articulos"), nombreFoto);
                    fotoArt.SaveAs(rutaFoto);

                    string rutaArchivoActual = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_nuevo + ".png");
                    string rutaArchivoNuevo = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_antiguo + ".png");

                    if (System.IO.File.Exists(rutaArchivoActual))
                    {
                        System.IO.FileInfo archivo = new System.IO.FileInfo(rutaArchivoActual);
                        archivo.MoveTo(rutaArchivoNuevo);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean editar_imagen(String nombre_antiguo, String nombre_nuevo)
        {

            try
            {
                string rutaActual = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_antiguo + ".png");
                string nuevaRuta = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_nuevo + ".png");

                System.IO.File.Move(rutaActual, nuevaRuta);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}