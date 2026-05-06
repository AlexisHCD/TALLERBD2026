using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Entidad;
using Negocio;

namespace PWebJS
{
    public partial class LocProvi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocPro>> Obtener()
        {
            var respuesta = new Respuesta<List<ELocPro>>() { estado = false, objeto = new List<ELocPro>() };
            try
            {
                var lista = new NLocPro().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<ELocPro>();
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.ToString();
            }

            return respuesta;
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocReg>> ObtenerRegiones()
        {
            var respuesta = new Respuesta<List<ELocReg>>() { estado = false, objeto = new List<ELocReg>() };
            try
            {
                var lista = new NLocReg().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<ELocReg>();
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.ToString();
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(ELocPro obj)
        {
            try
            {
                return NLocPro.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "provincia") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(ELocPro obj)
        {
            try
            {
                return NLocPro.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "provincia") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdPro)
        {
            try
            {
                return NLocPro.Eliminar(IdPro);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "provincia") };
            }
        }

        private static string ObtenerMensajeLocalidad(string mensaje, string entidad)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                return "No fue posible guardar la " + entidad + ".";
            }

            var texto = mensaje.ToLowerInvariant();
            if (texto.Contains("nombre"))
            {
                return "El nombre de la " + entidad + " ya está registrado. Ingrese uno diferente.";
            }

            if (texto.Contains("foreign") || texto.Contains("reference") || texto.Contains("conflict"))
            {
                return "No se puede eliminar la " + entidad + " porque tiene registros asociados.";
            }

            return mensaje;
        }
    }
}