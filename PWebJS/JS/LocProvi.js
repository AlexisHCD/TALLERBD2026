var table
$(document).ready(function () {
    console.log('LocProvi.js loaded');
    try {
        cargarRegiones();
        cargarDatos();
    } catch (e) {
        console.error('LocProvi.cargarDatos exception:', e);
        $('#debugProvi').text(e.message || e.toString()).show();
    }

    $('#btnNuevo').on('click', function () {
        $('#textId').val('0');
        $('#ComboReg').val('');
        $('#TextNombre').val('');
        $('#modalTitle').text('Nueva Provincia');
        $('#modalGrid').modal('show');
    });

    $('#ComboReg').on('change', function () {
        // Actualizar validación al cambiar región
    });

    $('#Grid').on('click', '.btnEditar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idPro = cells.eq(0).text().trim();
        var nombre = cells.eq(1).text().trim();
        var region = cells.eq(2).text().trim();

        $('#textId').val(idPro);
        $('#TextNombre').val(nombre);
        $('#ComboReg').val($('#ComboReg option:contains("' + region + '")').attr('value'));
        $('#modalTitle').text('Editar Provincia');
        $('#modalGrid').modal('show');
    });

    $('#Grid').on('click', '.btnEliminar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idPro = cells.eq(0).text().trim();

        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción no se puede deshacer',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then(function (result) {
            if (result.isConfirmed) {
                $.LoadingOverlay('show');
                $.ajax({
                    type: 'POST',
                    url: 'LocProvi.aspx/Eliminar',
                    data: JSON.stringify({ IdPro: parseInt(idPro) }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        $.LoadingOverlay('hide');
                        if (data && data.d && data.d.estado) {
                            Swal.fire('Éxito', 'Provincia eliminada correctamente', 'success');
                            cargarDatos();
                        } else if (data && data.d) {
                            Swal.fire('Error', obtenerMensajeEliminacion(data.d.valor, 'provincia'), 'error');
                        } else {
                            Swal.fire('Error', obtenerMensajeEliminacion(null, 'provincia'), 'error');
                        }
                    },
                    error: function (xhr) {
                        $.LoadingOverlay('hide');
                        var mensaje = obtenerMensajeEliminacion(obtenerDetalleError(xhr), 'provincia');
                        Swal.fire('Error', mensaje, 'error');
                    }
                });
            }
        });
    });

    function obtenerDetalleError(xhr) {
        if (!xhr) {
            return null;
        }

        if (xhr.responseJSON) {
            return xhr.responseJSON.Message || xhr.responseJSON.message || xhr.responseJSON.error || null;
        }

        if (xhr.responseText) {
            try {
                var data = JSON.parse(xhr.responseText);
                return data.Message || data.message || data.error || xhr.responseText;
            } catch (e) {
                return xhr.responseText;
            }
        }

        return xhr.statusText || null;
    }

    function obtenerMensajeEliminacion(valor, entidad) {
        if (!valor) {
            return 'No se pudo eliminar la ' + entidad + '.';
        }

        var texto = valor.toString().toLowerCase();
        if (texto.indexOf('conflicted') !== -1 || texto.indexOf('reference') !== -1 || texto.indexOf('foreign key') !== -1) {
            return 'No se puede eliminar la ' + entidad + ' porque tiene registros asociados. Elimine primero los registros dependientes.';
        }

        return valor;
    }

    $('#btnGuardarCambios').on('click', function () {
        if (!validarFormulario()) {
            return;
        }

        var idPro = parseInt($('#textId').val());
        var nombre = $('#TextNombre').val().trim();
        var idReg = parseInt($('#ComboReg').val());

        $.LoadingOverlay('show');

        var operacion = idPro === 0 ? 'Ingresar' : 'Actualizar';
        var datos = {
            obj: {
                IdPro: idPro,
                Nombre: nombre,
                IdReg: idReg
            }
        };

        $.ajax({
            type: 'POST',
            url: 'LocProvi.aspx/' + operacion,
            data: JSON.stringify(datos),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $.LoadingOverlay('hide');
                if (data.d.estado) {
                    Swal.fire('Éxito', operacion === 'Ingresar' ? 'Provincia ingresada correctamente' : 'Provincia actualizada correctamente', 'success');
                    $('#modalGrid').modal('hide');
                    cargarDatos();
                } else {
                    Swal.fire('Error', data.d.valor || 'No se pudo guardar', 'error');
                }
            },
            error: function () {
                $.LoadingOverlay('hide');
                Swal.fire('Error', 'Error al guardar', 'error');
            }
        });
    });

    $('#TextNombre').on('input', function () {
        var text = $(this).val().replace(/[0-9]/g, '');
        var titleCasedText = text.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCasedText);
    });

    $('#TextNombre').on('keypress', function (event) {
        if (event.key >= '0' && event.key <= '9') {
            event.preventDefault();
            Swal.fire('Validación', 'El nombre de la provincia solo admite letras', 'warning');
        }
    });

    function validarFormulario() {
        var nombre = $('#TextNombre').val().trim();
        var idReg = $('#ComboReg').val();

        if (idReg === '' || idReg === '0') {
            Swal.fire('Validación', 'Debe seleccionar una región', 'warning');
            return false;
        }
        if (nombre === '') {
            Swal.fire('Validación', 'El nombre de la provincia es requerido', 'warning');
            return false;
        }
        if (/\d/.test(nombre)) {
            Swal.fire('Validación', 'El nombre de la provincia no debe contener números', 'warning');
            return false;
        }
        return true;
    }

    function cargarRegiones() {
        console.log('LocProvi.cargarRegiones start', { hasAjax: !!$.ajax, hasOverlay: !!$.LoadingOverlay });
        $.ajax({
            type: 'POST',
            url: './LocProvi.aspx/ObtenerRegiones',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                console.log('LocProvi.ObtenerRegiones response:', data);
                if (data && data.d && data.d.estado) {
                    var options = '<option value="">-- Seleccione Región --</option>';
                    $.each(data.d.objeto, function (index, item) {
                        options += '<option value="' + item.IdReg + '">' + item.Nombre + '</option>';
                    });
                    $('#ComboReg').html(options);
                    $('#debugProvi').hide().text('');
                }
            }
        });
    }

    function cargarDatos() {
        console.log('LocProvi.cargarDatos start', { hasAjax: !!$.ajax, hasOverlay: !!$.LoadingOverlay });
        if ($.LoadingOverlay) {
            $.LoadingOverlay('show');
        }
        $.ajax({
            type: 'POST',
            url: './LocProvi.aspx/Obtener',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if ($.LoadingOverlay) {
                    $.LoadingOverlay('hide');
                }
                console.log('LocProvi.Obtener response:', data);
                if (data && data.d && data.d.estado) {
                    if ($.fn.DataTable && $.fn.DataTable.isDataTable('#Grid')) {
                        $('#Grid').DataTable().destroy();
                    }

                    var html = '';
                    $.each(data.d.objeto, function (index, item) {
                        html += '<tr>';
                        html += '<td>' + item.IdPro + '</td>';
                        html += '<td>' + item.Nombre + '</td>';
                        html += '<td>' + (item.Reg ? item.Reg.Nombre : '') + '</td>';
                        html += '<td>';
                        html += '<button class="btn btn-xs btn-info btnEditar" style="padding: 2px 6px; font-size: 11px;"><i class="fas fa-edit"></i></button> ';
                        html += '<button class="btn btn-xs btn-danger btnEliminar" style="padding: 2px 6px; font-size: 11px;"><i class="fas fa-trash"></i></button>';
                        html += '</td>';
                        html += '</tr>';
                    });

                    $('#Grid tbody').html(html);
                    $('#totalRegistros').text(data.d.objeto.length);

                    if ($.fn.DataTable) {
                        $('#Grid').DataTable({
                            paging: true,
                            searching: true,
                            ordering: true,
                            lengthMenu: [10, 25, 50],
                            language: {
                                url: '//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json'
                            }
                        });
                    }
                    $('#debugProvi').hide().text('');
                    $('#debugProvi').hide().text('');
                } else {
                    console.error('LocProvi.Obtener response inválida:', data);
                    var mensaje = (data && data.d && data.d.valor) || JSON.stringify(data) || 'No se pudo cargar';
                    $('#debugProvi').text(mensaje).show();
                    Swal.fire('Error', mensaje, 'error');
                }
            },
            error: function (xhr, status, err) {
                if ($.LoadingOverlay) {
                    $.LoadingOverlay('hide');
                }
                console.error('LocProvi.Obtener error:', status, err, xhr.responseText);
                var mensaje = xhr.responseText || 'Error en la carga de datos';
                $('#debugProvi').text(mensaje).show();
                Swal.fire('Error', mensaje, 'error');
            },
            complete: function (xhr) {
                console.log('LocProvi.Obtener status:', xhr.status);
                if (xhr.status !== 200 && xhr.responseText) {
                    $('#debugProvi').text(xhr.responseText).show();
                }
            }
        });
    }
});

$(document).ajaxError(function (event, xhr, settings, err) {
    if (settings && settings.url && settings.url.indexOf('LocProvi.aspx') !== -1) {
        console.error('LocProvi ajaxError:', settings.url, xhr.responseText);
    }
});

$(document).ajaxSend(function (event, xhr, settings) {
    if (settings && settings.url && settings.url.indexOf('LocProvi.aspx') !== -1) {
        console.log('LocProvi ajaxSend:', settings.url, settings.type);
    }
});

window.addEventListener('error', function (event) {
    console.error('LocProvi window error:', event.message);
});
