var linha = $("#templateLinha").html();

$(document).ready(function () {
    $("#btnAdd").click(function () {
        $(".empty").parent().remove();
        $("#grid").append(linha);
    });
});

function Remover(linha) {
    $(linha).parent().parent().remove();
}
if ($("#formTurno").find("#IdRegistroTurno").val() != "0") {
    $("#formTurno input").attr("readonly", "readonly");
    $("#formTurno input select").attr("disable", "disable");
    $(".form-control").attr("readonly", "readonly");
    $(".form-control").attr("disable", "true");
    $(".form - group").attr("disable", "true");
}

