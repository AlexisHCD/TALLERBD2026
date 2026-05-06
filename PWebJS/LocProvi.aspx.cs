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
        /*
            Code-behind de LocProvi.aspx.

            Esta página se maneja principalmente desde JavaScript (JS/LocProvi.js) que consume estos WebMethods.
            Funcionalidades típicas:
            - Obtener listado de provincias (para la grilla)
            - Obtener regiones (para el combo del modal)
            - CRUD: ingresar, actualizar, eliminar

            La capa Web se encarga de exponer los métodos y traducir errores.
            La lógica/DB se delega a la capa Negocio (NLocPro, NLocReg).
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se deja vacío porque la carga de datos se hace por AJAX desde el JS.
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocPro>> Obtener()
        {
            /*
                Lista todas las provincias.
                EnableSession = false: no requiere Session.
                Se retorna como JSON para que el front-end arme la grilla.
            */
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
            /*
                Trae regiones para llenar el combo del modal.
                Esto permite asignar cada provincia a una región.
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
        public static Respuesta<bool> Ingresar(ELocPro obj)
        {
            /*
                Inserta una provincia.
                El objeto llega desde el formulario del modal.
            */
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
            /*
                Actualiza una provincia.
                Se espera que obj contenga IdPro y los campos editables.
            */
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
            /*
                Elimina una provincia.
                Si tiene comunas asociadas, lo normal es que la BD no permita eliminarla.
            */
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
            /*
                Convierte mensajes técnicos en mensajes más claros.
                Revisa por:
                - "nombre" duplicado
                - errores de referencia/relación al eliminar
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