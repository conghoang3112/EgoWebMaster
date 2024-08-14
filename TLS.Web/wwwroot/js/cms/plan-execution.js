$(document).ready(() => {

    function init() {
        initDataTable();
        initEvent();
    }

    // init event
    var initEvent = function () {
        $('[data-search-table-filter="search"]').keyup(function (e) {
            plan_exe_dt.search(e.target.value).draw();
        });
    }

    // init datatable
    var initDataTable = function () {
        // Setup - add a text input to each footer cell
        plan_exe_dt = $("#page_table").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: true,
            stateSave: false,
            ordering: false,
            paging: true,
            scrollX: true,
            ajax: {
                url: "/CmsPlanExecution/GetAllPaging",
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
                    data: 'companyName'
                },
                {
                    data: 'phoneNumber'
                },
                {
                    data: 'budget'
                },
                {
                    data: 'createddate'
                }
            ],
        });

    }





    init();
})