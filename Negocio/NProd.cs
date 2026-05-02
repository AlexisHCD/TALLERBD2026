using Datos;
using Entidad;
using System.Collections.Generic;

// Definición local para el conjunto de componentes lógicos o Reglas de Negocio en la arquitectura del proyecto.
namespace Negocio
{
    // Clase pública representativa a las tareas del componente administrativo Producto.
    public class NProd
    {
        public List<EProd> Listar()
        {
            return DProd.Instancia.Listar();
        }

        public static Respuesta<bool> Ingresar(EProd obj)
        {
            bool Respuesta = false;
            Respuesta = DProd.Instancia.Ingresar(obj);
            return new Respuesta<bool>() { estado = Respuesta };
        }

        public static Respuesta<bool> Actualizar(EProd obj)
        {
            bool Respuesta = false;
            Respuesta = DProd.Instancia.Actualizar(obj);
            return new Respuesta<bool>() { estado = Respuesta };
        }

        public static Respuesta<bool> Eliminar(int Id)
        {
            bool Respuesta = false;
            Respuesta = DProd.Instancia.Eliminar(Id);
            return new Respuesta<bool>() { estado = Respuesta };
        }
    }
}
