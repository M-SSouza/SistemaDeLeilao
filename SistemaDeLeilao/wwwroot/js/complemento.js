//Adicionando a mascara de dinheiro
$(document).ready(function () {
    $("#money").maskMoney({
        allowNegative: false,
        thousands: '.',
        decimal: ',',
        allowEmpty: true
    });
});