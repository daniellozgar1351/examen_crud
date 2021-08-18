using Newtonsoft.Json;
using ServicioWeb.Dominio;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Mvc;


namespace examen_fronend_DanielGLopez.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");

            var request = clienteHttp.GetAsync("api/Usuarios").Result;
            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var listado = JsonConvert.DeserializeObject<List<UsuariosDatos>>(resulString);
                
                foreach (var user in listado)
                {
                    int edad = getEdad(user.fechaNacimiento);
                    user.edad = edad;

                }

                return View(listado);
            }

            return View(new List<UsuariosDatos>());
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Nuevo(UsuariosDatos usuario)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");
            var request = clienteHttp.PostAsync("api/Usuarios", usuario, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<bool>(resulString);

                if (respuesta)
                {
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Actualizar(int id)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");
            var request = clienteHttp.GetAsync("api/Usuarios/" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var infoUser = JsonConvert.DeserializeObject<UsuariosDatos>(resulString);

                return View(infoUser);
            }

            return View();

        }

        [HttpPost]
        public ActionResult Actualizar(UsuariosDatos usuario)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");
            var request = clienteHttp.PutAsync("api/Usuarios", usuario, new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<bool>(resulString);

                if (respuesta)
                {
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");
            var request = clienteHttp.DeleteAsync("api/Usuarios/" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var respuesta = JsonConvert.DeserializeObject<bool>(resulString);

                if (respuesta)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Detalle(int id)
        {

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://localhost:44348/");
            var request = clienteHttp.GetAsync("api/Usuarios/" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resulString = request.Content.ReadAsStringAsync().Result;
                var infoUser = JsonConvert.DeserializeObject<UsuariosDatos>(resulString);

                return View(infoUser);
            }

            return View();

        }

        //Función para calcular edad
        public static int getEdad(DateTime fechaNacimiento)
        {
            //DateTime fNacimiento = Convert.ToDateTime(fechaNacimiento);
            DateTime nacimiento = new DateTime(fechaNacimiento.Year, fechaNacimiento.Month, fechaNacimiento.Day);
            int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            return edad;
        }


    }
}