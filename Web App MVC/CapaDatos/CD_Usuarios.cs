using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        // Returns a list of all users from the database
        public List<Usuario> Listar()
        {
            List <Usuario> lista = new List<Usuario>();

            try
            {
                using (oconexion)
                {
                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            lista.Add(
                                new Usuario() {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                    Activo= Convert.ToBoolean(dr["Activo"])
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Usuario>();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        // Registers new user with Stored Procedure in the database
        public int RegistrarUsuario(Usuario user, out string Mensaje)
        { 
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", user.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", user.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", user.Correo);
                    cmd.Parameters.AddWithValue("Clave", user.Clave);
                    cmd.Parameters.AddWithValue("Activo", user.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;

            }
            finally
            {
                oconexion.Close();
            }

            return idGenerado;

        }

        // Modifies user with Stored Procedure in the database
        public bool EditarUsuario(Usuario user, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", user.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", user.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", user.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", user.Correo);
                    cmd.Parameters.AddWithValue("Activo", user.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
  
            }
            finally
            {
                oconexion.Close();
            }

            return resultado;

        }

        // Deletes a user from the database
        public bool EliminarUsuario(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("Delete Top (1) From USUARIO Where IdUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;                
            }
            finally
            {
                oconexion.Close();
            }

            return resultado;
        }

    }
}
