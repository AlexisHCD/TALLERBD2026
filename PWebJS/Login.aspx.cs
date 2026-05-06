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
        /*
            Code-behind de Login.aspx.

            Esta clase expone WebMethods para que el JavaScript (JS/Login.js) pueda:
            - Ingresar (validar usuario/contraseña)
            - Registrar (crear un usuario nuevo)

            Importante:
            - Se habilita Session en los WebMethods para poder guardar el nombre del usuario conectado.
            - La verificación e inserción real se delega a la capa Negocio (NUsua).
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            // No se hace nada en el load porque el flujo se controla desde JavaScript.
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Respuesta<bool> Ingresar(string Nombre, string Pass)
        {
            /*
                Valida las credenciales.
                Si el usuario es correcto, se guarda el Nombre en Session para que el resto del sistema
                lo use como indicador de "sesión iniciada".
            */
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
            /*
                Registra un nuevo usuario.
                Se crea un EUsua con Nombre y Pass y se delega la inserción a NUsua.

                Nota: EnableSession está activo, aunque este método no guarda Session directamente.
                (Se deja así por consistencia con Ingresar y por si el front-end lo necesita.)
            */
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