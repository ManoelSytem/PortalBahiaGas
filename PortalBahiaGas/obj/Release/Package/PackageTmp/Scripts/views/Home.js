function onSubmit() {
    var dataInicial = new Date($("#DataFinal").val());
    var dataFinal = new Date($("#DataInicial").val());
    if (dataFinal > dataInicial) {
        alert("Data final não pode ser menor que data inicial.");
        return;
    }

    $('#form1').submit();
    return true;
}