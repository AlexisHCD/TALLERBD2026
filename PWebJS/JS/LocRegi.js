window.__LocRegiLoaded = true;
var table
// Inicialización: carga datos y configura eventos de la vista.
$(document).ready(function () {
    console.log('LocRegi.js loaded');
    try {
        cargarDatos();
    } catch (e) {
        console.error('LocRegi.cargarDatos exception:', e);
        $('#debugRegion').text(e.message || e.toString()).show();
    }

    // Abrir modal para crear una nueva región.
    $('#btnNuevo').on('click', function () {
        $('#textId').val('0');
        $('#TextNombre').val('');
        $('#modalTitle').text('Nueva Región');
        $('#modalGrid').modal('show');
    });

    // Editar una región desde la fila seleccionada.
    $('#Grid').on('click', '.btnEditar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idReg = cells.eq(0).text().trim();
        var nombre = cells.eq(1).text().trim();

        $('#textId').val(idReg);
        $('#TextNombre').val(nombre);
        $('#modalTitle').text('Editar Región');
        $('#modalGrid').modal('show');
    });

    // Eliminar una región con confirmación.
    $('#Grid').on('click', '.btnEliminar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idReg = cells.eq(0).text().trim();

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
                    url: 'LocRegi.aspx/Eliminar',
                    data: JSON.stringify({ IdReg: parseInt(idReg) }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        $.LoadingOverlay('hide');
                        if (data && data.d && data.d.estado) {
                            Swal.fire('Éxito', 'Región eliminada correctamente', 'success');
                            cargarDatos();
                        } else if (data && data.d) {
                            Swal.fire('Error', obtenerMensajeEliminacion(data.d.valor, 'región'), 'error');
                        } else {
                            Swal.fire('Error', obtenerMensajeEliminacion(null, 'región'), 'error');
                        }
                    },
                    error: function (xhr) {
                        $.LoadingOverlay('hide');
                        var mensaje = obtenerMensajeEliminacion(obtenerDetalleError(xhr), 'región');
                        Swal.fire('Error', mensaje, 'error');
                    }
                });
            }
        });
    });

    // Extraer detalles de error desde la respuesta del servidor.
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

    // Mensajes amigables para eliminación con dependencias.
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

    // Guardar cambios (crear o actualizar).
    $('#btnGuardarCambios').on('click', function () {
        if (!validarFormulario()) {
            return;
        }

        var idReg = parseInt($('#textId').val());
        var nombre = $('#TextNombre').val().trim();

        $.LoadingOverlay('show');

        var operacion = idReg === 0 ? 'Ingresar' : 'Actualizar';
        var datos = {
            obj: {
                IdReg: idReg,
                Nombre: nombre
            }
        };

        $.ajax({
            type: 'POST',
            url: 'LocRegi.aspx/' + operacion,
            data: JSON.stringify(datos),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $.LoadingOverlay('hide');
                if (data.d.estado) {
                    Swal.fire('Éxito', operacion === 'Ingresar' ? 'Región ingresada correctamente' : 'Región actualizada correctamente', 'success');
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

    // Formato y limpieza del nombre de región.
    $('#TextNombre').on('input', function () {
        var text = $(this).val().replace(/[0-9]/g, '');
        var titleCasedText = text.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCasedText);
    });

    // Validación rápida: evitar números al escribir.
    $('#TextNombre').on('keypress', function (event) {
        if (event.key >= '0' && event.key <= '9') {
            event.preventDefault();
            Swal.fire('Validación', 'El nombre de la región solo admite letras', 'warning');
        }
    });

    // Validaciones mínimas antes de enviar.
    function validarFormulario() {
        var nombre = $('#TextNombre').val().trim();
        if (nombre === '') {
            Swal.fire('Validación', 'El nombre de la región es requerido', 'warning');
            return false;
        }
        if (/\d/.test(nombre)) {
            Swal.fire('Validación', 'El nombre de la región no debe contener números', 'warning');
            return false;
        }
        return true;
    }

    // Cargar la grilla desde el servidor y configurar DataTables.
    function cargarDatos() {
        console.log('LocRegi.cargarDatos start', { hasAjax: !!$.ajax, hasOverlay: !!$.LoadingOverlay });
        if ($.LoadingOverlay) {
            $.LoadingOverlay('show');
        }
        $.ajax({
            type: 'POST',
            url: './LocRegi.aspx/Obtener',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if ($.LoadingOverlay) {
                    $.LoadingOverlay('hide');
                }
                console.log('LocRegi.Obtener response:', data);
                if (data && data.d && data.d.estado) {
                    if ($.fn.DataTable && $.fn.DataTable.isDataTable('#Grid')) {
                        $('#Grid').DataTable().destroy();
                    }

                    var html = '';
                    $.each(data.d.objeto, function (index, item) {
                        html += '<tr>';
                        html += '<td>' + item.IdReg + '</td>';
                        html += '<td>' + item.Nombre + '</td>';
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
                    $('#debugRegion').hide().text('');
                    $('#debugRegion').hide().text('');
                } else {
                    console.error('LocRegi.Obtener response inválida:', data);
                    var mensaje = (data && data.d && data.d.valor) || JSON.stringify(data) || 'No se pudo cargar';
                    $('#debugRegion').text(mensaje).show();
                    Swal.fire('Error', mensaje, 'error');
                }
            },
            error: function (xhr, status, err) {
                if ($.LoadingOverlay) {
                    $.LoadingOverlay('hide');
                }
                console.error('LocRegi.Obtener error:', status, err, xhr.responseText);
                var mensaje = xhr.responseText || 'Error en la carga de datos';
                $('#debugRegion').text(mensaje).show();
                Swal.fire('Error', mensaje, 'error');
            },
            complete: function (xhr) {
                console.log('LocRegi.Obtener status:', xhr.status);
                if (xhr.status !== 200 && xhr.responseText) {
                    $('#debugRegion').text(xhr.responseText).show();
                }
            }
        });
    }
});

$(document).ajaxError(function (event, xhr, settings, err) {
    if (settings && settings.url && settings.url.indexOf('LocRegi.aspx') !== -1) {
        console.error('LocRegi ajaxError:', settings.url, xhr.responseText);
    }
});

$(document).ajaxSend(function (event, xhr, settings) {
    if (settings && settings.url && settings.url.indexOf('LocRegi.aspx') !== -1) {
        console.log('LocRegi ajaxSend:', settings.url, settings.type);
    }
});

window.addEventListener('error', function (event) {
    console.error('LocRegi window error:', event.message);
});
