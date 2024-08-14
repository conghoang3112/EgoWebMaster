$(document).ready(() => {

    function init() {
        initDataTable();
        initEvent();
    }

    // init event
    var initEvent = function () {
        $('[data-search-table-filter="search"]').keyup(function (e) {
            news_dt.search(e.target.value).draw();
        });
    }

    // init datatable
    var initDataTable = function () {
        // Setup - add a text input to each footer cell
        news_dt = $("#page_table").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: true,
            stateSave: false,
            ordering: false,
            paging: true,
            scrollX: true,
            ajax: {
                url: "/CmsNews/GetAllPaging",
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
                    data: 'title'
                },
                {
                    data: 'createdDate'
                },
                {

                    data: 'thumnailImageUrl',
                    render: function (data) {
                        return `<img style="width:100px" src="${data}"/>`;
                    }
                },
                {
                    data: 'author'
                },
                {
                    data: 'isPublish',
                    render: function (data) {
                        if (data == true) {
                            return `<span class="badge badge-success">Active</span>`;
                        } else {
                            return `<span class="badge badge-secondary">Disable</span>`
                        }
                    }
                },
                {
                    data: 'id',
                    render: function (data) {
                        return `
                            <a href="#" class="btn-sm btn-light btn-active-light-primary"
                                data-kt-menu-trigger="click"
                                data-kt-menu-placement="bottom-start">
                                Action
                            </a>

                            <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-200px py-4"
                                data-kt-menu="true">
                                <!--begin::Menu item-->
                                <div class="menu-item px-3">
                                    <a href="/CmsNews/Edit?id=${data}" class="menu-link px-3">
                                       Sửa
                                    </a>
                                </div>
                                <!--end::Menu item-->

                                <!--begin::Menu item-->
                                <div class="menu-item px-3">
                                    <a href="#" class="menu-link px-3 btn-delete"  o-id="${data}">
                                       Xóa
                                    </a>
                                </div>
                                <!--end::Menu item-->

                            </div>`
                    }
                }
            ],
        });
        news_dt.on("draw", function () {
            KTMenu.createInstances();
            $(".btn-delete").on("click", onDeleteButtonClick);
        })
    }


    function onDeleteButtonClick() {
        if (confirm("Bạn có chắc muốn xóa blog này không?") == true) {
            var objectId = $(this).attr('o-id');
            $.ajax({
                url: '/CmsNews/Delete/' + objectId,
                method: 'POST',
                async: true,
                success: function (res) {
                    toastr.success(res.message);
                    if (res.code == 200) {
                        news_dt.draw();
                    }
                }
            })
        } 
    }


    init();
})
