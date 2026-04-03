using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

// Espacio de nombres que agrupa las clases de la capa de acceso a datos.
namespace Datos
{
    // Clase pública DUsua encargada de manejar los datos persistentes de los usuarios.
    public class DUsua
    {
        // Instancia de Conexion que proporciona parámetros de acceso a la base de datos.
        private Conexion Cn = new Conexion();

        // Variable privada y estática para aplicar el patrón Singleton y mantener una única instancia en memoria.
        public static DUsua _instancia = null;

        // Propiedad de acceso global a la instancia única.
        public static DUsua Instancia
        {
            get
            {
                // Si aún no se creó, aquí se inicializa por única vez.
                if (_instancia == null)
                {
                    _instancia = new DUsua();
                }
                return _instancia;
            }
        }

        // Método para recoger un listado de todos los usuarios registrados.
        public List<EUsua> Listar()
        {
            // Inicializa nueva Lista para acumular objetos de Usuario.
            List<EUsua> Lis = new List<EUsua>();
            // Declaración controlada del flujo a base de datos.
            using (SqlConnection con = new SqlConnection(Conexion.Conex))
            {
                // Usa un procedimiento explícito de selección "Bus_Usua".
                SqlCommand cmd = new SqlCommand("Bus_Usua", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Abre conexión contra el motor de DB.
                    con.Open();
                    // Devuelve todos los matches sobre el script solicitado.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Itera secuencialmente llenando la lista con el DataReader.
                    while (dr.Read())
                    {
                        // Agrega una instancia nueva y le asigna las propiedades recopiladas.
                        Lis.Add(new EUsua()
                        {
                            IdUsu = Convert.ToInt32(dr["IdUsu"]),
                            Nombre = dr["Nombre"].ToString(),
                            Pass = dr["Pass"].ToString(),
                        });
                    }
                    // Cierra canal de lectura de datos.
                    dr.Close();
                    // Regresa lista al negocio/presentación.
                    return Lis;
                }
                catch (Exception)
                {
                    // Caídas durante este proceso arrojan una lista vacía/nula.
                    Lis = null;
                    return Lis;
                }
            }
        }
        // Método que realiza un alta de registro (inserción) de un usuario en la tabla.
        public bool Insertar(EUsua obj)
        {
            // Asigna falso indicando que la labor aún no es exitosa.
            bool respuesta = false;
            // Prepara bloque using por su cualidad destructiva del conector evitando bloqueos en memoria.
            using (SqlConnection con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Relaciona Comando de SQL con script interno "Ing_Usua".
                    SqlCommand cmd = new SqlCommand("Ing_Usua", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Introduce texto en variable parámetro Nombre extraída del objeto de entrada.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Pasa como parámetro el password del mismo desde su fuente original.
                    cmd.Parameters.AddWithValue("Pass", obj.Pass);

                    // Petición de estado Online con base de datos.
                    con.Open();
                    // Interfaz final a nivel procesal por la de que acciona modificaciones estructurales en tabla sin respuesta iterativa.
                    cmd.ExecuteNonQuery();
                    // Confirma final feliz y altera estado al terminar ininterrumpidamente el bloque superior.
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    // Fallas revierten o aseguran no se notifique como éxito, devolviendo en bandera "Falso" (False).
                    respuesta = false;
                }
            }
            return respuesta;
        }
        // Método para actualizar de manera general los datos que constituyen una cuenta de usuario.
        public bool Actualizar(EUsua obj)
        {
            // Fija resultado general apuntado a verdadero provisoriamente.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Llama y apunta la instrucción comando hacia "Act_Usua".
                    SqlCommand cmd = new SqlCommand("Act_Usua", Con);
                    // Establece un mapeo entre la llave IdUsu enviada a objeto y procesada allí.
                    cmd.Parameters.AddWithValue("IdUsu", obj.IdUsu);
                    // Nombre modificado/nuevo que se insertará vía Actualización.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Constraseña asociada a actualizar.
                    cmd.Parameters.AddWithValue("Pass", obj.Pass);
                    // Dispone de un canal output configurado en formato Bit esperando resultado natural tras SP ejecutarse.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Despliega paso seguro al SQL Engine.
                    Con.Open();
                    // Causa operación interna sobre el servidor.
                    cmd.ExecuteNonQuery();
                    // Convierte Output y resetea sobre Respuesta lo emitido por Motor de Datos.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Niega confirmación de modificación tras un accidente o error en medio transaccional.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Método paralelo para actualizar parcialmente solo la contraseña (o propiedades alternativas según script) del Usuario.
        public bool Actualizar2(EUsua obj)
        {
            // Identificador pre-cargado asumiendo su ejecución inmaculada.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Cita un script secundario designado "Act_Usua2".
                    SqlCommand cmd = new SqlCommand("Act_Usua2", Con);
                    // Refiere por igual al ID transaccional a afectar.
                    cmd.Parameters.AddWithValue("IdUsu", obj.IdUsu);
                    // Envía únicamente Contraseña presuntamente en un cambio específico aislado.
                    cmd.Parameters.AddWithValue("Pass", obj.Pass);
                    // Acopla Bit transductor como monitor de un cierre de ejecución satisfactorio en DB.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Apertura de conexión de canal.
                    Con.Open();
                    // Ejecuta Script de modificación.
                    cmd.ExecuteNonQuery();
                    // Absorbe parámetro Out booleano a local Respuesta.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // De no poder terminar o interrupciones devolver falso indicando fallo.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }
        // Método utilizado para verificar mediante validación una cuenta existente.
        public bool Verificar(EUsua obj)
        {
            // Denota como base preestablecida fallo si es que no existe.
            bool respuesta = false;
            using (SqlConnection con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Prepara llamada a Store Procedure "IngSis" el cual devuelve 1 si encuentra usuario válido por credenciales.
                    SqlCommand cmd = new SqlCommand("IngSis", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Extrae valores de Usuario modelo al script validador.
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Pass", obj.Pass);

                    // Emite conexión.
                    con.Open();
                    // Interroga valor a la DB usando SqlDataReader.
                    SqlDataReader reader = cmd.ExecuteReader();
                    // Evalúa presencia y acceso a los bloques transmitidos al dar read.
                    if (reader.Read())
                    {
                        // Si existe resultado con un 1 es que hubo aciertos y lo equipara asignando "true" a la respuesta.
                        respuesta = reader.GetInt32(0) == 1;
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción (log, re-throw, etc.)
                    // Ante fallo o errores durante consulta finaliza en false.
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
