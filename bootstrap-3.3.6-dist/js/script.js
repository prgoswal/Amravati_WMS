//    $('ul.nav li.dropdown').hover(function() {
//  $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
//}, function() {
//  $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
//});
$(document).ready(function () {
    $(function () {
        $("#header").load("");
        $("#footer").load("footer.html");
    });

    $('.navbar .dropdown').hover(function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
    }, function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105)
    });

    //$('a').tooltip({ placement: "bottom" });

   
    //$('.secondname').tooltip({
    //    placement: "right",
    //    trigger: "focus"
    //});

});