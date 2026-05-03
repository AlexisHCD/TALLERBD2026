$(document).ready(function () {
    $('#btnIniciarSesion').on('click', function () {
        var nombre = ($('#username').val() || '').trim();
        var pass = ($('#password').val() || '').trim();

        if (!nombre || !pass) {
            swal('Mensaje', 'Debe ingresar usuario y contraseña', 'warning');
            return;
        }

        var data = JSON.stringify({ Nombre: nombre, Pass: pass });

        AjaxPost('../Login.aspx/Ingresar', data,
            function (response) {
                if (response.estado) {
                    window.location.href = 'Inicio.aspx';
                } else {
                    swal('Mensaje', response.valor || 'No se pudo iniciar sesión', 'warning');
                }
            },
            function () {
                swal('Mensaje', 'No se pudo iniciar sesión', 'warning');
            },
            function () {
            });
    });

    $('#btnCrearCuenta').on('click', function () {
        $('#TextNuevoUsuario').val('');
        $('#TextNuevaClave').val('');
        $('#modalUsuario').modal('show');
    });

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
