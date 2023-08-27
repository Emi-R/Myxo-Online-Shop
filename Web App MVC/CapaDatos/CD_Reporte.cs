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
    public class CD_Reporte
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-AR")),
                                    IdTransaccion = dr["IdTransaccion"].ToString()
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Reporte>();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        public Dashboard VerDashboard()
        {
           Dashboard dashboard = new Dashboard();

            try
            {
                using (oconexion)
                {

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dashboard = new Dashboard()
                            {
                                totalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                totalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                totalProducto = Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                dashboard = new Dashboard();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return dashboard;
        }

    }
}
