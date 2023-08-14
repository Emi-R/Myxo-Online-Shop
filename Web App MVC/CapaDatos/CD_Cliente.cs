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
    public class CD_Cliente
    {

        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        // Returns a list of all clients from the database
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (oconexion)
                {
                    string query = "select IdCliente, Nombres, Apellidos, Correo, Clave, Reestablecer from Cliente";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Cliente>();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        // Registers new client with Stored Procedure in the database
        public int RegistrarCliente(Cliente client, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", client.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", client.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", client.Correo);
                    cmd.Parameters.AddWithValue("Clave", client.Clave);
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

        // Changes client password
        public bool CambiarClave(int idCliente, string nuevaClave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("Update Cliente set clave = @nuevaclave, reestablecer = 0 where idcliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@nuevaClave", nuevaClave);
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

        // Resets client password
        public bool ReestablecerClave(int idCliente, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("Update cliente set clave = @clave, reestablecer = 1 where idcliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@clave", clave);
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
