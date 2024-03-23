using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Productos
{
    public class ProductoClaseCategoria
    {
        public Producto Producto { get; set; }
        public Clases Clase { get; set; }
        public Categorias Categoria { get; set; }
    }
}