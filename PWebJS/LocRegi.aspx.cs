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
    public partial class LocRegi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocReg>> Obtener()
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
        public static Respuesta<bool> Ingresar(ELocReg obj)
        {
            try
            {
                return NLocReg.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "región") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(ELocReg obj)
        {
            try
            {
                return NLocReg.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "región") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdReg)
        {
            try
            {
                return NLocReg.Eliminar(IdReg);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "región") };
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