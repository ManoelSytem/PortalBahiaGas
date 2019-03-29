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
