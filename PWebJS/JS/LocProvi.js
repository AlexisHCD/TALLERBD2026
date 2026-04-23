var table
$(document).ready(function () {
    cargarDatos();
    ObtenerRegi();
});

function cargarDatos() {

    if ($.fn.DataTable.isDataTable('#Grid')) {
        $('#Grid').DataTable().destroy();
    }
    $('#Grid tbody').html('');
    AjaxGet("../LocProvi.aspx/Obtener",
        function (response) {
            $(".card-body").LoadingOverlay("hide");
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $("<tr>").append(
                        $("<td>").text(i + 1),
                        $("<td>").text(row.Nombre),
                        $("<td>").text(row.Reg.Nombre),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-sm btn-primary mr-1").text("Editar").data("ELocPro", row),
                            $("<button>").addClass("btn btn-sm btn-danger").text("Eliminar").data("ELocPro", row.IdPro)
                        )
                    ).appendTo("#Grid tbody");
                })
            }
            table = $('#Grid').DataTable({
                responsive: true
            });
        },
        function () {
            $(".card-body").LoadingOverlay("hide");
        },
        function () {
            $(".card-body").LoadingOverlay("show");
        })
}
function ObtenerRegi() {
    $("#ComboIngMod").html("");
    AjaxGet("../LocRegi.aspx/Obtener",
        function (response) {
            $(".card-body").LoadingOverlay("hide");
            $("<option>").attr({ "value": "0" }).text("Seleccione Región").appendTo("#ComboIngMod")
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $("<option>").attr({ "value": row.IdReg }).text(row.Nombre).appendTo("#ComboIngMod");
                })
            }
        },
        function () {
            $(".card-body").LoadingOverlay("hide");
        },
        function () {
            $(".card-body").LoadingOverlay("show");
        })
}
$('#TextIngMod').on('input', function () {
    // Convertir a Title Case
    var text = $(this).val();
    var titleCasedText = text.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });

    // Obtener la posición actual del cursor
    var cursorPosition = this.selectionStart;

    // Establecer el texto convertido en el campo de texto
    $(this).val(titleCasedText);

    // Restaurar la posición del cursor al final del texto
    this.setSelectionRange(cursorPosition, cursorPosition);
});
$('#TextIngMod').on('keypress', function (event) {
    // Verificar si la tecla presionada es un dígito
    if (event.key >= '0' && event.key <= '9') {
        event.preventDefault();
        alert('Solo se permiten letras.');
    }
});
$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-primary mr-1"]', function () {

    var model = $(this).data("ELocPro")
    $("#textId").val(model.IdPro);
    $("#TextIngMod").val(model.Nombre);
    $("#ComboIngMod").val(model.IdReg);
    $('#modalGrid').modal('show');
})

$('#btnNuevo').on('click', function () {

    $("#textId").val(0);
    $("#TextIngMod").val("");
    $("select#ComboIngMod").prop('selectedIndex', 0);
    $('#modalGrid').modal('show');
})
$('#btnGuardarCambios').on('click', function () {
    var camposvacios = false;
    var fields = $(".model").serializeArray();
    $.each(fields, function (i, field) {
        if (!field.value) {
            camposvacios = true;
            return false;
        }
    });
    if (!camposvacios) {

        var request = {
            obj: {
                IdPro: parseInt($("#textId").val()),
                Nombre: $("#TextIngMod").val(),
                IdReg: $("#ComboIngMod").val(),
            }
        }
        if (parseInt($("#textId").val()) == 0) {

            AjaxPost("../LocProvi.aspx/Ingresar", JSON.stringify(request),
                function (response) {
                    $(".modal-body").LoadingOverlay("hide");
                    if (response.estado) {
                        cargarDatos();
                        $('#modalGrid').modal('hide');
                        swal("Ingreso fue realizado correctamente")
                    } else {
                        swal("oops!", "Seleccione un registro valido", "warning")
                    }
                },
                function () {
                    $(".modal-body").LoadingOverlay("hide");
                },
                function () {
                    $(".modal-body").LoadingOverlay("show");
                })
        } else {
            AjaxPost("../LocProvi.aspx/Actualizar", JSON.stringify(request),
                function (response) {
                    $(".modal-body").LoadingOverlay("hide");
                    if (response.estado) {
                        cargarDatos();
                        $('#modalGrid').modal('hide');
                        swal("Actualización fue realizado correctamente")
                    } else {
                        swal("oops!", "Seleccione un registro valido", "warning")
                    }
                },
                function () {
                    $(".modal-body").LoadingOverlay("hide");
                },
                function () {
                    $(".modal-body").LoadingOverlay("show");
                })
        }
    } else {
        swal("Mensaje", "Es necesario completar todos los campos", "warning")
    }
})
$('#Grid tbody').on('click', 'button[class="btn btn-sm btn-danger"]', function () {

    var request = { IdPro: String($(this).data("ELocPro")) };
    console.log("Request data: ", request);
    swal({
        title: "Mensaje",
        text: "¿Está seguro realizar la eliminación?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        cancelButtonColor: '#d33',
        confirmButtonText: "Sí",
        cancelButtonText: "No",
        closeOnConfirm: false,
    }, function () {
        console.log("Confirm clicked");

        AjaxPost("../LocProvi.aspx/Eliminar", JSON.stringify(request),
            function (response) {
                console.log("Response received: ", response);
                if (response.estado) {
                    cargarDatos();
                    swal.close();
                } else {
                    swal("Mensaje", "No se pudo eliminar el registro", "warning");
                }
            },
            function (error) {
                console.log("Error: ", error);
            },
            function () {
                console.log("Complete");
            });
    });
});
