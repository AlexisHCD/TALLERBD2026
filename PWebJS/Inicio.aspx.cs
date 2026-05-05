using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;

namespace PWebJS
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var nombre = Session["Nombre"] as string;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    loginInvitacion.Visible = true;
                    btnIniciarSesion.Visible = true;
                    loginEstado.Visible = false;
                    return;
                }

                loginEstado.InnerText = "Usuario conectado: " + nombre;
                loginEstado.Visible = true;
                loginInvitacion.Visible = false;
                btnIniciarSesion.Visible = false;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static Respuesta<bool> CerrarSesion()
        {
            try
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                return new Respuesta<bool>() { estado = true };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>() { estado = false, valor = ex.Message };
            }
        }
    }
}