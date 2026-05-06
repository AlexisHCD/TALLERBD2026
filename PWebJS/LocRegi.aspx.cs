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
        /*
            Code-behind de LocRegi.aspx.

            Esta clase expone WebMethods para que el JavaScript (JS/LocRegi.js) pueda:
            - Obtener el listado de regiones
            - Insertar, actualizar y eliminar regiones

            La capa Web solo coordina la respuesta y captura errores.
            Las operaciones reales se delegan a la capa Negocio (NLocReg).
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // La página se apoya en JavaScript para cargar y manipular datos, por eso aquí no hay lógica.
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocReg>> Obtener()
        {
            /*
                Lista todas las regiones.
                EnableSession = false porque este método no depende de Session.
                Se retorna JSON para ser consumido por AJAX.
            */
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
            /*
                Inserta una región.
                Si ocurre un error típico (por ejemplo nombre duplicado), se devuelve un mensaje más amigable.
            */
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
            /*
                Actualiza una región existente.
                El objeto debe venir con el IdReg y el Nombre.
            */
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
            /*
                Elimina una región por su Id.
                Puede fallar si existen provincias/comunas asociadas (restricción de clave foránea).
            */
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
            /*
                Traduce errores técnicos a mensajes más entendibles para el usuario.
                Se detectan casos comunes como nombre repetido o conflictos por relaciones.
            */
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