using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Home
{
    public class LO_HomeController : Controller
    {
        // GET: LO_Home
        public Producto buscar_producto(String nombrePro)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto producto = (from producto_buscar in db.Producto
                                         where producto_buscar.nombrePro == nombrePro
                                         select producto_buscar).FirstOrDefault();

                    producto = definir_precio(producto);

                    return producto;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Categorias buscar_categoria(String nombreCat)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Categorias categoria = (from categoria_buscar in db.Categorias
                                            where categoria_buscar.nombreCat == nombreCat
                                            select categoria_buscar).FirstOrDefault();

                    return categoria;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Producto[] buscar_favoritos(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal idCli = (from usuario in db.Usuarios
                                     where usuario.emailCli == emailCli
                                     select usuario.idUsu).FirstOrDefault();

                    Producto[] favoritos = (from favorito in db.Favoritos
                                            join producto in db.Producto on favorito.idPro equals producto.idPro
                                            where favorito.idUsu == idCli
                                            select producto).ToArray();

                    return favoritos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Clases[] buscar_clases(String nombreCat)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Clases[] clases = (from clase in db.Clases
                                       join categoria in db.Categorias on clase.idCat equals categoria.idCat
                                       where categoria.nombreCat == nombreCat
                                       select clase).ToArray();

                    return clases;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Producto[] buscar_recomendados()
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto[] productos = (from Producto in db.Producto
                                            select Producto).ToArray();

                    productos = definir_precios(productos);

                    return productos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Producto[] buscar_articulos_recomendados(String nombrePro)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal idCla = (from producto in db.Producto
                                     where producto.nombrePro == nombrePro
                                     select producto.idCla).FirstOrDefault();

                    decimal idCat = (from producto in db.Producto
                                     where producto.nombrePro == nombrePro
                                     select producto.idCat).FirstOrDefault();

                    Producto[] productos = (from producto in db.Producto
                                            where producto.idCat == idCat && producto.idCla == idCla && producto.nombrePro != nombrePro
                                            select producto).Take(3).ToArray();

                    productos = definir_precios(productos);

                    return productos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Producto[] buscar_novedad()
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto[] productos = (from Producto in db.Producto
                                            select Producto).ToArray();

                    productos = definir_precios(productos);

                    return productos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Producto[] buscar_catalogo(Categorias categoria)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto[] catalogo = (from productos in db.Producto
                                           where productos.idCat == categoria.idCat
                                           select productos).ToArray();

                    catalogo = definir_precios(catalogo);

                    return catalogo;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Producto[] definir_precios(Producto[] productos)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    for (int i = 0; i < productos.Length; i++)
                    {
                        Producto producto = productos[i];

                        var query = from componente in db.Componentes_producto
                                    join articulo in db.Articulos on componente.idArt equals articulo.idArt
                                    where componente.idPro == producto.idPro
                                    select new
                                    {
                                        componente.cantidadComPro,
                                        articulo.precioArt
                                    };

                        var componentes = query.ToArray();

                        decimal precio = 0;

                        decimal sub_precio;

                        for (int j = 0; j < componentes.Length; j++)
                        {

                            sub_precio = 0;

                            sub_precio = componentes[j].cantidadComPro.Value * componentes[j].precioArt.Value;

                            precio = precio + sub_precio;

                        }

                        productos[i].precioPro = precio;
                    }

                    return productos;
                }
            }
            catch (Exception ex)
            {

                return productos;
            }
        }

        public Producto definir_precio(Producto productos)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    Producto producto = productos;

                    var query = from componente in db.Componentes_producto
                                join articulo in db.Articulos on componente.idArt equals articulo.idArt
                                where componente.idPro == producto.idPro
                                select new
                                {
                                    componente.cantidadComPro,
                                    articulo.precioArt
                                };

                    var componentes = query.ToArray();

                    decimal precio = 0;

                    decimal sub_precio;

                    for (int j = 0; j < componentes.Length; j++)
                    {

                        sub_precio = 0;

                        sub_precio = componentes[j].cantidadComPro.Value * componentes[j].precioArt.Value;

                        precio = precio + sub_precio;

                    }

                    productos.precioPro = precio;
                }

                return productos;
            }
            catch (Exception ex)
            {

                return productos;
            }
        }

        public Boolean agregar_favorito(String nombrePro, String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto producto = buscar_producto(nombrePro);

                    Usuarios usuario = (from usuario_buscar in db.Usuarios
                                        where usuario_buscar.emailCli == emailCli
                                        select usuario_buscar).FirstOrDefault();

                    decimal? idFav_promesa = db.Favoritos.Max(t => (decimal?)t.idFav);

                    decimal idFav = 0;

                    if (idFav_promesa.HasValue)
                    {
                        idFav = idFav_promesa.Value + 1;
                    }

                    Favoritos favorito = new Favoritos();

                    favorito.idFav = idFav;
                    favorito.idUsu = usuario.idUsu;
                    favorito.idPro = producto.idPro;

                    db.Favoritos.Add(favorito);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Boolean quitar_favorito(String nombrePro, String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto producto = buscar_producto(nombrePro);

                    Usuarios usuario = (from usuario_buscar in db.Usuarios
                                        where usuario_buscar.emailCli == emailCli
                                        select usuario_buscar).FirstOrDefault();

                    Favoritos favorito = (from favorito_buscar in db.Favoritos
                                          where favorito_buscar.idUsu == usuario.idUsu && favorito_buscar.idPro == producto.idPro
                                          select favorito_buscar).FirstOrDefault();

                    db.Favoritos.Remove(favorito);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Producto[] filtrar_catalogo(String nombreCla, String nombreCat)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto[] productos = (from producto in db.Producto
                                            join categoria in db.Categorias on producto.idCat equals categoria.idCat
                                            join clase in db.Clases on producto.idCla equals clase.idCla
                                            where categoria.nombreCat == nombreCat && clase.nombreCla == nombreCla
                                            select producto).ToArray();

                    productos = definir_precios(productos);

                    return productos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Usuarios buscar_usuario(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    Usuarios usuario = (from usuario_buscar in db.Usuarios
                                        where usuario_buscar.emailCli == emailCli
                                        select usuario_buscar).FirstOrDefault();

                    return usuario;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public String anadir_carrito(String nombrePro, String emailCli, String dedicatoria)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Producto producto = buscar_producto(nombrePro);

                    Usuarios usuario = buscar_usuario(emailCli);

                    Carrito Car_busqueda = (from carrito_buscar in db.Carrito
                                            where carrito_buscar.idPro == producto.idPro && carrito_buscar.idUsu == usuario.idUsu
                                            select carrito_buscar).FirstOrDefault();

                    if (Car_busqueda != null)
                    {
                        return "Se ha producido un error";
                    }

                    decimal? idCar_promesa = db.Carrito.Max(t => (decimal?)t.idCar);

                    decimal idCar = 0;

                    if (idCar_promesa.HasValue)
                    {
                        idCar = idCar_promesa.Value + 1;
                    }

                    Carrito carrito = new Carrito();

                    carrito.idCar = idCar;
                    carrito.idUsu = usuario.idUsu;
                    carrito.idPro = producto.idPro;
                    carrito.DescripcionCar = dedicatoria;
                    carrito.cantidadCar = 1;

                    db.Carrito.Add(carrito);

                    db.SaveChanges();
                }
                return "Se a guardado con exito";
            }
            catch (Exception)
            {

                return "Se a producido algun error";
            }
        }
    }
}