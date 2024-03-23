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
    
    public partial class Articulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articulos()
        {
            this.Cartera_proveedores = new HashSet<Cartera_proveedores>();
            this.Componentes_producto = new HashSet<Componentes_producto>();
        }
    
        public decimal idArt { get; set; }
        public Nullable<decimal> precioArt { get; set; }
        public string nombreArt { get; set; }
        public string descripcionArt { get; set; }
        public Nullable<decimal> precioDistribuidorArt { get; set; }
        public Nullable<decimal> precioComercialArt { get; set; }
        public Nullable<decimal> stockMinimoArt { get; set; }
        public Nullable<decimal> stockArt { get; set; }
        public string fotoArt { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cartera_proveedores> Cartera_proveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Componentes_producto> Componentes_producto { get; set; }
    }
}
