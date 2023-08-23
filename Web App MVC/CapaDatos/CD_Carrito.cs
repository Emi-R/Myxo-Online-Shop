using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Carrito
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        // Returns true if already exists a cart registered to a specific client
        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_ExisteCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            finally
            {
                oconexion.Close();
            }

            return resultado;

        }

        // Makes the transaction in the DB whether the clients are adding or removing products to their cart
        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje)
        {
            bool resultado = true;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_OperacionCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.AddWithValue("Sumar", sumar);
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

        // Return how many products does the client has in his/her cart
        public int CantidadEnCarrito(int idcliente)
        {
            int resultado = 0;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("Select Count(*) From Carrito Where IdCliente = @idcliente", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            finally
            {
                oconexion.Close();
            }

            return resultado;
        }

        // Returns a list of all products from the database
        public List<Carrito> ListarProducto(int idcliente)
        {
            List<Carrito> lista = new List<Carrito>();

            try
            {
                using (oconexion)
                {
                    string query = "Select * From fn_obtenerCarritoCliente(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Carrito()
                                {
                                    producto = new Producto()
                                    {
                                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        oMarca = new Marca()
                                        {
                                            Descripcion = dr["DescMarca"].ToString()
                                        },
                                        Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                        Stock = Convert.ToInt32(dr["Stock"]),
                                        RutaImagen = dr["RutaImagen"].ToString(),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                    },
                                    Cantidad = Convert.ToInt32(dr["Cantidad"])

                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Carrito>();
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            finally
            {
                oconexion.Close();
            }

            return resultado;

        }
    }
}
