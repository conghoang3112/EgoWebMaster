$(document).ready(() => {

    function init() {
        initDataTable();
        initEvent();
    }

    // init event
    var initEvent = function () {
        $('[data-search-table-filter="search"]').keyup(function (e) {
            survey_dt.search(e.target.value).draw();
        });
    }

    // init datatable
    var initDataTable = function () {
        // Setup - add a text input to each footer cell
        survey_dt = $("#page_table").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: true,
            stateSave: false,
            ordering: false,
            paging: true,
            scrollX: true,
            ajax: {
                url: "/CmsSurvey/GetAllPaging",
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
                    data: 'customerName'
                },
                {
                    data: 'customerPhone'
                },
                {
                    data: 'companyName'
                },
                {
                    data: 'businessField'
                },
                {
                    data: 'title'
                },
                {
                    data: 'mediaChannel'
                },
                {
                    data: 'mainMediaChannelLink'
                },
                {
                    data: 'conversionMediaChannel'
                },
                {
                    data: 'conversionMediaChannelLink'
                },
                {
                    data: 'createdDate'
                },
            ],
        });
       
    }


    


    init();
})