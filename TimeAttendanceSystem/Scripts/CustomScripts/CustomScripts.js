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

function EmployeeEditData(ID, MethodName) {

    $.ajax({
        type: "Get",
        Contenttype: "application/json",
        //url: '@Url.Action("GetEmployeeDetailsByMachineCode")' + '?machineCode=' + $('#MachineId').val(),
        url: MethodName + Id, //'@Url.Action("GetEmployeeDetailsByMachineCode")' + '?machineCode=' + $('#MachineId').val(),
        success: function (data) {
            $("#Department").append($("<option></option>").val("0").html("Select"));
            if (data.status ==true) {
                $('#MachineId').val(data.response[0].L_UID);
                $('#FirstName').val(data.response[0].First_Name);
                $('#MiddleName').val(data.response[0].Middle_Name);
                $('#LastName').val(data.response[0].Last_Name);
                $("#Department").val(data.response[0].E_DEPT_ID);
                $("#MainContent_ddlCompany").val(data.response[0].Branch_Id);
                $("#Designation").val(data.response[0].E_DES_ID);
                $("#MainContent_ddlShift").val(data.response[0].Shift_Id);
                GetShiftSchedule("SP_GetShiftDetails_Employee", data.response[0].Shift_Id, "Code,Title,StartTime,EndTime,Tolerence,Type,NextDay");
                if (data.response[0].E_EType == "S") {
                    $("#StaffType").prop('checked', true);
                    $("#EmployeeShifttr").css("display", "none");
                }
                else if (data.response[0].E_EType == "P") {
                    $("#MainContent_RadPartTime").prop('checked', true);
                    $("#MainContent_RadFullTime").prop('checked', false);
                    $("#MainContent_rbtFaculty").prop('checked', true);
                    $("#EmployeeShifttr").css("display", "block");
                }
                else if (data.response[0].E_EType == "F") {
                    $("#MainContent_RadPartTime").prop('checked', false);
                    $("#MainContent_RadFullTime").prop('checked', true);
                    $("#MainContent_rbtFaculty").prop('checked', true);
                    $("#EmployeeShifttr").css("display", "block");
                }
                if (data.response[0].E_STATUS == "Y") {
                    $("#IsActive").prop('checked', true);
                }
                else {
                    $("#IsActive").prop('checked', false);
                }



            }

        }

    });
}