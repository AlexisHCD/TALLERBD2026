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
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static Respuesta<List<EProd>> Obtener()
        {
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
