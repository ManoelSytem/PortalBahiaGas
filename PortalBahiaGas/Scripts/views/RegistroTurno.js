$(document).ready(function () {
    $(this).tab('show');
    if ($("#Turno option:selected").val() != 2) {
        $("#abaOdorizador").hide();
    }
});

function lazyLoad(response) {

    if (!response.Erro) {
        $("#loadMe").modal("hide");

        $("#modalmensagem").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
        $("#modalmensagem .modal-body").text(response.Mensagem);
    } else {
        $("#loadMe").modal("hide");

        $("#modalmensagem").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
          
        $("#modalmensagem .modal-body").text(response.Mensagem);
    }

}


function AdicionarOperador(id) {

    var OperadorCamacariText = $("#OperadorCamacari option:selected").text();
    var OperadorCamacariValor = $("#OperadorCamacari option:selected").val();

    var OperadorFeiraText = $("#OperadorFeira option:selected").text();
    var OperadorFeiraValor = $("#OperadorFeira option:selected").val();

    var OperadorSalvadorText = $("#OperadorSalvador option:selected").text();
    var OperadorSalvadorVal = $("#OperadorSalvador option:selected").val();


    var OperadorSalaControleText = $("#OperadorSalaControle option:selected").text();
    var OperadorSalaControleValor = $("#OperadorSalaControle option:selected").val();


    $.ajax({
        url: "../../Sala/AlocacaoOperador",
        data: { codigoProthues: OperadorCamacariValor, local: "CAMAÇARI", nomeOperador: OperadorCamacariText, codigoProthuesF: OperadorFeiraValor, localF: "FEIRA DE SANTANA", nomeOperadorF: OperadorFeiraText, codigoProthuesS: OperadorSalvadorVal, localS: "SALVADOR", nomeOperadorS: OperadorSalvadorText, codigoProthuesSl: OperadorSalaControleValor, localSl: "SALA DE CONTROLE", nomeOperadorSl: OperadorSalaControleText, registroTurnoCodigo: id },
        type: 'get'
    }).done(function (data) {
        $("#panel").html(data);
    });

}


function Salvar(pForm) {
    $(".btn btn-success").disabled = 'true';
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        $(".btn btn-success").disabled = 'false';
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
        lazyLoad(data);
    });
}


function SalvarGasoduto(pForm) {
    $(".btn btn-success").attr("disabled", true);
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
        lazyLoad(data);
    });
}

function SalvarPontoEntrega(pForm) {
    $(".btn btn-success").attr("disabled", true);
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
        $(".btn btn-success").disable = 'true';
    });
}

function SalvarCliente(pForm) {
    $(".btn btn-success").attr("disabled", true);
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
        lazyLoad(data);
    });
}

function SalvarOdorizador(pForm) {
    $(".btn btn-success").attr("disabled", true);
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
        lazyLoad(data);
    });
}

function SalvarOcorrencia(pForm) {

    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });

    $(".btn-salvar").attr("disabled", true);
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        $(".btn-salvar").attr("disabled", false);
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaOcorrencia"
            }, function (data) {
                $('#conteudoOcorrencia').html(data);
                $("#loadMe").modal("hide");
            });
            $('#OcorrenciaModal').modal('toggle');
            $('.modal-backdrop').remove();
        } else {
            lazyLoad(data);
        }
    });
}


function SalvarPendencia(pForm) {

    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });

    $(".btn-salvar").attr("disabled", true);
    $.ajax({
        url: "../../RegistroTurno/Cadastrar",
        data: $("#" + pForm).serializeArray(),
        type: 'post'
    }).done(function (data) {
        $(".btn-salvar").attr("disabled", false);
        if (!data.Erro) {
            ObterAba({
                id: $("#" + pForm).find("#IdRegistroTurno").val(),
                aba: "abaPendencia"
            }, function (data) {
                $('#conteudoPendencia').html(data);
                $("#loadMe").modal("hide");
            });
            $('#PendenciaModal').modal('toggle');
            $('.modal-backdrop').remove();
        } else {
            lazyLoad(data);
        }
        });
}

function ObterAba(dados, funcao) {
    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });

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
        $("#loadMe").modal("hide");
        $(this).tab('show');
    });
}

function PopUp(pIdOcorrencia, pIdRegistroTurno) {
    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });

   
    var dados = { id: pIdOcorrencia, idRegistroTurno: pIdRegistroTurno };
    $.ajax({
        url: '../../RegistroTurno/PopUpOcorrencia',
        data: dados,
        type: 'post'
    }).done(function (data) {
        if (!data.Erro) {
            $("#loadMe").modal("hide");
            $('#modalWrapper').html(data);
            $("#OcorrenciaModal").modal({
                backdrop: "static", //remove ability to close modal with click
                keyboard: false,
                show: true //remove option to close with keyboard
            });
        } else {
            $("#modalmensagem .modal-body").text(response.Mensagem);
        }
    });
}


function CalcularTotalVazaoRetirada() {
    var elements = document.getElementsByClassName("input-grid vazao");
    var total = 0;
    var existeVazio = false;
    for (var i = 0; i < elements.length; i++) {
        if (!elements[i].value) {
            existeVazio = true;
        }
    }

    if (!existeVazio) {
        for (var i = 0; i < elements.length; i++) {
            if (!elements[i].value) {
                existeVazio = true;
            } else {
                total += elements[i].value.replace(".", "");
            }
        }

        $.ajax({
            url: '../../RegistroTurno/ObterTotalMetroCubico',
            changeMonth: true,
            changeYear: true,
            dataType: 'json',
            type: 'get',
            data: { vazaoEntrada: total },
            success: function (data) {
                for (var i = 0; data.length > i; i++) {
                    var totalformatado = data[i];
                }
                $("#totalVazaoEntrada").html("");
                $("#totalVazaoEntrada").html(totalformatado);;
            }
        });
    }

}

function PopUpPendencia(pIdPendencia, pIdRegistroTurno) {
    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });

    var dados = { id: pIdPendencia, idRegistroTurno: pIdRegistroTurno };
    $.ajax({
        url: '../../RegistroTurno/PopUpPendencia',
        data: dados,
        type: 'post'
    }).done(function (data) {
        $("#loadMe").modal("hide");
        $('#modalWrapperPendencia').html(data);
        $("#PendenciaModal").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true //Display loader!
        });
    });
}
