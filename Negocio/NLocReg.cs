using Datos;
using Entidad;
using System.Collections.Generic;

// Espacio de nombres para encapsular operaciones lógicas e interacciones de negocio.
namespace Negocio
{
    // Clase manejadora de flujo orientada al control interno de la entidad Región (ELocReg).
    public class NLocReg
    {
        // Peticiona y despliega la obtención global de Regiones.
        public List<ELocReg> Listar()
        {
            // Ejecuta y devuelve la colección conseguida desde su acceso a datos DLocReg correspondiente.
            return DLocReg.Instancia.Listar();
        }

        // Método estático encajando la petición de crear una memoria persistente para una nueva Región.
        public static Respuesta<bool> Ingresar(ELocReg obj)
        {
            // Presuponer el resultado como no-satisfactorio hasta comprobarlo en BD.
            bool Respuesta = false;
            // Procesa directamente hacia Datos el objeto entidad enviando el comando insert nativo.
            Respuesta = DLocReg.Instancia.Ingresar(obj);
            // Remite a la aplicación superior la conversión a objeto envoltorio Respuesta tipificada.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método lógico mediante el cual se ordenan y procesan los cambios de una Región específica sobre parámetros existentes.
        public static Respuesta<bool> Actualizar(ELocReg obj)
        {
            // Inicializa flag a false.
            bool Respuesta = false;
            // Se sobrepone el resultado de la función sobre dicho flag informando cómo transcurrió todo en el SQL.
            Respuesta = DLocReg.Instancia.Actualizar(obj);
            // Retorna validando si el UPDATE culminó en éxito (true).
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Método lógico encargado de recibir el Id de destino para ejecutar el acto destitutivo de una Región en el sistema.
        public static Respuesta<bool> Eliminar(int Id)
        {
            // Inicia una variable de control con valor primario restrictivo (False).
            bool Respuesta = false;
            // Inicia eliminación a través de capa D delegada del borrado físico o en cadena del ID.
            Respuesta = DLocReg.Instancia.Eliminar(Id);
            // Enmascara al estado primario dentro del formato transaccional Respuesta y lo expone.
            return new Respuesta<bool>() { estado = Respuesta };
        }
    }
}