$(document).ready(() => {

    function init() {
        initDataTable();
        initEvent();
    }

    // init event
    var initEvent = function () {
        $('[data-search-table-filter="search"]').keyup(function (e) {
            contact_dt.search(e.target.value).draw();
        });
    }

    // init datatable
    var initDataTable = function () {
        // Setup - add a text input to each footer cell
        contact_dt = $("#page_table").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: true,
            stateSave: false,
            ordering: false,
            paging: true,
            scrollX: true,
            ajax: {
                url: "/CmsContact/GetAllPaging",
                type: 'GET',
                data: function (data) {
                    delete data.columns;
                    delete data.search.regex;
                },
                // Ko được ghi đè success call back function
                dataSrc: function (json) {
                    return json.data;
                }
            },
            columns: [
                {
                    data: 'index',
                    orderable: false,
                },
                {
                    data: 'name'
                },
                {
                    data: 'phoneNumber'
                },
                {
                    data: 'businessAreas'
                },
                {
                    data: 'company'
                },
                {
                    data: 'email'
                },
                {
                    data: 'position'
                },
                {
                    data: 'solutionOfInterest'
                },
                {
                    data: 'purpose'
                },
                {
                    data: 'createdDate',
                },

            ],
        });
        contact_dt.on("draw", function () {
            KTMenu.createInstances();
            $(".btn-delete").on("click", onDeleteButtonClick);
        })
    }


    function onDeleteButtonClick() {
        var objectId = $(this).attr('o-id');
        $.ajax({
            url: '/CmsContact/Delete/' + objectId,
            method: 'DELETE',
            async: true,
            success: function (res) {
                toastr.success(res.message);
                if (res.code == 200) {
                    contact_dt.draw();
                }
            }
        })
    }


    init();
})