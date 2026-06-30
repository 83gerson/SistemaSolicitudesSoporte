$(document).ready(function () {


    // Confirmación antes de enviar el formulario con JAVASCRIPT

    $("#formSolicitud").on("submit", function (e) {
        var confirmar = confirm("¿Está seguro de que desea registrar esta solicitud?");
        if (!confirmar) {
            e.preventDefault();
        }
    });


    // Cálculo en pantalla de los caracteres escritos con JAVASCRIPT
    $("#descripcionTexto").on("keyup", function () {
        var longitud = $(this).val().length;
        $("#contadorCaracteres").text(longitud + " / 500 caracteres");
    });

    // Filtra solicitudes sin recargar la página con AJAX
    function filtrarSolicitudes() {
        var categoria = $("#filtroCategoria").val();
        var estado = $("#filtroEstado").val();

        $.ajax({
            url: "/Solicitudes/FiltrarAjax",
            type: "GET",
            data: { categoria: categoria, estado: estado },
            dataType: "json",
            success: function (data) {
                var tbody = $("#tablaSolicitudes tbody");
                tbody.empty();

                if (data.length === 0) {
                    $("#sinResultados").show();
                } else {
                    $("#sinResultados").hide();
                    $.each(data, function (i, item) {
                        var fila =
                            "<tr>" +
                            "<td>" + item.Id + "</td>" +
                            "<td>" + item.NombreSolicitante + "</td>" +
                            "<td>" + item.Categoria + "</td>" +
                            "<td>" + item.Estado + "</td>" +
                            "<td>" + item.FechaRegistro + "</td>" +
                            "<td><a class='btn btn-info btn-xs' href='/Solicitudes/Details/" + item.Id + "'>Ver detalle</a></td>" +
                            "</tr>";
                        tbody.append(fila);
                    });
                }
            },
            error: function () {
                alert("Ocurrió un error al filtrar las solicitudes.");
            }
        });
    }

    $("#filtroCategoria, #filtroEstado").on("change", filtrarSolicitudes);
});
