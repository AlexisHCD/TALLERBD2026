using System;
using System.Data;
using System.Data.SqlClient;

// Se define el espacio de nombres 'Datos' para organizar las clases relacionadas con la conexión a datos.
namespace Datos
{
    // Clase pública DLogin encargada de gestionar los procesos de inicio de sesión o validación de credenciales.
    public class DLogin
    {
        // Se crea una instancia de la clase Conexion para acceder a los métodos y strings de la base de datos.
        private Conexion Cn = new Conexion();

        // Instancia estática privada para implementar el patrón de diseño Singleton.
        public static DLogin _instancia = null;

        // Propiedad estática para proveer una única instancia de DLogin a lo largo de toda la aplicación.
        public static DLogin Instancia
        {
            get
            {
                // Verifica si la instancia es nula (es decir, no ha sido creada aún).
                if (_instancia == null)
                {
                    // Si es nula, inicializa una nueva instancia de la clase.
                    _instancia = new DLogin();
                }
                // Si ya fue creada o acaba de ser inicializada, la devuelve.
                return _instancia;
            }
        }

        // Método que recibe un Nombre de usuario y un Password (Pass) para validar el inicio de sesión del sistema.
        public int IngSig(string Nombre, string Pass)
        {
            // Inicializa la variable Respuesta en 0, asumiendo que un valor de 0 indica fallo o acceso denegado.
            int Respuesta = 0;

            // Bloque using para asegurar la liberación automática y segura de la conexión luego de su uso.
            using (SqlConnection connection = new SqlConnection(Conexion.Conex))
            {
                try
                {
                    // Prepara el comando SQL referenciando al procedimiento almacenado "IngSig" por su nombre.
                    SqlCommand command = new SqlCommand("IngSig", connection);
                    // Agrega el parámetro de entrada con el Nombre digitado por el usuario en la vista.
                    command.Parameters.AddWithValue("Nombre", Nombre);
                    // Agrega el parámetro correspondiente para validar el Password de ese usuario.
                    command.Parameters.AddWithValue("Pass", Pass);
                    // Define un parámetro de salida (Output) temporal de tipo entero para recibir de retorno el ID del usuario validado.
                    command.Parameters.Add("IdUsu", SqlDbType.Int).Direction = ParameterDirection.Output;
                    // Se denota explicitamente la naturaleza del llamado a procedimiento y no tabla.
                    command.CommandType = CommandType.StoredProcedure;

                    // Se abre conexión con la interfaz del motor de datos.
                    connection.Open();

                    // Ejecución sin retorno relacional; el resultado se deposita íntegramente en las variables de Output asociadas.
                    command.ExecuteNonQuery();

                    // Convierte el valor de Output recabado en objeto Int32 que se transmitirá hacia base para continuar el Login.
                    Respuesta = Convert.ToInt32(command.Parameters["IdUsu"].Value);

                }
                catch (Exception)
                {
                    // En caso de que se lance cualquier excepción en el proceso (red, caídas, etc.), se devuelve un fallo silenciado (0).
                    Respuesta = 0;
                }
            }
            // Retorna al llamador de negocio o vista superior el número Id del usuario o valor 0 fallido.
            return Respuesta;
        }
    }
}