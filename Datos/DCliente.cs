using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidad;

// Se define el espacio de nombres 'Datos' para agrupar las clases que interactúan con la base de datos.
namespace Datos
{
    // Clase pública DCliente que gestiona las operaciones CRUD para la entidad Cliente.
    public class DCliente
    {
        // Instancia de Conexion.
        private Conexion Cn = new Conexion();

        // Variable estática privada para implementar el patrón de diseño Singleton.
        public static DCliente _instancia = null;

        // Propiedad estática que devuelve la única instancia de la clase DCliente.
        public static DCliente Instancia
        {
            get
            {
                // Si la instancia aún no existe, se crea una nueva.
                if (_instancia == null)
                {
                    _instancia = new DCliente();
                }
                // Si ya existe, se retorna la actual, evitando múltiples instancias.
                return _instancia;
            }
        }

        // Método que devuelve una lista de todos los clientes desde la base de datos.
        public List<ECliente> Listar()
        {
            // Se inicializa una lista vacía de tipo ECliente.
            List<ECliente> Lis = new List<ECliente>();
            // Se crea una conexión a la base de datos usando la cadena de conexión estática del sistema.
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                // Se configura el comando SQL apuntando al procedimiento almacenado "Bus_Cliente".
                SqlCommand cmd = new SqlCommand("Bus_Cliente", oConexion);
                // Se especifica explícitamente que es un procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Se abre la conexión a la base de datos.
                    oConexion.Open();
                    // Se ejecuta la lectura de datos del comando y se obtiene el SqlDataReader.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Bucle para leer cada fila de los resultados devueltos por la base de datos.
                    while (dr.Read())
                    {
                        // Se agrega un nuevo objeto ECliente a la lista y se mapean todos sus campos desde el DataReader.
                        Lis.Add(new ECliente()
                        {
                            IdP_Cli = Convert.ToInt32(dr["IdP_Cli"].ToString()),
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
                        });
                    }
                    // Se cierra el SqlDataReader tras terminar la lectura.
                    dr.Close();
                    // Se retorna la lista rellenada con todos los clientes.
                    return Lis;
                }
                catch (Exception)
                {
                    // Si ocurre cualquier excepción, la lista se vuelve nula para indicar un fallo.
                    Lis = null;
                    return Lis;
                }
            }
        }
        // Método para verificar la existencia de un cliente a través de su RUT.
        public bool Buscar(ECliente obj)
        {
            // Inicializa la respuesta, asumiendo inicialmente que fue exitosa.
            bool Respuesta = true;
            // Usa una conexión SQL obteniendo la cadena registrada.
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Llama al procedimiento "Bus_Rut_Cliente".
                    SqlCommand cmd = new SqlCommand("Bus_Rut_Cliente", Con);
                    // Añade el parámetro Rut con el cual el procedimiento buscará al cliente.
                    cmd.Parameters.AddWithValue("Rut", obj.Rut);
                    // Declara un parámetro de salida que devolverá '1' si existe o '0' si no existe.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abre la conexión.
                    Con.Open();
                    // Ejecuta el comando SQL que procesa la búsqueda por Rut sin esperar un conjunto de filas de vuelta.
                    cmd.ExecuteNonQuery();
                    // Extrae el valor devuelto en el parámetro Sql de salida y lo convierte a booleano.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Ante cualquier interrupción o error, se asume que falló.
                    Respuesta = false;
                }
            }
            // Devuelve el estado de la búsqueda indicando si lo encontró.
            return Respuesta;
        }

        // Método utilizado para registrar un nuevo cliente en el sistema.
        public bool Ingresar(ECliente obj)
        {
            // Retorna un estado de éxito, que podrá alterarse si falla la transacción.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Dispone el comando al procedimiento almacenado de ingreso.
                    SqlCommand cmd = new SqlCommand("Ing_Cliente", Con);
                    // Suministra todos los datos vitales del cliente como parámetros del comando.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Rut", obj.Rut);
                    cmd.Parameters.AddWithValue("IdCom", obj.IdCom);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Tel", obj.Tel);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Giro", obj.Giro);
                    // Añade un parámetro que actúa como receptor del estado final de éxito que arroja el Store Procedure.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abrir paso a la Base de Datos.
                    Con.Open();
                    // Se ejecuta la secuencia insert.
                    cmd.ExecuteNonQuery();
                    // Almacena en respuesta el estado booleano de si se logró concretar el registro.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        // Método para modificar los datos de un cliente ya registrado.
        public bool Actualizar(ECliente obj)
        {
            // Inicialización de la variable que informa el éxito de la función.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Enlace hacia el procedimiento almacenado que ejecuta el Update en base de datos.
                    SqlCommand cmd = new SqlCommand("Act_Cliente", Con);
                    // Se entrega el Id principal del cliente con los nuevos valores a actualizar.
                    cmd.Parameters.AddWithValue("IdP_Cli", obj.IdP_Cli);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Rut", obj.Rut);
                    cmd.Parameters.AddWithValue("IdCom", obj.IdCom);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Tel", obj.Tel);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Giro", obj.Giro);
                    // Captura la respuesta de comprobación final proveída por el procedimiento.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se abre conexión en base de datos.
                    Con.Open();
                    // Consuma la operación mediante su ejecución.
                    cmd.ExecuteNonQuery();
                    // Recibe el booleano si la actualización ocurrió sin inconvenientes o choques con el motor.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        // Método usado para remover un cliente de acuerdo con su número de identificador (Id).
        public bool Eliminar(int Id)
        {
            // Predefine el estado a exitoso.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Llama el procedimiento que efectúa el Delete o anulación de estado.
                    SqlCommand cmd = new SqlCommand("Eli_Cliente", Con);
                    // Pasa como argumento de ejecución el número ID del cliente seleccionado.
                    cmd.Parameters.AddWithValue("IdP_Cli", Id);
                    // Recibe a través de un Bit de Output su resultado.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se inicializa conexión.
                    Con.Open();
                    // Lanza el script dentro de SQL Server.
                    cmd.ExecuteNonQuery();
                    // Almacena en nuestra respuesta el valor recuperado.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Devuelve negativo si una excepción corta el bloque lógico.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Método dedicado a obtener el Id del último cliente interactuado en la base de datos.
        public int ObtenerUltimoId()
        {
            // Empieza contando en cero.
            int ultimoId = 0;

            try
            {
                // Inicia bloque de limpieza automática para la conexión.
                using (SqlConnection Con = new SqlConnection(Conexion.Conex))
                {
                    Con.Open(); // Realiza la apertura de la conexión hacia el motor SQL.

                    // Preparativos enlazando hacia el SP "Ult_Cliente".
                    SqlCommand cmd = new SqlCommand("Ult_Cliente", Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecutar el comando obteniéndo sólo la primera celda en la primera fila devuelta (escala).
                    object resultado = cmd.ExecuteScalar();

                    // Valida que exista realmente una entrega de parte del ExecuteScalar y que no es nulo SQL.
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        // Castea el objeto entrante hacia un valor entero.
                        ultimoId = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error según tu política imprimendo al menos en consola.
                Console.WriteLine($"Error en capa de datos: {ex.Message}");
                // Eleva la responsabilidad a la siguiente capa por si esta requiere informarlo directamente.
                throw; 
            }

            // Devuelve el Id capturado a partir del motor.
            return ultimoId;
        }

        // Aquí podrías agregar más métodos para interactuar con la base de datos según tus necesidades
    }
}