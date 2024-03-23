using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Articulos
{
    public class ArticuloProveedor
    {
        public Articulos Articulo { get; set; }
        public Cartera_proveedores Distribuidor { get; set; }
    }
}