using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Productos
{
    public class LO_ProductosController : Controller
    {
        public ProductoClaseCategoria[] buscar_productos()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_productos = from productos in db.Producto
                                      join clase in db.Clases on productos.idCla equals clase.idCla
                                      join categoria in db.Categorias on productos.idCat equals categoria.idCat
                                      select new ProductoClaseCategoria
                                      {
                                          Producto = productos,
                                          Clase = clase,
                                          Categoria = categoria
                                      };

                return lista_productos.ToArray();
            }
        }

        public String[] buscar_clases(String nombreCat)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_clases = from clases in db.Clases
                                   join categoria in db.Categorias on clases.idCat equals categoria.idCat
                                   where categoria.nombreCat == nombreCat
                                   select clases.nombreCla;

                return lista_clases.ToArray();
            }
        }

        public Categorias[] buscar_categoria()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_categoria = from categoria in db.Categorias
                                      select categoria;

                return lista_categoria.ToArray();
            }
        }

        public ComponentesProductoClaseCategoria buscar_producto(String nombrePro)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var datos_producto = (from producto in db.Producto
                                      join clase in db.Clases on producto.idCla equals clase.idCla
                                      join categoria in db.Categorias on producto.idCat equals categoria.idCat
                                      join componentes in db.Componentes_producto on producto.idPro equals componentes.idPro into joinedData
                                      where producto.nombrePro == nombrePro
                                      select new ComponentesProductoClaseCategoria
                                      {
                                          Producto = producto,
                                          Clase = clase,
                                          Categoria = categoria,
                                          Componentes = (Componentes_producto[])joinedData.ToArray()
                                      }).FirstOrDefault();

                return datos_producto;
            }
        }

        public String eliminar_producto(String nombrePro)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    Boolean estatus = eliminar_imagenes(nombrePro);

                    if (!estatus)
                    {
                        return "Se ha producido un error";
                    }

                    var producto_eliminar = (from producto in db.Producto
                                             where producto.nombrePro == nombrePro
                                             select producto).FirstOrDefault();

                    String estatus_cartera = eliminar_componentes(producto_eliminar.idPro);

                    db.Producto.Remove(producto_eliminar);

                    db.SaveChanges();

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error" + ex;
                }

            }
        }

        public String eliminar_componentes(decimal idPro)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var componentes_eliminar = (from componentes in db.Componentes_producto
                                                where componentes.idPro == idPro
                                                select componentes);

                    for (int i = 0; i < componentes_eliminar.ToArray().Length; i++)
                    {
                        db.Componentes_producto.Remove(componentes_eliminar.ToArray()[i]);

                        db.SaveChanges();
                    }

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error";
                }

            }
        }

        public Boolean eliminar_imagenes(String nombrePro)
        {
            Boolean estatus = false;

            nombrePro = nombrePro.Trim().ToLower().Replace(" ", "");

            for (int i = 0; i < 4; i++)
            {
                string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Productos/"), nombrePro + i + ".png"); ;

                try
                {
                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                        estatus = true;
                    }
                    else
                    {
                        estatus = false;
                    }
                }
                catch (Exception)
                {
                    estatus = false;
                }
            }

            return estatus;
        }

        public String agregar_producto(String fotoPro,
                                      String nombrePro,
                                      String descripcionPro,
                                      String nombreCat,
                                      String nombreCla)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    decimal? idPro_promesa = db.Producto.Max(t => (decimal?)t.idPro);

                    decimal idPro = 0;

                    if (idPro_promesa.HasValue)
                    {
                        idPro = idPro_promesa.Value + 1;
                    }

                    decimal idCat = (from categoria in db.Categorias
                                     where categoria.nombreCat == nombreCat
                                     select categoria.idCat).SingleOrDefault();

                    decimal idCla = (from clase in db.Clases
                                     where clase.nombreCla == nombreCla
                                     select clase.idCat).SingleOrDefault();

                    Producto producto = new Producto();
                    producto.idPro = idPro;
                    producto.nombrePro = nombrePro;
                    producto.descripcionPro = descripcionPro;
                    producto.idCat = idCat;
                    producto.idCla = idCla;
                    producto.fotoPro = fotoPro;

                    db.Producto.Add(producto);

                    db.SaveChanges();

                    return "Se ha registrado con exito";
                }
                catch (DbEntityValidationException ex)
                {
                    String error = "";

                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            error = $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                        }
                    }

                    return error;
                }

            }
        }

        public String agregar_componentes(List<Componentes_producto> lista_componentes)
        {

            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    for (int i = 0; i < lista_componentes.Count; i++)
                    {
                        Componentes_producto componente = lista_componentes[i];

                        decimal? idCom_promesa = db.Componentes_producto.Max(t => (decimal?)t.idComPro);

                        decimal idcom = 0;

                        if (idCom_promesa.HasValue)
                        {
                            idcom = idCom_promesa.Value + 1;
                        }

                        componente.idComPro = idcom;

                        db.Componentes_producto.Add(componente);

                        db.SaveChanges();

                    }

                    return "Se ha registrado con exito";
                }
            }
            catch (Exception ex)
            {
                return "Se ha producido un error " + ex;
            }
        }

        public Boolean subir_fotos(HttpPostedFileBase[] fotoArt, String nombreArt)
        {

            try
            {
                for (int i = 0; i < fotoArt.Length; i++)
                {
                    if (fotoArt[i] != null && fotoArt[i].ContentLength > 0)
                    {
                        string nombreFoto = Path.GetFileName(nombreArt + i + ".png");
                        string rutaFoto = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Productos/"), nombreFoto);
                        fotoArt[i].SaveAs(rutaFoto);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public String editar_producto(String nombre_editar,
                                      String fotoPro,
                                      String nombrePro,
                                      String descripcionPro,
                                      String nombreCat,
                                      String nombreCla,
                                      decimal precioPro)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var producto = db.Producto.SingleOrDefault(t => t.nombrePro == nombre_editar);

                    if (producto == null)
                    {
                        return "Se ha producido un error";
                    }

                    decimal idCat = (from categoria in db.Categorias
                                     where categoria.nombreCat == nombreCat
                                     select categoria.idCat).SingleOrDefault();

                    decimal idCla = (from clase in db.Clases
                                     where clase.nombreCla == nombreCla
                                     select clase.idCat).SingleOrDefault();

                    producto.nombrePro = nombrePro;
                    producto.descripcionPro = descripcionPro;
                    producto.idCat = idCat;
                    producto.idCla = idCla;
                    producto.precioPro = precioPro;

                    String ruta_imagen = "https://localhost:44394/Galeria/Articulos/" + fotoPro + ".png";

                    producto.fotoPro = ruta_imagen;

                    db.SaveChanges();

                    return "Se ha registrado con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error " + ex;
                }

            }
        }

        public Boolean editar_imagen(String nombre_antiguo, String nombre_nuevo, HttpPostedFileBase[] fotoArt)
        {

            try
            {
                for (int i = 0; i < fotoArt.Length; i++)
                {
                    if (fotoArt != null && fotoArt[i].ContentLength > 0)
                    {
                        string nombreFoto = Path.GetFileName(nombre_nuevo + i);
                        string rutaFoto = Path.Combine(HostingEnvironment.MapPath("~/Galeria/Articulos"), nombreFoto);
                        fotoArt[i].SaveAs(rutaFoto);

                        string rutaArchivoActual = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_nuevo + ".png");
                        string rutaArchivoNuevo = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_antiguo + i + ".png");

                        if (System.IO.File.Exists(rutaArchivoActual))
                        {
                            System.IO.FileInfo archivo = new System.IO.FileInfo(rutaArchivoActual);
                            archivo.MoveTo(rutaArchivoNuevo);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
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

                for (int i = 0; i < 4; i++)
                {
                    string rutaActual = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_antiguo + i + ".png");
                    string nuevaRuta = HostingEnvironment.MapPath("~/Galeria/Articulos/" + nombre_nuevo + i + ".png");

                    System.IO.File.Move(rutaActual, nuevaRuta);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string generar_descripcion(List<Componentes_producto> componentes)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    string texto = "Contiene: \n";

                    String nombrePro;

                    for (int i = 0; i < componentes.Count; i++)
                    {
                        Componentes_producto componente = (Componentes_producto)componentes[i];

                        nombrePro = (from articulo in db.Articulos
                                     where articulo.idArt == componente.idArt
                                     select articulo.nombreArt).FirstOrDefault();

                        texto = texto + "• x" + componentes[i].cantidadComPro + " " + nombrePro + "\n";
                    }

                    return texto;
                }
            }
            catch (Exception ex)
            {
                return "" + ex;
            }
        }
    }
}