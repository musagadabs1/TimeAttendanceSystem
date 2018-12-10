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
function AddEmployee() {
    var res = validate();
    if (res==false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        Name: $('#empName').val(),
        Position: $('#position').val(),
        Office: $('#office').val(),
        Salary: $('#salary').val()
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
    $('#Name').css('border-color', 'lightgrey');
    $('#position').css('border-color', 'lightgrey');
    $('#office').css('border-color', 'lightgrey');
    $('#salary').css('border-color', 'lightgrey');
    $.ajax({
        url: 'Employess/GetEmployeeById' + empId,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType:'json',
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#position').val(result.Position);
            $('#office').val(result.Office);
            $('#salary').val(result.Salary);
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
        Name: $('#Name').val(),
        Position: $('#position').val(),
        Office: $('#office').val(),
        Salary: $('#salary').val()
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
            $('#Id').val("");
            $('#Name').val("");
            $('#position').val("");
            $('#office').val("");
            $('#salary').val("");
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
    $('#Id').val("");
    $('#Name').val("");
    $('#position').val("");
    $('#office').val("");
    $('#salary').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#position').css('border-color', 'lightgrey');
    $('#office').css('border-color', 'lightgrey');
    $('#salary').css('border-color', 'lightgrey');
}

//Function for validating entries
function validate() {
    var isValide = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#position').val().trim() == "") {
        $('#position').css('border-color', 'Red');
        isValide = false;
    }
    else {
        $('#position').css('border-color', 'lightgrey');   
    }
    if ($('#position').val().trim() == "") {
        $('#position').css('border-color', 'Red');
        isValide = false;
    }
    else {
        $('#position').css('border-color', 'lightgrey');
    }
}