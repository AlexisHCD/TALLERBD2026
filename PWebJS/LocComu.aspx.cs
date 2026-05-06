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
        /*
            Code-behind de LocComu.aspx.

            Esta página se apoya en JavaScript (LocComu.js) para hacer el CRUD.
            Desde el front-end se llaman estos métodos [WebMethod] para:
            - Listar comunas
            - Traer regiones y provincias (para combos dependientes)
            - Insertar / actualizar / eliminar

            La lógica de acceso a datos no se implementa aquí, se delega a la capa Negocio
            (por ejemplo NLocCom, NLocReg, NLocPro).
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // La carga de datos se realiza desde JavaScript, por eso Page_Load queda vacío.
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<List<ELocCom>> Obtener()
        {
            /*
                Retorna el listado de comunas.
                Se envuelve en Respuesta<T> para que el front-end pueda saber si fue exitoso
                y además recibir un mensaje en caso de error.
            */
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
            /*
                Trae todas las regiones para llenar el combo de Región en el modal.
                Esto permite que el usuario seleccione una región antes de elegir provincia.
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
        public static Respuesta<List<ELocPro>> ObtenerProvincias(int IdReg)
        {
            /*
                Trae las provincias de una región específica (IdReg).
                Se filtra la lista completa por IdReg y se retorna solo lo necesario.
            */
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
            /*
                Inserta una comuna.
                El objeto llega desde el formulario del modal (por AJAX).
                Si hay un error típico (por ejemplo nombre repetido), se transforma a un mensaje más amigable.
            */
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
            /*
                Actualiza una comuna existente.
                Se espera que obj incluya el IdCom y el resto de campos.
            */
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
            /*
                Elimina una comuna por su Id.
                Puede fallar si la comuna está referenciada por otras tablas (por ejemplo direcciones o clientes).
            */
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
            /*
                Convierte mensajes técnicos (DB/SQL) a mensajes entendibles para el usuario.
                La idea es detectar casos comunes como:
                - nombre duplicado
                - restricción por clave foránea al eliminar
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