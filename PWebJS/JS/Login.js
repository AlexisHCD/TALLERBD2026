// Inicialización de la pantalla de login y acciones del usuario.
$(document).ready(function () {
    // Envía las credenciales al servidor.
    function iniciarSesion() {
        var nombre = ($('#username').val() || '').trim();
        var pass = ($('#password').val() || '').trim();

        if (!nombre || !pass) {
            swal('Mensaje', 'Debe ingresar usuario y contraseña', 'warning');
            return;
        }

        var data = JSON.stringify({ Nombre: nombre, Pass: pass });

        $.LoadingOverlay('show');
        AjaxPost('../Login.aspx/Ingresar', data,
            function (response) {
                $.LoadingOverlay('hide');
                if (response.estado) {
                    window.location.href = 'Inicio.aspx';
                } else {
                    swal('Mensaje', response.valor || 'No se pudo iniciar sesión', 'warning');
                }
            },
            function () {
                $.LoadingOverlay('hide');
                swal('Mensaje', 'No se pudo iniciar sesión', 'warning');
            },
            function () {
            });
    }

    // Permite iniciar sesión con Enter.
    $('#loginForm').on('submit', function (event) {
        event.preventDefault();
        iniciarSesion();
    });

    // Iniciar sesión con botón.
    $('#btnIniciarSesion').on('click', function () {
        iniciarSesion();
    });

    // Abre el modal para crear usuario.
    $('#btnCrearCuenta').on('click', function () {
        $('#TextNuevoUsuario').val('');
        $('#TextNuevaClave').val('');
        $('#modalUsuario').modal('show');
    });

    // Registra un nuevo usuario en el sistema.
    $('#btnRegistrarUsuario').on('click', function () {
        var nombre = ($('#TextNuevoUsuario').val() || '').trim();
        var pass = ($('#TextNuevaClave').val() || '').trim();

        if (!nombre || !pass) {
            swal('Mensaje', 'Debe ingresar usuario y contraseña', 'warning');
            return;
        }

        var data = JSON.stringify({ Nombre: nombre, Pass: pass });

        AjaxPost('../Login.aspx/Registrar', data,
            function (response) {
                if (response.estado) {
                    $('#modalUsuario').modal('hide');
                    swal('Mensaje', 'Usuario creado correctamente', 'success');
                } else {
                    swal('Mensaje', response.valor || 'No se pudo crear el usuario', 'warning');
                }
            },
            function () {
                swal('Mensaje', 'No se pudo crear el usuario', 'warning');
            },
            function () {
            });
    });
});
