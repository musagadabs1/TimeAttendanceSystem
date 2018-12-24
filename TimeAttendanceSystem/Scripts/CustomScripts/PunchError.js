function LoadGrid(methodName, colNames, update, updateColName, updateClassName,sDelete,deleteColName, deleteClassName, gridId)
{
    try
    {
        var array = colNames.split(',');
        $.ajax({
            type: 'GET',
            url: methodName, 
            datatype: 'json',
            contentType: 'application/json',
            success: function (data) {
                var trHTML = '';
                var td = "";
                $(data).each(function (key,value) {
                    var obj = '';
                    if (sDelete === "Delete") {
                        obj += '<td><input  class="' + deleteClassName + '" type="button" id=' + value[deleteColName] + ' value="DELETE"/></td>';
                    }
                    for (var i = 0; i < array.length; i++) {
                        obj += '<td>' + value[array[i]] + '</td>';
                    }
                    if (update === "Update") {
                        //obj += '<td><input  class="' + updateClassName + '" type="button" id=' + value[updateColName] + ' value="EDIT"/></td>';
                        obj += '<td><input  class="' + updateClassName + '" type="button" id=' +updateColName + ' value="EDIT"/></td>';
                    }
                    trHTML += '<tr>' + obj + '</tr >';
                });
                $(gridId).find("tr:gt(0)").remove();
                if (trHTML !== '') {
                    $(gridId).css("display", "block");
                    $(gridId).append(trHTML);
                }
                else {
                    $(gridId).css("display", "none");
                    alert("There is no Record on the Selected Date");
                }
            },
            error: function (err) {
                alert(err.message);
            }

        });
    }
    catch (e)
    {
        alert(e);
    }
}

function SaveData(method,vDate, eTime ,terminalId, empId, mode,remark, sendMail,operation)
{
    try
    {
        var data = {
            VDate: vDate,
            Time: eTime,
            Terminal: terminalId,
            EMPID: empId,
            Mode: mode,
            Operation:operation,
            SendMail:sendMail
        };
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url:method,
            data: JSON.stringify(data),
            success: function (content) {
                if (content.success==true) {
                    if (operation=='Insert') {
                        alert("Manual Punch added Successfully!!");
                    }
                    else {
                        alert("Deleted Successfully!!");
                    }
                }
                else {
                    alert(content.message);
                }
            },
            error: function (err) {
                alert(err);
            }
        });
    }
    catch (e)
    {
        alert(e);
    }
}

function Recompile(vDate) {
    try
    {
        $.ajax({
            type: 'GET',
            contentType: 'application/json',
            url:"Recompile?date=" + vDate,
            success: function (data) {
                if (data.success==true) {
                    alert("Compiled Successfully");
                    var deptId = $('#Department').val();
                    LoadGrid(method, "Name,Count", "Update", "EmpID", "EditButtons", "", "", "", vDate, deptId, 0, "#location");
                }
                //else {
                   // alert(data.error);
                //}
            },
            error: function (err) {
                alert(err);
            }
        });
    }
    catch (e)
    {
        alert(e);
    }
}