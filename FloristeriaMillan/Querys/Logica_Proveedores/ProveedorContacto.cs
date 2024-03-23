using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Proveedores
{
    public class ProveedorContacto
    {
        public Proveedores Proveedor { get; set; }
        public Contacto_proveedor Contacto { get; set; }
    }
}