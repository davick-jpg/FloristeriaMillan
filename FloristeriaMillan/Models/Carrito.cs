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
    
    public partial class Carrito
    {
        public decimal idCar { get; set; }
        public decimal idUsu { get; set; }
        public decimal idPro { get; set; }
        public string DescripcionCar { get; set; }
        public Nullable<decimal> cantidadCar { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}