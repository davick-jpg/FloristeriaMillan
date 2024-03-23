using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FloristeriaMillan.Querys.Logica_Usuarios
{
    public class LO_UsuariosController : Controller
    {
        public Usuarios[] buscar_usuarios()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_usuarios = from usuarios in db.Usuarios
                                     where usuarios.idRol != 3
                                     select usuarios;

                return lista_usuarios.ToArray();
            }
        }

        public Rol[] buscar_roles()
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var lista_roles = from rol in db.Rol select rol;

                return lista_roles.ToArray();
            }
        }

        public UsuarioRol buscar_usuario(String correo)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var usuario_busqueda = (from usuario in db.Usuarios
                                            join rol in db.Rol on usuario.idRol equals rol.idRol
                                            where usuario.emailCli == correo
                                            select new UsuarioRol
                                            {
                                                Usuario = usuario,
                                                Rol = rol
                                            }).FirstOrDefault();

                    return usuario_busqueda;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public Rol buscar_rol(String nombre_rol)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var rol_busqueda = (from rol in db.Rol
                                        where rol.nombreRol == nombre_rol
                                        select rol).FirstOrDefault();

                    return rol_busqueda;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public String eliminar_usuario(String email_usuario)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    var usuario_eliminar = (from usuario in db.Usuarios
                                            where usuario.emailCli == email_usuario
                                            select usuario).FirstOrDefault();

                    db.Usuarios.Remove(usuario_eliminar);

                    db.SaveChanges();

                    return "Se elimino con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error";
                }

            }
        }

        public String agregar_usuario(String nombre,
                                      String email,
                                      String password,
                                      decimal telefono,
                                      String idrol)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                try
                {
                    decimal? idUsu_promesa = db.Usuarios.Max(t => (decimal?)t.idUsu);

                    decimal idUsu = 0;

                    Rol rol = buscar_rol(idrol);

                    if (idUsu_promesa.HasValue)
                    {
                        idUsu = idUsu_promesa.Value + 1;
                    }

                    Usuarios usuario = new Usuarios();
                    usuario.idUsu = idUsu;
                    usuario.nombreCli = nombre;
                    usuario.emailCli = email;
                    usuario.passwordCli = password;
                    usuario.telefonoCli = telefono;
                    usuario.idRol = rol.idRol;
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();

                    return "Se ha registrado con exito";
                }
                catch (Exception ex)
                {
                    return "Se ha producido un error " + ex;
                }

            }
        }

        public String editar_usuario(String correo_editar,
                                     String nombre,
                                     String email,
                                     String password,
                                     decimal telefono,
                                     String idrol)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var usuario = db.Usuarios.SingleOrDefault(t => t.emailCli == correo_editar);

                if (usuario == null && idrol != null)
                {
                    return "Se ha producido un error";
                }

                try
                {
                    Rol rol = buscar_rol(idrol);

                    usuario.nombreCli = nombre;
                    usuario.emailCli = email;
                    usuario.passwordCli = password;
                    usuario.telefonoCli = telefono;
                    usuario.idRol = rol.idRol;
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