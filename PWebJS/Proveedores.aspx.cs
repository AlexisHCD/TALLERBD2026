using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using Entidad;
using Negocio;

namespace PWebJS
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static Respuesta<List<EProv>> Obtener()
        {
            var respuesta = new Respuesta<List<EProv>>() { estado = false, objeto = new List<EProv>() };
            try
            {
                var lista = new NProv().Listar();
                respuesta.estado = true;
                respuesta.objeto = lista ?? new List<EProv>();
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
        public static Respuesta<bool> Ingresar(EProv obj)
        {
            try
            {
                return NProv.Ingresar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Actualizar(EProv obj)
        {
            try
            {
                return NProv.Actualizar(obj);
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Eliminar(int IdProv)
        {
            try
            {
                return NProv.Eliminar(IdProv);
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
