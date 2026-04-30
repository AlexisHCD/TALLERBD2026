var table
$(document).ready(function () {
    cargarRegiones();
    cargarDatos();

    $('#btnNuevo').on('click', function () {
        $('#textId').val('0');
        $('#ComboReg').val('');
        $('#ComboPro').val('').prop('disabled', true);
        $('#TextNombre').val('');
        $('#modalTitle').text('Nueva Comuna');
        $('#modalGrid').modal('show');
    });

    $('#ComboReg').on('change', function () {
        var idReg = $(this).val();
        if (idReg === '' || idReg === '0') {
            $('#ComboPro').html('<option value="">-- Seleccione Provincia --</option>').prop('disabled', true);
        } else {
            cargarProvincias(idReg);
        }
    });

    $('#Grid').on('click', '.btnEditar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idCom = cells.eq(0).text().trim();
        var nombre = cells.eq(1).text().trim();
        var provincia = cells.eq(2).text().trim();
        var region = cells.eq(3).text().trim();

        $('#textId').val(idCom);
        $('#TextNombre').val(nombre);
        
        // Buscar el IdReg del combo por nombre
        var idRegValue = $('#ComboReg option').filter(function(){ return $(this).text().trim() === region; }).val();
        $('#ComboReg').val(idRegValue);
        
        // Cargar provincias y luego seleccionar
        if (idRegValue) {
            cargarProvincias(idRegValue, function() {
                var idProValue = $('#ComboPro option').filter(function(){ return $(this).text().trim() === provincia; }).val();
                $('#ComboPro').val(idProValue);
            });
        }

        $('#modalTitle').text('Editar Comuna');
        $('#modalGrid').modal('show');
    });

    $('#Grid').on('click', '.btnEliminar', function () {
        var row = $(this).closest('tr');
        var cells = row.find('td');
        var idCom = cells.eq(0).text().trim();

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
                    url: './LocComu.aspx/Eliminar',
                    data: JSON.stringify({ IdCom: parseInt(idCom) }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        $.LoadingOverlay('hide');
                        if (data.d && data.d.estado) {
                            Swal.fire('Éxito', 'Comuna eliminada correctamente', 'success');
                            cargarDatos();
                        } else {
                            var msg = (data && data.d && data.d.valor) ? data.d.valor : 'No se pudo eliminar';
                            Swal.fire('Error', msg, 'error');
                        }
                    },
                    error: function (xhr, status, err) {
                        $.LoadingOverlay('hide');
                        console.error('Eliminar error', status, err, xhr.responseText);
                        Swal.fire('Error', 'Error en la eliminación: ' + status, 'error');
                    }
                });
            }
        });
    });

    $('#btnGuardarCambios').on('click', function () {
        if (!validarFormulario()) {
            return;
        }

        var idCom = parseInt($('#textId').val());
        var nombre = $('#TextNombre').val().trim();
        var idPro = parseInt($('#ComboPro').val());
        var idReg = parseInt($('#ComboReg').val());

        $.LoadingOverlay('show');

        var operacion = idCom === 0 ? 'Ingresar' : 'Actualizar';
        var datos = {
            obj: {
                IdCom: idCom,
                Nombre: nombre,
                IdPro: idPro,
                IdReg: idReg
            }
        };

        $.ajax({
            type: 'POST',
            url: './LocComu.aspx/' + operacion,
            data: JSON.stringify(datos),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $.LoadingOverlay('hide');
                if (data.d && data.d.estado) {
                    Swal.fire('Éxito', operacion === 'Ingresar' ? 'Comuna ingresada correctamente' : 'Comuna actualizada correctamente', 'success');
                    $('#modalGrid').modal('hide');
                    cargarDatos();
                } else {
                    var msg = (data && data.d && data.d.valor) ? data.d.valor : 'No se pudo guardar';
                    Swal.fire('Error', msg, 'error');
                }
            },
            error: function (xhr, status, err) {
                $.LoadingOverlay('hide');
                console.error('Guardar error', status, err, xhr.responseText);
                Swal.fire('Error', 'Error al guardar: ' + status, 'error');
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
        var idPro = $('#ComboPro').val();

        if (idReg === '' || idReg === '0') {
            Swal.fire('Validación', 'Debe seleccionar una Región', 'warning');
            return false;
        }
        if (idPro === '' || idPro === '0') {
            Swal.fire('Validación', 'Debe seleccionar una Provincia', 'warning');
            return false;
        }
        if (nombre === '') {
            Swal.fire('Validación', 'El nombre es requerido', 'warning');
            return false;
        }
        return true;
    }

    function cargarRegiones() {
        // Try GET first (server allows GET). Use explicit relative path to avoid routing issues.
        $.ajax({
            type: 'GET',
            url: './LocComu.aspx/ObtenerRegiones',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data && data.d && data.d.estado) {
                    var options = '<option value="">-- Seleccione Región --</option>';
                    $.each(data.d.objeto, function (index, item) {
                        options += '<option value="' + item.IdReg + '">' + item.Nombre + '</option>';
                    });
                    $('#ComboReg').html(options);
                } else {
                    console.warn('No regiones returned or estado false', data);
                    // fallback: try POST
                    cargarRegionesPostFallback();
                }
            },
            error: function (xhr, status, err) {
                console.error('cargarRegiones GET error', status, err, xhr.responseText);
                // fallback to POST
                cargarRegionesPostFallback();
            }
        });
    }

    function cargarRegionesPostFallback() {
        $.ajax({
            type: 'POST',
            url: './LocComu.aspx/ObtenerRegiones',
            data: '{}',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data && data.d && data.d.estado) {
                    var options = '<option value="">-- Seleccione Región --</option>';
                    $.each(data.d.objeto, function (index, item) {
                        options += '<option value="' + item.IdReg + '">' + item.Nombre + '</option>';
                    });
                    $('#ComboReg').html(options);
                } else {
                    console.error('cargarRegiones POST returned no data', data);
                    Swal.fire('Error', 'No se pudieron cargar las regiones', 'error');
                }
            },
            error: function (xhr, status, err) {
                console.error('cargarRegiones POST error', status, err, xhr.responseText);
                Swal.fire('Error', 'Error al cargar regiones: ' + status, 'error');
            }
        });
    }

    function cargarProvincias(idReg, callback) {
        $.ajax({
            type: 'POST',
            url: './LocComu.aspx/ObtenerProvincias',
            data: JSON.stringify({ IdReg: parseInt(idReg) }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data && data.d && data.d.estado) {
                    var options = '<option value="">-- Seleccione Provincia --</option>';
                    $.each(data.d.objeto, function (index, item) {
                        options += '<option value="' + item.IdPro + '">' + item.Nombre + '</option>';
                    });
                    $('#ComboPro').html(options).prop('disabled', false);
                    if (callback) callback();
                } else {
                    console.warn('No provincias returned', data);
                    $('#ComboPro').html('<option value="">-- Seleccione Provincia --</option>').prop('disabled', true);
                }
            },
            error: function (xhr, status, err) {
                console.error('cargarProvincias error', status, err, xhr.responseText);
                $('#ComboPro').html('<option value="">-- Seleccione Provincia --</option>').prop('disabled', true);
            }
        });
    }

    function cargarDatos() {
        $.LoadingOverlay('show');
        $.ajax({
            type: 'GET',
            url: './LocComu.aspx/Obtener',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $.LoadingOverlay('hide');
                if (data && data.d && data.d.estado) {
                    if ($.fn.DataTable.isDataTable('#Grid')) {
                        $('#Grid').DataTable().destroy();
                    }

                    var html = '';
                    $.each(data.d.objeto, function (index, item) {
                        html += '<tr>';
                        html += '<td>' + item.IdCom + '</td>';
                        html += '<td>' + item.Nombre + '</td>';
                        html += '<td>' + (item.Pro ? item.Pro.Nombre : '') + '</td>';
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
                    Swal.fire('Error', (data && data.d && data.d.valor) || 'No se pudo cargar', 'error');
                }
            },
            error: function (xhr, status, err) {
                $.LoadingOverlay('hide');
                console.error('cargarDatos error', status, err, xhr.responseText);
                Swal.fire('Error', 'Error en la carga de datos', 'error');
            }
        });
    }
});
