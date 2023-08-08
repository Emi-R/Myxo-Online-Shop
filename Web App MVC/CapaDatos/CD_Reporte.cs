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
    public class CD_Reporte
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

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
