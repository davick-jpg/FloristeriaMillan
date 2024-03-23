using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Pedidos
{
    public class FacturaUsuario
    {
        public Factura factura { get; set; }

        public Usuarios usuarios { get; set; }

        public string descripcion { get; set; }

    }
}