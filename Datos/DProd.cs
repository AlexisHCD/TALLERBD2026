using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidad;

// Se define el espacio de nombres 'Datos' para agrupar las clases de conexión a la base de datos.
namespace Datos
{
    // Clase pública DProd encargada de la gestión de datos para la entidad Producto.
    public class DProd
    {
        private Conexion Cn = new Conexion();

        public static DProd _instancia = null;

        public static DProd Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new DProd();
                }
                return _instancia;
            }
        }

        public List<EProd> Listar()
        {
            List<EProd> Lis = new List<EProd>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                SqlCommand cmd = new SqlCommand("Bus_Prod", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Lis.Add(new EProd()
                        {
                            IdProd = Convert.ToInt32(dr["IdProd"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            FInc = dr["FInc"].ToString(),
                            CInc = dr["CInc"].ToString(),
                            CAct = dr["CAct"].ToString(),
                            CArr = dr["CArr"].ToString(),
                            TAct = dr["TAct"].ToString(),
                            VArr = dr["VArr"].ToString(),
                        });
                    }
                    dr.Close();
                    return Lis;
                }
                catch (Exception)
                {
                    Lis = null;
                    return Lis;
                }
            }
        }

        public bool Ingresar(EProd obj)
        {
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Ing_Prod", Con);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("FInc", obj.FInc);
                    cmd.Parameters.AddWithValue("CInc", obj.CInc);
                    cmd.Parameters.AddWithValue("CAct", obj.CAct);
                    cmd.Parameters.AddWithValue("CArr", obj.CArr);
                    cmd.Parameters.AddWithValue("TAct", obj.TAct);
                    cmd.Parameters.AddWithValue("VArr", obj.VArr);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        public bool Actualizar(EProd obj)
        {
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Act_Prod", Con);
                    cmd.Parameters.AddWithValue("IdProd", obj.IdProd);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("FInc", obj.FInc);
                    cmd.Parameters.AddWithValue("CInc", obj.CInc);
                    cmd.Parameters.AddWithValue("CAct", obj.CAct);
                    cmd.Parameters.AddWithValue("CArr", obj.CArr);
                    cmd.Parameters.AddWithValue("TAct", obj.TAct);
                    cmd.Parameters.AddWithValue("VArr", obj.VArr);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        public bool Eliminar(int Id)
        {
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Eli_Prod", Con);
                    cmd.Parameters.AddWithValue("IdProd", Id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }
    }
}
