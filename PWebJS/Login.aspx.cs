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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(string Nombre, string Pass)
        {
            try
            {
                var respuesta = NUsua.Verificar(new EUsua { Nombre = Nombre, Pass = Pass });
                if (respuesta.estado)
                {
                    HttpContext.Current.Session["Nombre"] = Nombre;
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Registrar(string Nombre, string Pass)
        {
            try
            {
                return NUsua.Insertar(new EUsua { Nombre = Nombre, Pass = Pass });
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }
    }
}