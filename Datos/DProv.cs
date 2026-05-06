using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidad;

// Se define el espacio de nombres 'Datos' para agrupar las clases de conexión a la base de datos.
namespace Datos
{
    // Clase pública DProv encargada de la gestión de datos para la entidad Proveedor.
    public class DProv
    {
        // Instancia de la clase Conexion para acceder a la cadena de conexión.
        private Conexion Cn = new Conexion();

        // Variable estática privada para implementar el patrón Singleton.
        public static DProv _instancia = null;

        // Propiedad estática que retorna la única instancia de la clase DProv.
        public static DProv Instancia
        {
            get
            {
                // Si la instancia es nula, se crea una nueva.
                if (_instancia == null)
                {
                    _instancia = new DProv();
                }
                // Se retorna la instancia creada.
                return _instancia;
            }
        }

        // Método para obtener una lista de todos los proveedores desde la base de datos.
        public List<EProv> Listar()
        {
            // Se crea una lista vacía de tipo EProv (Entidad Proveedor).
            List<EProv> Lis = new List<EProv>();
            // Se utiliza un bloque using para garantizar el cierre de la conexión al finalizar.
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                // Se prepara el comando con el procedimiento almacenado "Bus_Prov".
                SqlCommand cmd = new SqlCommand("Bus_Prov", oConexion);
                // Se especifica que el comando es de tipo procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Se abre la conexión a la base de datos.
                    oConexion.Open();
                    // Se ejecuta el lector de datos para obtener los resultados.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Se recorre cada una de las filas devueltas por la consulta.
                    while (dr.Read())
                    {
                        // Se instancia un nuevo proveedor y se mapean sus propiedades con los datos leídos.
                        Lis.Add(new EProv()
                        {
                            IdProv = Convert.ToInt32(dr["IdProv"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Rut = dr["Rut"].ToString(),
                            IdReg = Convert.ToInt32(dr["IdReg"].ToString()),
                            Reg = new ELocReg() { Nombre = dr["NombreRegion"].ToString() },
                            IdPro = Convert.ToInt32(dr["IdPro"].ToString()),
                            Pro = new ELocPro() { Nombre = dr["NombreProvincia"].ToString() },
                            IdCom = Convert.ToInt32(dr["IdCom"].ToString()),
                            Com = new ELocCom() { Nombre = dr["NombreComuna"].ToString() },
                            Direccion = dr["Direccion"].ToString(),
                            Tel = dr["Tel"].ToString(),
                            Email = dr["Email"].ToString(),
                            Giro = dr["Giro"].ToString(),
                            Descr = dr["Descr"].ToString(),
                        });
                    }
                    // Se cierra el SqlDataReader una vez terminada la lectura.
                    dr.Close();
                    // Se retorna la lista de proveedores.
                    return Lis;
                }
                catch (Exception)
                {
                    // En caso de error, se retorna nulo para reflejar un fallo con el acceso a datos.
                    Lis = null;
                    return Lis;
                }
            }
        }

        // Método para ingresar un nuevo proveedor a la base de datos.
        public bool Ingresar(EProv obj)
        {
            // Variable para almacenar el resultado de la operación, inicialmente true.
            bool Respuesta = true;
            // Bloque using para la conexión a la base de datos.
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Comando que llama al procedimiento almacenado "Ing_Prov".
                    SqlCommand cmd = new SqlCommand("Ing_Prov", Con);
                    // Se asignan los valores del objeto EProv a los parámetros del procedimiento.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Rut", obj.Rut);
                    cmd.Parameters.AddWithValue("IdCom", obj.IdCom);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Tel", obj.Tel);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Giro", obj.Giro);
                    cmd.Parameters.AddWithValue("Descr", obj.Descr);
                    // Parámetro de salida para capturar el resultado lógico de la base de datos.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    // Se define de tipo procedimiento almacenado.
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abre la conexión.
                    Con.Open();
                    // Ejecuta el comando en la base de datos sin devolver filas.
                    cmd.ExecuteNonQuery();
                    // Captura el valor de retorno en la variable Respuesta.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            // Retorna si el ingreso fue exitoso o no.
            return Respuesta;
        }

        // Método para actualizar los datos de un proveedor existente en la base de datos.
        public bool Actualizar(EProv obj)
        {
            // Variable booleana para guardar el estado de respuesta.
            bool Respuesta = true;
            // Se establece la conexión a SQL Server predeterminada en app.
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Procedimiento almacenado asignado para actualizar: "Act_Prov".
                    SqlCommand cmd = new SqlCommand("Act_Prov", Con);
                    // Se pasa el IdProv para identificar qué proveedor se va a modificar.
                    cmd.Parameters.AddWithValue("IdProv", obj.IdProv);
                    // Se envían los demás valores a actualizar que posee el objeto entidad.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Rut", obj.Rut);
                    cmd.Parameters.AddWithValue("IdCom", obj.IdCom);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Tel", obj.Tel);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Giro", obj.Giro);
                    cmd.Parameters.AddWithValue("Descr", obj.Descr);
                    // Se captura el estado de respuesta del servidor relacional.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Conexión abierta al flujo de la Base de Datos.
                    Con.Open();
                    // Se dispara el script y se aplican los cambios de UPDATE subyacentes.
                    cmd.ExecuteNonQuery();
                    // Asigna el valor del parámetro de salida a la variable bool.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            // Devuelve la confirmación.
            return Respuesta;
        }

        // Método diseñado para eliminar de la base de datos un proveedor por su Id.
        public bool Eliminar(int Id)
        {
            // Establece el inicio de la verificación en verdadero.
            bool Respuesta = true;
            // Prepara el objeto de conexión de SQL en un using statement seguro.
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Referencia al procedimiento "Eli_Prov" encargado del borrado.
                    SqlCommand cmd = new SqlCommand("Eli_Prov", Con);
                    // Se carga el identificador específico (IdProv) que se desea eliminar.
                    cmd.Parameters.AddWithValue("IdProv", Id);
                    // Parámetro output de confirmación procesal.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    // Tipo de comando es Store Procedure.
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Apertura de conexión de canal de datos.
                    Con.Open();
                    // Ejecuta el accionar que afecta los registros en la base de datos alterando el scope.
                    cmd.ExecuteNonQuery();
                    // Cambia dinámicamente si el eliminador se concretó.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // En situación de conflicto (ej: Foreign Keys o Timeout), se asume fracaso de remoción.
                    Respuesta = false;
                }
            }
            // Termina devolviendo la bandera al entorno que gestiona el negocio.
            return Respuesta;
        }
    }
}