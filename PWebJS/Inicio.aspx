<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PWebJS.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--
        Página de inicio.
        Esta vista muestra un mensaje diferente dependiendo de si el usuario tiene sesión iniciada.
        La lógica que decide qué se ve (invitación o estado) está en Inicio.aspx.cs.
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--
        Layout simple con Bootstrap:
        - Un alert verde para mostrar el usuario conectado.
        - Un alert azul para invitar a iniciar sesión.
        - Un botón/enlace para ir a Login.aspx.

        Todos los controles tienen runat="server" para poder cambiar su visibilidad desde el code-behind.
    --%>
    <div class="row">
        <div class="col-sm-12">
            <%--
                loginEstado: mensaje de estado cuando el usuario ya está autenticado.
                visible="false" es el valor inicial; el servidor lo cambia según la sesión.
            --%>
            <div class="alert alert-success" id="loginEstado" runat="server" visible="false"></div>

            <%--
                loginInvitacion: mensaje cuando no hay sesión.
                Se muestra junto con el botón para redirigir al Login.
            --%>
            <div class="alert alert-info" id="loginInvitacion" runat="server" visible="false">
                Para continuar debes iniciar sesión.
            </div>

            <%--
                Botón/enlace para ir a la página de Login.
                Se muestra solo si no se detecta un nombre de usuario en Session.
            --%>
            <a id="btnIniciarSesion" runat="server" class="btn btn-sm btn-primary" href="Login.aspx" visible="false">Iniciar sesión</a>
        </div>
    </div>
</asp:Content>

