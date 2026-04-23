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
            var dt = new NLocPro().Filtrar(IdReg);

            foreach (DataRow row in dt.Rows)
            {
                resultado.Add(new ELocPro
                {
                    IdPro = GetInt(row, "IdPro"),
                    Nombre = GetString(row, "Nombre")
                });
            }

            return resultado;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<ELocCom> ObtenerComunas(int IdPro)
        {
            var resultado = new List<ELocCom>();
            var dt = new NLocCom().Filtrar(IdPro);

            foreach (DataRow row in dt.Rows)
            {
                resultado.Add(new ELocCom
                {
                    IdCom = GetInt(row, "IdCom"),
                    Nombre = GetString(row, "Nombre")
                });
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
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
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
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
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

        private static int GetInt(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
            {
                return 0;
            }

            int value;
            return int.TryParse(row[columnName].ToString(), out value) ? value : 0;
        }

        private static string GetString(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
            {
                return string.Empty;
            }

            return row[columnName].ToString();
        }
    }
}
