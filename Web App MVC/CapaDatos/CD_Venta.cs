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
    public class CD_Venta
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        public bool Registrar(Venta venta, DataTable DetalleVenta, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("TotalProducto", venta.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", venta.MontoTotal);
                    cmd.Parameters.AddWithValue("Contacto", venta.Contacto);
                    cmd.Parameters.AddWithValue("IdLocalidad", venta.IdLocalidad);
                    cmd.Parameters.AddWithValue("Telefono", venta.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", venta.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", venta.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            finally
            {
                oconexion.Close();
            }

            return respuesta;

        }

        public List<DetalleVenta> ListarCompras(int idcliente)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            try
            {
                using (oconexion)
                {
                    string query = "Select * From fn_ListarCompra(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new DetalleVenta()
                                {
                                    oProducto = new Producto()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                        RutaImagen = dr["RutaImagen"].ToString(),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                    },
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-AR")),
                                    IdTransaccion = dr["IdTransaccion"].ToString(),
                                    FechaTexto = dr["FechaVenta"].ToString()
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<DetalleVenta>();
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

    }
}
