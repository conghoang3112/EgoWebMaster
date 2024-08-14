$(document).ready(() => {
    const toggleBtn = document.querySelector(".sidebar-toggle");
    const closebtn = document.querySelector(".close-btn");
    const sidebar = document.querySelector(".sidebar");
    const btnSwitch = document.querySelector(".change-lg-btn")
    const btnSwitch2 = document.querySelector(".change-lg-btn2")
    var myModal = new bootstrap.Modal(document.getElementById('myModal'), {
        backdrop: "static"
    })

    if ($("#select_lg_ctrl").val() == 'vi') {
        btnSwitch.textContent = 'ENG';
        btnSwitch2.textContent = 'ENG';
    } else {
        btnSwitch.textContent = 'VN';
        btnSwitch2.textContent = 'VN';
    }

    btnSwitch.addEventListener("click", function () {
        const initialText = 'VN';

        if (btnSwitch.textContent.includes(initialText)) {
            btnSwitch.textContent = initialText;
        } else {

            btnSwitch.textContent = 'VN';
        }
        changeLanguage(btnSwitch.textContent);
    });

    btnSwitch2.addEventListener("click", function () {
        const initialText = 'VN';

        if (btnSwitch2.textContent.includes(initialText)) {

            btnSwitch2.textContent = initialText;
        } else {
            btnSwitch2.textContent = 'VN';
        }
        changeLanguage(btnSwitch2.textContent);
    });

    const changeLanguage = function (culture) {
        if (culture == "VN") {
            document.getElementById("select_lg_ctrl").selectedIndex = 1;
        } else {
            document.getElementById("select_lg_ctrl").selectedIndex = 0;
        }
        document.getElementById("change_language_form").submit();

    }

    toggleBtn.addEventListener("click", function () {
        sidebar.classList.toggle("show-sidebar");
    });

    closebtn.addEventListener("click", function () {
        sidebar.classList.remove("show-sidebar");
    });

    AOS.init();

    $("#contact_btn_submit").on("click", () => submitContact("#contact-form"));
    $("#receive_document_btn_submit").on("click", () => submitContact("#receive-docs-form"));


    function submitContact(formid) {
        var formData = new FormData($(formid)[0]);
        var formDataJson = Object.fromEntries(formData);
        debugger
        $.ajax({
            url: '/Contact/AddContact',
            type: 'POST',
            processData: false, // without any attempt to modify it by encoding as a query string
            contentType: false, // forcing jQuery not to add a Content-Type header for you,
            data: formData,
            success: function (res) {
                if (res.status == 200) {
                    if (formid == "#receive-docs-form") {
                        myModal.hide();
                    }
                }
                alert(res.message);
            },
            error: function (res) {
                alert("Error!");
            }
        });
        
    }

    

    $("#close-modal-btn").on("click", function () {
        myModal.hide();
    })
    var isShowModal = sessionStorage.getItem("isShowModal")
    if (window.location.pathname == "/" && isShowModal == null) {
        myModal.show()
        sessionStorage.setItem("isShowModal", "true")
    }

})