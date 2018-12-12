//Load data in Table when document is ready
$(document).ready(function () {
    loadData();
});
//Load Data Function
function loadData() {
    $.ajax({
        url: '/Employees/GetAllEmployee',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Postion + '</td>';
                html += '<td>' + item.Office + '</td>';
                html += '<td>' + item.Salary + '</td>';
                html += '<td><a href="#" onclick="return getEmployeeById(' + item.Id + ')">Edit</a> | <a href="#" onclick="deleteEmployee(' + item.Id + ')">Delete </a> </td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
}

//Add Employee Function
function addEmployee() {
    var res = validate();
    if (res==false) {
        return false;
    }
    var empObj = {
        Id: $('#employeeId').val(),
        Name: $('#empName').val(),
        Position: $('#empPosition').val(),
        Office: $('#empOffice').val(),
        Salary: $('#empSalary').val()
    };
    $.ajax({
        url: 'Employees/AddEmployee',
        data: JSON.stringify(empObj),
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');

        },
        error: function (err) {
            alert(err.responseText);
        }
    });
}

//Get Employee By Id
function getEmployeeById(empId) {
    $('#empName').css('border-color', 'lightgrey');
    $('#empPosition').css('border-color', 'lightgrey');
    $('#empOffice').css('border-color', 'lightgrey');
    $('#empSalary').css('border-color', 'lightgrey');
    $.ajax({
        url: 'Employess/GetEmployeeById' + empId,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType:'json',
        success: function (result) {
            $('#employeeId').val(result.Id);
            $('#empName').val(result.Name);
            $('#empPosition').val(result.Position);
            $('#empOffice').val(result.Office);
            $('#empSalary').val(result.Salary);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
    return false;
}

//Function for updating Employee Records
function updateEmployee() {
    var res = validate();
    if (res==false) {
        return false;
    }
    var empObj = {
        Name: $('#empName').val(),
        Position: $('#empPosition').val(),
        Office: $('#empOffice').val(),
        Salary: $('#empSalary').val()
    };
    $.ajax({
        url: 'Employees/UpdateEmployee',
        dataType: 'json',
        data: JSON.stringify(empObj),
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#employeeId').val("");
            $('#empName').val("");
            $('#empPosition').val("");
            $('#empOffice').val("");
            $('#empSalary').val("");
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
}
//Function for deleting employee record
function deleteEmployee(id){
    var ans = confirm('Are you sure you want to delete this record ?');
    if (ans) {
        $.ajax({
            url: 'Employees/DeleteEmployee' + id,
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            type: 'POST',
            success: function (result) {
                loadData();
            },
            error: function (err) {
                alert(err.responseText);
            }
        });
    }
}

//Function to clear Textboxes
function clearTextBoxes() {
    $('#employeeId').val("");
    $('#empName').val("");
    $('#empPosition').val("");
    $('#empOffice').val("");
    $('#empSalary').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#empName').css('border-color', 'lightgrey');
    $('#empPosition').css('border-color', 'lightgrey');
    $('#empOffice').css('border-color', 'lightgrey');
    $('#empSalary').css('border-color', 'lightgrey');
}

//Function for validating entries
function validate() {
    var isValide = true;
    if ($('#empName').val().trim() == "") {
        $('#empName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#empName').css('border-color', 'lightgrey');
    }
    if ($('#empPosition').val().trim() == "") {
        $('#empPosition').css('border-color', 'Red');
        isValide = false;
    }
    else {
        $('#empPosition').css('border-color', 'lightgrey');   
    }
    if ($('#empOffice').val().trim() == "") {
        $('#empOffice').css('border-color', 'Red');
        isValide = false;
    }
    else {
        $('#empOffice').css('border-color', 'lightgrey');
    }
    if ($('#empSalary').val().trim() == "") {
        $('#empSalary').css('border-color', 'Red');
        isValide = false;
    }
    else {
        $('#empSalary').css('border-color', 'lightgrey');
    }
}