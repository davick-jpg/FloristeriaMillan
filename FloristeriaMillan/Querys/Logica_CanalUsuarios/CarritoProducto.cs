using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_CanalUsuarios
{
    public class CarritoProducto
    {
        public Producto producto { get; set; }

        public Carrito carrito { get; set; }
    }
}