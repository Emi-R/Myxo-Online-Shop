using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDato = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarCategoria(Categoria cat, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(cat.Descripcion) || string.IsNullOrWhiteSpace(cat.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
               return objCapaDato.RegistrarCategoria(cat, out Mensaje);
            else
                return 0;
        }

        public bool EditarCategoria(Categoria cat, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(cat.Descripcion) || string.IsNullOrWhiteSpace(cat.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.EditarCategoria(cat, out Mensaje);
            else
                return false;
        }

        public bool EliminarCategoria(int id, out string Mensaje)
        {
            return objCapaDato.EliminarCategoria(id, out Mensaje);
        }

    }
}
