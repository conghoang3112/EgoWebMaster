
// collapse js
/* document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll("[data-accordion-target]");

    buttons.forEach((button) => {
        button.addEventListener("click", () => {
            const target = document.querySelector(
                button.getAttribute("data-accordion-target")
            );
            const expanded =
                button.getAttribute("aria-expanded") === "true" || false;

            target.classList.toggle("hidden");
            button.setAttribute("aria-expanded", !expanded);
        });
    });
}); */

$(document).ready(() => {
    // menu js
    const openMenu = document.querySelector(".open-menu");
    const closeMenu = document.querySelector(".close-menu");
    const mobileMenu = document.querySelector(".mobile-menu");

    // slider js
    openMenu.addEventListener("click", function () {
        mobileMenu.classList.add("open");
    });

    closeMenu.addEventListener("click", function () {
        mobileMenu.classList.remove("open");
    });

    $(".owl-carousel").owlCarousel({
        loop: true,
        margin: 10,
        items: 1,
        autoplay: true,
        autoplayTimeout: 2500,
        autoplayHoverPause: true
    });

    


    $("#survey-submit-btn").on("click", () => submitContact("#survey-form"));


    function submitContact(formid) {
        var formData = new FormData($(formid)[0]);
        var formDataJson = Object.fromEntries(formData);
        $('#survey-submit-btn').prop('disabled', true);
        $.ajax({
            url: '/Plan/AddSurvey',
            type: 'POST',
            processData: false, // without any attempt to modify it by encoding as a query string
            contentType: false, // forcing jQuery not to add a Content-Type header for you,
            data: formData,
            success: function (res) {
                $('#survey-submit-btn').prop('disabled', false);
                alert(res.message);
                if (res.status === 200) {
                    document.getElementById("survey-form").reset();
                    window.location.href = '/payment';
                }
            },
            error: function (res) {
                $('#survey-submit-btn').prop('disabled', false);
                alert("Error!");
            }
        });

    }

   

})