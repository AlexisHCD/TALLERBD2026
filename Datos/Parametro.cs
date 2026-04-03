// Se define el espacio de nombres 'Datos' para agrupar esta clase de utilidad con el resto del acceso a datos.
namespace Datos
{
    // Clase pública Parametro útil para transportar de manera unificada el nombre y valor de los parámetros SQL.
    public class Parametro
    {
        // Propiedad que almacenará el nombre del parámetro (ej: "@Id").
        public string Nombre { get; set; }
        // Propiedad de tipo genérico (object) que guardará el valor correspondiente a dicho parámetro.
        public object Valor { get; set; }

        // Constructor de la clase Parametro que exige definir el nombre y el valor al momento de ser instanciada.
        public Parametro(string nombre, object valor)
        {
            // Asigna el nombre provisto en los argumentos a la propiedad pública de clase.
            Nombre = nombre;
            // Asigna el valor del argumento a su homólogo público sin importar su tipo subyacente.
            Valor = valor;
        }
    }
}