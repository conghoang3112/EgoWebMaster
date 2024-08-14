
$(document).ready(() => {
    $("#plan-exe-submit-btn").on("click", () => submitContact("#plan-execution-form"));

    function submitContact(formid) {
        var formData = new FormData($(formid)[0]);
        var formDataJson = Object.fromEntries(formData);
        $('#plan-exe-submit-btn').prop('disabled', true);
        $.ajax({
            url: '/PlanExecution/AddPlanExecution',
            type: 'POST',
            processData: false, // without any attempt to modify it by encoding as a query string
            contentType: false, // forcing jQuery not to add a Content-Type header for you,
            data: formData,
            success: function (res) {
                $('#plan-exe-submit-btn').prop('disabled', false);
                alert(res.message);
                if (res.status === 200) {
                    document.getElementById("plan-execution-form").reset();
                }
            },
            error: function (res) {
                $('#plan-exe-submit-btn').prop('disabled', false);
                alert("Error!");
            }
        });

    }

})