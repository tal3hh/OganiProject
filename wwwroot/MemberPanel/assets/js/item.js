let item = JSON.parse(localStorage.getItem("products"));


let prcs = item.map(p => p.price)


let prcss=prcs.map(a=>a.slice(1))
let prcsss=prcss.map(Number)



let totalsum = document.querySelector(".price").children[1];
let totalsumm = Number(totalsum);




let pricesumm=0;
for (let i = 0; i < prcsss.length; i++) {

    pricesumm += prcsss[i];
    
}


totalsum.innerText = pricesumm + ".00$"




