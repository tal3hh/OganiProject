
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



 //owl-carousel start

  $('.owl-carousel').owlCarousel({
    loop:true,
    margin:10,
    nav:true,
    dots:false,

    smartSpeed:1500,
    animateIn:'linear',
    animateOut:'linear',


    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        },
        1000:{
            items:5
        }
    }
 })
//owl-carousel start



 //tab-menu -start
  let catnames =document.querySelectorAll(".category-name div")
 let content = document.querySelectorAll(".product-image .row")

  catnames.forEach(catname => {
    catname.addEventListener("click",function(){ 
        
      let actived=document.querySelector(".category-name .active");
      actived.classList.remove("active");
      this.classList.add("active");

      content.forEach(contnt =>{
        if (this.getAttribute("data-id")!=contnt.getAttribute("data-id")) {
          contnt.classList.add("d-none")
        }

        else{
          contnt.classList.remove("d-none")
        }


      })
   

    })
    
  });
 //tab-menu end


 //start sliders col-6
 $('.owl-carousel').owlCarousel({

  nav:true,
  // Infinity:true,
  autoplay:true,
  autoplayTimeout:1000,
  responsive:{
      0:{
          items:1
      },
      600:{
          items:3
      },
      1000:{
          items:5
      }
  }
})
 //end sliders col-6





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

count.innerText = getCount(productList)
function getCount(list) {
    return list.length;
};


//Add Basket 




 //Add Favori

let hearticon = document.querySelectorAll("#addheart")

if (JSON.parse(localStorage.getItem("FavoriProduct")) == null) {
    localStorage.setItem("FavoriProduct", JSON.stringify([]));
}

let favoriList = JSON.parse(localStorage.getItem("FavoriProduct"))
let heartcountt = document.querySelector(".heart-count")

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
        heartcountt.innerText = getCountheart(favoriList)

      

    })
});

heartcountt.innerText = getCountheart(favoriList)
function getCountheart(heart) {
    return heart.length;
}








