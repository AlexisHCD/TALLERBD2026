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
        /*
            Code-behind de Inicio.aspx.

            Objetivo:
            - Revisar la sesión para saber si hay un usuario "conectado".
            - Mostrar u ocultar controles de la vista (alerts y botón) según ese estado.

            Nota:
            - Se usa Session["Nombre"] como indicador simple de que el usuario inició sesión.
              Si viene vacío o null, se asume que no hay sesión.
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                    IsPostBack = false significa que es la primera vez que se carga la página.
                    En WebForms esto se usa para no repetir lógica en cada postback.
                */
                var nombre = Session["Nombre"] as string;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    /*
                        Si no hay nombre en sesión:
                        - Mostramos la invitación a iniciar sesión.
                        - Habilitamos el botón para ir a Login.
                        - Ocultamos el estado de usuario conectado.
                    */
                    loginInvitacion.Visible = true;
                    btnIniciarSesion.Visible = true;
                    loginEstado.Visible = false;
                    return;
                }

                /*
                    Si sí existe nombre en sesión:
                    - Mostramos el mensaje "Usuario conectado".
                    - Ocultamos la invitación y el botón de iniciar sesión.
                */
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
            /*
                WebMethod llamado desde JavaScript para cerrar sesión.
                EnableSession = true permite que este método acceda a la sesión.

                Pasos típicos para "cerrar sesión":
                - Clear(): elimina los valores guardados en la sesión.
                - Abandon(): finaliza la sesión actual y fuerza una nueva en la próxima petición.
            */
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