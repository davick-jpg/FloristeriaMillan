﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FloristeriaMillanEntities2 : DbContext
    {
        public FloristeriaMillanEntities2()
            : base("name=FloristeriaMillanEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Cartera_proveedores> Cartera_proveedores { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Clases> Clases { get; set; }
        public virtual DbSet<Componentes_producto> Componentes_producto { get; set; }
        public virtual DbSet<Contacto_proveedor> Contacto_proveedor { get; set; }
        public virtual DbSet<DireccionesCliente> DireccionesCliente { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Factura_desglosada> Factura_desglosada { get; set; }
        public virtual DbSet<Favoritos> Favoritos { get; set; }
        public virtual DbSet<Filtros> Filtros { get; set; }
        public virtual DbSet<FiltrosC> FiltrosC { get; set; }
        public virtual DbSet<FiltrosCategoria> FiltrosCategoria { get; set; }
        public virtual DbSet<FiltrosClases> FiltrosClases { get; set; }
        public virtual DbSet<Galeria> Galeria { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_operacion> Rol_operacion { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}