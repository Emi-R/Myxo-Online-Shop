using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public int RegistrarUsuario(Usuario user, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(user.Nombres) || string.IsNullOrWhiteSpace(user.Nombres))
            {
                Mensaje = "El nombre no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(user.Apellidos) || string.IsNullOrWhiteSpace(user.Apellidos))
            {
                Mensaje = "El apellido no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(user.Correo) || string.IsNullOrWhiteSpace(user.Correo))
            {
                Mensaje = "El correo no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                // Using Helper to generate the random password and encrypt it
                string clave = CN_Helper.GenerarClave();
                user.Clave = CN_Helper.ConvertirSHA256(clave);

                int resultado = objCapaDato.RegistrarUsuario(user, out Mensaje);

                if (resultado != 0)
                {
                    string asunto = "Nueva cuenta en Myxo Online Shop";
                    string mensajeCorreo = "<h3>Su cuenta ha sido creada correctamente</h3> </br> <p>Su contraseña para acceder es: !clave!</p>";
                    mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);

                    bool respuesta = CN_Helper.EnviarCorreo(user.Correo, asunto, mensajeCorreo);

                    if (respuesta)
                    {
                        return resultado;
                    }
                    else
                    {
                        Mensaje = "No se pudo enviar el correo";
                        return 0;
                    }
                }
                else
                {
                    return resultado;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool EditarUsuario(Usuario user, out string Mensaje)
        {
            Mensaje = string.Empty;

            //Text fields validations
            if (string.IsNullOrEmpty(user.Nombres) || string.IsNullOrWhiteSpace(user.Nombres))
            {
                Mensaje = "El nombre no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(user.Apellidos) || string.IsNullOrWhiteSpace(user.Apellidos))
            {
                Mensaje = "El apellido no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(user.Correo) || string.IsNullOrWhiteSpace(user.Correo))
            {
                Mensaje = "El correo no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.EditarUsuario(user, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool EliminarUsuario(int id, out string Mensaje)
        {
            return objCapaDato.EliminarUsuario(id, out Mensaje);
        }

    }
}
