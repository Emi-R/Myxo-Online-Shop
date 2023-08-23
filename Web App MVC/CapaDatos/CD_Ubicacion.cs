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
    public class CD_Ubicacion
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        // Returns a list of all provinces from the database
        public List<Provincia> ObtenerProvincias()
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (oconexion)
                {
                    string query = "select * from Provincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    IdProvincia = dr["IdProvincia"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),

                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Provincia>();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        // Returns a list of all districts from the database
        public List<Localidad> ObtenerLocalidades(string idprovincia)
        {
            List<Localidad> lista = new List<Localidad>();

            try
            {
                using (oconexion)
                {
                    string query = "select * from Localidad where IdProvincia = @idprovincia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Localidad()
                                {
                                    IdLocalidad = dr["IdLocalidad"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),

                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Localidad>();
                throw;
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }


    }
}
