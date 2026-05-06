var table;
// Inicialización de la vista: carga datos y configura eventos.
$(document).ready(function () {
    cargarDatos();

    // Formatea el nombre a Title Case.
    $('#TextNombre').on('input', function () {
        var texto = $(this).val();
        var titleCase = texto.replace(/\w\S*/g, function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
        $(this).val(titleCase);
    });
});

// Carga la grilla principal de productos.
function cargarDatos() {

    if ($.fn.DataTable.isDataTable('#Grid')) {
        $('#Grid').DataTable().destroy();
    }
    $('#Grid tbody').html('');

    AjaxGet('../Productos.aspx/Obtener',
        function (response) {
            $('.card-body').LoadingOverlay('hide');
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $('<tr>').append(
                        $('<td>').text(i + 1),
                        $('<td>').text(row.Nombre),
                        $('<td>').text(row.FInc),
                        $('<td>').text(row.CInc),
                        $('<td>').text(row.CAct),
                        $('<td>').text(row.CArr),
                        $('<td>').text(row.TAct),
                        $('<td>').text(row.VArr),
                        $('<td>').append(
                            $('<button>').addClass('btn btn-sm btn-primary mr-1').text('Editar').data('EProd', row),
                            $('<button>').addClass('btn btn-sm btn-danger').text('Eliminar').data('EProd', row.IdProd)
                        )
                    ).appendTo('#Grid tbody');
                });
            }
            table = $('#Grid').DataTable({
                responsive: true,
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json'
                }
            });
        },
        function () {
            $('.card-body').LoadingOverlay('hide');
        },
        function () {
            $('.card-body').LoadingOverlay('show');
        });
}

// Abre modal para nuevo producto.
$('#btnNuevo').on('click', function () {
    $('#textId').val(0);
    $('#TextNombre').val('');
    $('#TextFInc').val('');
    $('#TextCInc').val('');
    $('#TextCAct').val('');
    $('#TextCArr').val('');
    $('#TextTAct').val('');
    $('#TextVArr').val('');
    $('#modalGrid').modal('show');
});

// Carga datos en modal para edición.
$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-primary mr-1"]', function () {
    var model = $(this).data('EProd');

    $('#textId').val(model.IdProd);
    $('#TextNombre').val(model.Nombre || '');
    $('#TextFInc').val(model.FInc || '');
    $('#TextCInc').val(model.CInc || '');
    $('#TextCAct').val(model.CAct || '');
    $('#TextCArr').val(model.CArr || '');
    $('#TextTAct').val(model.TAct || '');
    $('#TextVArr').val(model.VArr || '');

    $('#modalGrid').modal('show');
});

// Valida y guarda el producto (crear o actualizar).
$('#btnGuardarCambios').on('click', function () {
    var request = {
        obj: {
            IdProd: parseInt($('#textId').val() || '0'),
            Nombre: ($('#TextNombre').val() || '').trim(),
            FInc: ($('#TextFInc').val() || '').trim(),
            CInc: ($('#TextCInc').val() || '').trim(),
            CAct: ($('#TextCAct').val() || '').trim(),
            CArr: ($('#TextCArr').val() || '').trim(),
            TAct: ($('#TextTAct').val() || '').trim(),
            VArr: ($('#TextVArr').val() || '').trim()
        }
    };

    if (!request.obj.Nombre || !request.obj.FInc || !request.obj.CInc || !request.obj.CAct || !request.obj.CArr || !request.obj.TAct || !request.obj.VArr) {
        swal('Mensaje', 'Es necesario completar todos los campos', 'warning');
        return;
    }

    var url = request.obj.IdProd === 0 ? '../Productos.aspx/Ingresar' : '../Productos.aspx/Actualizar';
    var mensajeOk = request.obj.IdProd === 0 ? 'Ingreso fue realizado correctamente' : 'Actualización fue realizada correctamente';

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

// Elimina un producto con confirmación.
$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-danger"]', function () {
    var request = { IdProd: String($(this).data('EProd')) };

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
        AjaxPost('../Productos.aspx/Eliminar', JSON.stringify(request),
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
