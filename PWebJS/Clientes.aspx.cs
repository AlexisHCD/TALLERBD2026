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
        /*
            Code-behind de Clientes.aspx.

            Esta clase expone varios métodos estáticos marcados con [WebMethod] para que el JavaScript
            (JS/Clientes.js) pueda llamar al servidor vía AJAX y recibir/mandar datos en formato JSON.

            La lógica de negocio real no está aquí: se delega a la capa Negocio (por ejemplo NCliente,
            NLocReg, NLocPro, NLocCom). Acá solo se arma la respuesta y se manejan errores.
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // En esta página no se carga nada por postback.
            // La grilla y el modal se manejan principalmente desde JavaScript.
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static Respuesta<List<ECliente>> Obtener()
        {
            /*
                Devuelve la lista de clientes.
                - Se usa UseHttpGet=true porque normalmente el listado se solicita con GET desde el navegador.
                - Respuesta<T> es un envoltorio típico para retornar: estado (ok/error), objeto (datos) y valor (mensaje).
            */
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
            /*
                Obtiene todas las regiones para llenar el combo de Región.
                Se entrega como JSON para que el front-end construya las opciones del <select>.
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
                respuesta.valor = ex.Message;
            }

            return respuesta;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<ELocPro> ObtenerProvincias(int IdReg)
        {
            /*
                Obtiene las provincias asociadas a una región.
                Se filtra manualmente la lista completa de provincias según el IdReg recibido.
                (Es una forma simple de resolverlo sin consultas adicionales.)
            */
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
            /*
                Obtiene las comunas asociadas a una provincia.
                Similar al método de provincias: se filtra la lista completa usando el IdPro.
            */
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
            /*
                Inserta un nuevo cliente.
                El objeto "obj" llega desde el front-end (normalmente desde el formulario del modal).
                Si hay error, se transforma el mensaje en algo más amigable para mostrar al usuario.
            */
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
            /*
                Actualiza un cliente existente.
                Se espera que el obj traiga el IdP_Cli (hidden input) más el resto de campos.
            */
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
            /*
                Normaliza algunos mensajes de error comunes a textos más claros.
                La idea es que si la capa de datos lanza un error por clave duplicada, acá se pueda
                detectar si el problema es con "rut" o con "email".
            */
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
            /*
                Elimina un cliente según su Id.
                En general esto se llama desde un botón "Eliminar" en la columna Acciones de la tabla.
            */
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
