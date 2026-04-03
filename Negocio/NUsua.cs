using Datos;
using Entidad;
using System.Collections.Generic;

// Se define el espacio aplicacional 'Negocio' dictaminando las reglas para la manipulación de información.
namespace Negocio
{
    // Clase controladora dedicada a orquestar las verificaciones y procesos operativos de Usuario (NUsua).
    public class NUsua
    {
        // Instancia interna comentada o suspendida previamente.
        //private NUsua Datos = new NUsua();

        // Control y mediación encargada de realizar un Insert de un nuevo operador Usuario devolviendo respuesta.
        public static Respuesta<bool> Insertar(EUsua obj)
        {
            // Apunta y ejecuta usando la clase Instanciada DUsua el comando Inserción devolviendo estado en 'respuesta'.
            bool respuesta = DUsua.Instancia.Insertar(obj);
            // Empaqueta como éxito/fallo retornando variable y sumando además la descripción literal devuelta al front.
            return new Respuesta<bool>() { estado = respuesta, valor = respuesta ? "Usuario insertado correctamente" : "Error al insertar usuario" };
        }

        // Bloque central para consultar si un usuario presenta la acreditación o credencial adecuada en base de datos.
        public static Respuesta<bool> Verificar(EUsua obj)
        {
            // Responde al método homónimo Verificar consultando en origen (SQL Server).
            bool respuesta = DUsua.Instancia.Verificar(obj); 
            // Devuelve flag estado junto con texto representativo del acierto "Inicio... exitoso" frente al error "Incorrecto".
            return new Respuesta<bool>() { estado = respuesta, valor = respuesta ? "Inicio de sesión exitoso" : "Usuario o contraseña incorrectos" };
        }

        // Extractor unificado para recolectar el listado integral de Cuentas alojadas.
        public List<EUsua> Listar()
        {
            // Atajo con puente directo a capa Persistente (Datos) recuperando estructura de Lista EUsua.
            return DUsua.Instancia.Listar();
        }
    }
}
