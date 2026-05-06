<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PWebJS.Login" %>

<%--
    Página de inicio de sesión.
    A diferencia de otras pantallas, esta no usa MasterPage (es un HTML completo) para que se vea como
    una pantalla independiente.

    La lógica (validar usuario, registrar usuario) se hace con JavaScript (JS/Login.js) llamando WebMethods
    del code-behind (Login.aspx.cs) y trabajando con Session.
--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--
        Referencias a estilos y scripts necesarios para el formulario:
        - Bootstrap: estilos y componentes
        - CSS propio: estilos de esta pantalla
        - Iconos: Simple Line Icons / Bootstrap Icons
        - jQuery: base para muchas funciones y plugins
    --%>
    <link href="Assets/Plugins/bootstrap.4.5.2/bootstrap.min.css" rel="stylesheet" />
    <link href="css/IniciarSesion/Styles.css" rel="stylesheet" />
    <link href="Assets/Plugins/Simple_Line_Icons/simple-line-icons.min.css" rel="stylesheet" />
    <link href="Assets/Plugins/bootstrap-icons-1.2.2/font/bootstrap-icons.css" rel="stylesheet" />
    <script src="Assets/Plugins/jquery/jquery.3.5.1.min.js"></script>
    <script src="Assets/Plugins/bootstrap.4.5.2/bootstrap.min.js"></script>

    

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="registration-form">
        <%--
            Formulario de login.
            Ojo: este <form> no es runat="server" porque el envío se intercepta con JavaScript.
            El JS toma usuario/clave y hace la llamada AJAX al WebMethod Ingresar.
        --%>
        <form id="loginForm">
            <div class="form-icon">
                <span><i class="bi bi-person-fill"></i></span>
            </div>
            <div class="form-group">
                <%-- Input del nombre de usuario. --%>
                <input type="text" class="form-control item" id="username" placeholder="Usuario" />
            </div>
            <div class="form-group">
                <%-- Input de contraseña. Se envía tal cual al WebMethod (según la lógica actual del sistema). --%>
                <input type="password" class="form-control item" id="password" placeholder="Contraseña" />
            </div>
            <div class="form-group">
                <%-- Botón submit: el JS lo usa para iniciar sesión sin recargar la página. --%>
                <button id="btnIniciarSesion" type="submit" class="btn btn-block create-account">Iniciar Sesión</button>
            </div>
            <div class="form-group">
                <%-- Abre el modal para registrar un usuario nuevo. --%>
                <button id="btnCrearCuenta" type="button" class="btn btn-block btn-secondary">Crear usuario</button>
            </div>
        </form>
        
    </div>

    <%--
        Modal para registrar un nuevo usuario.
        Se abre desde el botón "Crear usuario" y se confirma con "Registrar".
        El JS llama al WebMethod Registrar.
    --%>
    <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Crear usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="TextNuevoUsuario" class="col-form-label">Usuario:</label>
                        <input type="text" class="form-control form-control-sm" id="TextNuevoUsuario" />
                    </div>
                    <div class="form-group">
                        <label for="TextNuevaClave" class="col-form-label">Contraseña:</label>
                        <input type="password" class="form-control form-control-sm" id="TextNuevaClave" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button id="btnRegistrarUsuario" type="button" class="btn btn-sm btn-primary">Registrar</button>
                </div>
            </div>
        </div>
    </div>

    <%--
        Scripts de la pantalla:
        - Login.js: eventos del formulario, llamadas AJAX a Ingresar/Registrar, manejo del modal.
        - Utilidades.js: funciones comunes (por ejemplo AjaxPost) usadas en varias páginas.
        - LoadingOverlay / SweetAlert: feedback visual (cargando / alertas).
    --%>
    <script src="JS/Login.js"></script>
    <script src="JS/Utilidades.js"></script>
    <script src="Assets/Plugins/loadingoverlay/loadingoverlay.js"></script>

    <link href="Assets/Plugins/sweetalert2/sweetalert.css" rel="stylesheet" />
    <script src="Assets/Plugins/sweetalert2/sweetalert.js"></script>
</body>
</html>
