using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using Entidad;
using Negocio;

namespace PWebJS
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static Respuesta<List<ECliente>> Obtener()
        {
            var respuesta = new Respuesta<List<ECliente>>() { estado = false, objeto = new List<ECliente>() };
            try
            {
                var lista = new NCliente().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<ECliente>();
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.Message;
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
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
                respuesta.valor = ex.Message;
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<ELocPro> ObtenerProvincias(int IdReg)
        {
            var resultado = new List<ELocPro>();
            var lista = new NLocPro().Listar();

            if (lista != null)
            {
                foreach (var item in lista)
                {
                    if (item.IdReg == IdReg)
                    {
                        resultado.Add(new ELocPro
                        {
                            IdPro = item.IdPro,
                            Nombre = item.Nombre,
                            IdReg = item.IdReg
                        });
                    }
                }
            }

            return resultado;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<ELocCom> ObtenerComunas(int IdPro)
        {
            var resultado = new List<ELocCom>();
            var lista = new NLocCom().Listar();

            if (lista != null)
            {
                foreach (var item in lista)
                {
                    if (item.IdPro == IdPro)
                    {
                        resultado.Add(new ELocCom
                        {
                            IdCom = item.IdCom,
                            Nombre = item.Nombre,
                            IdPro = item.IdPro,
                            IdReg = item.IdReg
                        });
                    }
                }
            }

            return resultado;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(ECliente obj)
        {
            try
            {
                return NCliente.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeCliente(ex.Message) };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(ECliente obj)
        {
            try
            {
                return NCliente.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ObtenerMensajeCliente(ex.Message) };
            }
        }

        private static string ObtenerMensajeCliente(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                return "No fue posible guardar el cliente.";
            }

            var texto = mensaje.ToLowerInvariant();
            if (texto.Contains("rut"))
            {
                return "El RUT ya está registrado. Ingrese uno diferente.";
            }

            if (texto.Contains("email"))
            {
                return "El email ya está registrado. Ingrese uno diferente.";
            }

            return mensaje;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdP_Cli)
        {
            try
            {
                return NCliente.Eliminar(IdP_Cli);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }

    }
}
