//import { url } from "inspector";

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

function SaveData(methodName,vDate, eTimeH, eTimeM,terminalId, empId,name,mode,remark, sendMail,operation)
{
    try
    {
        var editEntry = {};
        editEntry.Date= vDate;
        editEntry.TimeHH= eTimeH;
        editEntry.TimeMM = eTimeM;
        editEntry.TerminalID = terminalId;
        editEntry.EmployeeID = empId;
        editEntry.Name = name;
        editEntry.Mode = mode;
        editEntry.Remark = remark;
        editEntry.SendMail = sendMail;
        editEntry.Operation = operation;
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: methodName,
            data: '{editEntry:' + JSON.stringify(editEntry) + '}',
            success: function (content) {
                //if (content.success==true) {
                    if (operation =='INSERT') {
                        alert("Manual Punch added Successfully!!");
                    }
                    else {
                        alert("Deleted Successfully!!");
                    }
               // }
                //else {
                    //alert(content.message);
                //}
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

function Recompile(methodName,vDate) {
    try
    {
        $.ajax({
            type: 'GET',
            contentType: 'application/json',
            url: methodName,
            success: function (data) {
                //if (data.success==true) {
                    alert("Compiled Successfully");
                    var deptId = $('#Department').val();
                    LoadGrid(method, "Name,Count", "Update", "EmpID", "EditButtons", "", "", "", vDate, deptId, 0, "#location");
                //}
            },
            error: function (err) {
                alert(err.message);
            }
        });
    }
    catch (e)
    {
        alert(e.message);
    }
}