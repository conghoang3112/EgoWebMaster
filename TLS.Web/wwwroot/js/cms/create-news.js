$(document).ready(function () {
    // init data
    var croppedImage = {
        fileData: null,
        title: null
    }
    //var tagify = null;

    // init function 
    const init = function () {
        initEvent();
        // Initial TinyMCE and config plugin
        initTinymce('#app-text-editor');
        initTagify();
    }

    // init event
    const initEvent = function () {
        $('#open-crop-modal-btn').on('click', () => openCropImageModal(event, 964 / 643));
        $('#uploadFileImageInput').change(onFileCroperChange);
        $('#save-crop-img-btn').on('click', () => getCropImge(croppedImage, '.cropped_img_preview'));

        $('#save_btn').on('click', onSave);
    }

    const initTagify = function () {
        var element = document.querySelector('#news_tag');
        tagify = new Tagify(element);
    }

    // ========================= FUNCTION ==========================


    function onSave() {
        if (isFormValid()) {
            const formData = new FormData($('#page-form')[0]);
            formData.append('ImageFile', croppedImage.fileData);
            formData.append('ImageTitle', croppedImage.title == null ? '' : croppedImage.title);
            formData.set('Content', tinymce.activeEditor.getContent());
            formData.set('IsPublish', $('#isPublishCheckbox').is(':checked'));
            var newsTag = tagify.getCleanValue().map((tag) => tag.value).join(", ");
            formData.set('Tags', newsTag)
            const formProps = Object.fromEntries(formData);
            $.ajax({
                url: '/CmsNews/Create',
                type: 'POST',
                processData: false, // without any attempt to modify it by encoding as a query string
                contentType: false, // forcing jQuery not to add a Content-Type header for you,
                data: formData,
                success: function (res) {
                    if (res.code == 200) {
                        window.location.replace('/CmsNews/Index?message=' + res.message + '&code=200');
                    } else {
                        toastr.error(res.message)
                    }
                },
                error: function (res) {
                    toastr.error('Đã có lỗi xảy ra!')
                }
            });
        }
    }


    function isFormValid() {
        if ($(".cropped_img_preview").attr("src") == "") {
            toastr.error("Bạn chưa chọn ảnh!")
            return false;
        } else if ($("[name='Title']").val().trim() == "") {
            toastr.error("Bạn chưa nhập tiêu đề");
            return false;
        }
        return true;
    }

    init();
})