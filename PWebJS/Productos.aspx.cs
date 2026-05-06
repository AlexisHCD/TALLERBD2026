using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using Entidad;
using Negocio;

namespace PWebJS
{
    public partial class Productos : System.Web.UI.Page
    {
        /*
            Code-behind de Productos.aspx.

            Al igual que otras pantallas del sistema, el front-end (JS/Productos.js) consume estos WebMethods
            para hacer el CRUD vía AJAX y trabajar con respuestas en JSON.

            La capa Web solo expone endpoints y maneja errores básicos.
            La lógica real (listar/ingresar/actualizar/eliminar) se delega a la capa Negocio (NProd).
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // La página se carga y luego el JavaScript se encarga de pedir los datos y llenar la tabla.
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static Respuesta<List<EProd>> Obtener()
        {
            /*
                Retorna el listado de productos.
                UseHttpGet = true porque normalmente este método se consume como una consulta (GET).
                Se devuelve un Respuesta<List<EProd>> para mantener un formato estándar en el front-end.
            */
            var respuesta = new Respuesta<List<EProd>>() { estado = false, objeto = new List<EProd>() };
            try
            {
                var lista = new NProd().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<EProd>();
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.Message;
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(EProd obj)
        {
            /*
                Inserta un producto.
                El objeto "obj" llega desde el modal (form) en Productos.aspx.
                En caso de error, se retorna un mensaje más claro mediante ObtenerMensajeProducto.
            */
            try
            {
                return NProd.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeProducto(ex.Message) };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(EProd obj)
        {
            /*
                Actualiza un producto existente.
                Se espera que el obj contenga IdProd y el resto de campos.
            */
            try
            {
                return NProd.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeProducto(ex.Message) };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdProd)
        {
            /*
                Elimina un producto por Id.
                El front-end normalmente lo llama desde la columna Acciones de la grilla.
            */
            try
            {
                return NProd.Eliminar(IdProd);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeProducto(ex.Message) };
            }
        }

        private static string ObtenerMensajeProducto(string mensaje)
        {
            /*
                Interpreta algunos mensajes de error comunes y los devuelve en un formato más amigable.
                Esto ayuda a mostrar en pantalla errores que el usuario pueda entender.
            */
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                return "No fue posible guardar el producto.";
            }

            var texto = mensaje.ToLowerInvariant();
            if (texto.Contains("nombre"))
            {
                return "El nombre del producto ya está registrado. Ingrese uno diferente.";
            }

            return mensaje;
        }
    }
}
