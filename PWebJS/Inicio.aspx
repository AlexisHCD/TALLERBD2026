<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PWebJS.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="alert alert-success" id="loginEstado" runat="server" visible="false"></div>
            <div class="alert alert-info" id="loginInvitacion" runat="server" visible="false">
                Para continuar debes iniciar sesión.
            </div>
            <a id="btnIniciarSesion" runat="server" class="btn btn-sm btn-primary" href="Login.aspx" visible="false">Iniciar sesión</a>
        </div>
    </div>
</asp:Content>

