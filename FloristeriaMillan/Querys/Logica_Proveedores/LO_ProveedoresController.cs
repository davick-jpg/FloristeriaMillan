using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_Proveedores
{
    public class LO_ProveedoresController : Controller
    {
        // GET: LO_Proveedores
        public Proveedores[] buscar_proveedores()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_proveedores = from proveedores in db.Proveedores select proveedores;

                return lista_proveedores.ToArray();
            }
        }

        public ProveedorContacto buscar_proveedor(String nombre_proveedor)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var proveedor = (from proveedores in db.Proveedores
                                     join contacto in db.Contacto_proveedor on proveedores.idProv equals contacto.idProv
                                     where proveedores.nombreEmpresaProv == nombre_proveedor
                                     select new ProveedorContacto
                                     {
                                         Proveedor = proveedores,
                                         Contacto = contacto
                                     }).FirstOrDefault();

                    return proveedor;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public String eliminar_proveedor(String nombre_proveedor)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var proveedor = (from proveedores in db.Proveedores
                                     join contacto in db.Contacto_proveedor on proveedores.idProv equals contacto.idProv
                                     where proveedores.nombreEmpresaProv == nombre_proveedor
                                     select new ProveedorContacto
                                     {
                                         Proveedor = proveedores,
                                         Contacto = contacto
                                     }).FirstOrDefault();

                    db.Proveedores.Remove(proveedor.Proveedor);
                    db.Contacto_proveedor.Remove(proveedor.Contacto);

                    db.SaveChanges();

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error";
                }

            }
        }

        public String agregar_proveedor(String nombre_empresa, String direccion_empresa, decimal telefono_empresa, String nombre_contacto, decimal telefono_contacto, String email_contacto)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    decimal? idProv_promesa = db.Proveedores.Max(t => (decimal?)t.idProv);
                    decimal? idCont_promesa = db.Contacto_proveedor.Max(t => (decimal?)t.idProvCon);

                    decimal idProv = 0;
                    decimal idCont = 0;

                    if (idProv_promesa.HasValue && idCont_promesa.HasValue)
                    {
                        idProv = idProv_promesa.Value + 1;
                        idCont = idCont_promesa.Value + 1;
                    }

                    Proveedores proveedor = new Proveedores();
                    proveedor.idProv = idProv;
                    proveedor.nombreEmpresaProv = nombre_empresa;
                    proveedor.direccionEmpresaProv = direccion_empresa;
                    proveedor.telefonoEmpresaProv = telefono_empresa;
                    db.Proveedores.Add(proveedor);
                    db.SaveChanges();

                    Contacto_proveedor contacto = new Contacto_proveedor();
                    contacto.idProvCon = idCont;
                    contacto.idProv = idProv;
                    contacto.nombreCon = nombre_contacto;
                    contacto.telefonoCon = telefono_contacto;
                    contacto.emailCon = email_contacto;
                    db.Contacto_proveedor.Add(contacto);
                    db.SaveChanges();

                    return "Se ha registrado con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error";
                }

            }
        }

        public String editar_proveedor(String editar_empresa, String nombre_empresa, String direccion_empresa, decimal telefono_empresa, String nombre_contacto, decimal telefono_contacto, String email_contacto)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var proveedor = db.Proveedores.SingleOrDefault(t => t.nombreEmpresaProv == editar_empresa);

                if (proveedor == null)
                {
                    return "Se ha producido un error";
                }

                var contacto = db.Contacto_proveedor.SingleOrDefault(t => t.idProv == proveedor.idProv);

                if (contacto == null)
                {
                    return "Se ha producido un error";
                }

                try
                {
                    proveedor.nombreEmpresaProv = nombre_empresa;
                    proveedor.direccionEmpresaProv = direccion_empresa;
                    proveedor.telefonoEmpresaProv = telefono_empresa;
                    db.SaveChanges();

                    contacto.nombreCon = nombre_contacto;
                    contacto.telefonoCon = telefono_contacto;
                    contacto.emailCon = email_contacto;
                    db.SaveChanges();

                    return "Se ha editado con exito";
                }
                catch (Exception)
                {
                    return "Se ha producido un error";
                }
            }

        }
    }
}