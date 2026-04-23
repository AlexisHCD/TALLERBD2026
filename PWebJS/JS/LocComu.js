var table
$(document).ready(function () {
    cargarDatos();
    ObtenerRegi();
    ObtenerProv();
    $("#ComboIngModReg").change(function () {
        var idReg = $(this).val();
        Filtrar(idReg);
    });
});

function cargarDatos() {

    if ($.fn.DataTable.isDataTable('#Grid')) {
        $('#Grid').DataTable().destroy();
    }
    $('#Grid tbody').html('');
    AjaxGet("../LocComu.aspx/Obtener",
        function (response) {
            $(".card-body").LoadingOverlay("hide");
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $("<tr>").append(
                        $("<td>").text(i + 1),
                        $("<td>").text(row.Nombre),
                        $("<td>").text(row.Pro.Nombre),
                        $("<td>").text(row.Reg.Nombre),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-sm btn-primary mr-1").text("Editar").data("ELocCom", row),
                            $("<button>").addClass("btn btn-sm btn-danger").text("Eliminar").data("ELocCom", row.IdCom)
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
    $("#ComboIngModReg").html("");
    AjaxGet("../LocRegi.aspx/Obtener",
        function (response) {
            $(".card-body").LoadingOverlay("hide");
            $("<option>").attr({ "value": "0" }).text("Seleccione Región").appendTo("#ComboIngModReg")
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $("<option>").attr({ "value": row.IdReg }).text(row.Nombre).appendTo("#ComboIngModReg");
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
function ObtenerProv() {
    $("#ComboIngModPro").html("");
    AjaxGet("../LocProvi.aspx/Obtener",
        function (response) {
            $(".card-body").LoadingOverlay("hide");
            $("<option>").attr({ "value": "0" }).text("Seleccione Provincia").appendTo("#ComboIngModPro")
            if (response.estado) {
                $.each(response.objeto, function (i, row) {
                    $("<option>").attr({ "value": row.IdPro }).text(row.Nombre).appendTo("#ComboIngModPro");
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

function Filtrar(idReg) {
    $.ajax({
        type: "POST",
        url: "LocComu.aspx/Filtrar",
        data: JSON.stringify({ IdReg: idReg }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var provincias = response.d;
            var comboIngModPro = $("#ComboIngModPro");
            comboIngModPro.empty();
            $.each(provincias, function (i, provincia) {
                $("<option>").val(provincia.IdPro).text(provincia.Nombre).appendTo(comboIngModPro);
            });
        },
        error: function (error) {
            console.error("Error al obtener provincias:", error);
        }
    });
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

    var model = $(this).data("ELocCom")
    $("#textId").val(model.IdCom);
    $("#TextIngMod").val(model.Nombre);
    $("#ComboIngModReg").val(model.IdReg);
    $("#ComboIngModPro").val(model.IdPro);
    $('#modalGrid').modal('show');
})
$('#btnNuevo').on('click', function () {

    $("#textId").val(0);
    $("#TextIngMod").val("");
    $("select#ComboIngModPro").prop('selectedIndex', 0);
    $("select#ComboIngModReg").prop('selectedIndex', 0);
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
                IdCom: parseInt($("#textId").val()),
                Nombre: $("#TextIngMod").val(),
                IdPro: $("#ComboIngModPro").val(),
                IdReg: $("#ComboIngModReg").val(),
            }
        }
        if (parseInt($("#textId").val()) == 0) {

            AjaxPost("../LocComu.aspx/Ingresar", JSON.stringify(request),
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
            AjaxPost("../LocComu.aspx/Actualizar", JSON.stringify(request),
                function (response) {
                    Filtrar(idReg);
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

    var request = { IdCom: String($(this).data("ELocCom")) };
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

        AjaxPost("../LocComu.aspx/Eliminar", JSON.stringify(request),
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
