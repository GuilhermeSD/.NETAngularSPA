//var soma = 0; 
function somaDosPares(x) {
    var soma = 0;
    for(var i=0; i<=x; i++){
        if(i%2==0) {
            soma +=i;
        }
    }

    return soma;
}

function somaAnteriores(x) {
    var soma = 0;
    for(var i=0; i<x; i++){
        //if(i%2==0) {
            soma +=i;
        //}
    }

    return soma;
}

console.log(somaAnteriores(4)); //6
console.log(somaAnteriores(5)); //10