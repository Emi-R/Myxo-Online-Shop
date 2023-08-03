using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marca objCapaDato = new CD_Marca();

        public List<Marca> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarMarca(Marca mar, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(mar.Descripcion) || string.IsNullOrWhiteSpace(mar.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.RegistrarMarca(mar, out Mensaje);
            else
                return 0;
        }

        public bool EditarMarca(Marca mar, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(mar.Descripcion) || string.IsNullOrWhiteSpace(mar.Descripcion))
            {
                Mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.EditarMarca(mar, out Mensaje);
            else
                return false;
        }

        public bool EliminarMarca(int id, out string Mensaje)
        {
            return objCapaDato.EliminarMarca(id, out Mensaje);
        }
    }
}
