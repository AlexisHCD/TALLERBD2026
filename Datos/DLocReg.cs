using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

// Espacio de nombres que agrupa a todas las clases relacionadas con la conexión y operaciones de base de datos.
namespace Datos
{
    // Clase DLocReg destinada a la gestión en base de datos de la entidad Región.
    public class DLocReg
    {
        // Se instancia un objeto de Conexion para acceder a las credenciales estáticas al motor SQL.
        private Conexion Cn = new Conexion();

        // Instancia estática privada indispensable para el diseño Singleton.
        public static DLocReg _instancia = null;

        // Atributo estático que verifica y retorna la única instancia de conexión en ejecución a esta clase.
        public static DLocReg Instancia
        {
            get
            {
                // Si es nulo, significa que es la primera vez que se invoca. Se crea la instancia en esta línea.
                if (_instancia == null)
                {
                    _instancia = new DLocReg();
                }
                // Retorna en todos los casos el objeto instanciado.
                return _instancia;
            }
        }

        // Método para solicitar una lista con todas las regiones alojadas en la plataforma (Base de datos).
        public List<ELocReg> Listar()
        {
            // Propuesta de lista en estructura vacía donde se irán almacenando los ELocReg.
            List<ELocReg> Lis = new List<ELocReg>();
            // Bloque que delimita la vida útil de la conexión instanciada en SqlConnection con liberación de recursos automático.
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                // El comando utilizará el SP "Bus_LReg".
                SqlCommand cmd = new SqlCommand("Bus_LReg", oConexion);
                // Tipo de comando es de tipo procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Abrimos los conductos hacia el SQL.
                    oConexion.Open();
                    // Lee todos los bloques arrojados por el script.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Interroga la siguiente fila de repuesta disponible.
                    while (dr.Read())
                    {
                        // Instancia por cada fila un nuevo objeto ELocReg captando "IdReg" y "Nombre".
                        Lis.Add(new ELocReg()
                        {
                            IdReg = Convert.ToInt32(dr["IdReg"]),
                            Nombre = dr["Nombre"].ToString(),
                        });
                    }
                    // Desvincula e inhabilita lector actual de Data Reader.
                    dr.Close();
                    // Retorna lista llena a la capa que lo demandó.
                    return Lis;
                }
                catch (Exception)
                {
                    // Manejo en caso de caídas anulará la colección dejándola null.
                    Lis = null;
                    return Lis;
                }
            }
        }

        // Método por el que se envía un nuevo objeto con el propósito de convertirse en persistente.
        public bool Ingresar(ELocReg obj)
        {
            // Set del estatus de la respuesta esperando confirmación.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Preparativos ejecutivos utilizando al SP llamado "Ing_LReg".
                    SqlCommand cmd = new SqlCommand("Ing_LReg", Con);
                    // Introduce campo alfabético del contenedor nombre.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Solicita del SP un bit que informará de efectividad de guardado.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Proceso de abriertura en vía SqlServer.
                    Con.Open();
                    // Se remite a procesar la tabla sin devolver una consulta general.
                    cmd.ExecuteNonQuery();
                    // Convierte la contestación proveniente de base de datos a lógica computacional Booleana.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        // Vía la cual se permite cambiar la designación de nombre de una Región específica pre-ingresada.
        public bool Actualizar(ELocReg obj)
        {
            // Asigna inicialmente como éxito una transacción de nivel UPDATE.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Selecciona script interno de nombre "Act_LReg".
                    SqlCommand cmd = new SqlCommand("Act_LReg", Con);
                    // Vincula y parametriza con el código interno de la Región, actuando como clave primaria.
                    cmd.Parameters.AddWithValue("IdReg", obj.IdReg);
                    // Traspaso de campo textual ("Nombre").
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Define qué campo será utilizado para capturar en bit si la query corrió perfecta.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Acceso concedido creando conexión.
                    Con.Open();
                    // Comienza en cascada ejecución de script final con parámetros.
                    cmd.ExecuteNonQuery();
                    // Recopila el último bit arrojado por MS-SQL para transformarlo al tipo Bool.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Respuesta;
        }

        // Medio para purgar de manera definitiva uno de los registros clasificados como Region utilizando la ID.
        public bool Eliminar(int Id)
        {
            // Indica True a ser alterado si el motor impidiera su remoción en tabla.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Llama "Eli_LReg" de la estructura SQL provista.
                    SqlCommand cmd = new SqlCommand("Eli_LReg", Con);
                    // Incorpora id en @IdReg para discriminar fila individual.
                    cmd.Parameters.AddWithValue("IdReg", Id);
                    // Obtención de respuesta provista internamente confirmando éxito en output.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Pide y mantiene libre de forma temporal conexión hacia la fuente.
                    Con.Open();
                    // Resuelve en plano SQL Server la tarea Delete u Ocultamiento lógico.
                    cmd.ExecuteNonQuery();
                    // Recupera output asigando como booleano en "Respuesta".
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