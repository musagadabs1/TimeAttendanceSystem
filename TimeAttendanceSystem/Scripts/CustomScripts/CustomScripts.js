function GetShiftSchedule(colNames) {
    var array = ColNames.split(',');
    $.ajax({
        type: "Get",
        Contenttype: "application/json",
        //url: MethodName + "&id=" + id + "",
        url: '@Url.Action("GetShiftDetail")' + '?shiftId=' + $("#ddlShift").val(),
        cache: false,
        success: function (data) {
            var trHTML = '';
            var td = "";
            $(data.response).each(function (key, value) {
                var obj = '';
                for (var i = 0; i < array.length; i++) {
                    obj += '<td>' + value[array[i]] + '</td>';
                }
                trHTML += '<tr>' + obj + '</tr >';
            });
            $("#ShiftDetails").find("tr:gt(0)").remove();
            $('#ShiftDetails').append(trHTML);
            $("#ShiftDetails").css("display", "block");
        }
    });
}