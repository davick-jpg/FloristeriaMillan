//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FloristeriaMillan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Componentes_producto
    {
        public decimal idComPro { get; set; }
        public decimal idArt { get; set; }
        public decimal idPro { get; set; }
        public Nullable<decimal> cantidadComPro { get; set; }
    
        public virtual Articulos Articulos { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
