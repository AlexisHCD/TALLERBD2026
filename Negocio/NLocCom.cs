using Datos;
using Entidad;
using System.Collections.Generic;
using System.Data;

// Se define el espacio de nombres 'Negocio' donde se ubicarán las reglas y la lógica de la aplicación.
namespace Negocio
{
    // Clase pública NLocCom (Negocio Localidad Comuna) que interactúa con la capa de datos para la entidad Comuna.
    public class NLocCom
    {

        // Método encargado de listar todas las comunas registradas, devuelve una lista de objetos ELocCom.
        public List<ELocCom> Listar()
        {
            // Se invoca el método Listar() exponiéndose desde la instancia de acceso a datos (DLocCom).
            return DLocCom.Instancia.Listar();
        }

        // Método que permite obtener una colección de datos en formato DataTable de comunas filtradas por su identificador.
        public DataTable Filtrar(int IdCom)
        {
            // Ejecuta la función Filtrar pasándole el parámetro numérico de identificación y devuelve la tabla resultante.
            return DLocCom.Instancia.Filtrar(IdCom);
        }

        // Función estática encargada de ordenar la inserción de un nuevo registro para Comuna y empaquetar una Respuesta.
        public static Respuesta<bool> Ingresar(ELocCom obj)
        {
            // Se define el estado inicial en 'falso', denotando que la inserción aún no ocurre o no es exitosa.
            bool Respuesta = false;
            // Llama la correspondiente función a nivel de Datos y guarda su retorno.
            Respuesta = DLocCom.Instancia.Ingresar(obj);
            // Retorna a quien lo invocó un objeto de tipo Respuesta genérico asignando el estado devuelto al atributo 'estado'.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método estático para gestionar de forma transicional la actualización de una comuna.
        public static Respuesta<bool> Actualizar(ELocCom obj)
        {
            // Inicializa en 'false' un indicador para controlar si la operación resulta positiva o con tropiezos.
            bool Respuesta = false;
            // Dispara el proceso de actualización en la BD y sobreescribe la variable 'Respuesta'.
            Respuesta = DLocCom.Instancia.Actualizar(obj);
            // Responde con la aserción o negación de la ejecución, empaquetada.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método transaccional dedicado a encargar la eliminación física o lógica de un registro en la BD.
        public static Respuesta<bool> Eliminar(int Id)
        {
            // Set del booleano para certificar negación predeterminada.
            bool Respuesta = false;
            // Transfiere la acción a la capa de datos pasando únicamente el Id, almacenando 'true' si surte efecto.
            Respuesta = DLocCom.Instancia.Eliminar(Id);
            // Devuelve una nueva instancia de 'Respuesta' acarreando la confirmación de la labor exigida.
            return new Respuesta<bool>() { estado = Respuesta };
        }
    }
}