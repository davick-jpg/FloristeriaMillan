using FloristeriaMillan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloristeriaMillan.Querys.Logica_ControlAcceso
{
    public class LO_controlAcceso
    {

        public Usuarios EncontrarUsuario(String correo, String clave)
        {
            using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
            {
                var usuario = (from d in db.Usuarios
                               where d.emailCli == correo.Trim() &&
                               d.passwordCli == clave
                               select d).FirstOrDefault();

                return usuario;
            }
        }

        public Usuarios RegistrarUsuario(decimal idUsu, String correo, String clave, String nombre, long telefono, int idRol)
        {

            try
            {

                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {

                    Usuarios usuario = (from usuario_bus in db.Usuarios
                                        where usuario_bus.idUsu == idUsu
                                        select usuario_bus).FirstOrDefault();

                    usuario.emailCli = correo.Trim();
                    usuario.passwordCli = clave.Trim();
                    usuario.nombreCli = nombre.Trim();
                    usuario.telefonoCli = telefono;
                    usuario.idRol = idRol;

                    db.SaveChanges();
                    return usuario;
                }
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public Usuarios generar_default()
        {
            Usuarios usuario = new Usuarios();

            try
            {
                using (FloristeriaMillanEntities2 db = new FloristeriaMillanEntities2())
                {
                    decimal? idUsu_promesa = db.Usuarios.Max(t => (decimal?)t.idUsu);

                    decimal idUsu = 0;

                    if (idUsu_promesa.HasValue)
                    {
                        idUsu = idUsu_promesa.Value + 1;
                    }

                    usuario.idUsu = idUsu;
                    usuario.nombreCli = "";
                    usuario.emailCli = "default" + idUsu + "@general.com";
                    usuario.idRol = 3;

                    db.Usuarios.Add(usuario);

                    db.SaveChanges();
                }

                return usuario;
            }
            catch (Exception ex)
            {
                return usuario;
            }
        }
    }
}