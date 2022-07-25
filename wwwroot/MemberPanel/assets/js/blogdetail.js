import {favoriCount,basketCount,getCountheart,dropdowns,searchfilterdropdown} from "./common.js"

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


//start searchfilter
$(document).ready(function () {

    $("#all-categ").click(function () {
        $(".dropdown-content-cate").toggle(800);

    })
});
//end searchfilter


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

