using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Pedidos
{
    public class LO_PedidosController : Controller
    {
        // GET: LO_Pedidos
        public FacturaUsuario[] buscar_pedidos()
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    FacturaUsuario[] facturas = (from factura in db.Factura
                                                 join usuario in db.Usuarios on factura.idUsu equals usuario.idUsu
                                                 select new FacturaUsuario
                                                 {
                                                     factura = factura,
                                                     usuarios = usuario
                                                 }).ToArray();

                    facturas = generar_descripcion(facturas);

                    return facturas;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FacturaUsuario[] generar_descripcion(FacturaUsuario[] facturas)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    String descripcion = "";

                    for (int i = 0; i < facturas.Length; i++)
                    {
                        descripcion = "";

                        decimal idfac = facturas[i].factura.idFac;

                        ProductoDesgloce[] productos = (from factura in db.Factura
                                                        join desglose in db.Factura_desglosada on factura.idFac equals desglose.idFac
                                                        join producto in db.Producto on desglose.idPro equals producto.idPro
                                                        where desglose.idFac == idfac
                                                        select new ProductoDesgloce
                                                        {
                                                            desgloce = desglose,
                                                            producto = producto
                                                        }).ToArray();

                        for (int j = 0; j < productos.Length; j++)
                        {
                            descripcion = descripcion + productos[j].desgloce.cantidadProductoFacDes + "x " + productos[j].producto.nombrePro + "\n";
                        }

                        facturas[i].descripcion = descripcion;
                    }

                    return facturas;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public String finalizar_factura(decimal idFac)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Factura factura = (from factura_buscar in db.Factura
                                       where factura_buscar.idFac == idFac
                                       select factura_buscar).FirstOrDefault();

                    db.Factura.Attach(factura);

                    factura.estatusFac = (decimal?)1;

                    db.SaveChanges();
                }

                return "Se registro con exito";
            }
            catch (Exception ex)
            {
                return "Algo salio mal lo siento";
            }
        }

        public String cancelar_factura(decimal idFac)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    Factura factura = (from factura_buscar in db.Factura
                                       where factura_buscar.idFac == idFac
                                       select factura_buscar).FirstOrDefault();

                    db.Factura.Attach(factura);

                    factura.estatusFac = 2;

                    db.SaveChanges();
                }

                return "Se registro con exito";
            }
            catch (Exception)
            {
                return "Algo salio mal lo siento";
            }
        }

        public FacturaUsuario[] filtrar_factura(decimal idEstatus)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    FacturaUsuario[] facturas = (from factura_buscar in db.Factura
                                                  join usuario in db.Usuarios on factura_buscar.idUsu equals usuario.idUsu
                                                  where factura_buscar.estatusFac == idEstatus
                                                  select new FacturaUsuario
                                                  {
                                                      factura = factura_buscar,
                                                      usuarios = usuario
                                                  }).ToArray();

                    facturas = generar_descripcion(facturas);

                    return facturas;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}