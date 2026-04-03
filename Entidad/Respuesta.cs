// Espacio de nombres que agrupa a todas las clases de la capa Entidad.
namespace Entidad
{
    // Clase genérica Respuesta<T> utilizada para estandarizar el retorno de información y estados entre capas.
    public class Respuesta<T>
    {
        // Propiedad booleana que indica si la operación fue exitosa (true) o fallida (false).
        public bool estado { get; set; }

        // Propiedad de texto para devolver mensajes, alertas o descripciones de error generados.
        public string valor { get; set; }

        // Propiedad genérica 'objeto' que transporta cualquier dato o entidad (T) solicitada desde la base de datos.
        public T objeto { get; set; }
    }
}
