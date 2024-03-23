using FloristeriaMillan.Models;
using FloristeriaMillan.Querys.Logica_Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Ventas
{
    public class LO_VentasController : Controller
    {
        // GET: LO_Ventas
        public decimal calcular_total_venta(List<Componentes_producto> componentes)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal total = 0;

                    if (componentes != null)
                    {
                        for (int i = 0; i < componentes.Count; i++)
                        {
                            decimal idArt = componentes[i].idArt;

                            decimal precio = (decimal)(from articulo in db.Articulos
                                                       where articulo.idArt == idArt
                                                       select articulo.precioArt).FirstOrDefault();

                            total = total + (precio * componentes[i].cantidadComPro.Value);
                        }
                    }

                    return total;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public String registrar_venta(String email, decimal total)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal idusu = (from usuario in db.Usuarios
                                     where usuario.emailCli == email
                                     select usuario.idUsu).FirstOrDefault();

                    decimal? idFac_promesa = db.Factura.Max(t => (decimal?)t.idFac);

                    decimal idFac = 0;

                    if (idFac_promesa.HasValue)
                    {
                        idFac = idFac_promesa.Value + 1;
                    }

                    Factura factura = new Factura();

                    factura.idFac = idFac;
                    factura.idUsu = idusu;
                    factura.totalFac = total;
                    factura.metodoPagoFac = "Mostrador";
                    factura.estatusFac = 1;

                    db.Factura.Add(factura);
                    db.SaveChanges();

                    return "Se registro en exito";
                }
            }
            catch (Exception)
            {

                return "Se ha producido un error";
            }
        }
    }
}