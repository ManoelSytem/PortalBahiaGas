$(document).ready(function () {
    $('.hora').mask('99:99', { autoclear: false, placeholder: " " });
    $('.horaVazia').mask('99:99', { autoclear: false, placeholder: " " });
    $('.dataHora').mask('99/99/9999 99:99', { autoclear: false, placeholder: " " });
    $('.vazao').maskNumber();
    $(".decimal").maskMoney({ thousands: '.', decimal: ',', allowZero: false });
    $('.telefone').focusout(function () {
        var phone, element;
        element = $(this);
        element.unmask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.mask("(99) 99999-999?9", { placeholder: " " });
        } else {
            element.mask("(99) 9999-9999?9", { placeholder: " " });
        }
    }).trigger('focusout');
});