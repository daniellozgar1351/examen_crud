using ServicioWeb.Datos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace examen_back_DanielGLopezWA.Controllers
{
    public class UsuarioController : ApiController
    {

        LibreriaConnections BD = new LibreriaConnections();


        public IEnumerable<usuarios> Get()
        {
            var listadoUsuarios = BD.usuarios.ToList();
            return listadoUsuarios;
        }


        [HttpGet]
        public usuarios Get(int id)
        {
            var usuario = BD.usuarios.FirstOrDefault(x => x.id == id);
            return usuario;
        }

        [HttpPost]
        public bool AddUser(usuarios usuario)
        {
            BD.usuarios.Add(usuario);
            return BD.SaveChanges() > 0;
        }


        [HttpPut]
        public bool UpdateUser(usuarios usuario)
        {
            var usuarioActualizar = BD.usuarios.FirstOrDefault(x => x.id == usuario.id);
            usuarioActualizar.name = usuario.name;
            usuarioActualizar.email = usuario.email;
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
