function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function JQueryAjaxPost(form) {
    $.Validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $('#firstTab').html(response.html);
                    resfreshAddNewTab($(form).attr('data-restUrl'), true);
                    $.notify(response.message, "success");
                    if (typeof activateJQueryTable !== 'undefined' && $.isFunction(activateJQueryTable))
                        activateJQueryTable();
                }
                    
                else {
                        $.notify(response.message, "error");
                }
                
            }
        }
        if ($(form).attr('enctype')=="multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);

    }
    return false;
}







//function jQueryAjaxPost(form) {
//    $.validator.unobtrusive.parse(form);
//    if ($(form).valid()) {
//        var ajaxConfig = {
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            success: function (response) {
//                if (response.success) {
//                    $("#firstTab").html(response.html);
//                    refreshAddNewTab($(form).attr('data-restUrl'), true);
//                    $.notify(response.message, "success");
//                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
//                        activatejQueryTable();
//                }
//                else {
//                    $.notify(response.message, "error");
//                }
//            }
//        }
//        if ($(form).attr('enctype') == "multipart/form-data") {
//            ajaxConfig["contentType"] = false;
//            ajaxConfig["processData"] = false;
//        }
//        $.ajax(ajaxConfig);

//    }
//    return false;
//}