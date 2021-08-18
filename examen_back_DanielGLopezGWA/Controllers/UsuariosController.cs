using ServicioWeb.Datos.Models;
using ServicioWeb.Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace examen_back_DanielGLopezGWA.Controllers
{
    public class UsuariosController : ApiController
    {
        LibraryConnection BD = new LibraryConnection();


        public IEnumerable<UsuariosDatos> Get()
        {


            var query = from u in BD.usuarios
                        join ud in BD.usuarios_datos
                            on u.id equals ud.idUsuario
                        select new UsuariosDatos { 
                            id = u.id,
                            name = u.name,
                            email = u.email,
                            direccion = ud.direccion,
                            telefono = ud.telefono,
                            fechaNacimiento = ud.fechaNacimiento
                        
                        };

            var listadoUsuarios = query.ToList();
            return listadoUsuarios;
        }


        [HttpGet]
        public UsuariosDatos Get(int id)
        {

            var query = from u in BD.usuarios
                        join ud in BD.usuarios_datos
                            on u.id equals ud.idUsuario
                        where u.id == id
                        select new UsuariosDatos
                        {
                            id = u.id,
                            name = u.name,
                            email = u.email,
                            direccion = ud.direccion,
                            telefono = ud.telefono,
                            fechaNacimiento = ud.fechaNacimiento

                        };

            UsuariosDatos listadoUsuarios = query.FirstOrDefault(x => x.id == id); 
            return listadoUsuarios;

            //var usuario = BD.usuarios.FirstOrDefault(x => x.id == id);
            //return usuario;
        }

        [HttpPost]
        public bool AddUser(UsuariosDatos usuarioDatos)
        {

            usuarios userTable = new usuarios();
            userTable.name = usuarioDatos.name;
            userTable.email = usuarioDatos.email;
            BD.usuarios.Add(userTable);
            BD.SaveChanges();


            usuarios_datos userDataTable = new usuarios_datos();
            userDataTable.idUsuario = userTable.id;
            userDataTable.direccion = usuarioDatos.direccion;
            userDataTable.telefono = usuarioDatos.telefono;
            userDataTable.fechaNacimiento = usuarioDatos.fechaNacimiento;
            BD.usuarios_datos.Add(userDataTable);

            return BD.SaveChanges() > 0;
        }


        [HttpPut]
        public bool UpdateUser(UsuariosDatos usuario)
        {

            var usuarioActualizar = BD.usuarios.FirstOrDefault(x => x.id == usuario.id);
            usuarioActualizar.name = usuario.name;
            usuarioActualizar.email = usuario.email;

          
            var usuarioActualizarDatos = BD.usuarios_datos.FirstOrDefault(x => x.idUsuario == usuario.id);
            usuarioActualizarDatos.direccion = usuario.direccion;
            usuarioActualizarDatos.telefono = usuario.telefono;
            usuarioActualizarDatos.fechaNacimiento = usuario.fechaNacimiento;


            return BD.SaveChanges() > 0;


        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var usuarioEliminar = BD.usuarios.FirstOrDefault(x => x.id == id);
            BD.usuarios.Remove(usuarioEliminar);
            return BD.SaveChanges() > 0;
        }


    }
}
