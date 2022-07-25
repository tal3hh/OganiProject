
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

//tab-menu

$(document).ready(function(){
  $("span").click(function(){
    $(".active").removeClass("active");
    $(this).addClass("active");
  })
})

let btn=document.querySelectorAll(".center span")
let content=document.querySelectorAll(".tab-menu .description")

btn.forEach(btns=>{
  btns.addEventListener("click",function(){

    
    content.forEach(contnt=>{
      if(this.getAttribute("data-id")!=contnt.getAttribute("data-id")){
        contnt.classList.add("d-none");
      }
      else{
        contnt.classList.remove("d-none");
      }
    })
   
  })
})
//tab-menu





//BasketFavoriCount

//let heartcount = document.querySelector(".heart-count")
//favoriCount(heartcount)

//function favoriCount(sum) {
//    sum.innerText = JSON.parse(localStorage.getItem("FavoriProduct")).length
//}





//let basketcount = document.querySelector(".basket-count")
//basketCount(basketcount)

//function basketCount(sum) {
//    sum.innerText = JSON.parse(localStorage.getItem("products")).length
//}
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



});

function getCount(list) {
    return list.length;
}
count.innerText = getCount(productList)


//Add Basket




//Add Favori

let hearticon = document.querySelectorAll("#addheart")

if (JSON.parse(localStorage.getItem("FavoriProduct")) == null) {
    localStorage.setItem("FavoriProduct", JSON.stringify([]));
}

let favoriList = JSON.parse(localStorage.getItem("FavoriProduct"))
let heartcount = document.querySelector(".heart-count")

hearticon.forEach(hearticons => {



    hearticons.addEventListener("click", function (e) {
        e.preventDefault();
        let favoriImage = this.parentNode.parentNode.previousElementSibling.getAttribute("src");
        let favoriname = this.parentNode.parentNode.nextElementSibling.childNodes[1].innerText;
        let favoriprice = this.parentNode.parentNode.parentNode.lastElementChild.innerText;
        let favoriid = this.parentNode.parentNode.parentNode.parentNode.getAttribute("data-id");

        let existproduct = favoriList.find(m => m.id == favoriid);

        if (existproduct == undefined) {
            favoriList.push({
                id: favoriid,
                image: favoriImage,
                name: favoriname,
                price: favoriprice

            });

            swal("Good job!", "Product added succes!", "success");

        }

        else {
            swal("Info!", "You have added this Product to your Cart,Please check your basket!", "info");

        }







        localStorage.setItem("FavoriProduct", JSON.stringify(favoriList))
        heartcount.innerText = getCountheart(favoriList)


    })
});

function getCountheart(heart) {
    return heart.length;
}

heartcount.innerText = getCountheart(favoriList)
//Add Favori






