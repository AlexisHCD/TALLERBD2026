function AjaxPost(url, data, funcionexito, funcionerror, funcionmientras) {

    jQuery.ajax({
        async: true,
        url: url,
        type: "POST",
        dataType: "json",
        data: data,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            funcionexito(data.d);
        },
        error: function (er) {
            funcionerror();
        },
        beforeSend: function () {
            funcionmientras();
        },
    });

}

function AjaxGet(url, funcionexito, funcionerror, funcionmientras) {

    jQuery.ajax({
        async: true,
        url: url,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            funcionexito(data.d);
        },
        error: function (er) {
            funcionerror();
        },
        beforeSend: function () {
            funcionmientras();
        },
    });

}