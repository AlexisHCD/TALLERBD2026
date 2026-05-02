using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Se define el espacio de nombres 'Datos' para agrupar todas las clases responsables del acceso a la base de datos.
namespace Datos
{
    // Clase pública DLocCom que gestiona las operaciones en la base de datos para la entidad Comuna (Localidad).
    public class DLocCom
    {
        // Se instancia la clase Conexion para poder obtener la cadena de conexión.
        private Conexion Cn = new Conexion();

        // Variable estática y privada que guardará la única instancia de la clase DLocCom.
        public static DLocCom _instancia = null;

        // Propiedad estática que retorna la instancia única, implementando el patrón Singleton.
        public static DLocCom Instancia
        {
            get
            {
                // Verifica si la instancia aún no existe.
                if (_instancia == null)
                {
                    // De no existir, crea una nueva instancia de DLocCom.
                    _instancia = new DLocCom();
                }
                // Retorna la instancia existente o la recién creada.
                return _instancia;
            }
        }

        // Método que realiza una consulta para listar todas las comunas almacenadas.
        public List<ELocCom> Listar()
        {
            // Inicializa una lista vacía de tipo ELocCom.
            List<ELocCom> Lis = new List<ELocCom>();
            // Configura un bloque using para asegurar que la conexión se cierre correctamente al terminar.
            using (SqlConnection oConexion = new SqlConnection(Conexion.Conex))
            {
                // Crea un comando para ejecutar el procedimiento almacenado "Bus_LCom".
                SqlCommand cmd = new SqlCommand("Bus_LCom", oConexion);
                // Establece el tipo de comando a procedimiento almacenado.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    // Abre la conexión a la base de datos.
                    oConexion.Open();
                    // Ejecuta el procedimiento y recupera los datos devueltos como un DataReader.
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Itera en ciclos a través de cada registro obtenido por el DataReader.
                    var provincias = DLocPro.Instancia.Listar(); // Obtiene la lista de provincias
                    var provinciaLookup = new Dictionary<int, ELocPro>(); // Crea un diccionario para el lookup
                    if (provincias != null) // Verifica si la lista de provincias no es nula
                    {
                        foreach (var provincia in provincias) // Itera sobre cada provincia
                        {
                            if (!provinciaLookup.ContainsKey(provincia.IdPro)) // Verifica si la provincia ya está en el diccionario
                            {
                                provinciaLookup.Add(provincia.IdPro, provincia); // Agrega la provincia al diccionario
                            }
                        }
                    }

                    while (dr.Read())
                    {
                        var idPro = Convert.ToInt32(dr["IdPro"].ToString());
                        var idReg = Convert.ToInt32(dr["IdReg"].ToString());
                        var nombreProvincia = dr["NombreProvincia"].ToString();
                        var nombreRegion = dr["NombreRegion"].ToString();

                        if (provinciaLookup.TryGetValue(idPro, out var provincia))
                        {
                            if (!string.IsNullOrWhiteSpace(provincia.Nombre))
                            {
                                nombreProvincia = provincia.Nombre;
                            }

                            if (provincia.Reg != null)
                            {
                                idReg = provincia.Reg.IdReg != 0 ? provincia.Reg.IdReg : idReg;
                                if (!string.IsNullOrWhiteSpace(provincia.Reg.Nombre))
                                {
                                    nombreRegion = provincia.Reg.Nombre;
                                }
                            }
                        }

                        // Instancia una nueva Comuna, mapea sus datos y la agrega a la lista de retorno.
                        Lis.Add(new ELocCom()
                        {
                            IdCom = Convert.ToInt32(dr["IdCom"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            IdPro = idPro,
                            Pro = new ELocPro() { IdPro = idPro, Nombre = nombreProvincia },
                            IdReg = idReg,
                            Reg = new ELocReg() { IdReg = idReg, Nombre = nombreRegion },
                        });
                    }
                    // Cierra el SqlDataReader una vez se han leído todos los registros.
                    dr.Close();
                    // Retorna la lista con los elementos extraídos de la base de datos.
                    return Lis;
                }
                catch (Exception)
                {
                    // Si ocurre una excepción, limpia o anula la respuesta de la lista.
                    Lis = null;
                    return Lis;
                }
            }
        }

        // Método que filtra comunas dependiendo de un Id y devuelve un formato tabular de datos.
        public DataTable Filtrar(int Id)
        {
            var dt = new DataTable();
            dt.Columns.Add("IdCom", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("IdPro", typeof(int));
            dt.Columns.Add("IdReg", typeof(int));

            var lista = Listar();
            if (lista == null)
            {
                return dt;
            }

            foreach (var item in lista)
            {
                if (item.IdPro == Id)
                {
                    dt.Rows.Add(item.IdCom, item.Nombre, item.IdPro, item.IdReg);
                }
            }

            return dt;
        }

        // Método de ingreso que inserta una nueva Comuna en la base de datos.
        public bool Ingresar(ELocCom obj)
        {
            // Declara el estado de la respuesta antes de ejecutar e inicializa a True.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Prepara el comando llamando al prodecimiento "Ing_LCom".
                    SqlCommand cmd = new SqlCommand("Ing_LCom", Con);
                    // Introduce la variable Nombre al comando para la base de datos.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Introduce la llave foránea Provincia en el comando SQL.
                    cmd.Parameters.AddWithValue("IdPro", obj.IdPro);
                    // Instancia un parámetro para obtener el resultado Bit o booleano de un Output.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Apertura de estado conectivo.
                    Con.Open();
                    // Interfaz final a la de base de datos para la ejecución sin lectura relacional.
                    cmd.ExecuteNonQuery();
                    // Recibe la conversión resultante desde la base de datos de 1 a True.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Fallos se reflejarán retornando False.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Método que realiza la actualización de datos de una Comuna en la base de datos.
        public bool Actualizar(ELocCom obj)
        {
            // Respuesta de efectividad por defecto en true, pendiente de fallos del motor lógico.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Refiere al procedimiento general para editar la Comuna guardada.
                    SqlCommand cmd = new SqlCommand("Act_LCom", Con);
                    // Establece un mapeo entre la llave primaria IdCom para identificar qué se va a editar.
                    cmd.Parameters.AddWithValue("IdCom", obj.IdCom);
                    // Propaga valores actualizados al comando.
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    // Incluye la llave foránea por si esta fue cambiada.
                    cmd.Parameters.AddWithValue("IdPro", obj.IdPro);
                    // Reclama la salida como parámetro configurada como Bit.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Se enruta hacia el origen y se inicia.
                    Con.Open();
                    // Desata la query en el ambiente Sql Server.
                    cmd.ExecuteNonQuery();
                    // Cambia la variable booleana final utilizando el estado retornado por el mismo SP.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Determina a False y notifica a las siguientes capas en su uso si hay detención de contexto.
                    Respuesta = false;
                }
            }
            return Respuesta;
        }

        // Función por la cual una Comuna puede ser removida en el sistema (eliminada).
        public bool Eliminar(int IdCom)
        {
            // Asigna un booleano por defecto al registro actual para notificar en cada caso.
            bool Respuesta = true;
            using (SqlConnection Con = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Construye un comando relacionado con el Script de "Eli_LCom".
                    SqlCommand cmd = new SqlCommand("Eli_LCom", Con);
                    // Paramétrica del ID de Comuna identificando inequívocamente la tupla en la base de datos.
                    cmd.Parameters.AddWithValue("IdCom", IdCom);
                    // Receptor de respuestas nativas del transaccional Output "Resultado".
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Concede pase físico de datos por TCP/IP o conducto abierto.
                    Con.Open();
                    // Ejecuta el evento sin interrupción devolviendo filas afectadas en segundo plano.
                    cmd.ExecuteNonQuery();
                    // La flag se nutre de la comprobación interna para finalizar.
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception)
                {
                    // Cualquier limitación o constrain foráneo cortará a "falso".
                    Respuesta = false;
                }
            }
            return Respuesta;
        }
    }
}