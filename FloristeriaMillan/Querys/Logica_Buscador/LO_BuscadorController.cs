using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Buscador
{
    public class LO_BuscadorController
    {
        // GET: LO_Buscador
        public String[] buscador_articulos(String cadena)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    string cadenaBusqueda = cadena.ToLower();

                    var resultados = from articulo in db.Articulos
                                     where articulo.nombreArt.ToLower().Contains(cadenaBusqueda)
                                     select articulo.nombreArt;

                    return resultados.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String[] buscador_productos(String cadena)
        {
            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    string cadenaBusqueda = cadena.ToLower();

                    var resultados = from producto in db.Producto
                                     where producto.nombrePro.ToLower().Contains(cadenaBusqueda)
                                     select producto.nombrePro;

                    return resultados.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}