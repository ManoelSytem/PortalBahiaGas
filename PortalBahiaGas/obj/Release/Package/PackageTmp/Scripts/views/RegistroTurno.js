$(document).ready(function () {
    $(this).tab('show');
    if ($("#Turno option:selected").val() != 2) {
        $("#abaOdorizador").hide();
    }
});

function Salvar(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaTurno"
            }, function (data) {
                $('#conteudoTurno').html(data);
            });
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaGasoduto"
            }, function (data) {
                $('#conteudoGasoduto').html(data);
                $(document).find("#abaGasoduto").removeClass("not-active");
                $(document).find("#abaGasoduto").tab('show');
            });
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaPontoEntrega"
            }, function (data) {
                $('#conteudoPontoEntrega').html(data);
                $(document).find("#abaPontoEntrega").removeClass("not-active");
            });
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaCliente"
            }, function (data) {
                $('#conteudoCliente').html(data);
                $(document).find("#abaCliente").removeClass("not-active");
            });
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaOcorrencia"
            }, function (data) {
                $('#conteudoOcorrencia').html(data);
                $(document).find("#abaOcorrencia").removeClass("not-active");
            });
            ObterAba({
                id: data.Objeto.Id,
                aba: "abaPendencia"
            }, function (data) {
                $('#conteudoPendencia').html(data);
                $(document).find("#abaPendencia").removeClass("not-active");
            });
            if ($("#Turno option:selected").val() == 2) {
                ObterAba({
                    id: data.Objeto.Id,
                    aba: "abaOdorizador"
                }, function (data) {
                    $('#conteudoOdorizador').html(data);
                    $(document).find("#abaOdorizador").removeClass("not-active");
                });
            } else {
                $("#abaOdorizador").hide();
            }
        }
        alert(data.Mensagem);
    });
}

function SalvarGasoduto(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaGasoduto"
            }, function (data) {
                $('#conteudoGasoduto').html(data);
            });
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaPontoEntrega"
            }, function (data) {
                $('#conteudoPontoEntrega').html(data);
                $(document).find("#abaPontoEntrega").removeClass("not-active");
                $(document).find("#abaPontoEntrega").tab('show');
            });
        }
        alert(data.Mensagem);
    });
}

function SalvarPontoEntrega(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaPontoEntrega"
            }, function (data) {
                $('#conteudoPontoEntrega').html(data);
            });
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaCliente"
            }, function (data) {
                $('#conteudoCliente').html(data);
                $(document).find("#abaCliente").removeClass("not-active");
                $(document).find("#abaCliente").tab('show');
            });
        }
        alert(data.Mensagem);
    });
}

function SalvarCliente(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaCliente"
            }, function (data) {
                $('#conteudoCliente').html(data);
            });
            if ($("#Turno option:selected").val() == 2) {
                ObterAba({
                    id: $("#" + pForm).find("#IdRegistroTurno").val(),
                    aba: "abaOdorizador"
                }, function (data) {
                    $('#conteudoOdorizador').html(data);
                    $(document).find("#abaOdorizador").removeClass("not-active");
                    $(document).find("#abaOdorizador").tab('show');
                });
            } else {
                ObterAba({
                    id: $("#" + pForm).find("#IdRegistroTurno").val(),
                    aba: "abaOcorrencia"
                }, function (data) {
                    $('#conteudoOcorrencia').html(data);
                    $(document).find("#abaOcorrencia").removeClass("not-active");
                    $(document).find("#abaOcorrencia").tab('show');
                });
            }
        }
        alert(data.Mensagem);
    });
}

function SalvarOdorizador(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaOdorizador"
            }, function (data) {
                $('#conteudoOdorizador').html(data);
            });
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaOcorrencia"
            }, function (data) {
                $('#conteudoOcorrencia').html(data);
                $(document).find("#abaOcorrencia").removeClass("not-active");
                $(document).find("#abaOcorrencia").tab('show');
            });
        }
        alert(data.Mensagem);
    });
}

function SalvarOcorrencia(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        alert(data.Mensagem);
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaOcorrencia"
            }, function (data) {
                $('#conteudoOcorrencia').html(data);
            });
            $('#OcorrenciaModal').modal('toggle');
            $('.modal-backdrop').remove();
        }
    });
}

function SalvarPendencia(pForm) {
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        alert(data.Mensagem);
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaPendencia"
            }, function (data) {
                $('#conteudoPendencia').html(data);
            });
            $('#PendenciaModal').modal('toggle');
            $('.modal-backdrop').remove();
        }
    });
}

function ObterAba(dados, funcao) {
    $.ajax({
        url: "../../RegistroTurno/ObterAbas",
        data: dados,
        type: 'post'
    }).done(function (data) {
        funcao(data);
    });
}

function SelecionarAba(pAba) {
    var div = $(pAba).attr('href');
    var id = $(div).find('#IdRegistroTurno').val();

    ObterAba({
        id: id,
        aba: $(pAba).attr("id")
    }, function (data) {
        $(div).html(data);
        $(this).tab('show');
    });
}

function PopUp(pIdOcorrencia, pIdRegistroTurno) {
    var dados = { id: pIdOcorrencia, idRegistroTurno: pIdRegistroTurno };
    $.ajax({
        url: '../../RegistroTurno/PopUpOcorrencia',
        data: dados,
        type: 'post'
    }).done(function (data) {
        $('#modalWrapper').html(data);
        $('#OcorrenciaModal').modal();
    });
}

function PopUpPendencia(pIdPendencia, pIdRegistroTurno) {
    var dados = { id: pIdPendencia, idRegistroTurno: pIdRegistroTurno };
    $.ajax({
        url: '../../RegistroTurno/PopUpPendencia',
        data: dados,
        type: 'post'
    }).done(function (data) {
        $('#modalWrapperPendencia').html(data);
        $('#PendenciaModal').modal();
    });
}
