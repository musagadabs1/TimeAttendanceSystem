function GetNextEntry(methodName, colNames, vieworUpdate, vieworUpdateColName, deleteCol, deleteColName, gridID) {
    try {
        var array = colNames.split(',');

        $.ajax({
            type: "GET",
            //url: base_url + "Handler.ashx?method=" + MethodName + "&Date=" + Date + "&EMPID=" + EmpNumber,
            url: methodName,
            datatype: "JSON",
            //cache: false,
            success: function (data) {
                var trHTML = '';
                var td = "";

                $(data.response).each(function (key, value) {
                    var obj = '';

                    for (var i = 0; i < array.length; i++) {
                        obj += '<td>' + value[array[i]] + '</td>';
                    }
                    if (deleteCol == "Delete") {
                        obj += '<td><input  class="Deletebuttons btn btn-primary" type="button" id=' + value[deleteColName] + ' value="DELETE"/></td>';
                    }
                    if (vieworUpdate == "View") {
                        obj += '<td><input  class="Editbuttons btn btn-primary" type="button" id=' + value[vieworUpdateColName] + ' value="VIEW"/></td>';
                    }
                    trHTML += '<tr>' + obj + '</tr >';
                });

                $(gridID).find("tr:gt(0)").remove();
                if (trHTML != '') {
                    $(gridID).css("display", "block");
                    $(gridID).append(trHTML);
                }
                else {
                    $(gridID).css("display", "none");
                    alert("There is no Record on the Selected Date");
                }
            }
        });
    }
    catch (e) {
        alert(e); 
    }
}
