using Datos;
using Entidad;
using System.Collections.Generic;

// Definición local para el conjunto de componentes lógicos o Reglas de Negocio en la arquitectura del proyecto.
namespace Negocio
{
    // Clase pública representativa a las tareas del componente administrativo Proveedor.
    public class NProv
    {

        // Exposición pública del vector general que lista cada Proveedor que el sistema alberga.
        public List<EProv> Listar()
        {
            // Apunta hacia la base delegando la recolección en DProv (Datos Proveedor) Listar().
            return DProv.Instancia.Listar();
        }

        // Petición controlada de alta o inserción de un elemento Proveedor.
        public static Respuesta<bool> Ingresar(EProv obj)
        {
            // Seteo en Falso anticipando error sistémico.
            bool Respuesta = false;
            // Orden asiganda hacia el motor de persistencia inyectando el objeto en la tabla.
            Respuesta = DProv.Instancia.Ingresar(obj);
            // Presentador unificado que comunica mediante estructura la certeza transaccional de estado.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Empleo lógico de actualización provisoria y total de datos sobre un Proveedor con ID existente.
        public static Respuesta<bool> Actualizar(EProv obj)
        {
            // Valor primitivo que actúa como precondición falsa para validar transcurridos y excepciones.
            bool Respuesta = false;
            // La entidad con datos editados es procesada y transmutada hacia base de datos real.
            Respuesta = DProv.Instancia.Actualizar(obj);
            // Regresa sobre la forma Response confirmando su labor positiva o negativa a quien lo requiera.
            return new Respuesta<bool>() { estado = Respuesta };
        }

        // Comando y puente de exclusión sobre un Proveedor.
        public static Respuesta<bool> Eliminar(int Id)
        {
            // Predispone Falso antes de pedir a BD la operación con efectos.
            bool Respuesta = false;
            // Transfiere directiva al objeto Data Access de Proveedor suprimiendo referenciado ID.
            Respuesta = DProv.Instancia.Eliminar(Id);
            // Retorna al formulario en ventana encapsulado si el efecto se propagó o impidió.
            return new Respuesta<bool>() { estado = Respuesta };
        }
    }
}
