<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PWebJS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <form id="loginForm">
            <div class="form-icon">
                <span><i class="bi bi-person-fill"></i></span>
            </div>
            <div class="form-group">
                <input type="text" class="form-control item" id="username" placeholder="Usuario" />
            </div>
            <div class="form-group">
                <input type="password" class="form-control item" id="password" placeholder="Contraseña" />
            </div>
            <div class="form-group">
                <button id="btnIniciarSesion" type="submit" class="btn btn-block create-account">Iniciar Sesión</button>
            </div>
            <div class="form-group">
                <button id="btnCrearCuenta" type="button" class="btn btn-block btn-secondary">Crear usuario</button>
            </div>
        </form>
        
    </div>

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
    <script src="JS/Login.js"></script>
    <script src="JS/Utilidades.js"></script>
    <script src="Assets/Plugins/loadingoverlay/loadingoverlay.js"></script>

    <link href="Assets/Plugins/sweetalert2/sweetalert.css" rel="stylesheet" />
    <script src="Assets/Plugins/sweetalert2/sweetalert.js"></script>
</body>
</html>
