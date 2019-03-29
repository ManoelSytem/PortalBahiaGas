var linha = '<div class="row">' +
                '<input type="hidden" name="id" value="0"/>' +
                '<div class="col-sm-2 col-header">' +
                    '<a href="#" onclick="Salvar(this);" class="visivel">' +
                        '<span class="glyphicon glyphicon-ok"></span>' +
                    '</a>' +
                    '<a href="#" onclick="Editar(this);" class="invisivel">' +
                        '<span class="glyphicon glyphicon-pencil"></span>' +
                    '</a>' +
                    '<a href="#" onclick="Remover(this);" class="invisivel">' +
                        '<span class="glyphicon glyphicon-remove"></span>' +
                    '</a>' +
                    '<a href="#" onclick="Cancelar(this);" class="visivel">' +
                        '<span class="glyphicon glyphicon-remove-circle"></span>' +
                    '</a>' +
                '</div>' +
                '<div class="col-sm-6 col-detail">' +
                    '<input type="text" class="input-grid" name="Nome" value="" />' +
                '</div>' +
                '<div class="col-sm-2 col-detail">' +
                    '<input type="text" class="input-grid decimal text-right" name="SetPointCcStk" value="" />' +
                '</div>' +
                '<div class="col-sm-2 col-detail">' +
                    '<input type="text" class="input-grid intervalo text-right" name="SetPointPsi" value="" />' +
                '</div>' +
            '</div>';

$(document).ready(function () {
    $("#btnAdd").click(function () {
        $(".empty").parent().remove();
        $("#grid").append(linha);
        $('.intervalo').mask('99-99', { autoclear: false, placeholder: " " });
        $(".decimal").maskMoney({ thousands: '.', decimal: ',', allowZero: false });
    });
});

function Cancelar(linha) {
    $(linha).parent().parent().find('input,select').each(function () {
        $(this).prop("disabled", true);
    });
    MudarBotoes(linha);
}

function MudarBotoes(linha) {
    var invisivel = $(linha).parent().parent().find('.invisivel');
    var visivel = $(linha).parent().parent().find('.visivel');

    invisivel.each(function () {
        $(this).removeClass("invisivel");
        $(this).addClass("visivel");
    });

    visivel.each(function () {
        $(this).removeClass("visivel");
        $(this).addClass("invisivel");
    });
}

function Editar(linha) {
    $(linha).parent().parent().find("input,select").each(function () {
        $(this).prop('disabled', false);
    });
    MudarBotoes(linha);
}

function Salvar(linha) {
    var usuario = {
        Id: $(linha).parent().parent().find("input[name='id']").val(),
        Nome: $(linha).parent().parent().find("input[name='Nome']").val(),
        SetPointCcStk: $(linha).parent().parent().find("input[name='SetPointCcStk']").val(),
        SetPointPsi: $(linha).parent().parent().find("input[name='SetPointPsi']").val()
    };

    $.ajax({
        url: "Odorizador/Salvar",
        data: usuario,
        type: 'post'
    }).done(function (data) {
        alert(data.Mensagem);
        if (!data.Erro) {
            location.reload();
        }
    });
}

function Remover(linha) {
    var usuario = {
        Id: $(linha).parent().parent().find("input[name='id']").val(),
        Nome: $(linha).parent().parent().find("input[name='Nome']").val(),
        SetPointCcStk: $(linha).parent().parent().find("input[name='SetPointCcStk']").val(),
        SetPointPsi: $(linha).parent().parent().find("input[name='SetPointPsi']").val()
    };
    if (usuario.Id == "0") {
        $(linha).parent().parent().remove();
    } else {
        if (confirm("Confirma a excluisão de odorizador: " + usuario.Nome + "?")) {
            $.ajax({
                url: "Odorizador/Remover",
                data: usuario,
                type: 'post'
            }).done(function (data) {
                alert(data.Mensagem);
                if (!data.Erro) {
                    location.reload();
                }
            });
        }
    }
}
