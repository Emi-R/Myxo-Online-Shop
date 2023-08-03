using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarProducto(Producto prod, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(prod.Nombre) || string.IsNullOrWhiteSpace(prod.Nombre))
            {
                Mensaje = "El nombre no puede estar vacio";
            }

            else if (string.IsNullOrEmpty(prod.Descripcion) || string.IsNullOrWhiteSpace(prod.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            else if (prod.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }

            else if (prod.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }

            else if (prod.Precio <= 0)
            {
                Mensaje = "Precio invalido";
            }

            else if (prod.Stock <= 0)
            {
                Mensaje = "Stock invalido";
            }

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.RegistrarProducto(prod, out Mensaje);
            else
                return 0;
        }

        public bool EditarProducto(Producto prod, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(prod.Nombre) || string.IsNullOrWhiteSpace(prod.Nombre))
            {
                Mensaje = "El nombre no puede estar vacio";
            }

            else if (string.IsNullOrEmpty(prod.Descripcion) || string.IsNullOrWhiteSpace(prod.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            else if (prod.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }

            else if (prod.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }

            else if (prod.Precio <= 0)
            {
                Mensaje = "Precio invalido";
            }

            else if (prod.Stock <= 0)
            {
                Mensaje = "Stock invalido";
            }

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.EditarProducto(prod, out Mensaje);
            else
                return false;
        }

        public bool EliminarProducto(int id, out string Mensaje)
        {
            return objCapaDato.EliminarProducto(id, out Mensaje);
        }

        public bool GuardarDatosImagen(Producto prod, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(prod, out Mensaje);
        }
    }
}
