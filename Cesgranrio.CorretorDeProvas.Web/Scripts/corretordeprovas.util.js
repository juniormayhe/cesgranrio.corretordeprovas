/*
Funções de uso comum
Author: Wanderley Mayhe Junior <juniormayhe@gmail.com>
*/

var padLeft = function (numero, posicoes, caracter) {
    return Array(posicoes - String(numero).length + 1).join(caracter || '0') + numero;
};

formatarCPF = function(numero){
    return numero.replace(/([0-9]{3})\.?([0-9]{3})\.?([0-9]{3})\-?([0-9]{2})/g, "$1.$2.$3/$4");
}
