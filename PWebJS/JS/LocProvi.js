var table
$(document).ready(function () {
    cargarRegiones();
    cargarDatos();

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
                        if (data.d.estado) {
                            Swal.fire('Éxito', 'Provincia eliminada correctamente', 'success');
                            cargarDatos();
                        } else {
                            Swal.fire('Error', data.d.valor || 'No se pudo eliminar', 'error');
                        }
                    },
                    error: function () {
                        $.LoadingOverlay('hide');
                        Swal.fire('Error', 'Error en la eliminación', 'error');
                    }
                });
            }
        });
    });

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
        var text = $(this).val();
        var titleCasedText = text.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCasedText);
    });

    function validarFormulario() {
        var nombre = $('#TextNombre').val().trim();
        var idReg = $('#ComboReg').val();

        if (idReg === '' || idReg === '0') {
            Swal.fire('Validación', 'Debe seleccionar una Región', 'warning');
            return false;
        }
        if (nombre === '') {
            Swal.fire('Validación', 'El nombre es requerido', 'warning');
            return false;
        }
        return true;
    }

    function cargarRegiones() {
        $.ajax({
            type: 'GET',
            url: 'LocProvi.aspx/ObtenerRegiones',
            dataType: 'json',
            success: function (data) {
                if (data.d.estado) {
                    var options = '<option value="">-- Seleccione Región --</option>';
                    $.each(data.d.objeto, function (index, item) {
                        options += '<option value="' + item.IdReg + '">' + item.Nombre + '</option>';
                    });
                    $('#ComboReg').html(options);
                }
            }
        });
    }

    function cargarDatos() {
        $.LoadingOverlay('show');
        $.ajax({
            type: 'GET',
            url: 'LocProvi.aspx/Obtener',
            dataType: 'json',
            success: function (data) {
                $.LoadingOverlay('hide');
                if (data.d.estado) {
                    if ($.fn.DataTable.isDataTable('#Grid')) {
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

                    $('#Grid').DataTable({
                        paging: true,
                        searching: true,
                        ordering: true,
                        lengthMenu: [10, 25, 50],
                        language: {
                            url: '//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json'
                        }
                    });
                } else {
                    Swal.fire('Error', data.d.valor || 'No se pudo cargar', 'error');
                }
            },
            error: function () {
                $.LoadingOverlay('hide');
                Swal.fire('Error', 'Error en la carga de datos', 'error');
            }
        });
    }
});
