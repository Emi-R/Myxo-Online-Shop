using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Producto producto = new Producto();
            bool conversion;

            producto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();

            if (producto != null)
            {
                producto.Base64 = CN_Helper.ConvertirBase64(Path.Combine(producto.RutaImagen, producto.NombreImagen), out conversion);
                producto.Extension = Path.GetExtension(producto.NombreImagen);
            }

            return View(producto);
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            lista = new CN_Categoria().Listar();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarMarcasPorCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marca().ListarMarcaporCategoria(idcategoria);

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListaProducto(int idcategoria, int idmarca)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Helper.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo

            }).Where(p =>
                p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria) &&
                p.oMarca.IdMarca == (idmarca == 0 ? p.oMarca.IdMarca : idmarca) &&
                p.Stock > 0 && p.Activo == true).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;
        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);
            bool respuesta = false;
            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";
            }
            else
            {
                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);

            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {
            List<Carrito> lista = new List<Carrito>();
            bool conversion;
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            lista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                producto = new Producto()
                {
                    IdProducto = oc.producto.IdProducto,
                    Nombre = oc.producto.Nombre,
                    oMarca = oc.producto.oMarca,
                    Precio = oc.producto.Precio,
                    RutaImagen = oc.producto.RutaImagen,
                    Base64 = CN_Helper.ConvertirBase64(Path.Combine(oc.producto.RutaImagen, oc.producto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.producto.NombreImagen)

                },
                Cantidad = oc.Cantidad
            }).ToList();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, sumar, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProvincias()
        {
            List<Provincia> lista = new List<Provincia>();

            lista = new CN_Ubicacion().ObtenerProvincias();

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerLocalidades(string idprovincia)
        {
            List<Localidad> lista = new List<Localidad>();

            lista = new CN_Ubicacion().ObtenerLocalidades(idprovincia);

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ProcesarPago(List<Carrito> oListaCarrito, Venta oVenta)
        {
            decimal total = 0;

            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("es-AR");
            detalle_venta.Columns.Add("IdProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(decimal));

            foreach(Carrito oCarrito in oListaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.producto.Precio;

                total += subtotal;

                detalle_venta.Rows.Add(new object[]{

                    oCarrito.producto.IdProducto,
                    oCarrito.Cantidad,
                    subtotal

                });
            }

            oVenta.MontoTotal = total;
            oVenta.IdCliente = ((Cliente)Session["Cliente"]).IdCliente;

            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = detalle_venta;

            return Json( new {Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=code0001&status=true"}, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PagoEfectuado()
        {
            string idtransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;

            if (status)
            {
                string mensaje = string.Empty;
                Venta oVenta = (Venta)TempData["Venta"];

                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];

                oVenta.IdTransaccion = idtransaccion;

                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

                ViewData["IdTransaccion"] = oVenta.IdTransaccion;
            }

            return View();
        }
    }

}