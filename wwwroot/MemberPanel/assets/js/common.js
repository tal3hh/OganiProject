//getBasketCount

export function getCount(list) {
  return list.length;
}
//getBasketCount


//getFavoriCount
export function getCountheart(heart) {
  return heart.length;
}
//getFavoriCount


//basketCount

export function basketCount(sum) {
  sum.innerText = JSON.parse(localStorage.getItem("products")).length
}
//basketCount


//favoriCount
export function favoriCount(sum) {
  sum.innerText = JSON.parse(localStorage.getItem("FavoriProduct")).length
}
//favoriCount


//header ALL CATEGORIES dropdown
export function dropdowns() {
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
}
//header ALL CATEGORIES dropdown



//searchfilter dropdown

export function searchfilterdropdown() {
  $(document).ready(function () {

    $("#all-categ").click(function () {
      $(".dropdown-content-cate").toggle(800);

    })
  });
}

//searchfilter dropdown

//add products to basket

export function addproducts() {

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
      let productcount=this.parentNode.parentNode.parentNode.lastElementChild.innerText;

      let existproduct = productList.find(m => m.id == productid);

      if (existproduct == undefined) {
        productList.push({
          id: productid,
          image: productimage,
          name: productname,
          price: productprice,
          count: 1

        });

        alert("Product Added Success!")





      }

      else {
        alert("You have added this Product to your Cart,Please check your basket")
      }


      localStorage.setItem("products", JSON.stringify(productList))
      count.innerText = getCount(productList)
    });



  })

  count.innerText = getCount(productList)
}
 //add products to basket



//add products to favourites

export function addproductsfav(){
  let hearticon=document.querySelectorAll("#addheart")

if(JSON.parse(localStorage.getItem("FavoriProduct"))==null){
  localStorage.setItem("FavoriProduct",JSON.stringify([]));
}

let favoriList=JSON.parse(localStorage.getItem("FavoriProduct"))
let heartcount=document.querySelector(".heart-count")

hearticon.forEach(hearticons => {



  hearticons.addEventListener("click",function(e){
    e.preventDefault();
    let favoriImage=this.parentNode.parentNode.previousElementSibling.getAttribute("src"); 
    let favoriname=this.parentNode.parentNode.nextElementSibling.childNodes[1].innerText;
    let favoriprice=this.parentNode.parentNode.parentNode.lastElementChild.innerText;
    let favoriid=this.parentNode.parentNode.parentNode.parentNode.getAttribute("data-id");

    let existproduct=favoriList.find(m=>m.id==favoriid);

    if(existproduct==undefined){
      favoriList.push({
        id:favoriid,
        image:favoriImage,
        name:favoriname,
        price:favoriprice
   
      });

      alert("Product Added Success!")
      
    }

    else{
      alert("You have added this Product to your  Favourites page,Please check your Favourites Page")
  
    }

  

    



  localStorage.setItem("FavoriProduct", JSON.stringify(favoriList))
  heartcount.innerText=getCountheart(favoriList)

   
  })
});

heartcount.innerText=getCountheart(favoriList)
}
//add products to favourites