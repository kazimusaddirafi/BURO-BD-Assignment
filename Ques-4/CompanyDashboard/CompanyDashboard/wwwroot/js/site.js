const connection = new signalR.HubConnectionBuilder()
    .withUrl("/updateHub")
    .build();

connection.on("ReceiveUpdate", function () {
    // Refresh the table data when receiving an update
    console.log("Hiiii")
    refreshTable();
});

connection.start().then(function () {
    console.log("SignalR Connected.");
}).catch(function (err) {
    return console.error(err.toString());
});

function refreshTable() {
    $.ajax({
        url: "http://localhost:5262/Dashboard/GetEmployeeCountByDepartmentApi",
        type: "GET",
        success: function (data) {
            $('#dashboardTable tbody').empty();
            $.each(data, function (index, item) {
                $('#dashboardTable tbody').append('<tr><td>' + item.departmentName + '</td><td>' + item.employeeCount + '</td></tr>');
            });
        },
        error: function (error) {
            console.error("Error refreshing table:", error);
        }
    });
}