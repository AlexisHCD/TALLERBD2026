using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PWebJS
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ruta = Request.AppRelativeCurrentExecutionFilePath ?? string.Empty;
            if (ruta.EndsWith("Login.aspx", StringComparison.OrdinalIgnoreCase))
            {
                if (mainNavbar != null)
                {
                    mainNavbar.Visible = false;
                }
                return;
            }

            if (Session["Nombre"] == null)
            {
                Response.Redirect("Login.aspx", true);
                return;
            }
        }
    }
}