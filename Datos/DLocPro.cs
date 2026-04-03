using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Espacio de nombres para las clases referentes al acceso a datos.
namespace Datos
{
    // Clase pública DLocPro que maneja las interacciones con la base de datos para la entidad Provincia.
    public class DLocPro
    {
        // Se instancia la clase Conexion para usar su información de conexión.
        private Conexion Cn = new Conexion();

        // Variable estática privada para la instancia única del patrón Singleton.
        public static DLocPro _instancia = null;

        // Propiedad estática para acceder a la instancia única de DLocPro.
        public static DLocPro Instancia
        {
            get
            {
                // Si la instancia aún no fue creada, se crea.
                if (_instancia == null)
                {
                    _instancia = new DLocPro();
                }
                // Se retorna la instancia actual.
                return _instancia;
            }
        }

        // Método que realiza una consulta para listar todas las provincias.
        public List<ELocPro> Listar()
        {
            // Crea una lista de provincias para almacenar los resultados devueltos por la base de datos.
            List<ELocPro> Lis = new List<ELocPro>();
            // Configura un bloque using para asegurar que la conexión se cierre de forma automática.
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                // Establece un comando referenciando al store procedure "Bus_LPro".
                SqlCommand cmd = new SqlCommand("Bus_LPro", oConexion);
                // Define el tipo de comando.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Abre la conexión.
                    oConexion.Open();
                    // Obtiene un objeto SqlDataReader tras ejecutar la lectura del comando.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Itera secuencialmente en el bucle sobre cada registro arrojado.
                    while (dr.Read())
                    {
                        // Agrega al listado un nuevo objeto mapeando los datos de base de datos a sus propiedades de clase.
                        Lis.Add(new ELocPro()
                        {
                            IdPro = Convert.ToInt32(dr["IdPro"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            IdReg = Convert.ToInt32(dr["IdReg"].ToString()),
                            Reg = new ELocReg() { Nombre = dr["NombreRegion"].ToString() },
                        });
                    }
                    // Cierra la interfaz de lectura del entorno relacional.
                    dr.Close();
                    // Retorna la colección en caso de éxito del proceso.
                    return Lis;
                }
                catch (Exception)
                {
                    // Ante cualquier eventualidad o condición crítica en tiempo de ejecución, la lista será anulada.
                    Lis = null;
                    return Lis;
                }
            }
        }

        // Método que filtra las provincias según un identificador (ID) y duevelve sus datos en un DataTable.
        public DataTable Filtrar(int Id)
        {
            // Declaración e inicialización del entorno tabular alojado en memoria para depositar respuesta.
            DataTable dt = new DataTable();
            // Colección manual de parámetros que espera el procedimiento.
            List<Parametro> parametros = new List<Parametro>();
            try
            {
                // Ingresa la condición de búsqueda mediante un parámetro "@Id".
                parametros.Add(new Parametro("@Id", Id));
                // Estabiliza una conexión local o del ambiente SQL usando los datos por default.
                using (SqlConnection conexion = new SqlConnection(Conexion.Conex))
                {
                    // Llama y acopla el componente de lógica almacenada a su comando.
                    SqlCommand cmd = new SqlCommand("Fil_Id_LPro", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Recorre e inserta cada uno de los parámetros recolectados en la colección de List.
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                    // Usa el intermediario que traduce entre data sets de memoria y scripts ejecutables.
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // Llena y asfalta la información estructural con las ocurrencias y datos obtenidos al DataTable.
                        da.Fill(dt);
                    }
                }
                // Regresa los datos devueltos en formato crudo.
                return dt;
            }
            catch (Exception ex)
            {
                // Las restricciones capturadas aquí generarán inmediatamente una excepción propagada al origen del llamado.
                throw ex;
            }
        }

        // Función para ingresar a nivel persistente una nueva Provincia.
        public bool Ingresar(ELocPro obj)
        {
            // Instancia el estado general para informar su exitoso paso inicial.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Define la dirección en store para ejecutar un Insert de nivel interno.
                    SqlCommand cmd = new SqlCommand("Ing_LPro", Con);
                    // Provee la variable del campo "Nombre" a partir de ELocPro.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Inserta el campo de jerarquía (clave exógena) que liga a una región mediante "IdReg".
                    cmd.Parameters.AddWithValue("IdReg", obj.IdReg);
                    // Designa parámetro receptor del valor que la base de datos emitirá (Output).
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se activa la sesión con la red SQL Server.
                    Con.Open();
                    // Interacción directa desde comando al motor transaccional.
                    cmd.ExecuteNonQuery();
                    // Guarda el true recogido si el ingreso fue sin incidentes o en otro caso false.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Asigna su estatus final negando el suceso dado un bloqueo por excepción.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Función diseñada con el propósito de aplicar modificaciones sobre datos pre-existentes de una Provincia.
        public bool Actualizar(ELocPro obj)
        {
            // Propuesta inicial dictamina bandera positiva (True).
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Selecciona nombre del proceso a usar por medio de Store Procedure ("Act_LPro").
                    SqlCommand cmd = new SqlCommand("Act_LPro", Con);
                    // Inclumina como llave de condición la ID de provincia enviada en el modelo objeto.
                    cmd.Parameters.AddWithValue("IdPro", obj.IdPro);
                    // Actualización a nivel texto de campo sobre "Nombre".
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Actualización o refrendo relacional para dependencia por región ("IdReg").
                    cmd.Parameters.AddWithValue("IdReg", obj.IdReg);
                    // Configuración sobre la salida esperada en el ámbito base de datos desde Output hacia booleano.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Abrir paso con base de datos.
                    Con.Open();
                    // Conflagra todos los parámetros a través del comando principal al host SQL.
                    cmd.ExecuteNonQuery();
                    // Transita desde variable parámetro al bit recogiendo resolución final.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Errores imprevistos fuerzan denegación del acto negando el éxito lógico general.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Método asignado a la labor de desaparición o exclusión de un dato en provincia alojado ("Borrado lógico/físico").
        public bool Eliminar(int IdPro)
        {
            // Determina éxito y settea default un True a verificar con proceso a base de datos.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Asigna a objeto comando su orden nativa.
                    SqlCommand cmd = new SqlCommand("Eli_LPro", Con);
                    // Vincula información requerida por la base de datos (id).
                    cmd.Parameters.AddWithValue("IdPro", IdPro);
                    // Sopesa estado final en receptor output sobre los esquemas de tablas transaccionales.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Autoriza abrir sesión vía instancia establecida.
                    Con.Open();
                    // Accionar ejecución del Delete a través de sus canales sin lectura bidireccional de contenido.
                    cmd.ExecuteNonQuery();
                    // Valida parámetro saliente transponiendo datos SQL a tipo bool local en "Respuesta".
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // La presencia de constrantes de interconexión con Comunas podría rechazar la acción.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }
    }
}