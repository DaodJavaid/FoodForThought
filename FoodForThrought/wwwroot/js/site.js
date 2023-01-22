// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(window).scroll(function () {
    $('nav').toggleClass('scrolled', $(this).scrollTop() > 200);
});


function OpensideBar() {
    document.getElementById("Open_side_bar_menu").style.width = "350px";
}

function ClosesideBar() {
    document.getElementById("Open_side_bar_menu").style.width = "0";
}


/*==========   Scroll Top Button   ==========*/

// Get the button:
let mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}


function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}



/*==========  image zoomsl Plugin  ==========*/
$(".zoomin").imagezoomsl();