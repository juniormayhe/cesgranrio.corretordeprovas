/*
Funções para validação de pontuação
Author: Wanderley Mayhe Junior <juniormayhe@gmail.com>
*/

$(document).ready(function () {
    $('.grade').on('change', function () {
        try {
            var valor = $(this).val();
            if (valor.endsWith(',')) {
                valor = parseFloat(valor.replace(',', '.'));
                if (isNaN(valor))
                    valor = '';
                $(this).val(valor);
            }
        }
        catch (err) {
        }
    });
});

/*sobreescrever métodos para evitar mensagem the field must be a number*/
$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}