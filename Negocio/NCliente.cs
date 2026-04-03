using Datos;
using Entidad;
using System.Collections.Generic;

// Espacio de nombres para la capa de Negocio, donde se gestiona la lógica de la aplicación y sirve de puente transversal.
namespace Negocio
{
    // Clase pública NCliente encargada de orquestar las operaciones sobre la entidad Cliente frente a la base de datos.
    public class NCliente
    {
        // Método que invoca la función de extracción a nivel de datos para listar todos los clientes.
        public List<ECliente> Listar()
        {
            // Llama a la instancia única Singleton de DCliente y consume su método Listar devolviendo el listado directamente.
            return DCliente.Instancia.Listar();
        }

        // Función puente que valida u obtiene una coincidencia o búsqueda de un cliente específico enviado desde Presentación.
        public bool Buscar(ECliente obj)
        {
            // Responde al usuario con el estado booleano nativo obtenido directo del análisis de BD.
            return DCliente.Instancia.Buscar(obj);
        }

        // Método lógico estático que comanda el alojamiento de nuevos Clientes en datos y envuelve el suceso en un contenedor Respuesta.
        public static Respuesta<bool> Ingresar(ECliente obj)
        {
            // Crea e inicializa el indicador local orientándolo a 'fallo' para ser optimista controlado.
            bool Respuesta = false;
            // Envía la orden y modelo a la base de datos reescribiendo la respuesta con su resultado.
            Respuesta = DCliente.Instancia.Ingresar(obj);
            // Retorna un nuevo objeto Respuesta con la bandera booleana determinando su éxito o fracaso al solicitante.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método estático destinado para aplicar actualizaciones y modificaciones a los atributos internos del cliente.
        public static Respuesta<bool> Actualizar(ECliente obj)
        {
            // Inicia variable respuesta en falso hasta que datos afirme lo contrario.
            bool Respuesta = false;
            // Interpela a DCliente inyectando el objeto recabado modificando la bandera actual.
            Respuesta = DCliente.Instancia.Actualizar(obj);
            // Retorna sobre la variable 'estado' del marco 'Respuesta' el producto final.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método de puente lógico encargado de instruir el borrado de cliente según el entero identificador suministrado.
        public static Respuesta<bool> Eliminar(int Id)
        {
            // Declaración controlada predispuesta a falso.
            bool Respuesta = false;
            // Transacción ejecutada consumiendo Eliminar sobre DCliente.Instancia en la capa enlazada.
            Respuesta = DCliente.Instancia.Eliminar(Id);
            // Empaqueta como estado interno del tipo 'Respuesta' el resolutivo y se eleva a presentación.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método lógico funcional exclusivo para consultar una ID recién auto-generada sin envoltorio (wrapper).
        public int ObtenerUltimoId()
        {
            // Delegar la llamada al método correspondiente en la capa de datos
            return DCliente.Instancia.ObtenerUltimoId();
        }
    }
}