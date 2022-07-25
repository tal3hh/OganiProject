

//header start ALL CATEGORIES dropdown

$(document).ready(function () {

    $(".dropbtns").click(function () {
        $("#myDropdown").toggle(1000);
    });

});

window.onclick = function (event) {
    if (!event.target.matches('.dropbtns')) {
        var dropdowns = document.getElementsByClassName("dropdown-contents");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}
//header end ALL CATEGORIES dropdown


//start searchfilterdropdown

$(document).ready(function () {

    $("#all-categ").click(function () {
        $(".dropdown-content-cate").toggle(800);

    })
});
//end searchfilterdropdown


//addproduct via localstorage
let fav=document.querySelector(".products .row")
let favories=[];
if (JSON.parse(localStorage.getItem("FavoriProduct")!=null)) {
   favories= JSON.parse(localStorage.getItem("FavoriProduct"))
   
}

ShowFavoruites();
function ShowFavoruites() {
    for (const favs of favories) {
        fav.innerHTML += `  <div class="col-lg-3 col-sm-12 data-id="${favs.id}">
        <div class="picture">
            <img src="${favs.image}" style="width: 100%;height: 100%;" alt="banana">
            <div class="icons">
               
                <div class="basket-icon">
                    <a href=""> <i class="fas fa-shopping-cart"></i></a>
                </div>
                <div class="detail-icon">
                    <a href=""> <i class="fas fa-info"></i></a>
                </div>
            </div>
            <div class="name">
                <a href="#">${favs.name}</a>
            </div>
            <div class="price">
                ${favs.price}
            </div>
        </div>
    </div>`
    }
   
}
//addproduct via localstorage



//BasketFavoriCount
let heartcount = document.querySelector(".heart-count")
favoriCount(heartcount)

function favoriCount(sum) {
    sum.innerText = JSON.parse(localStorage.getItem("FavoriProduct")).length
}





let basketcount = document.querySelector(".basket-count")
basketCount(basketcount)

function basketCount(sum) {
    sum.innerText = JSON.parse(localStorage.getItem("products")).length
}
//BasketFavoriCount


//Add Basket

let products = document.querySelectorAll("#addproduct")
let count = document.querySelector(".basket-count")

if (JSON.parse(localStorage.getItem("products") == null)) {
    localStorage.setItem("products", JSON.stringify([]));
}

let productList = JSON.parse(localStorage.getItem("products"))

products.forEach(product => {

    product.addEventListener("click", function (e) {
        e.preventDefault();


        let productimage = this.parentNode.parentNode.previousElementSibling.getAttribute("src");
        let productname = this.parentNode.parentNode.nextElementSibling.childNodes[1].innerText;
        let productprice = this.parentNode.parentNode.parentNode.lastElementChild.innerText;
        let productid = this.parentNode.parentNode.parentNode.parentNode.getAttribute("data-id");
        let productcount = this.parentNode.parentNode.parentNode.lastElementChild.innerText;

        let existproduct = productList.find(m => m.id == productid);

        if (existproduct == undefined) {
            productList.push({
                id: productid,
                image: productimage,
                name: productname,
                price: productprice,
                count: 1

            });

            swal("Good job!", "Product added succes!", "success");





        }

        else {
            swal("Info!", "You have added this Product to your Cart,Please check your basket!", "info");
        }


        localStorage.setItem("products", JSON.stringify(productList))
        count.innerText = getCount(productList)
    });



})

function getCount(list) {
    return list.length;
}
count.innerText = getCount(productList)


//Add Basket 


//rightcorner favoriproduct count 
let countfavo=document.querySelector(".product-count strong")
countfavo.innerText=JSON.parse(localStorage.getItem("FavoriProduct")).length
//rightcorner favoriproduct count 

//remove-all product
let removeall = document.querySelector("#remove-all");

if((localStorage.getItem("FavoriProduct")!=[])){
    removeall.style.display="block"
}
else{
    removeall.style.display="none"
}



removeall.addEventListener("click",function(e) {
    e.preventDefault();
    localStorage.removeItem("FavoriProduct");
    window.location.reload();

})
//remove-all product


