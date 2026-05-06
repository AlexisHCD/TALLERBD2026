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
using System.Data;

namespace PWebJS
{
    public partial class LocComu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocCom>> Obtener()
        {
            var respuesta = new Respuesta<List<ELocCom>>() { estado = false, objeto = new List<ELocCom>() };
            try
            {
                var lista = new NLocCom().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<ELocCom>();
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.ToString();
            }

            return respuesta;
        }

        [WebMethod]
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
        public static Respuesta<List<ELocPro>> ObtenerProvincias(int IdReg)
        {
            var respuesta = new Respuesta<List<ELocPro>>() { estado = false, objeto = new List<ELocPro>() };
            try
            {
                var lista = new List<ELocPro>();
                var provincias = new NLocPro().Listar();

                if (provincias != null)
                {
                    foreach (var item in provincias)
                    {
                        if (item.IdReg == IdReg)
                        {
                            lista.Add(new ELocPro()
                            {
                                IdPro = item.IdPro,
                                Nombre = item.Nombre,
                                IdReg = item.IdReg
                            });
                        }
                    }
                }

                respuesta.estado = true;
                respuesta.objeto = lista;
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.ToString();
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(ELocCom obj)
        {
            try
            {
                return NLocCom.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "comuna") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(ELocCom obj)
        {
            try
            {
                return NLocCom.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "comuna") };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdCom)
        {
            try
            {
                return NLocCom.Eliminar(IdCom);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeLocalidad(ex.Message, "comuna") };
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