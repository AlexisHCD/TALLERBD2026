using Datos;
using Entidad;
using System.Collections.Generic;
using System.Data;

// Espacio de nombres que encapsula la lógica de negocio de la aplicación.
namespace Negocio
{
    // Clase NLocPro que actúa como puente entre la presentación y los datos para la entidad Provincia.
    public class NLocPro
    {

        // Método que se encarga de solicitar a la base de datos la lista general de provincias.
        public List<ELocPro> Listar()
        {
            // Devuelve la lista obtenida llamando al método 'Listar' a través de la instancia Singleton en Datos.
            return DLocPro.Instancia.Listar();
        }

        // Devuelve un DataTable conteniendo los registros de provincias filtrados mediante el identificador.
        public DataTable Filtrar(int IdPro)
        {
            // Peticiona el flujo de datos segmentado por ID a su capa subyacente ('Filtrar').
            return DLocPro.Instancia.Filtrar(IdPro);
        }

        // Manejador del ingreso lógico de una provincia. Retorna un marco formalizado de tipo Respuesta.
        public static Respuesta<bool> Ingresar(ELocPro obj)
        {
            // Se asume estado de fallo en la aserción inicial.
            bool Respuesta = false;
            // Se envía a grabar usando el acceso de base de datos capturando su confirmación.
            Respuesta = DLocPro.Instancia.Ingresar(obj);
            // Instancia un reporte en objeto asigando el valor en 'estado' que reflejará si fue grabado correctamente o no.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Enlace lógico intermedio necesario para la re-escritura en tabla (Actualización) de los datos de Provincia.
        public static Respuesta<bool> Actualizar(ELocPro obj)
        {
            // Fija flag en falso por precaución previo al contacto DB.
            bool Respuesta = false;
            // Ejecuta el accionar y absorbe el dictamen del servidor sobre los registros afectados.
            Respuesta = DLocPro.Instancia.Actualizar(obj);
            // Regresa el empaque final en un constructor en línea de 'Respuesta'.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Emite y traslada de forma segura el mandato de dar de baja (eliminar) a una provincia usando solo su 'Id'.
        public static Respuesta<bool> Eliminar(int Id)
        {
            // Set por defecto.
            bool Respuesta = false;
            // Interviene los módulos inferiores informando cuál ID suprimir, recuperando en 'Respuesta' el acierto o denegación.
            Respuesta = DLocPro.Instancia.Eliminar(Id);
            // Finaliza devolviendo contenedor estandarizado según convención del sistema para el negocio.
            return new Respuesta<bool>() { estado = Respuesta };
        }

    }
}