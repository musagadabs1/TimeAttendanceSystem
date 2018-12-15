function LoadGrid(methodName, colNames, update, updateColName, updateClassName,sDelete,deleteColName, deleteClassName, date, id, empId, gridId)
{
    try
    {
        var array = colNames.split(',');
        $.ajax({
            type: 'GET',
            url: methodName + "?date=" + date + "&id=" + id + "&empId=" + empId,
            datatype: 'json',
            contentType: 'application/json',
            success: function (data) {
                var trHTML = '';
                var td = "";
                $(data).each(function (key,value) {
                    var obj = '';
                    if (sDelete == "Delete") {
                        obj += '<td><input  class="' + deleteClassName + '" type="button" id=' + value[deleteColName] + ' value="DELETE"/></td>';
                    }
                    for (var i = 0; i < array.length; i++) {
                        obj += '<td>' + value[array[i]] + '</td>';
                    }
                    if (update == "Update") {
                        obj += '<td><input  class="' + updateClassName + '" type="button" id=' + value[updateColName] + ' value="EDIT"/></td>';
                    }
                    trHTML += '<tr>' + obj + '</tr >';
                });
                $(gridId).find("tr:gt(0)").remove();
                if (trHTML != '') {
                    $(gridId).css("display", "block");
                    $(gridId).append(trHTML);
                }
                else {
                    $(gridId).css("display", "none");
                    alert("There is no Record on the Selected Date");
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

function SaveData(method,vDate, etime, terminalId, empId, eName, mode, remark, loginUser, operation, sendMail)
{
    try
    {
        var data = {
            VDate: vDate,
            Time: etime,
            Terminal: terminalId,
            EMPID: empId,
            Name: eName,
            Mode: mode,
            Remark: remark,
            LoginUser: loginUser,
            Operation: operation,
            SendMail:sendMail
        };
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url:method,// base_url + "Handler.ashx?method=SP_Manual_Entry",
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

function Recompile(method,vDate,deptId,elementId) {
    try
    {
        $.ajax({
            type: 'GET',
            contentType: 'application/json',
            url:"Recompile?date=" + vDate,
            success: function (data) {
                if (data.success==true) {
                    alert("Compiled Successfully");
                    var deptId = $("#" + deptId + "'");
                    LoadGrid(method, "Name,Count", "Update", "EmpID", "EditButtons", "", "", "", vDate, deptId, 0, "#" + elementId + "'");
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