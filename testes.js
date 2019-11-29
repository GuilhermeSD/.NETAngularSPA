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

console.log(somaDosPares(10)); //30
console.log(somaDosPares(6)); //12