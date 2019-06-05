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
                '<div class="col-sm-10 col-detail">' +
                    '<input type="text" class="input-grid" name="nome" value="" />' +
                '</div>' +
            '</div>';

$(document).ready(function () {
    $("#btnAdd").click(function () {
        $(".empty").parent().remove();
        $("#grid").append(linha);
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
        Nome: $(linha).parent().parent().find("input[name='nome']").val()
    };

    $.ajax({
        url: "Operador/Salvar",
        data: usuario,
        type: 'post'
    }).done(function (data) {
        alert(data.Mensagem);
        if (!data.Erro) {
            location.reload();
        }
    });
}

function SalvarOperadorSalaControle(pForm) {

    var checkboxes = document.querySelectorAll('[name=SalaControle]');
    var values = [];
    var valuesFalse = [];
    for (var i = 0; i < checkboxes.length; i++) {
        if ($(checkboxes[i]).prop("checked")) {
            values.push(checkboxes[i].value);
        } else {
          valuesFalse.push(checkboxes[i].value);
        }
    }
    alert(values);
    alert(valuesFalse);
    $.ajax({
        url: "Sala/SalvarOperadorSalaControle",
        data: { codigoOperadorSalaControle: values, operadorSalaControleFalse: valuesFalse },
        type: 'post'
    }).done(function (data) {
        alert(data.Mensagem);
        if (!data.Erro) {
            location.reload();
        }
    });
}

//alterar estado do checkbox
$(document).ready(function () {
    $('input[name="SalaControle"]').on('change', function () {
        if ($(this).prop("checked")) {
            $(this).prop("checked", true);
        } else {
            $(this).prop("checked",false);
        }
    });
});

function Remover(linha) {
    var usuario = {
        Id: $(linha).parent().parent().find("input[name='id']").val(),
        Nome: $(linha).parent().parent().find("input[name='nome']").val()
    };
    if (usuario.Id == "0") {
        $(linha).parent().parent().remove();
    } else {
        if (confirm("Confirma a exclusão do operador: " + usuario.Nome + "?")) {
            $.ajax({
                url: "Operador/Remover",
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
