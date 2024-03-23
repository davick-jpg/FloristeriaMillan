using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Pedidos
{
    public class ProductoDesgloce
    {
        public Factura_desglosada desgloce { get; set; }

        public Producto producto { get; set; }
    }
}