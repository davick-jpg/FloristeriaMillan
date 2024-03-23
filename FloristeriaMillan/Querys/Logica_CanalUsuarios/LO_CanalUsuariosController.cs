using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_CanalUsuarios
{
    public class LO_CanalUsuariosController : Controller
    {
        // GET: LO_CanalUsuarios
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

        public CarritoProducto[] buscar_carrito(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    CarritoProducto[] producto = (from producto_buscar in db.Producto
                                                  join carrito in db.Carrito on producto_buscar.idPro equals carrito.idPro
                                                  join usuario in db.Usuarios on carrito.idUsu equals usuario.idUsu
                                                  where usuario.emailCli == emailCli
                                                  select new CarritoProducto
                                                  {
                                                      carrito = carrito,
                                                      producto = producto_buscar
                                                  }).ToArray();

                    for (int i = 0; i < producto.Length; i++)
                    {
                        producto[i].producto = definir_precio(producto[i].producto);
                    }

                    return producto;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public decimal calcular_precio(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal? cantidad = 0;

                    CarritoProducto[] producto = (from producto_buscar in db.Producto
                                                  join carrito in db.Carrito on producto_buscar.idPro equals carrito.idPro
                                                  join usuario in db.Usuarios on carrito.idUsu equals usuario.idUsu
                                                  where usuario.emailCli == emailCli
                                                  select new CarritoProducto
                                                  {
                                                      carrito = carrito,
                                                      producto = producto_buscar
                                                  }).ToArray();

                    for (int i = 0; i < producto.Length; i++)
                    {
                        producto[i].producto = definir_precio(producto[i].producto);
                    }

                    for (int i = 0; i < producto.Length; i++)
                    {
                        cantidad = cantidad + (producto[i].producto.precioPro * producto[i].carrito.cantidadCar);
                    }

                    return cantidad.Value;
                }
            }
            catch (Exception)
            {
                return 0;
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

                    favoritos = definir_precios(favoritos);

                    return favoritos;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Usuarios editar_usuario(String correo_editar,
                                     String nombre,
                                     String calle,
                                     String colonia,
                                     String municipio,
                                     decimal codigo_postal,
                                     decimal numero)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var usuario = db.Usuarios.SingleOrDefault(t => t.emailCli == correo_editar);

                try
                {
                    usuario.nombreCli = nombre;
                    usuario.calleCli = calle;
                    usuario.coloniaCli = colonia;
                    usuario.municipioCli = municipio;
                    usuario.codigoPostalCli = codigo_postal;
                    usuario.telefonoCli = numero;
                    db.SaveChanges();

                    return usuario;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }

        public DireccionDescripcion[] buscar_direcciones(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    DireccionDescripcion[] direcciones = (from direccion in db.DireccionesCliente
                                                          join usuario in db.Usuarios on direccion.idUsu equals usuario.idUsu
                                                          where usuario.emailCli == emailCli
                                                          select new DireccionDescripcion
                                                          {
                                                              direccion = direccion,
                                                              descripcion = ""
                                                          }).ToArray();

                    for (int i = 0; i < direcciones.Length; i++)
                    {
                        direcciones[i].descripcion = direcciones[i].direccion.calleDir + ", " + direcciones[i].direccion.coloniaDir + ", " + direcciones[i].direccion.municipioDir + ", " + direcciones[i].direccion.codigoPostalCli;
                    }

                    return direcciones;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Boolean agregar_direccion(String emailCli,
                                         String identificador,
                                         String calle,
                                         String colonia,
                                         String municipio,
                                         decimal codigo_postal)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal? idDir_promesa = db.DireccionesCliente.Max(t => (decimal?)t.idDir);

                    decimal idDir = 0;

                    if (idDir_promesa.HasValue)
                    {
                        idDir = idDir_promesa.Value + 1;
                    }

                    Usuarios usuario = buscar_usuario(emailCli);

                    DireccionesCliente direccion = new DireccionesCliente();

                    direccion.idDir = idDir;
                    direccion.idUsu = usuario.idUsu;
                    direccion.nombreDir = identificador;
                    direccion.calleDir = calle;
                    direccion.coloniaDir = colonia;
                    direccion.municipioDir = municipio;
                    direccion.codigoPostalCli = codigo_postal;

                    db.DireccionesCliente.Add(direccion);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Boolean editar_direccion(String emailCli,
                                         String identificador,
                                         String calle,
                                         String colonia,
                                         String municipio,
                                         decimal codigo_postal)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    DireccionesCliente direccion = (from direccion_buscar in db.DireccionesCliente
                                                    join usuario in db.Usuarios on direccion_buscar.idUsu equals usuario.idUsu
                                                    where direccion_buscar.nombreDir == identificador && usuario.emailCli == emailCli
                                                    select direccion_buscar).FirstOrDefault();

                    direccion.calleDir = calle.Trim();
                    direccion.coloniaDir = colonia.Trim();
                    direccion.municipioDir = municipio.Trim();
                    direccion.codigoPostalCli = codigo_postal;

                    Boolean estatus = eliminar_direccion(direccion.nombreDir, emailCli);

                    db.DireccionesCliente.Add(direccion);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DireccionesCliente buscar_direccion(String identificador, String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    DireccionesCliente direccion = (from direccion_buscar in db.DireccionesCliente
                                                    join usuario in db.Usuarios on direccion_buscar.idUsu equals usuario.idUsu
                                                    where direccion_buscar.nombreDir == identificador && usuario.emailCli == emailCli
                                                    select direccion_buscar).FirstOrDefault();

                    return direccion;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Boolean eliminar_direccion(String identificador, String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    DireccionesCliente direccion = buscar_direccion(identificador, emailCli);

                    db.DireccionesCliente.Attach(direccion);

                    db.DireccionesCliente.Remove(direccion);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public Factura registrar_carrito(CarritoProducto[] carrito, String direccion, decimal estatus, String emailCli, Decimal total, String fecha)
        {

            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    DateTime fecha_pedido = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                    decimal? idFac_promesa = db.Factura.Max(t => (decimal?)t.idFac);

                    decimal idFac = 0;

                    if (idFac_promesa.HasValue)
                    {
                        idFac = idFac_promesa.Value + 1;
                    }

                    Usuarios cliente = (from cliente_buscar in db.Usuarios
                                        where cliente_buscar.emailCli == emailCli
                                        select cliente_buscar).FirstOrDefault();

                    Factura factura = new Factura();
                    factura.idFac = idFac;
                    factura.idUsu = cliente.idUsu;
                    factura.estatusFac = estatus;
                    factura.metodoPagoFac = "Tarjeta";
                    factura.fechaEntregaFac = fecha_pedido;
                    factura.direccionEntregaFac = direccion;
                    factura.totalFac = total;

                    db.Factura.Add(factura);

                    db.SaveChanges();

                    return factura;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public String desglosar_factura(Factura factura, CarritoProducto[] carrito)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    for (int i = 0; i < carrito.Length; i++)
                    {
                        decimal? idDes_promesa = db.Factura_desglosada.Max(t => (decimal?)t.idFacDes);

                        decimal idDes = 0;

                        if (idDes_promesa.HasValue)
                        {
                            idDes = idDes_promesa.Value + 1;
                        }

                        Factura_desglosada desgloce = new Factura_desglosada();

                        decimal total_parcial = ((decimal)carrito[i].carrito.cantidadCar) * ((decimal)carrito[i].producto.precioPro);

                        desgloce.idFacDes = idDes;
                        desgloce.idFac = factura.idFac;
                        desgloce.idPro = carrito[i].producto.idPro;
                        desgloce.cantidadProductoFacDes = carrito[i].carrito.cantidadCar;
                        desgloce.totalParcialFacDes = total_parcial;

                        db.Factura_desglosada.Add(desgloce);

                        db.SaveChanges();

                    }

                    return "Exito";
                }
            }
            catch (Exception)
            {
                return "Ha ocurrido un error";
            }
        }

        public Boolean eliminar_carrito(String emailCli)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Carrito[] carrito = (from carrito_buscar in db.Carrito
                                         join usuario in db.Usuarios on carrito_buscar.idUsu equals usuario.idUsu
                                         where usuario.emailCli == emailCli
                                         select carrito_buscar).ToArray();

                    for (int i = 0; i < carrito.Length; i++)
                    {
                        db.Carrito.Remove(carrito[i]);
                    }

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}