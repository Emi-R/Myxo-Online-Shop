using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario user = new Usuario();

            user = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Helper.ConvertirSHA256(clave)).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if (user.Reestablecer)
                {

                    TempData["IdUsuario"] = user.IdUsuario;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(user.Correo, false);

                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idUsuario, string claveActual, string nuevaClave, string confirmarClave)
        {
            Usuario user = new Usuario();

            user = new CN_Usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idUsuario)).FirstOrDefault();

            if (user.Clave != CN_Helper.ConvertirSHA256(claveActual))
            {
                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vclave"] = "";
            nuevaClave = CN_Helper.ConvertirSHA256(nuevaClave);

            string Mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idUsuario), nuevaClave, out Mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = idUsuario;
                ViewBag.Error = Mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario user = new Usuario();

            user = new CN_Usuarios().Listar().Where(u => u.Correo == correo).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Error = "No existe usuario registrado a ese correo";
                return View();
            }

            string mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().ReestablecerClave(user.IdUsuario, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}