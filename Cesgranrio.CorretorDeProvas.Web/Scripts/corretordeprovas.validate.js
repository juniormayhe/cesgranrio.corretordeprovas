/*
Funções para validação de pontuação
Author: Wanderley Mayhe Junior <juniormayhe@gmail.com>
*/
var somatorio = function () {
    try {
        var totalPontos = 0;
        $('.grade').each(function () {
            valor = new Intl.NumberFormat('en-US', { minimumFractionDigits: 2 }).format($(this).val().replace(',', '.'));

            totalPontos += parseFloat(valor);
            $('#total').val(new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, useGrouping: false }).format(totalPontos));
        });
    }
    catch (err) { }
};
$(document).ready(function () {
    $('.grade').each(function () { if ($(this).val() == '') $(this).val('0,00');});
    $('.grade').on('change', function () {
        try {
            var valor = $(this).val();
            if (valor.indexOf(',') > 0) {
                valor = parseFloat(valor.replace(',', '.'));
            }
            if (isNaN(valor)) {
                valor = '0,00';
            }
            else {
                valor = new Intl.NumberFormat('pt-BR', { minimumFractionDigits: 2, useGrouping: false }).format(valor);
                
            }
            $(this).val(valor);

            //somatorio();
            
        }
        catch (err) {
        }
    });
    //somatorio();    

});

/*sobreescrever métodos do mvc para evitar mensagem the field must be a number*/
$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}