// Se define el espacio de nombres 'Datos' para agrupar todas las clases de acceso a datos.
namespace Datos
{
    // Se crea la clase pública 'Conexion' que almacenará la información para conectarse a la base de datos.
    public class Conexion
    {
        // Se define una variable estática 'Conex' que contiene la cadena de conexión a SQL Server.
        // Conexion de mi pc - configurado para usar LocalDB
        public static string Conex = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SNet;Integrated Security=True";

        // Cadena de conexión alternativa comentada, útil para cambiar de entorno rápidamente.
        // Conexion de pc AIEP
        //public static string Conex = "Data source=SAANLABPC3102;Initial Catalog=SNet;User ID=SA;Password=LabAiep2026.";
    }
}
