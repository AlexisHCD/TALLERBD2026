var table;
$(document).ready(function () {
    cargarDatos();
    obtenerRegiones();

    $('#ComboReg').change(function () {
        var idReg = parseInt($(this).val() || '0');
        limpiarCombo('#ComboPro', 'Seleccione Provincia');
        limpiarCombo('#ComboCom', 'Seleccione Comuna');
        if (idReg > 0) {
            obtenerProvincias(idReg);
        }
    });

    $('#ComboPro').change(function () {
        var idPro = parseInt($(this).val() || '0');
        limpiarCombo('#ComboCom', 'Seleccione Comuna');
        if (idPro > 0) {
            obtenerComunas(idPro);
        }
    });

    $('#TextNombre, #TextGiro').on('input', function () {
        var texto = $(this).val();
        var titleCase = texto.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCase);
    });

    $('#TextNombre, #TextGiro').on('keypress', function (event) {
        if (event.key >= '0' && event.key <= '9') {
            event.preventDefault();
            swal('Mensaje', 'Solo se permiten letras', 'warning');
        }
    });

    $('#TextTelefono').on('input', function () {
        var limpio = $(this).val().replace(/[^0-9]/g, '');
        $(this).val(limpio);
    });

    $('#TextRut').on('blur', function () {
        var rut = ($(this).val() || '').trim();
        if (!rut) {
            return;
        }

        if (!validarRut(rut)) {
            $(this).val('');
            swal('Mensaje', 'Rut Malo', 'warning');
            return;
        }

        $(this).val(formatoRut(rut));
    });
});

function cargarDatos() {

    if ($.fn.DataTable.isDataTable('#Grid')) {
        $('#Grid').DataTable().destroy();
    }
    $('#Grid tbody').html('');

    AjaxGet('../Clientes.aspx/Obtener',
        function (response) {
            $('.card-body').LoadingOverlay('hide');
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $('<tr>').append(
                        $('<td>').text(i + 1),
                        $('<td>').text(row.Nombre),
                        $('<td>').text(row.Rut),
                        $('<td>').text(row.Com ? row.Com.Nombre : ''),
                        $('<td>').text(row.Direccion),
                        $('<td>').text(row.Tel),
                        $('<td>').text(row.Email),
                        $('<td>').text(row.Giro),
                        $('<td>').append(
                            $('<button>').addClass('btn btn-sm btn-primary mr-1').text('Editar').data('ECliente', row),
                            $('<button>').addClass('btn btn-sm btn-danger').text('Eliminar').data('ECliente', row.IdP_Cli)
                        )
                    ).appendTo('#Grid tbody');
                });
            }
            table = $('#Grid').DataTable({
                responsive: true
            });
        },
        function () {
            $('.card-body').LoadingOverlay('hide');
        },
        function () {
            $('.card-body').LoadingOverlay('show');
        });
}

function limpiarCombo(selector, texto) {
    $(selector).html('');
    $('<option>').attr({ value: '0' }).text(texto).appendTo(selector);
}

function obtenerRegiones() {
    limpiarCombo('#ComboReg', 'Seleccione Región');

    AjaxGet('../Clientes.aspx/ObtenerRegiones',
        function (response) {
            $('.modal-body').LoadingOverlay('hide');
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $('<option>').attr({ value: row.IdReg }).text(row.Nombre).appendTo('#ComboReg');
                });
            }
        },
        function () {
            $('.modal-body').LoadingOverlay('hide');
        },
        function () {
            $('.modal-body').LoadingOverlay('show');
        });
}

function obtenerProvincias(idReg, seleccionado) {
    $.ajax({
        type: 'POST',
        url: 'Clientes.aspx/ObtenerProvincias',
        data: JSON.stringify({ IdReg: idReg }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            limpiarCombo('#ComboPro', 'Seleccione Provincia');
            $.each(response.d, function (i, row) {
                $('<option>').val(row.IdPro).text(row.Nombre).appendTo('#ComboPro');
            });
            if (seleccionado) {
                $('#ComboPro').val(String(seleccionado));
            }
        }
    });
}

function obtenerComunas(idPro, seleccionado) {
    $.ajax({
        type: 'POST',
        url: 'Clientes.aspx/ObtenerComunas',
        data: JSON.stringify({ IdPro: idPro }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            limpiarCombo('#ComboCom', 'Seleccione Comuna');
            $.each(response.d, function (i, row) {
                $('<option>').val(row.IdCom).text(row.Nombre).appendTo('#ComboCom');
            });
            if (seleccionado) {
                $('#ComboCom').val(String(seleccionado));
            }
        }
    });
}

$('#btnNuevo').on('click', function () {
    $('#textId').val(0);
    $('#TextNombre').val('');
    $('#TextRut').val('');
    $('#TextDireccion').val('');
    $('#TextTelefono').val('');
    $('#TextEmail').val('');
    $('#TextGiro').val('');
    $('#ComboReg').val('0');
    limpiarCombo('#ComboPro', 'Seleccione Provincia');
    limpiarCombo('#ComboCom', 'Seleccione Comuna');
    $('#modalGrid').modal('show');
});

$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-primary mr-1"]', function () {
    var model = $(this).data('ECliente');

    $('#textId').val(model.IdP_Cli);
    $('#TextNombre').val(model.Nombre || '');
    $('#TextRut').val(model.Rut || '');
    $('#TextDireccion').val(model.Direccion || '');
    $('#TextTelefono').val(model.Tel || '');
    $('#TextEmail').val(model.Email || '');
    $('#TextGiro').val(model.Giro || '');

    $('#ComboReg').val(String(model.IdReg || 0));
    if (model.IdReg > 0) {
        obtenerProvincias(model.IdReg, model.IdPro);
    } else {
        limpiarCombo('#ComboPro', 'Seleccione Provincia');
    }

    if (model.IdPro > 0) {
        obtenerComunas(model.IdPro, model.IdCom);
    } else {
        limpiarCombo('#ComboCom', 'Seleccione Comuna');
    }

    $('#modalGrid').modal('show');
});

$('#btnGuardarCambios').on('click', function () {
    var request = {
        obj: {
            IdP_Cli: parseInt($('#textId').val() || '0'),
            Nombre: ($('#TextNombre').val() || '').trim(),
            Rut: ($('#TextRut').val() || '').trim(),
            IdReg: parseInt($('#ComboReg').val() || '0'),
            IdPro: parseInt($('#ComboPro').val() || '0'),
            IdCom: parseInt($('#ComboCom').val() || '0'),
            Direccion: ($('#TextDireccion').val() || '').trim(),
            Tel: ($('#TextTelefono').val() || '').trim(),
            Email: ($('#TextEmail').val() || '').trim(),
            Giro: ($('#TextGiro').val() || '').trim()
        }
    };

    if (!request.obj.Nombre || !request.obj.Rut || request.obj.IdReg === 0 || request.obj.IdPro === 0 || request.obj.IdCom === 0 || !request.obj.Direccion || !request.obj.Tel || !request.obj.Email || !request.obj.Giro) {
        swal('Mensaje', 'Es necesario completar todos los campos', 'warning');
        return;
    }

    if (!validarRut(request.obj.Rut)) {
        swal('Mensaje', 'Rut Malo', 'warning');
        return;
    }

    if (!/^\S+@\S+\.\S+$/.test(request.obj.Email)) {
        swal('Mensaje', 'Email inválido', 'warning');
        return;
    }

    var url = request.obj.IdP_Cli === 0 ? '../Clientes.aspx/Ingresar' : '../Clientes.aspx/Actualizar';
    var mensajeOk = request.obj.IdP_Cli === 0 ? 'Ingreso fue realizado correctamente' : 'Actualización fue realizada correctamente';

    AjaxPost(url, JSON.stringify(request),
        function (response) {
            $('.modal-body').LoadingOverlay('hide');
            if (response.estado) {
                cargarDatos();
                $('#modalGrid').modal('hide');
                swal(mensajeOk);
            } else {
                swal('Oops!', response.valor || 'No fue posible guardar', 'warning');
            }
        },
        function () {
            $('.modal-body').LoadingOverlay('hide');
        },
        function () {
            $('.modal-body').LoadingOverlay('show');
        });
});

$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-danger"]', function () {
    var request = { IdP_Cli: String($(this).data('ECliente')) };

    swal({
        title: 'Mensaje',
        text: '¿Está seguro realizar la eliminación?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí',
        cancelButtonText: 'No',
        closeOnConfirm: false
    }, function () {
        AjaxPost('../Clientes.aspx/Eliminar', JSON.stringify(request),
            function (response) {
                if (response.estado) {
                    cargarDatos();
                    swal.close();
                } else {
                    swal('Mensaje', 'No se pudo eliminar el registro', 'warning');
                }
            },
            function () {
            },
            function () {
            });
    });
});

function validarRut(rut) {
    try {
        var limpio = (rut || '').toUpperCase().replace(/\./g, '').replace(/-/g, '');
        if (limpio.length < 2) {
            return false;
        }

        var cuerpo = limpio.slice(0, -1);
        var dv = limpio.slice(-1);
        if (!/^\d+$/.test(cuerpo)) {
            return false;
        }

        var rutAux = parseInt(cuerpo, 10);
        var m = 0;
        var s = 1;

        while (rutAux !== 0) {
            s = (s + (rutAux % 10) * (9 - (m++ % 6))) % 11;
            rutAux = Math.floor(rutAux / 10);
        }

        var dvCalculado = String.fromCharCode(s !== 0 ? s + 47 : 75);
        return dvCalculado === dv;
    } catch (e) {
        return false;
    }
}

function formatoRut(rut) {
    var limpio = (rut || '').replace(/\./g, '').replace(/-/g, '');
    if (limpio.length < 2) {
        return limpio;
    }

    var cont = 0;
    var format = '-' + limpio.substring(limpio.length - 1);

    for (var i = limpio.length - 2; i >= 0; i--) {
        format = limpio.substring(i, i + 1) + format;
        cont++;
        if (cont === 3 && i !== 0) {
            format = '.' + format;
            cont = 0;
        }
    }

    return format;
}
