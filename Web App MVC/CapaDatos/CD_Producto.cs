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
    public class CD_Producto
    {
        private SqlConnection oconexion = new SqlConnection(Conexion.cn);

        // Returns a list of all products from the database
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (oconexion)
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("Select p.IdProducto, p.Nombre, p.Descripcion,");
                    query.AppendLine("m.IdMarca, m.Descripcion[DescMarca],");
                    query.AppendLine("c.IdCategoria, c.Descripcion[DescCat],");
                    query.AppendLine("p.Precio, p.Stock,p.RutaImagen, p.NombreImagen, p.Activo");
                    query.AppendLine("From PRODUCTO p");
                    query.AppendLine("Inner Join MARCA m on p.IdMarca = m.IdMarca");
                    query.AppendLine("Inner Join CATEGORIA c on c.IdCategoria = p.IdCategoria");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Producto()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),

                                    oMarca = new Marca()
                                    {
                                        IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                        Descripcion = dr["DescMarca"].ToString()
                                    },

                                    oCategoria = new Categoria()
                                    {
                                        IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                        Descripcion = dr["DescCat"].ToString()
                                    },

                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Producto>();
            }
            finally
            {
                oconexion.Close();
            }

            return lista;
        }

        // Registers new product with Stored Procedure in the database
        public int RegistrarProducto(Producto prod, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);

                    cmd.Parameters.AddWithValue("Nombre", prod.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", prod.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", prod.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", prod.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", prod.Precio);
                    cmd.Parameters.AddWithValue("Stock", prod.Stock);
                    cmd.Parameters.AddWithValue("Activo", prod.Activo);
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

        // Modifies product with Stored Procedure in the database
        public bool EditarProducto(Producto prod, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarProducto", oconexion);

                    cmd.Parameters.AddWithValue("IdProducto", prod.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", prod.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", prod.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", prod.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", prod.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", prod.Precio);
                    cmd.Parameters.AddWithValue("Stock", prod.Stock);
                    cmd.Parameters.AddWithValue("Activo", prod.Activo);
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

        // Delete product with Stored Procedure in the database
        public bool EliminarProducto(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);

                    cmd.Parameters.AddWithValue("IdProducto", id);
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

        public bool GuardarDatosImagen(Producto prod, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (oconexion)
                {
                    string query = "Update Producto set RutaImagen = @RutaImagen, NombreImagen = @NombreImagen Where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.Parameters.AddWithValue("@RutaImagen", prod.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", prod.NombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", prod.IdProducto);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        resultado = true;
                    else
                        Mensaje = "No se pudo actualizar la imagen";

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
