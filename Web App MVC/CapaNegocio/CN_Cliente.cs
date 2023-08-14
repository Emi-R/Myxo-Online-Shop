using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {

        private CD_Cliente objCapaDato = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarCliente(Cliente client, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(client.Nombres) || string.IsNullOrWhiteSpace(client.Nombres))
            {
                Mensaje = "El nombre no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(client.Apellidos) || string.IsNullOrWhiteSpace(client.Apellidos))
            {
                Mensaje = "El apellido no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(client.Correo) || string.IsNullOrWhiteSpace(client.Correo))
            {
                Mensaje = "El correo no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                // Using Helper to generate the random password and encrypt it
                client.Clave = CN_Helper.ConvertirSHA256(client.Clave);

               return objCapaDato.RegistrarCliente(client, out Mensaje);

            }
            else
            {
                return 0;
            }
        }

            public bool CambiarClave(int idCliente, string nuevaClave, out string Mensaje)
            {
                return objCapaDato.CambiarClave(idCliente, nuevaClave, out Mensaje);
            }


            public bool ReestablecerClave(int idCliente, string correo, out string Mensaje)
            {
                Mensaje = string.Empty;

                // Using Helper to generate the random password and encrypt it
                string nuevaClave = CN_Helper.GenerarClave();
                bool resultado = objCapaDato.ReestablecerClave(idCliente, CN_Helper.ConvertirSHA256(nuevaClave), out Mensaje);

                if (resultado)
                {
                    string asunto = "Contraseña reestablecida";
                    string mensajeCorreo = "<h3>Su contraseña ha sido reestablecida</h3> </br> <p>Su nueva contraseña para acceder es: !clave!</p>";
                    mensajeCorreo = mensajeCorreo.Replace("!clave!", nuevaClave);

                    bool respuesta = CN_Helper.EnviarCorreo(correo, asunto, mensajeCorreo);

                    if (respuesta)
                    {
                        return true;
                    }
                    else
                    {
                        Mensaje = "No se pudo enviar el correo";
                        return false;
                    }
                }
                else
                {
                    Mensaje = "No se pudo reestablecer la contraseña";
                    return false;
                }
            }
        }
    }
}
