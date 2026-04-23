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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
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
        public static Respuesta<List<ELocPro>> ObtenerProvincias(int IdReg)
        {
            var respuesta = new Respuesta<List<ELocPro>>() { estado = false, objeto = new List<ELocPro>() };
            try
            {
                DataTable dt = new NLocPro().Filtrar(IdReg);
                var lista = new List<ELocPro>();
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        lista.Add(new ELocPro()
                        {
                            IdPro = GetInt(row, "IdPro"),
                            Nombre = GetString(row, "Nombre"),
                            IdReg = GetInt(row, "IdReg")
                        });
                    }
                }
                
                respuesta.estado = true;
                respuesta.objeto = lista;
            }
            catch (Exception ex)
            {
                respuesta.valor = ex.Message;
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
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
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
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
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
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }

        private static int GetInt(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? Convert.ToInt32(row[columnName]) : 0;
        }

        private static string GetString(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? row[columnName].ToString() : "";
        }
    }
}