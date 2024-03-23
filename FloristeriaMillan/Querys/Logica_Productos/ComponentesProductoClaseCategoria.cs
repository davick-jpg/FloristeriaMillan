using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloristeriaMillan.Querys.Logica_Productos
{
    public class ComponentesProductoClaseCategoria
    {
        public Producto Producto { get; set; }
        public Clases Clase { get; set; }
        public Categorias Categoria { get; set; }
        public Componentes_producto[] Componentes { get; set; }
    }
}