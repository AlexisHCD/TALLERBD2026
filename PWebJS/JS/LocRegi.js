var table
$(document).ready(function () {
    cargarDatos();

    $('#btnNuevo').on('click', function () {
        $('#textId').val('0');
        $('#TextNombre').val('');
        $('#modalTitle').text('Nueva Región');
        $('#modalGrid').modal('show');
    });

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
                        if (data.d.estado) {
                            Swal.fire('Éxito', 'Región eliminada correctamente', 'success');
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

    $('#TextNombre').on('input', function () {
        var text = $(this).val();
        var titleCasedText = text.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCasedText);
    });

    function validarFormulario() {
        var nombre = $('#TextNombre').val().trim();
        if (nombre === '') {
            Swal.fire('Validación', 'El nombre es requerido', 'warning');
            return false;
        }
        return true;
    }

    function cargarDatos() {
        $.LoadingOverlay('show');
        $.ajax({
            type: 'GET',
            url: 'LocRegi.aspx/Obtener',
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
