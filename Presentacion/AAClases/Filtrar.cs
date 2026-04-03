// Espacio de nombres para las clases accesorias de Presentación.
namespace Presentacion.AAClases
{
    // Clase pública Filtrar utilizada generalmente para poblar ComboBoxes o controles vinculados con pares clave/valor.
    public class Filtrar
    {
        // Propiedad que guarda el texto visible para el usuario en la interfaz.
        public string Texto { get; set; }

        // Propiedad que guarda el valor real o interno oculto asociado a dicha selección.
        public object Valor { get; set; }
    }

}