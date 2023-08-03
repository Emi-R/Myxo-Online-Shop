using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        // -------------------------- Category --------------------------
        #region Category
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria cat)
        {
            object resultado;
            string mensaje = string.Empty;

            if (cat.IdCategoria == 0)
            {
                resultado = new CN_Categoria().RegistrarCategoria(cat, out mensaje);

            }
            else
            {
                resultado = new CN_Categoria().EditarCategoria(cat, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().EliminarCategoria(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        // --------------------------- Brand ----------------------------
        #region Brand
        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca mar)
        {
            object resultado;
            string mensaje = string.Empty;

            if (mar.IdMarca == 0)
            {
                resultado = new CN_Marca().RegistrarMarca(mar, out mensaje);

            }
            else
            {
                resultado = new CN_Marca().EditarMarca(mar, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().EliminarMarca(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        // -------------------------- Product ---------------------------
        #region Product
        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(string prod, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool success = true;
            bool saveImageSucess = true;

            Producto objProducto = new Producto();
            objProducto = JsonConvert.DeserializeObject<Producto>(prod);

            decimal precio;

            if (decimal.TryParse(objProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-AR"), out precio))
                objProducto.Precio = precio;
            else
                return Json(success = false, mensaje = "El formato del precio debe ser ##.##", JsonRequestBehavior.AllowGet);


            if (objProducto.IdProducto == 0)
            {
                int idProductoGenerado = new CN_Producto().RegistrarProducto(objProducto, out mensaje);

                if(idProductoGenerado != 0)
                    objProducto.IdProducto = idProductoGenerado;
                else
                    success = false;
            }
            else
            {
                success = new CN_Producto().EditarProducto(objProducto, out mensaje);
            }

            if(success)
            {
                if(archivoImagen != null)
                {
                    string path = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string imageName = string.Concat(objProducto.IdProducto.ToString(), extension);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(path, imageName));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        saveImageSucess = false;

                    }

                    if(saveImageSucess)
                    {
                        objProducto.RutaImagen = path;
                        objProducto.NombreImagen = imageName;

                        bool respuesta = new CN_Producto().GuardarDatosImagen(objProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se registro el producto pero hubo problemas con la imagen";
                    }
                }
            }

            return Json( new { operacionExitosa = success, idGenerado = objProducto.IdProducto, mensaje = mensaje, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().EliminarMarca(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}